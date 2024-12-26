using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Lean.Pool;

public class CustomerView : MonoBehaviour
{
    public Animator customerAnimator;
    public NavMeshAgent agent;
    public GameObject targetTable;

    public GameObject food1Image;
    public GameObject food2Image;
    public GameObject food3Image;

    public int foodEatingcapacity;
    public int foodEaten;
    public bool isCustomerEating;
    public bool isComming;
    public bool isGoing;
    public int orderNumber;

    private void OnEnable() 
    {
        AllLockAndUnlockView all = AllLockAndUnlockView.instance;
        if(all.IsBurgerUnlocked() && all.IsFriesUnlocked() && all.IsCokeUnlocked()){orderNumber = Random.Range(0, 3);}
        if (all.IsBurgerUnlocked() && all.IsFriesUnlocked() && !all.IsCokeUnlocked()) { orderNumber = Random.Range(0, 2);}
        if(all.IsBurgerUnlocked() && !all.IsFriesUnlocked() && !all.IsCokeUnlocked()){orderNumber = Random.Range(0, 0);}
    }

    public void ResetOrders()
    {
        AllLockAndUnlockView all = AllLockAndUnlockView.instance;
        if(all.IsBurgerUnlocked() && all.IsFriesUnlocked() && all.IsCokeUnlocked()){orderNumber = Random.Range(0, 3);}
        if (all.IsBurgerUnlocked() && all.IsFriesUnlocked() && !all.IsCokeUnlocked()) { orderNumber = Random.Range(0, 2);}
        if(all.IsBurgerUnlocked() && !all.IsFriesUnlocked() && !all.IsCokeUnlocked()){orderNumber = Random.Range(0, 0);}
    }

