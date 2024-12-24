using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
using DG.Tweening;

public class TableView : MonoBehaviour
{
    public static TableView instance;
    public GameObject moneyPrefab;
    public Transform moneyInstancePos;
    public Transform foodTransferPos;
    
    public Transform customerSitPos;
    public Transform customerSitPos2;
    public Transform customerSitPos3;
    public Transform customerSitPos4;

    public GameObject Customer1Ontable;
    public GameObject Customer2Ontable;
    public GameObject Customer3Ontable;
    public GameObject Customer4Ontable;

    public int moneyTransferCount;
    public int customerCount;
    [HideInInspector] public int customersInTable;
    public GameObject[] moneys;
    public List<GameObject> destroyableMoney = new List<GameObject>();
    public GameObject[] table1FirstFoodDummy;
    public int firstFoodOnFirstTableCount;
    public GameObject[] table1FriesDummy;
    public int friesOnFirstTableCount;
    public GameObject[] table1CokeDummy;
    public int cokeOnFirstTableCount;
    public bool isTableBooked;
    public bool isBurgerOnTable,isFriesOnTable,isCokeOnTable;
    public bool isPlayerOnTable;
    public bool isWaiterOnTable;


    private void Awake() {instance = this;}


    private void Start() 
    {
        if(Controller.instance.customersController.waitingCustomers.Count >= customerCount)
        {
            Controller.instance.customersController.CallWaitingCustomer(customerCount, this.transform.position, this.gameObject);
        }
        else
        {
            StartCoroutine(Controller.instance.customersController.InstantiateCustomers(customerCount, this.transform.position,this.gameObject));
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isPlayerOnTable = true;
        }
    }
    private void OnCollisionExit(Collision other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isPlayerOnTable = false;
            if (firstFoodOnFirstTableCount != 0 || friesOnFirstTableCount != 0 || cokeOnFirstTableCount != 0)
            {
                if (!Customer1Ontable.GetComponent<CustomerView>().isCustomerEating && isTableBooked && Customer1Ontable.GetComponent<CustomerView>().orderNumber == 0) { Invoke(nameof(BuyShirtByCustomer), 1f); }
                if (!Customer1Ontable.GetComponent<CustomerView>().isCustomerEating && isTableBooked && Customer1Ontable.GetComponent<CustomerView>().orderNumber == 1) { Invoke(nameof(BuyJeansByCustomer), 1f); }
                if (!Customer1Ontable.GetComponent<CustomerView>().isCustomerEating && isTableBooked && Customer1Ontable.GetComponent<CustomerView>().orderNumber == 2) { Invoke(nameof(BuyFrockByCustomer), 1f); }
            }
        }
    }
    public void InstanceAndMoveMoney1Customer()
    {
        if(moneyTransferCount == moneys.Length) return;
        GameObject money = LeanPool.Spawn(moneyPrefab, moneyInstancePos.position, moneyPrefab.transform.rotation);
        destroyableMoney.Add(money);
        money.transform.DOMove(moneys[moneyTransferCount].transform.position, 0.4f).SetEase(Ease.InExpo).OnComplete(() =>
         {
             moneys[moneyTransferCount].SetActive(true);
             moneyTransferCount++;
             LeanPool.Despawn(money);
         });
        GameObject money2 = LeanPool.Spawn(moneyPrefab, moneyInstancePos.position, moneyPrefab.transform.rotation);
        destroyableMoney.Add(money);
        money2.transform.DOMove(moneys[moneyTransferCount].transform.position, 0.4f).SetEase(Ease.InExpo).OnComplete(() =>
         {
             moneys[moneyTransferCount].SetActive(true);
             moneyTransferCount++;
             LeanPool.Despawn(money2);
         });
    }
    public void InstanceAndMoveMoney4Customer()
    {
        if(moneyTransferCount == moneys.Length) return;
        GameObject money = LeanPool.Spawn(moneyPrefab, customerSitPos.position, moneyPrefab.transform.rotation);
        money.transform.DOMove(moneys[moneyTransferCount].transform.position, 0.5f).SetEase(Ease.InExpo).OnComplete(() =>
         {
             moneys[moneyTransferCount].SetActive(true);
             moneyTransferCount++;
             LeanPool.Despawn(money);
         });
        GameObject money2 = LeanPool.Spawn(moneyPrefab, customerSitPos2.position, moneyPrefab.transform.rotation);
        money2.transform.DOMove(moneys[moneyTransferCount].transform.position, 0.5f).SetEase(Ease.InExpo).OnComplete(() =>
         {
             moneys[moneyTransferCount].SetActive(true);
             moneyTransferCount++;
             LeanPool.Despawn(money2);
         });
        GameObject money3 = LeanPool.Spawn(moneyPrefab, customerSitPos3.position, moneyPrefab.transform.rotation);
        money3.transform.DOMove(moneys[moneyTransferCount].transform.position, 0.5f).SetEase(Ease.InExpo).OnComplete(() =>
         {
             moneys[moneyTransferCount].SetActive(true);
             moneyTransferCount++;
             LeanPool.Despawn(money3);
         });
        GameObject money4 = LeanPool.Spawn(moneyPrefab, customerSitPos4.position, moneyPrefab.transform.rotation);
        money4.transform.DOMove(moneys[moneyTransferCount].transform.position, 0.5f).SetEase(Ease.InExpo).OnComplete(() =>
         {
             moneys[moneyTransferCount].SetActive(true);
             moneyTransferCount++;
             LeanPool.Despawn(money4);
         });
    }

    public void BuyShirtByCustomer()
    {
        if(!isTableBooked) return;
        if(isPlayerOnTable)
        {
            if(GlobalData.instance.isFoodOnHand || GlobalData.instance.isFoodMovingToTable) return;
        }
        
        if(Customer1Ontable.GetComponent<CustomerView>().foodEaten >= Customer1Ontable.GetComponent<CustomerView>().foodEatingcapacity)
        {

            MoveCustomerAfterEat();
            if(Controller.instance.customersController.waitingCustomers.Count >= customerCount)
            {
                Controller.instance.customersController.CallWaitingCustomer(customerCount, this.transform.position,this.gameObject);
            }
            else if(Controller.instance.customersController.waitingCustomers.Count < customerCount)
            {
                StartCoroutine(Controller.instance.customersController.InstantiateCustomers(customerCount, this.transform.position,this.gameObject));
            }
            return;
        }

        if(firstFoodOnFirstTableCount == 0 )
        {
            Customer1Ontable.GetComponent<CustomerView>().InvokEat(3);

            if (customerCount == 1)
            {
                Customer1Ontable.GetComponent<CustomerView>().isCustomerEating = false;
                Customer1Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("SitIdle", true);
                Customer1Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("Sit", false);
                Customer1Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("Eat", false);
                return;
            }
            else
            {            
                Customer1Ontable.GetComponent<CustomerView>().isCustomerEating = false;
                Customer1Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("SitIdle", true);
                Customer1Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("Sit", false);
                Customer1Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("Eat", false);

                Customer2Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("SitIdle", true);
                Customer2Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("Sit", false);
                Customer2Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("Eat", false);

                Customer3Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("SitIdle", true);
                Customer3Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("Sit", false);
                Customer3Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("Eat", false);

                Customer4Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("SitIdle", true);
                Customer4Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("Sit", false);
                Customer4Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("Eat", false);
                return;
            }
        }
        else
        {
            if (customerCount == 1)
            {
                Customer1Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("SitIdle", false);
            }
            else
            {
                Customer1Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("SitIdle", false);
                Customer2Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("SitIdle", false);
                Customer3Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("SitIdle", false);
                Customer4Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("SitIdle", false);
            }
        }


        CancelInvoke();

        if(firstFoodOnFirstTableCount > 0)
        {
            Customer1Ontable.GetComponent<CustomerView>().CancelInvokEat();

            PlayEatAnimation();
            Customer1Ontable.GetComponent<CustomerView>().isCustomerEating = true;
            GameObject food = LeanPool.Spawn(CoockerView.instance.firstFoodPrefab, table1FirstFoodDummy[firstFoodOnFirstTableCount - 1].transform.position, table1FirstFoodDummy[firstFoodOnFirstTableCount - 1].transform.rotation);
            table1FirstFoodDummy[firstFoodOnFirstTableCount - 1].SetActive(false);
            Customer1Ontable.GetComponent<CustomerView>().foodEaten++;
            
            // int index = firstFoodOnFirstTableCount;
            firstFoodOnFirstTableCount--;
            food.transform.DOScale(Vector3.zero, 0.6f).SetEase(Ease.InOutBack).OnComplete(() => 
            {
                if(customerCount == 1){InstanceAndMoveMoney1Customer();}
                if(customerCount > 1){InstanceAndMoveMoney1Customer(); InstanceAndMoveMoney1Customer();}
                LeanPool.Despawn(food);
                if(firstFoodOnFirstTableCount > 0)
                {
                    Invoke(nameof(BuyShirtByCustomer), 0.2f);
                }
                if(firstFoodOnFirstTableCount == 0)
                {
                    isBurgerOnTable = false;
                    Invoke(nameof(BuyShirtByCustomer), 0.2f);
                }
            });
        }
        else
        {

        }
    }
    public void BuyJeansByCustomer()
    {
        if(!isTableBooked) return;
        if(isPlayerOnTable)
        {
            if(GlobalData.instance.isFoodOnHand || GlobalData.instance.isFoodMovingToTable) return;
        }

        if(Customer1Ontable.GetComponent<CustomerView>().foodEaten >= Customer1Ontable.GetComponent<CustomerView>().foodEatingcapacity)
        {

            MoveCustomerAfterEat();
            if(Controller.instance.customersController.waitingCustomers.Count >= customerCount)
            {
                Controller.instance.customersController.CallWaitingCustomer(customerCount, this.transform.position,this.gameObject);
            }
            else if(Controller.instance.customersController.waitingCustomers.Count < customerCount)
            {
                StartCoroutine(Controller.instance.customersController.InstantiateCustomers(customerCount, this.transform.position,this.gameObject));
            }
            return;
        }

        if(friesOnFirstTableCount == 0 )
        {
            Customer1Ontable.GetComponent<CustomerView>().InvokEat(3);
            if (customerCount == 1)
            {
                Customer1Ontable.GetComponent<CustomerView>().isCustomerEating = false;
                Customer1Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("SitIdle", true);
                Customer1Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("Sit", false);
                Customer1Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("Eat", false);
                return;
            }
            else
            {
                Customer1Ontable.GetComponent<CustomerView>().isCustomerEating = false;
                Customer1Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("SitIdle", true);
                Customer1Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("Sit", false);
                Customer1Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("Eat", false);

                Customer2Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("SitIdle", true);
                Customer2Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("Sit", false);
                Customer2Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("Eat", false);

                Customer3Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("SitIdle", true);
                Customer3Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("Sit", false);
                Customer3Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("Eat", false);

                Customer4Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("SitIdle", true);
                Customer4Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("Sit", false);
                Customer4Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("Eat", false);
                return;
            }
        }
        else
        {
            if (customerCount == 1)
            {
                Customer1Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("SitIdle", false);
            }
            else
            {
                Customer1Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("SitIdle", false);
                Customer2Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("SitIdle", false);
                Customer3Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("SitIdle", false);
                Customer4Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("SitIdle", false);
            }
        }


        if(friesOnFirstTableCount > 0)
        {
            Customer1Ontable.GetComponent<CustomerView>().CancelInvokEat();

            PlayEatAnimation();
            Customer1Ontable.GetComponent<CustomerView>().isCustomerEating = true;
            GameObject food = LeanPool.Spawn(CoockerView.instance.secondFoodPrefab, table1FriesDummy[friesOnFirstTableCount - 1].transform.position, table1FriesDummy[friesOnFirstTableCount - 1].transform.rotation);
            table1FriesDummy[friesOnFirstTableCount - 1].SetActive(false);
            Customer1Ontable.GetComponent<CustomerView>().foodEaten++;
            
            friesOnFirstTableCount--;
            food.transform.DOScale(Vector3.zero, 0.6f).SetEase(Ease.InOutBack).OnComplete(() => 
            {
                if(customerCount == 1){InstanceAndMoveMoney1Customer();}
                if(customerCount > 1){InstanceAndMoveMoney1Customer(); InstanceAndMoveMoney1Customer();}
                LeanPool.Despawn(food);
                if(friesOnFirstTableCount > 0)
                {
                    Invoke(nameof(BuyJeansByCustomer), 0.2f);
                }
                if(friesOnFirstTableCount == 0)
                {
                    isFriesOnTable = false;
                    Invoke(nameof(BuyJeansByCustomer), 0.2f);
                }
            });
        }
        else
        {

        }
    }
    public void BuyFrockByCustomer()
    {
        if(!isTableBooked) return;
        if(isPlayerOnTable)
        {
            if(GlobalData.instance.isFoodOnHand || GlobalData.instance.isFoodMovingToTable) return;
        }

        if(Customer1Ontable.GetComponent<CustomerView>().foodEaten >= Customer1Ontable.GetComponent<CustomerView>().foodEatingcapacity)
        {

            MoveCustomerAfterEat();
            if(Controller.instance.customersController.waitingCustomers.Count >= customerCount)
            {
                Controller.instance.customersController.CallWaitingCustomer(customerCount, this.transform.position,this.gameObject);
            }
            else if(Controller.instance.customersController.waitingCustomers.Count < customerCount)
            {
                StartCoroutine(Controller.instance.customersController.InstantiateCustomers(customerCount, this.transform.position,this.gameObject));
            }
            return;
        }

        if(cokeOnFirstTableCount == 0 )
        {
            Customer1Ontable.GetComponent<CustomerView>().InvokEat(3);

            if (customerCount == 1)
            {
                Customer1Ontable.GetComponent<CustomerView>().isCustomerEating = false;
                Customer1Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("SitIdle", true);
                Customer1Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("Sit", false);
                Customer1Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("Eat", false);
                return;
            }
            else
            {
                Customer1Ontable.GetComponent<CustomerView>().isCustomerEating = false;
                Customer1Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("SitIdle", true);
                Customer1Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("Sit", false);
                Customer1Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("Eat", false);

                Customer2Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("SitIdle", true);
                Customer2Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("Sit", false);
                Customer2Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("Eat", false);

                Customer3Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("SitIdle", true);
                Customer3Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("Sit", false);
                Customer3Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("Eat", false);

                Customer4Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("SitIdle", true);
                Customer4Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("Sit", false);
                Customer4Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("Eat", false);
                return;
            }
        }
        else
        {
            if (customerCount == 1)
            {
                Customer1Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("SitIdle", false);
            }
            else
            {
                Customer1Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("SitIdle", false);
                Customer2Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("SitIdle", false);
                Customer3Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("SitIdle", false);
                Customer4Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("SitIdle", false);
            }
        }

        if(cokeOnFirstTableCount > 0)
        {
            Customer1Ontable.GetComponent<CustomerView>().CancelInvokEat();

            PlayEatAnimation();
            Customer1Ontable.GetComponent<CustomerView>().isCustomerEating = true;
            GameObject food = LeanPool.Spawn(CoockerView.instance.thirdFoodPrefab, table1CokeDummy[cokeOnFirstTableCount - 1].transform.position, table1CokeDummy[cokeOnFirstTableCount - 1].transform.rotation);
            table1CokeDummy[cokeOnFirstTableCount - 1].SetActive(false);
            Customer1Ontable.GetComponent<CustomerView>().foodEaten++;
            
            cokeOnFirstTableCount--;
            food.transform.DOScale(Vector3.zero, 0.6f).SetEase(Ease.InOutBack).OnComplete(() => 
            {
                if(customerCount == 1){InstanceAndMoveMoney1Customer();}
                if(customerCount > 1){InstanceAndMoveMoney1Customer();InstanceAndMoveMoney1Customer();}
                LeanPool.Despawn(food);
                if(cokeOnFirstTableCount > 0)
                {
                    Invoke(nameof(BuyFrockByCustomer), 0.2f);
                }
                if(cokeOnFirstTableCount == 0)
                {
                    isCokeOnTable = false;
                    Invoke(nameof(BuyFrockByCustomer), 0.2f);
                }
            });
        }
        else
        {

        }
    }

    public void CollectMoney()
    {
        if(moneyTransferCount > 0)
        {
            int a = moneyTransferCount;
            Controller.instance.currencyController.AddMoney(a * Controller.instance.currencyController.perBundelMoney);
            Controller.instance.uiController.UpdateMoneyTexts();
            moneyTransferCount = 0;
            Controller.instance.gameController.PlayHaptic();
            for (int i = 0; i < a; i++)
            {
                GameObject m = LeanPool.Spawn(Controller.instance.currencyController.moneyPrefab, moneys[i].transform.position, moneys[i].transform.rotation);
                m.transform.DOMove(PLayerView.instance.moneyMovePos.position, 0.7f).OnComplete(() => { LeanPool.Despawn(m); });
                m.transform.DOScale(Vector3.zero, 0.5f);

                moneys[i].SetActive(false);
            }
        }
    }

    public void MoveCustomerAfterEat()
    {
        isTableBooked = false;
        DailyTaskView.instance.CustomerServedCount(1);

        if(customerCount == 1)
        {
            CustomerView customerView = Customer1Ontable.GetComponent<CustomerView>();

            customerView.food1Image.SetActive(false);
            customerView.food2Image.SetActive(false);
            customerView.food3Image.SetActive(false);

            customerView.isCustomerEating = false;
            customerView.agent.enabled = true;
            customerView.isComming = false;
            customerView.agent.SetDestination(Controller.instance.customersController.destroyPos.position);

            customerView.customerAnimator.SetBool("Sit", false);
            customerView.customerAnimator.SetBool("Eat", false);
            customerView.customerAnimator.SetBool("SitIdle", false);
            customerView.customerAnimator.SetBool("Run", true);
        }
        else
        {
            CustomerView customerView1 = Customer1Ontable.GetComponent<CustomerView>();
            CustomerView customerView2 = Customer2Ontable.GetComponent<CustomerView>();
            CustomerView customerView3 = Customer3Ontable.GetComponent<CustomerView>();
            CustomerView customerView4 = Customer4Ontable.GetComponent<CustomerView>();

            customerView1.food1Image.SetActive(false);
            customerView1.food2Image.SetActive(false);
            customerView1.food3Image.SetActive(false);

            customersInTable = 0;
            customerView1.isCustomerEating = false;
            customerView1.agent.enabled = true;
            customerView1.isComming = false;
            customerView1.agent.SetDestination(Controller.instance.customersController.destroyPos.position);
            customerView1.customerAnimator.SetBool("Sit", false);
            customerView1.customerAnimator.SetBool("Eat", false);
            customerView1.customerAnimator.SetBool("SitIdle", false);
            customerView1.customerAnimator.SetBool("Run", true);

            customerView2.food1Image.SetActive(false);
            customerView2.food2Image.SetActive(false);
            customerView2.food3Image.SetActive(false);

            customerView2.agent.enabled = true;
            customerView2.isComming = false;
            customerView2.agent.SetDestination(Controller.instance.customersController.destroyPos.position);
            customerView2.customerAnimator.SetBool("Sit", false);
            customerView2.customerAnimator.SetBool("Eat", false);
            customerView2.customerAnimator.SetBool("SitIdle", false);
            customerView2.customerAnimator.SetBool("Run", true);

            customerView3.food1Image.SetActive(false);
            customerView3.food2Image.SetActive(false);
            customerView3.food3Image.SetActive(false);

            customerView3.agent.enabled = true;
            customerView3.isComming = false;
            customerView3.agent.SetDestination(Controller.instance.customersController.destroyPos.position);
            customerView3.customerAnimator.SetBool("Sit", false);
            customerView3.customerAnimator.SetBool("Eat", false);
            customerView3.customerAnimator.SetBool("SitIdle", false);
            customerView3.customerAnimator.SetBool("Run", true);
            
            customerView4.food1Image.SetActive(false);
            customerView4.food2Image.SetActive(false);
            customerView4.food3Image.SetActive(false);

            customerView4.agent.enabled = true;
            customerView4.isComming = false;
            customerView4.agent.SetDestination(Controller.instance.customersController.destroyPos.position);
            customerView4.customerAnimator.SetBool("Sit", false);
            customerView4.customerAnimator.SetBool("Eat", false);
            customerView4.customerAnimator.SetBool("SitIdle", false);
            customerView4.customerAnimator.SetBool("Run", true);
        }
    }

    public void PlayEatAnimation()
    {
        if(customerCount==1)
        {
            Customer1Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("Eat", true);
            Customer1Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("SitIdle", false);
        }
        else
        {
            Customer1Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("Eat", true);
            Customer1Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("SitIdle", false);
            Customer2Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("Eat", true);
            Customer2Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("SitIdle", false);
            Customer3Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("Eat", true);
            Customer3Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("SitIdle", false);
            Customer4Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("Eat", true);
            Customer4Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("SitIdle", false);
        }
    }
}
