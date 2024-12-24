using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Lean.Pool;
using DG.Tweening;

public class CustomersController : MonoBehaviour
{
    public GameObject specialCustomer;
    public GameObject custmersPrefab;
    public Transform customerInatancePos;
    public Transform destroyPos;
    public Transform[] waitDestinations;
    public List<GameObject> waitingCustomers = new List<GameObject>();
    public int waitDesCount;
    Vector3 destination;

    public int moneyTransfared;

    private void Start() 
    {
        if(Tutorial.instance.IsTutorialComplete())
        {
            StartCoroutine(InstantiateCustomerAndWait(4,15));
        }
        else
        {
            //Controller.instance.currencyController.AddMoney(20000);

        }
    }

    public void CallInstantiateCustomerAndWait()
    {
        StartCoroutine(InstantiateCustomerAndWait(4,15));
    }

    public IEnumerator InstantiateCustomers(int customerCount, Vector3 destination, GameObject targetTable)
    {
        for (int i = 0; i < customerCount; i++)
        {
            GameObject customer = LeanPool.Spawn(custmersPrefab, customerInatancePos.position, custmersPrefab.transform.rotation);
            customer.GetComponent<CustomerView>().agent.SetDestination(destination);
            customer.GetComponent<CustomerView>().foodEaten = 0;
            customer.GetComponent<CustomerView>().targetTable = targetTable;
            customer.GetComponent<CustomerView>().isComming = true;

            if(i == 0){targetTable.GetComponent<TableView>().Customer1Ontable = customer;}
            if(i == 1){targetTable.GetComponent<TableView>().Customer2Ontable = customer;}
            if(i == 2){targetTable.GetComponent<TableView>().Customer3Ontable = customer;}
            if(i == 3){targetTable.GetComponent<TableView>().Customer4Ontable = customer;}

            yield return new WaitForSeconds(0.5f);
        }
    }

    public IEnumerator InstantiateCustomerAndWait(int count,int time)
    {
        yield return new WaitForSeconds(time);

        for (int i = 0; i < count; i++)
        {
            GameObject customer = LeanPool.Spawn(custmersPrefab, customerInatancePos.position, custmersPrefab.transform.rotation);
            customer.GetComponent<CustomerView>().isComming = true;
            
            Vector3 destination;
            if(count == 1){destination = Controller.instance.customersController.waitDestinations[3].position;}
            else
            {
                destination = Controller.instance.customersController.waitDestinations[i].position;
            }
            //customer.GetComponent<CustomerView>().agent.SetDestination(destination);
            customer.transform.DOMove(destination, 3f).OnComplete(() =>
            {
                customer.GetComponent<CustomerView>().customerAnimator.SetBool("Idle", true);
                waitingCustomers.Add(customer);
            });
            yield return new WaitForSeconds(1f);
        }
    }

    public void CallWaitingCustomer(int Count,Vector3 destination,GameObject targetTable)
    {
        for (int i = 0; i < Count; i++)
        {
            waitingCustomers[0].GetComponent<CustomerView>().agent.SetDestination(destination);
            waitingCustomers[0].GetComponent<CustomerView>().foodEaten = 0;
            waitingCustomers[0].GetComponent<CustomerView>().targetTable = targetTable;
            waitingCustomers[0].GetComponent<CustomerView>().customerAnimator.SetBool("Idle", false);

            if (i == 0) { targetTable.GetComponent<TableView>().Customer1Ontable = waitingCustomers[0]; }
            if (i == 1) { targetTable.GetComponent<TableView>().Customer2Ontable = waitingCustomers[0]; }
            if (i == 2) { targetTable.GetComponent<TableView>().Customer3Ontable = waitingCustomers[0]; }
            if (i == 3) { targetTable.GetComponent<TableView>().Customer4Ontable = waitingCustomers[0]; }

            waitingCustomers.Remove(waitingCustomers[0]);
        }
        StartCoroutine(InstantiateCustomerAndWait(Count,1));
    }

    public void EatFoodBySpecialCustomer()
    {
        StartCoroutine(SpecialCustomerView.instance.EatFood());
    }
}