    private void Update() 
    {
        food1Image.transform.LookAt(Camera.main.transform.position);
        food2Image.transform.LookAt(Camera.main.transform.position);
        food3Image.transform.LookAt(Camera.main.transform.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("DestroyPos") && !isComming)
        {
            foodEaten = 0;
            isCustomerEating = false;
            LeanPool.Despawn(this.gameObject);
        }


        if(other.gameObject.layer == 6 && isComming)
        {
            if (targetTable.tag == other.gameObject.tag)
            {
                if (other.gameObject.GetComponent<TableView>().customerCount == 1) //if single person table, set orderNumber according to table tag
                {
                    agent.enabled = false;

                    if (targetTable.CompareTag("Table1")) { orderNumber = 0; food1Image.SetActive(true); } //if table 1, order shirts
                    else if (targetTable.CompareTag("Table2")) { orderNumber = 1; food2Image.SetActive(true); } //if table 2, order jeans and so on
                    else if (targetTable.CompareTag("Table6")) { orderNumber = 0; food1Image.SetActive(true); }
                    else if (targetTable.CompareTag("Table7")) { orderNumber = 1; food2Image.SetActive(true); }
                    else if (targetTable.CompareTag("Table5")) { orderNumber = 2; food3Image.SetActive(true); }
                    else if (targetTable.CompareTag("Table8")) { orderNumber = 2; food3Image.SetActive(true); }
                    else if (targetTable.CompareTag("Table3")) //is mix table
                    {
                        orderNumber = Random.Range(0, 3);

                        if (orderNumber == 0) { food1Image.SetActive(true); }
                        else if (orderNumber == 1) { food2Image.SetActive(true); }
                        else if (orderNumber == 2) { food3Image.SetActive(true); }
                    }
                    else if (targetTable.CompareTag("Table4")) //is mix table
                    {
                        orderNumber = Random.Range(0, 3);

                        if (orderNumber == 0) { food1Image.SetActive(true); }
                        else if (orderNumber == 1) { food2Image.SetActive(true); }
                        else if (orderNumber == 2) { food3Image.SetActive(true); }
                    }

                    transform.position = other.gameObject.GetComponent<TableView>().customerSitPos.position;
                    transform.rotation = other.gameObject.GetComponent<TableView>().customerSitPos.rotation;
                    StartCoroutine(SitOnTable());
                }
                if(other.gameObject.GetComponent<TableView>().customerCount > 1)
                {
                    if(other.gameObject.GetComponent<TableView>().customersInTable == 3)
                    {
                        agent.enabled = false;

                        //if (other.gameObject.GetComponent<TableView>().Customer1Ontable.GetComponent<CustomerView>().orderNumber == 0) { food1Image.SetActive(true); orderNumber = 0; }
                        //if (other.gameObject.GetComponent<TableView>().Customer1Ontable.GetComponent<CustomerView>().orderNumber == 1) { food2Image.SetActive(true); orderNumber = 1;}
                        //if (other.gameObject.GetComponent<TableView>().Customer1Ontable.GetComponent<CustomerView>().orderNumber == 2) { food3Image.SetActive(true); orderNumber = 2;}

                        orderNumber = Random.Range(0, 3);

                        if (orderNumber == 0) { food1Image.SetActive(true); }
                        else if (orderNumber == 1) { food2Image.SetActive(true); }
                        else if (orderNumber == 2) { food3Image.SetActive(true); }

                        transform.position = other.gameObject.GetComponent<TableView>().customerSitPos4.position;
                        transform.rotation = other.gameObject.GetComponent<TableView>().customerSitPos4.rotation;
                        other.gameObject.GetComponent<TableView>().customersInTable++;
                        StartCoroutine(SitOnTable());
                    }
                    if(other.gameObject.GetComponent<TableView>().customersInTable == 2)
                    {
                        agent.enabled = false;

                        //if (other.gameObject.GetComponent<TableView>().Customer1Ontable.GetComponent<CustomerView>().orderNumber == 0) { food1Image.SetActive(true); orderNumber = 0;}
                        //if (other.gameObject.GetComponent<TableView>().Customer1Ontable.GetComponent<CustomerView>().orderNumber == 1) { food2Image.SetActive(true); orderNumber = 1;}
                        //if (other.gameObject.GetComponent<TableView>().Customer1Ontable.GetComponent<CustomerView>().orderNumber == 2) { food3Image.SetActive(true); orderNumber = 2;}

                        orderNumber = Random.Range(0, 3);

                        if (orderNumber == 0) { food1Image.SetActive(true); }
                        else if (orderNumber == 1) { food2Image.SetActive(true); }
                        else if (orderNumber == 2) { food3Image.SetActive(true); }

                        transform.position = other.gameObject.GetComponent<TableView>().customerSitPos3.position;
                        transform.rotation = other.gameObject.GetComponent<TableView>().customerSitPos3.rotation;
                        other.gameObject.GetComponent<TableView>().customersInTable++;
                        StartCoroutine(SitOnTable());
                    }
                    if(other.gameObject.GetComponent<TableView>().customersInTable == 1)
                    {
                        agent.enabled = false;

                        //if (other.gameObject.GetComponent<TableView>().Customer1Ontable.GetComponent<CustomerView>().orderNumber == 0) { food1Image.SetActive(true); orderNumber = 0;}
                        //if (other.gameObject.GetComponent<TableView>().Customer1Ontable.GetComponent<CustomerView>().orderNumber == 1) { food2Image.SetActive(true); orderNumber = 1;}
                        //if (other.gameObject.GetComponent<TableView>().Customer1Ontable.GetComponent<CustomerView>().orderNumber == 2) { food3Image.SetActive(true); orderNumber = 2;}
                        
                        orderNumber = Random.Range(0, 3);

                        if (orderNumber == 0) { food1Image.SetActive(true); }
                        else if (orderNumber == 1) { food2Image.SetActive(true); }
                        else if (orderNumber == 2) { food3Image.SetActive(true); }

                        transform.position = other.gameObject.GetComponent<TableView>().customerSitPos2.position;
                        transform.rotation = other.gameObject.GetComponent<TableView>().customerSitPos2.rotation;
                        other.gameObject.GetComponent<TableView>().customersInTable++;
                        StartCoroutine(SitOnTable());
                    }
                    if(other.gameObject.GetComponent<TableView>().customersInTable == 0)
                    {
                        agent.enabled = false;

                        //if (orderNumber == 0) { food1Image.SetActive(true); }
                        //if (orderNumber == 1) { food2Image.SetActive(true); }
                        //if (orderNumber == 2) { food3Image.SetActive(true); }
                        if (targetTable.CompareTag("Table1")) { orderNumber = 0; food1Image.SetActive(true); } //if table 1, order shirts
                        else if (targetTable.CompareTag("Table2")) { orderNumber = 1; food2Image.SetActive(true); } //if table 2, order jeans and so on
                        else if (targetTable.CompareTag("Table6")) { orderNumber = 0; food1Image.SetActive(true); }
                        else if (targetTable.CompareTag("Table7")) { orderNumber = 1; food2Image.SetActive(true); }
                        else if (targetTable.CompareTag("Table5")) { orderNumber = 2; food3Image.SetActive(true); }
                        else if (targetTable.CompareTag("Table8")) { orderNumber = 2; food3Image.SetActive(true); }
                        else if (targetTable.CompareTag("Table3")) //is mix table
                        {
                            orderNumber = Random.Range(0, 3);

                            if (orderNumber == 0) { food1Image.SetActive(true); }
                            else if (orderNumber == 1) { food2Image.SetActive(true); }
                            else if (orderNumber == 2) { food3Image.SetActive(true); }
                        }
                        else if (targetTable.CompareTag("Table4")) //is mix table
                        {
                            orderNumber = Random.Range(0, 3);

                            if (orderNumber == 0) { food1Image.SetActive(true); }
                            else if (orderNumber == 1) { food2Image.SetActive(true); }
                            else if (orderNumber == 2) { food3Image.SetActive(true); }
                        }

                        transform.position = other.gameObject.GetComponent<TableView>().customerSitPos.position;
                        transform.rotation = other.gameObject.GetComponent<TableView>().customerSitPos.rotation;
                        other.gameObject.GetComponent<TableView>().customersInTable++;
                        StartCoroutine(SitOnTable());
                    }
                }
            }
        }
    }

    public IEnumerator SitOnTable()
    {
        targetTable.gameObject.GetComponent<TableView>().isTableBooked = true;
        customerAnimator.SetBool("Sit", true);
        customerAnimator.SetBool("Idle", false);
        customerAnimator.SetBool("Eat", false);
        customerAnimator.SetBool("SitIdle", false);
        yield return new WaitForSeconds(1f);
        Eat();
    }

    public void Eat()
    {
        TableView tableView = targetTable.gameObject.GetComponent<TableView>();
        if (!tableView.isBurgerOnTable && !tableView.isFriesOnTable && !tableView.isCokeOnTable) return;


        
        if(orderNumber == 0)
        {
            if(targetTable.gameObject.GetComponent<TableView>().firstFoodOnFirstTableCount == 0) return;
            isCustomerEating = true;
            customerAnimator.SetBool("Eat", true);
            customerAnimator.SetBool("SitIdle", false);
            targetTable.gameObject.GetComponent<TableView>().BuyShirtByCustomer();
        }
        if(orderNumber == 1)
        {
            if(targetTable.gameObject.GetComponent<TableView>().friesOnFirstTableCount == 0) return;
            isCustomerEating = true;
            customerAnimator.SetBool("Eat", true);
            customerAnimator.SetBool("SitIdle", false);
            targetTable.gameObject.GetComponent<TableView>().BuyJeansByCustomer();
        }
        if(orderNumber == 2)
        {
            if(targetTable.gameObject.GetComponent<TableView>().cokeOnFirstTableCount == 0) return;
            isCustomerEating = true;
            customerAnimator.SetBool("Eat", true);
            customerAnimator.SetBool("SitIdle", false);
            targetTable.gameObject.GetComponent<TableView>().BuyFrockByCustomer();
        }
    }

    public void InvokEat(float time)
    {
        InvokeRepeating(nameof(Eat), time, time);
    }
    public void CancelInvokEat()
    {
        CancelInvoke(nameof(Eat));
    }
}
