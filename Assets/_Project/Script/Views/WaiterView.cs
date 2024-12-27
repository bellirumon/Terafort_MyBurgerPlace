using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Lean.Pool;
using DG.Tweening;

public class WaiterView : MonoBehaviour
{
    public static WaiterView instance;
    public enum State
    {
        idle = 0,
        walk = 1,
        walkWithBox = 2,
        idleWithBox = 3,
    }
    public int firstFoodOnhandCount;
    public int secondFoodOnhandCount;
    public int thirdFoodOnhandCount;
    public int maxFoodOnHandCount;

    public MeshRenderer[] dummyBurgers;
    public MeshRenderer[] dummyFries;
    public MeshRenderer[] dummyCoke;
    [Space]

    public Transform burgerPoint;
    public Transform friesPoint;
    public Transform cokePoint;

    public Transform table1Point;
    public Transform table2Point;
    public Transform table3Point;
    public Transform table4Point;

    public Transform packagingTablePoint;
    public Transform[] tablePoints;
    public Transform[] onlyTablePoints;

    public GameObject targetTable;

    public GameObject randomSelectedTable;

    public Animator animator;
    public NavMeshAgent agent;

    public bool isShirtsOnHouse, isJeansOnHouse, isFrocksOnHouse;

    public bool isTable1, isTable2, isTable3, isTable4, isTable5, isTable6, isTable7, isTable8, isDeliveryTable1, isDeliveryTable2;
    public bool onTheWayToServe;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("WaiterSpeed"))
        {
            agent.speed = PlayerPrefs.GetFloat("WaiterSpeed");
        }
        if (PlayerPrefs.HasKey("WaiterCapacity"))
        {
            maxFoodOnHandCount = PlayerPrefs.GetInt("WaiterCapacity");
        }
        InvokeRepeating(nameof(MoveToDestination), 1f, 3f);
    }

    public void ResetAndBackToCurt()
    {
        ResetTablesBool();
        for (int i = 0; i < dummyBurgers.Length; i++)
        {
            dummyBurgers[i].enabled = false;
        }
        for (int i = 0; i < dummyFries.Length; i++)
        {
            dummyFries[i].enabled = false;
        }
        for (int i = 0; i < dummyCoke.Length; i++)
        {
            dummyCoke[i].enabled = false;
        }

        animator.SetInteger("Base", 1);

        firstFoodOnhandCount = 0;
        secondFoodOnhandCount = 0;
        thirdFoodOnhandCount = 0;

        MoveWaiterToCurt();
    }

    public void MoveToDestination()
    {
        CheckFoods();
        if (isShirtsOnHouse) { agent.SetDestination(burgerPoint.position); animator.SetInteger("Base", (int)State.walk); CancelInvoke(nameof(MoveToDestination)); }
        if (isJeansOnHouse) { agent.SetDestination(friesPoint.position); animator.SetInteger("Base", (int)State.walk); CancelInvoke(nameof(MoveToDestination)); }
        if (isFrocksOnHouse) { agent.SetDestination(cokePoint.position); animator.SetInteger("Base", (int)State.walk); CancelInvoke(nameof(MoveToDestination)); }
    }

    public void CheckFoods()
    {
        //First Table......
        if (AllLockAndUnlockView.instance.table1.GetComponent<TableView>().Customer1Ontable != null)
        {
            if (AllLockAndUnlockView.instance.table1.GetComponent<TableView>().Customer1Ontable.GetComponent<CustomerView>().orderNumber == 0)
            {
                isShirtsOnHouse = true;
            }
            else if (AllLockAndUnlockView.instance.table1.GetComponent<TableView>().Customer1Ontable.GetComponent<CustomerView>().orderNumber == 1)
            {
                isJeansOnHouse = true;
            }
            else if (AllLockAndUnlockView.instance.table1.GetComponent<TableView>().Customer1Ontable.GetComponent<CustomerView>().orderNumber == 2)
            {
                isFrocksOnHouse = true;
            }
        }

        //Second Table .....
        if (AllLockAndUnlockView.instance.table2.GetComponent<TableView>().Customer1Ontable != null)
        {
            if (AllLockAndUnlockView.instance.table2.GetComponent<TableView>().Customer1Ontable.GetComponent<CustomerView>().orderNumber == 0)
            {
                isShirtsOnHouse = true;
            }
            else if (AllLockAndUnlockView.instance.table2.GetComponent<TableView>().Customer1Ontable.GetComponent<CustomerView>().orderNumber == 1)
            {
                isJeansOnHouse = true;
            }
            else if (AllLockAndUnlockView.instance.table2.GetComponent<TableView>().Customer1Ontable.GetComponent<CustomerView>().orderNumber == 2)
            {
                isFrocksOnHouse = true;
            }
        }

        //Third Table .....
        if (AllLockAndUnlockView.instance.table3.GetComponent<TableView>().Customer1Ontable != null)
        {
            if (AllLockAndUnlockView.instance.table3.GetComponent<TableView>().Customer1Ontable.GetComponent<CustomerView>().orderNumber == 0)
            {
                isShirtsOnHouse = true;
            }
            else if (AllLockAndUnlockView.instance.table3.GetComponent<TableView>().Customer1Ontable.GetComponent<CustomerView>().orderNumber == 1)
            {
                isJeansOnHouse = true;
            }
            else if (AllLockAndUnlockView.instance.table3.GetComponent<TableView>().Customer1Ontable.GetComponent<CustomerView>().orderNumber == 2)
            {
                isFrocksOnHouse = true;
            }
        }

        //Fourth Table .....
        if (AllLockAndUnlockView.instance.table4.GetComponent<TableView>().Customer1Ontable != null)
        {
            if (AllLockAndUnlockView.instance.table4.GetComponent<TableView>().Customer1Ontable.GetComponent<CustomerView>().orderNumber == 0)
            {
                isShirtsOnHouse = true;
            }
            else if (AllLockAndUnlockView.instance.table4.GetComponent<TableView>().Customer1Ontable.GetComponent<CustomerView>().orderNumber == 1)
            {
                isJeansOnHouse = true;
            }
            else if (AllLockAndUnlockView.instance.table4.GetComponent<TableView>().Customer1Ontable.GetComponent<CustomerView>().orderNumber == 2)
            {
                isFrocksOnHouse = true;
            }
        }
        //Fifth Table .....
        if (AllLockAndUnlockView.instance.table5.GetComponent<TableView>().Customer1Ontable != null)
        {
            if (AllLockAndUnlockView.instance.table5.GetComponent<TableView>().Customer1Ontable.GetComponent<CustomerView>().orderNumber == 0)
            {
                isShirtsOnHouse = true;
            }
            else if (AllLockAndUnlockView.instance.table5.GetComponent<TableView>().Customer1Ontable.GetComponent<CustomerView>().orderNumber == 1)
            {
                isJeansOnHouse = true;
            }
            else if (AllLockAndUnlockView.instance.table5.GetComponent<TableView>().Customer1Ontable.GetComponent<CustomerView>().orderNumber == 2)
            {
                isFrocksOnHouse = true;
            }
        }
        //Sixth Table .....
        if (AllLockAndUnlockView.instance.table6.GetComponent<TableView>().Customer1Ontable != null)
        {
            if (AllLockAndUnlockView.instance.table6.GetComponent<TableView>().Customer1Ontable.GetComponent<CustomerView>().orderNumber == 0)
            {
                isShirtsOnHouse = true;
            }
            else if (AllLockAndUnlockView.instance.table6.GetComponent<TableView>().Customer1Ontable.GetComponent<CustomerView>().orderNumber == 1)
            {
                isJeansOnHouse = true;
            }
            else if (AllLockAndUnlockView.instance.table6.GetComponent<TableView>().Customer1Ontable.GetComponent<CustomerView>().orderNumber == 2)
            {
                isFrocksOnHouse = true;
            }
        }
        //Seventh Table .....
        if (AllLockAndUnlockView.instance.table7.GetComponent<TableView>().Customer1Ontable != null)
        {
            if (AllLockAndUnlockView.instance.table7.GetComponent<TableView>().Customer1Ontable.GetComponent<CustomerView>().orderNumber == 0)
            {
                isShirtsOnHouse = true;
            }
            else if (AllLockAndUnlockView.instance.table7.GetComponent<TableView>().Customer1Ontable.GetComponent<CustomerView>().orderNumber == 1)
            {
                isJeansOnHouse = true;
            }
            else if (AllLockAndUnlockView.instance.table7.GetComponent<TableView>().Customer1Ontable.GetComponent<CustomerView>().orderNumber == 2)
            {
                isFrocksOnHouse = true;
            }
        }
        //Eigth Table .....
        if (AllLockAndUnlockView.instance.table8.GetComponent<TableView>().Customer1Ontable != null)
        {
            if (AllLockAndUnlockView.instance.table8.GetComponent<TableView>().Customer1Ontable.GetComponent<CustomerView>().orderNumber == 0)
            {
                isShirtsOnHouse = true;
            }
            else if (AllLockAndUnlockView.instance.table8.GetComponent<TableView>().Customer1Ontable.GetComponent<CustomerView>().orderNumber == 1)
            {
                isJeansOnHouse = true;
            }
            else if (AllLockAndUnlockView.instance.table8.GetComponent<TableView>().Customer1Ontable.GetComponent<CustomerView>().orderNumber == 2)
            {
                isFrocksOnHouse = true;
            }
        }
    }

    public void ResetFoodsBool()
    {
        isShirtsOnHouse = false;
        isJeansOnHouse = false;
        isFrocksOnHouse = false;
    }

    public void ResetTablesBool()
    {
        isTable1 = false;
        isTable2 = false;
        isTable3 = false;
        isTable4 = false;
        isTable5 = false;
        isTable6 = false;
        isTable7 = false;
        isTable8 = false;
        isDeliveryTable1 = false;
        isDeliveryTable2 = false;
    }

    static bool alreadyCalledFlag1 = false;
    static bool alreadyCalledFlag2 = false;
    static bool alreadyCalledFlag3 = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("WaiterBurgerPoint"))
        {
            if (CoockerView.instance.firstFoodStored <= 0)
            {
                CoockerView.instance.StopMakingShirts();
                CoockerView.instance.SewShirtsAndStack();
            }
            Callburger();
        }
        else if (other.gameObject.CompareTag("WaiterFriesPoint"))
        {
            if (CoockerView.instance.secondFoodStored <= 0)
            {
                CoockerView.instance.StopMakingJeans();
                CoockerView.instance.SewJeansAndStack();
            }
            CallFries();
        }
        else if (other.gameObject.CompareTag("WaiterCokePoint"))
        {
            if (CoockerView.instance.thirdFoodStored <= 0)
            {
                CoockerView.instance.StopMakingFrocks();
                CoockerView.instance.SewFrocksAndStack();
            }
            CallCoke();
        }

        else if (other.gameObject.CompareTag("WaiterTable1Point") && isTable1)
        {
            if (firstFoodOnhandCount > 0) { MoveShirtsToTable(); animator.SetInteger("Base", (int)State.idleWithBox); }
            else if (secondFoodOnhandCount > 0) { MoveJeansToTable(); animator.SetInteger("Base", (int)State.idleWithBox); }
            else if (thirdFoodOnhandCount > 0) { MoveFrocksToTable(); animator.SetInteger("Base", (int)State.idleWithBox); }
        }
        else if (other.gameObject.CompareTag("WaiterTable2Point") && isTable2)
        {
            if (firstFoodOnhandCount > 0) { MoveShirtsToTable(); animator.SetInteger("Base", (int)State.idleWithBox); }
            else if (secondFoodOnhandCount > 0) { MoveJeansToTable(); animator.SetInteger("Base", (int)State.idleWithBox); }
            else if (thirdFoodOnhandCount > 0) { MoveFrocksToTable(); animator.SetInteger("Base", (int)State.idleWithBox); }
        }
        else if (other.gameObject.CompareTag("WaiterTable3Point") && isTable3)
        {
            if (firstFoodOnhandCount > 0) { MoveShirtsToTable(); animator.SetInteger("Base", (int)State.idleWithBox); }
            else if (secondFoodOnhandCount > 0) { MoveJeansToTable(); animator.SetInteger("Base", (int)State.idleWithBox); }
            else if (thirdFoodOnhandCount > 0) { MoveFrocksToTable(); animator.SetInteger("Base", (int)State.idleWithBox); }
        }
        else if (other.gameObject.CompareTag("WaiterTable4Point") && isTable4)
        {
            if (firstFoodOnhandCount > 0) { MoveShirtsToTable(); animator.SetInteger("Base", (int)State.idleWithBox); }
            else if (secondFoodOnhandCount > 0) { MoveJeansToTable(); animator.SetInteger("Base", (int)State.idleWithBox); }
            else if (thirdFoodOnhandCount > 0) { MoveFrocksToTable(); animator.SetInteger("Base", (int)State.idleWithBox); }
        }
        else if (other.gameObject.CompareTag("WaiterTable5Point") && isTable5)
        {
            if (firstFoodOnhandCount > 0) { MoveShirtsToTable(); animator.SetInteger("Base", (int)State.idleWithBox); }
            else if (secondFoodOnhandCount > 0) { MoveJeansToTable(); animator.SetInteger("Base", (int)State.idleWithBox); }
            else if (thirdFoodOnhandCount > 0) { MoveFrocksToTable(); animator.SetInteger("Base", (int)State.idleWithBox); }
        }
        else if (other.gameObject.CompareTag("WaiterTable6Point") && isTable6)
        {
            if (firstFoodOnhandCount > 0) { MoveShirtsToTable(); animator.SetInteger("Base", (int)State.idleWithBox); }
            else if (secondFoodOnhandCount > 0) { MoveJeansToTable(); animator.SetInteger("Base", (int)State.idleWithBox); }
            else if (thirdFoodOnhandCount > 0) { MoveFrocksToTable(); animator.SetInteger("Base", (int)State.idleWithBox); }
        }
        else if (other.gameObject.CompareTag("WaiterTable7Point") && isTable7)
        {
            if (firstFoodOnhandCount > 0) { MoveShirtsToTable(); animator.SetInteger("Base", (int)State.idleWithBox); }
            else if (secondFoodOnhandCount > 0) { MoveJeansToTable(); animator.SetInteger("Base", (int)State.idleWithBox); }
            else if (thirdFoodOnhandCount > 0) { MoveFrocksToTable(); animator.SetInteger("Base", (int)State.idleWithBox); }
        }
        else if (other.gameObject.CompareTag("WaiterTable8Point") && isTable8)
        {
            if (firstFoodOnhandCount > 0) { MoveShirtsToTable(); animator.SetInteger("Base", (int)State.idleWithBox); }
            else if (secondFoodOnhandCount > 0) { MoveJeansToTable(); animator.SetInteger("Base", (int)State.idleWithBox); }
            else if (thirdFoodOnhandCount > 0) { MoveFrocksToTable(); animator.SetInteger("Base", (int)State.idleWithBox); }
        }


        if (other.gameObject.CompareTag("WaiterDeliverTablePoint") && isDeliveryTable1)
        {
            PackagingView packagingView = AllLockAndUnlockView.instance.packingTable1.GetComponent<PackagingView>();

            if (firstFoodOnhandCount > 0)
            {
                if (packagingView.burgrOnTableCount >= packagingView.dummyBurgers.Length - 1)
                {
                    animator.SetInteger("Base", (int)State.idleWithBox);
                    agent.enabled = false;
                    //ResetAndBackToCurt();
                    MoveToTableOnly(0);
                    return;
                }
                MoveBurgerToDeliveryTable(AllLockAndUnlockView.instance.packingTable1); animator.SetInteger("Base", (int)State.idleWithBox);
            }

            if (secondFoodOnhandCount > 0)
            {
                if (packagingView.friesOnTableCount >= packagingView.dummyFries.Length - 1)
                {
                    agent.enabled = false;
                    animator.SetInteger("Base", (int)State.idleWithBox);
                    //ResetAndBackToCurt();
                    MoveToTableOnly(1);
                    return;
                }
                MoveFriesToDeliveryTable(AllLockAndUnlockView.instance.packingTable1); animator.SetInteger("Base", (int)State.idleWithBox);
            }

            if (thirdFoodOnhandCount > 0)
            {
                if (packagingView.cokeOnTableCount >= packagingView.dummyCoke.Length - 1)
                {
                    agent.enabled = false;
                    animator.SetInteger("Base", (int)State.idleWithBox);
                    //ResetAndBackToCurt();
                    MoveToTableOnly(2);
                    return;
                }
                MoveCokeToDeliveryTable(AllLockAndUnlockView.instance.packingTable1); animator.SetInteger("Base", (int)State.idleWithBox);
            }
        }
        else if (other.gameObject.CompareTag("WaiterDeliverTablePoint2") && isDeliveryTable2)
        {
            PackagingView packagingView = AllLockAndUnlockView.instance.packingTable2.GetComponent<PackagingView>();

            if (firstFoodOnhandCount > 0)
            {
                if (packagingView.burgrOnTableCount >= packagingView.dummyBurgers.Length - 1)
                {
                    agent.enabled = false;
                    animator.SetInteger("Base", (int)State.idleWithBox);
                    //ResetAndBackToCurt();
                    MoveToTableOnly(0);
                    return;
                }
                MoveBurgerToDeliveryTable(AllLockAndUnlockView.instance.packingTable2); animator.SetInteger("Base", (int)State.idleWithBox);
            }

            if (secondFoodOnhandCount > 0)
            {
                if (packagingView.friesOnTableCount >= packagingView.dummyFries.Length - 1)
                {
                    agent.enabled = false;
                    animator.SetInteger("Base", (int)State.idleWithBox);
                    //ResetAndBackToCurt();
                    MoveToTableOnly(1);
                    return;
                }
                MoveFriesToDeliveryTable(AllLockAndUnlockView.instance.packingTable2); animator.SetInteger("Base", (int)State.idleWithBox);
            }

            if (thirdFoodOnhandCount > 0)
            {
                if (packagingView.cokeOnTableCount >= packagingView.dummyCoke.Length - 1)
                {
                    agent.enabled = false;
                    animator.SetInteger("Base", (int)State.idleWithBox);
                    //ResetAndBackToCurt();
                    MoveToTableOnly(2);
                    return;
                }
                MoveCokeToDeliveryTable(AllLockAndUnlockView.instance.packingTable2); animator.SetInteger("Base", (int)State.idleWithBox);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("WaiterBurgerPoint"))
        {
            CoockerView.instance.isCalled = false;
        }
        else if (other.gameObject.CompareTag("WaiterFriesPoint"))
        {
            CoockerView.instance.isCalled = false;
        }
        else if (other.gameObject.CompareTag("WaiterCokePoint"))
        {
            CoockerView.instance.isCalled = false;
        }
    }

    public void Callburger()
    {
        agent.enabled = false;
        animator.SetInteger("Base", (int)State.idleWithBox);
        InvokeRepeating(nameof(MoveShirtsToHand), 0.21f, 0.21f);

    }
    public void CallFries()
    {
        agent.enabled = false;
        animator.SetInteger("Base", (int)State.idleWithBox);
        InvokeRepeating(nameof(MoveJeansToHand), 0.21f, 0.21f);

    }
    public void CallCoke()
    {
        agent.enabled = false;
        animator.SetInteger("Base", (int)State.idleWithBox);
        InvokeRepeating(nameof(MoveFrocksToHand), 0.21f, 0.21f);
    }

    public void MoveShirtsToHand()
    {
        if (CoockerView.instance.firstFoodStored <= 0)
        {
            CancelInvoke(nameof(MoveShirtsToHand));
            InvokeRepeating(nameof(MoveShirtsToHand), 3f, 0.21f);
            return;
        };

        if (CoockerView.instance.firstFoodStored == CoockerView.instance.firstFoodMax)
        {
            Controller.instance.uiController.food1MaxText.SetActive(false);
        }

        GameObject firstFood = LeanPool.Spawn(CoockerView.instance.firstFoodPrefab, CoockerView.instance.firstFoodDummys[CoockerView.instance.firstFoodStored - 1].transform.position, CoockerView.instance.firstFoodDummys[CoockerView.instance.firstFoodStored - 1].transform.rotation);
        firstFood.transform.DOMove(dummyBurgers[firstFoodOnhandCount].transform.position, 0.2f).OnComplete(() =>
        {
            dummyBurgers[firstFoodOnhandCount].enabled = true;
            LeanPool.Despawn(firstFood);
            firstFoodOnhandCount++;

            if (firstFoodOnhandCount >= maxFoodOnHandCount)
            {
                CancelInvoke(nameof(MoveShirtsToHand));
                //MovewaiterTotable();
                MoveToTable(0);
                if (!CoockerView.instance.isShirtSewing && CoockerView.instance.firstFoodFabricStored > 0) 
                { 
                    CoockerView.instance.CookFirstFoodAgain(0); 
                    //CoockerView.instance.isShirtSewing = true; 
                }
            }
        });

        CoockerView.instance.firstFoodDummys[CoockerView.instance.firstFoodStored - 1].SetActive(false);
        CoockerView.instance.firstFoodStored--;

        if (CoockerView.instance.firstFoodStored == 0)
        {
            CancelInvoke(nameof(MoveShirtsToHand));
            //MovewaiterTotable();
            MoveToTable(0);
            CoockerView.instance.StopMakingShirts();
            
            if (!CoockerView.instance.isShirtSewing && CoockerView.instance.firstFoodFabricStored > 0)
            {
                CoockerView.instance.CookFirstFoodAgain(0);
                //CoockerView.instance.isShirtSewing = true; 
            }

            //CoockerView.instance.CookFirstFoodAgain(0); CoockerView.instance.isShirtSewing = true;
            //if (!CoockerView.instance.isBurgerCooking) { CoockerView.instance.CoockFirstFoodAgain(0); CoockerView.instance.isBurgerCooking = true; }
        };
    }
    public void MoveJeansToHand()
    {
        if (CoockerView.instance.secondFoodStored <= 0)
        {
            CancelInvoke(nameof(MoveJeansToHand));
            InvokeRepeating(nameof(MoveJeansToHand), 3f, 0.21f);
            return;
        };

        if (CoockerView.instance.secondFoodStored == CoockerView.instance.secondFoodMax)
        {
            Controller.instance.uiController.food2MaxText.SetActive(false);
            //CoockerView.instance.CoockSecondFoodAgain(1);
        }

        GameObject firstFood = LeanPool.Spawn(CoockerView.instance.secondFoodPrefab, CoockerView.instance.secondFoodDummys[CoockerView.instance.secondFoodStored - 1].transform.position, CoockerView.instance.secondFoodDummys[CoockerView.instance.secondFoodStored - 1].transform.rotation);
        firstFood.transform.DOMove(dummyFries[secondFoodOnhandCount].transform.position, 0.2f).OnComplete(() =>
        {
            dummyFries[secondFoodOnhandCount].enabled = true;
            LeanPool.Despawn(firstFood);
            secondFoodOnhandCount++;

            if (secondFoodOnhandCount >= maxFoodOnHandCount)
            {
                CancelInvoke(nameof(MoveJeansToHand));
                //MovewaiterTotable();
                MoveToTable(1);

                if (!CoockerView.instance.isJeansSewing && CoockerView.instance.secondFoodFabricStored > 0) 
                { 
                    CoockerView.instance.CookSecondFoodAgain(0); 
                    //CoockerView.instance.isJeansSewing = true; 
                }
            }
        });

        CoockerView.instance.secondFoodDummys[CoockerView.instance.secondFoodStored - 1].SetActive(false);
        CoockerView.instance.secondFoodStored--;

        if (CoockerView.instance.secondFoodStored == 0)
        {
            CancelInvoke(nameof(MoveJeansToHand));
            //MovewaiterTotable();
            MoveToTable(1);
            CoockerView.instance.StopMakingJeans();
            
            if (!CoockerView.instance.isJeansSewing && CoockerView.instance.secondFoodFabricStored > 0)
            {
                CoockerView.instance.CookSecondFoodAgain(0); 
            }
            //CoockerView.instance.isJeansSewing = true;
            //if (!CoockerView.instance.isFriesCooking) { CoockerView.instance.CoockSecondFoodAgain(0); CoockerView.instance.isFriesCooking = true; }
        };
    }
    public void MoveFrocksToHand()
    {
        if (CoockerView.instance.thirdFoodStored <= 0)
        {
            CancelInvoke(nameof(MoveFrocksToHand));
            InvokeRepeating(nameof(MoveFrocksToHand), 3f, 0.21f);
            return;
        };

        if (CoockerView.instance.thirdFoodStored == CoockerView.instance.thirdFoodMax)
        {

            Controller.instance.uiController.food3MaxText.SetActive(false);
            //CoockerView.instance.CoockThirdFoodAgain(1);
        }

        GameObject firstFood = LeanPool.Spawn(CoockerView.instance.thirdFoodPrefab, CoockerView.instance.thirdFoodDummys[CoockerView.instance.thirdFoodStored - 1].transform.position, CoockerView.instance.thirdFoodDummys[CoockerView.instance.thirdFoodStored - 1].transform.rotation);
        firstFood.transform.DOMove(dummyCoke[thirdFoodOnhandCount].transform.position, 0.2f).OnComplete(() =>
        {
            dummyCoke[thirdFoodOnhandCount].enabled = true;
            LeanPool.Despawn(firstFood);
            thirdFoodOnhandCount++;

            if (thirdFoodOnhandCount >= maxFoodOnHandCount)
            {
                CancelInvoke(nameof(MoveFrocksToHand));
                //MovewaiterTotable();
                MoveToTable(2);
                if (!CoockerView.instance.isFrocksSewing && CoockerView.instance.thirdFoodFabricStored > 0) 
                { 
                    CoockerView.instance.CookThirdFoodAgain(0); 
                    //CoockerView.instance.isFrocksSewing = true; 
                }
            }
        });

        CoockerView.instance.thirdFoodDummys[CoockerView.instance.thirdFoodStored - 1].SetActive(false);
        CoockerView.instance.thirdFoodStored--;

        if (CoockerView.instance.thirdFoodStored == 0)
        {
            CancelInvoke(nameof(MoveFrocksToHand));
            //MovewaiterTotable();
            MoveToTable(2);
            CoockerView.instance.StopMakingFrocks();
            if (!CoockerView.instance.isFrocksSewing && CoockerView.instance.thirdFoodFabricStored > 0)
            {
                CoockerView.instance.CookThirdFoodAgain(0);
            }
            //CoockerView.instance.CookThirdFoodAgain(0); CoockerView.instance.isFrocksSewing = true;
            // if (!CoockerView.instance.isCokeCooking) { CoockerView.instance.CoockThirdFoodAgain(0); CoockerView.instance.isCokeCooking = true; }
        };
    }


    bool bothDeliveryTablesUnlocked = false;
    public void MoveToTable(int foodNo)
    {
        agent.enabled = true;
        animator.SetInteger("Base", (int)State.walkWithBox);

        int randomSelect = 0;
        int moveToSimpleTableOrDeliveryTable = 0; 

        if (AllLockAndUnlockView.instance.IsDeliverLandUnlocked())
        {
            if (AllLockAndUnlockView.instance.IsPackTable2Unlocked()) //if both delivery tables unlocked
            {
                bothDeliveryTablesUnlocked = true;
            }
            else //if only one delivery table unlocked
            {
                bothDeliveryTablesUnlocked = false;
            }

            moveToSimpleTableOrDeliveryTable = Random.Range(0, 4); //25% chance to move to delivery tables
        }
        else //if no delivery tables unlocked
        {
            moveToSimpleTableOrDeliveryTable = 1; //100% chance to move to simple tables
        }


        if (moveToSimpleTableOrDeliveryTable == 0) //move to delivery table
        {
            int deliveryTable1or2;
            if (bothDeliveryTablesUnlocked)
            {
                deliveryTable1or2 = Random.Range(0, 2);
            }
            else
            {
                deliveryTable1or2 = 0;
            }

            if (deliveryTable1or2 == 0) //move to delivery table 1
            {
                randomSelect = 8;

                //PackagingView packagingView = AllLockAndUnlockView.instance.packingTable1.GetComponent<PackagingView>();

                agent.SetDestination(tablePoints[randomSelect].position);
                isDeliveryTable1 = true;
                return;
            }
            else //move to delivery table 2
            {
                randomSelect = 9;

                //PackagingView packagingView = AllLockAndUnlockView.instance.packingTable2.GetComponent<PackagingView>();

                agent.SetDestination(tablePoints[randomSelect].position);
                isDeliveryTable2 = true;
                return;
            }
        }
        else //move to one of the 7 tables with customers
        {
            //only allows those indexes to be passed whose corresponding tables are already unlocked

            if (foodNo == 0) //shirts
            {
                //table 1 is always unlocked

                if (AllLockAndUnlockView.instance.IsTable4Unlocked()) //if 4 is unlocked, all else will be unlocked as well
                {
                    randomSelect = PickRandomNumber(new int[] { 0, 5, 2, 3 });
                }
                else
                {
                    if (AllLockAndUnlockView.instance.IsTable3Unlocked()) //if 3 is unlocked, all except 4 will already be unlocked 
                    {
                        randomSelect = PickRandomNumber(new int[] { 0, 5, 2});
                    }
                    else
                    {
                        if (AllLockAndUnlockView.instance.IsTable6Unlocked()) //if 6 unlocked, then table1 is definitely unlocked
                        {
                            randomSelect = Random.Range(0, 2) * 5;
                        }
                        else
                        {
                            randomSelect = 0;
                        }
                    }
                }
            }
            else if (foodNo == 1) //jeans
            {
                if (AllLockAndUnlockView.instance.IsTable4Unlocked()) //if 4 is unlocked, all else will be unlocked as well
                {
                    randomSelect = PickRandomNumber(new int[] { 1, 6, 2, 3 });
                }
                else
                {
                    if (AllLockAndUnlockView.instance.IsTable3Unlocked()) //if 3 is unlocked, all except 4 will already be unlocked 
                    {
                        randomSelect = PickRandomNumber(new int[] { 1, 6, 2 });
                    }
                    else
                    {
                        if (AllLockAndUnlockView.instance.IsTable7Unlocked()) //if 7 unlocked, then table2 is definitely unlocked
                        {
                            randomSelect = Random.Range(0, 2) * 6;
                        }
                        else
                        {
                            randomSelect = 1;
                        }
                    }
                }
            }
            else if (foodNo == 2) //frocks
            {
                if (AllLockAndUnlockView.instance.IsTable4Unlocked()) //if 4 is unlocked, all else will be unlocked as well
                {
                    randomSelect = PickRandomNumber(new int[] { 4, 7, 2, 3 });
                }
                else
                {
                    if (AllLockAndUnlockView.instance.IsTable3Unlocked()) //if 3 is unlocked, all except 4 will already be unlocked 
                    {
                        randomSelect = PickRandomNumber(new int[] { 4, 7, 2 });
                    }
                    else
                    {
                        if (AllLockAndUnlockView.instance.IsTable8Unlocked()) //if 8 unlocked, then table5 is definitely unlocked
                        {
                            randomSelect = (Random.Range(0, 2) * 3) + 4;
                        }
                        else
                        {
                            randomSelect = 4;
                        }
                    }
                }
            }
        }


        static int PickRandomNumber(int[] possibleNums)
        {
            int selectedIndex = Random.Range(0, possibleNums.Length);
            return possibleNums[selectedIndex];
        }

        if (randomSelect == 0) { randomSelectedTable = AllLockAndUnlockView.instance.table1; isTable1 = true; } 
        else if (randomSelect == 1) { randomSelectedTable = AllLockAndUnlockView.instance.table2; isTable2 = true; } 
        else if (randomSelect == 2) { randomSelectedTable = AllLockAndUnlockView.instance.table3; isTable3 = true; } 
        else if (randomSelect == 3) { randomSelectedTable = AllLockAndUnlockView.instance.table4; isTable4 = true; }
        else if (randomSelect == 4) { randomSelectedTable = AllLockAndUnlockView.instance.table5; isTable5 = true; }
        else if (randomSelect == 5) { randomSelectedTable = AllLockAndUnlockView.instance.table6; isTable6 = true; }
        else if (randomSelect == 6) { randomSelectedTable = AllLockAndUnlockView.instance.table7; isTable7 = true; }
        else if (randomSelect == 7) { randomSelectedTable = AllLockAndUnlockView.instance.table8; isTable8 = true; }

        TableView tableView = randomSelectedTable.GetComponent<TableView>();

        if(tableView.Customer1Ontable != null)
        {
            if(tableView.Customer1Ontable.GetComponent<CustomerView>().orderNumber == foodNo)
            {
                agent.SetDestination(tablePoints[randomSelect].position);
                targetTable = randomSelectedTable;
                onTheWayToServe = true;
            }
            else
            {
                isTable1 = false;
                isTable2 = false;
                isTable3 = false;
                isTable4 = false;
                isTable5 = false;
                isTable6 = false;
                isTable7 = false;
                isTable8 = false;

                MoveToTable(foodNo);
            }
        }
        else if(tableView.Customer1Ontable == null)
        {
            MoveToTableOnly(foodNo);
        }
    }


    public void MoveToTableOnly(int foodNo)
    {
        ResetTablesBool();
        agent.enabled = true;
        animator.SetInteger("Base", (int)State.walkWithBox);

        int randomSelect = 0;

        //only allows those indexes to be passed whose corresponding tables are already unlocked

        if (foodNo == 0) //shirts
        {
            //table 1 is always unlocked

            if (AllLockAndUnlockView.instance.IsTable4Unlocked()) //if 4 is unlocked, all else will be unlocked as well
            {
                randomSelect = PickRandomNumber(new int[] { 0, 5, 2, 3 });
            }
            else
            {
                if (AllLockAndUnlockView.instance.IsTable3Unlocked()) //if 3 is unlocked, all except 4 will already be unlocked 
                {
                    randomSelect = PickRandomNumber(new int[] { 0, 5, 2 });
                }
                else
                {
                    if (AllLockAndUnlockView.instance.IsTable6Unlocked()) //if 6 unlocked, then table1 is definitely unlocked
                    {
                        randomSelect = Random.Range(0, 2) * 5;
                    }
                    else
                    {
                        randomSelect = 0;
                    }
                }
            }
        }
        else if (foodNo == 1) //jeans
        {
            if (AllLockAndUnlockView.instance.IsTable4Unlocked()) //if 4 is unlocked, all else will be unlocked as well
            {
                randomSelect = PickRandomNumber(new int[] { 1, 6, 2, 3 });
            }
            else
            {
                if (AllLockAndUnlockView.instance.IsTable3Unlocked()) //if 3 is unlocked, all except 4 will already be unlocked 
                {
                    randomSelect = PickRandomNumber(new int[] { 1, 6, 2 });
                }
                else
                {
                    if (AllLockAndUnlockView.instance.IsTable7Unlocked()) //if 7 unlocked, then table2 is definitely unlocked
                    {
                        randomSelect = Random.Range(0, 2) * 6;
                    }
                    else
                    {
                        randomSelect = 1;
                    }
                }
            }
        }
        else if (foodNo == 2) //frocks
        {
            if (AllLockAndUnlockView.instance.IsTable4Unlocked()) //if 4 is unlocked, all else will be unlocked as well
            {
                randomSelect = PickRandomNumber(new int[] { 4, 7, 2, 3 });
            }
            else
            {
                if (AllLockAndUnlockView.instance.IsTable3Unlocked()) //if 3 is unlocked, all except 4 will already be unlocked 
                {
                    randomSelect = PickRandomNumber(new int[] { 4, 7, 2 });
                }
                else
                {
                    if (AllLockAndUnlockView.instance.IsTable8Unlocked()) //if 8 unlocked, then table5 is definitely unlocked
                    {
                        randomSelect = (Random.Range(0, 2) * 3) + 4;
                    }
                    else
                    {
                        randomSelect = 4;
                    }
                }
            }
        }

        int PickRandomNumber(int[] possibleNums)
        {
            int selectedIndex = Random.Range(0, possibleNums.Length);
            return possibleNums[selectedIndex];
        }

        if (randomSelect == 0) { randomSelectedTable = AllLockAndUnlockView.instance.table1; isTable1 = true;}
        else if (randomSelect == 1) { randomSelectedTable = AllLockAndUnlockView.instance.table2; isTable2 = true;}
        else if (randomSelect == 2) { randomSelectedTable = AllLockAndUnlockView.instance.table3; isTable3 = true;}
        else if (randomSelect == 3) { randomSelectedTable = AllLockAndUnlockView.instance.table4; isTable4 = true;}
        else if (randomSelect == 4) { randomSelectedTable = AllLockAndUnlockView.instance.table5; isTable5 = true;}
        else if (randomSelect == 5) { randomSelectedTable = AllLockAndUnlockView.instance.table6; isTable6 = true;}
        else if (randomSelect == 6) { randomSelectedTable = AllLockAndUnlockView.instance.table7; isTable7 = true;}
        else if (randomSelect == 7) { randomSelectedTable = AllLockAndUnlockView.instance.table8; isTable8 = true;}

        TableView tableView = randomSelectedTable.GetComponent<TableView>();

        if(tableView.Customer1Ontable != null)
        {
            agent.SetDestination(onlyTablePoints[randomSelect].position);
            targetTable = randomSelectedTable;
        }
        else if(tableView.Customer1Ontable == null)
        {
            MoveToTableOnly(foodNo);
        }
    }


    //public void MoveToRandomTable(int without)
    //{
    //    ResetTablesBool();
    //    int randomSelect = Random.Range(0, 9);
    //    if (randomSelect == without) { MoveToRandomTable(without); return; }

    //    if (randomSelect == 0) { randomSelectedTable = AllLockAndUnlockView.instance.table1; isTable1 = true;}
    //    if (randomSelect == 1) { randomSelectedTable = AllLockAndUnlockView.instance.table2; isTable2 = true;}
    //    if (randomSelect == 2) 
    //    { 
    //        if(!AllLockAndUnlockView.instance.IsTable3Unlocked())
    //        {
    //            randomSelectedTable = null;
    //            MoveToRandomTable(2);
    //            return;
    //        }
    //        else
    //        {
    //            randomSelectedTable = AllLockAndUnlockView.instance.table3; isTable3 = true;
    //        }
    //    }
    //    if (randomSelect == 3) 
    //    {
    //        if(!AllLockAndUnlockView.instance.IsTable4Unlocked())
    //        {
    //            randomSelectedTable = null;
    //            MoveToRandomTable(3);
    //            return;
    //        }
    //        else
    //        {
    //            randomSelectedTable = AllLockAndUnlockView.instance.table4; isTable4 = true;
    //        }
    //    }
    //    if (randomSelect == 4) 
    //    { 
    //        if(!AllLockAndUnlockView.instance.IsDeliverLandUnlocked())
    //        {
    //            randomSelectedTable = null;
    //            MoveToRandomTable(4);
    //            return;
    //        }
    //        else
    //        {
    //            isDeliveryTable1 = true;
    //        }
    //    }
    //    if (randomSelect == 5) 
    //    { 
    //        if(!AllLockAndUnlockView.instance.IsPackTable2Unlocked())
    //        {
    //            randomSelectedTable = null;
    //            MoveToRandomTable(5);
    //            return;
    //        }
    //        else
    //        {
    //            isDeliveryTable2 = true;
    //        }
    //    }
    //    if (randomSelect == 6) 
    //    { 
    //        if(!AllLockAndUnlockView.instance.IsTable5Unlocked())
    //        {
    //            randomSelectedTable = null;
    //            MoveToRandomTable(tablePoints.Length);
    //            return;
    //        }
    //        else
    //        {
    //            randomSelectedTable = AllLockAndUnlockView.instance.table5; isTable5 = true;
    //        }
    //    }
    //    if (randomSelect == 7) 
    //    { 
    //        if(!AllLockAndUnlockView.instance.IsTable6Unlocked())
    //        {
    //            randomSelectedTable = null;
    //            MoveToRandomTable(tablePoints.Length);
    //            return;
    //        }
    //        else
    //        {
    //            randomSelectedTable = AllLockAndUnlockView.instance.table6; isTable6 = true;
    //        }
    //    }
    //    if (randomSelect == 8) 
    //    { 
    //        if(!AllLockAndUnlockView.instance.IsTable7Unlocked())
    //        {
    //            randomSelectedTable = null;
    //            MoveToRandomTable(tablePoints.Length);
    //            return;
    //        }
    //        else
    //        {
    //            randomSelectedTable = AllLockAndUnlockView.instance.table7; isTable7 = true;
    //        }
    //    }

    //    agent.enabled = true;
    //    agent.SetDestination(tablePoints[randomSelect].position);
    //    animator.SetInteger("Base", (int)State.walkWithBox);
    //    targetTable = randomSelectedTable;
    //}

    public void MoveShirtsToTable()
    {
        TableView tableView = targetTable.GetComponent<TableView>();
        if (tableView.firstFoodOnFirstTableCount == tableView.table1FirstFoodDummy.Length) 
        { 
            if (tableView.customerCount == 1)
            {
                if (!tableView.Customer1Ontable.GetComponent<CustomerView>().isCustomerEating && tableView.isTableBooked) { tableView.Customer1Ontable.GetComponent<CustomerView>().Eat(); }
            }
            if (tableView.customerCount > 1)
            {
                if (tableView.isTableBooked)
                {
                    if (!tableView.Customer1Ontable.GetComponent<CustomerView>().isCustomerEating && tableView.isTableBooked) { tableView.Customer1Ontable.GetComponent<CustomerView>().Eat(); }
            
                }
            }
            Invoke(nameof(ResetAndBackToCurt), 0.5f); 
            return; 
        }
        if (firstFoodOnhandCount == 0) return;

        tableView.Customer1Ontable.GetComponent<CustomerView>().isCustomerEating = false;
        GameObject firstFood = LeanPool.Spawn(CoockerView.instance.firstFoodPrefab, dummyBurgers[firstFoodOnhandCount - 1].transform.position, dummyBurgers[firstFoodOnhandCount - 1].transform.rotation);
        dummyBurgers[firstFoodOnhandCount - 1].enabled = false;

        firstFood.transform.DOMove(tableView.table1FirstFoodDummy[tableView.firstFoodOnFirstTableCount].transform.position, 0.04f).SetEase(Ease.InQuint).OnComplete(() =>
        {
            tableView.table1FirstFoodDummy[tableView.firstFoodOnFirstTableCount].SetActive(true);

            tableView.firstFoodOnFirstTableCount++;
            firstFoodOnhandCount--;

            LeanPool.Despawn(firstFood);
            if (tableView.firstFoodOnFirstTableCount == tableView.table1FirstFoodDummy.Length) 
            { 
                if (tableView.customerCount == 1)
                {
                    if (!tableView.Customer1Ontable.GetComponent<CustomerView>().isCustomerEating && tableView.isTableBooked) { tableView.Customer1Ontable.GetComponent<CustomerView>().Eat(); }
                }
                if (tableView.customerCount > 1)
                {
                    if (tableView.isTableBooked)
                    {
                        if (!tableView.Customer1Ontable.GetComponent<CustomerView>().isCustomerEating && tableView.isTableBooked) { tableView.Customer1Ontable.GetComponent<CustomerView>().Eat(); }
                
                    }
                }

                Invoke(nameof(ResetAndBackToCurt), 0.5f); 
                return; 
            }


            if (firstFoodOnhandCount != 0 && !tableView.table1FirstFoodDummy[tableView.table1FirstFoodDummy.Length - 1].activeInHierarchy)
            {
                Invoke(nameof(MoveShirtsToTable), 0.05f);
            }
            if (firstFoodOnhandCount == 0)
            {

                animator.SetInteger("Base", (int)State.idle);
                tableView.isBurgerOnTable = true;
                Invoke(nameof(MoveWaiterToCurt), 3f);

                if (tableView.customerCount == 1)
                {
                    if (!tableView.Customer1Ontable.GetComponent<CustomerView>().isCustomerEating && tableView.isTableBooked) { tableView.Customer1Ontable.GetComponent<CustomerView>().Eat(); }
                }
                if (tableView.customerCount > 1)
                {
                    if (tableView.isTableBooked)
                    {
                        if (!tableView.Customer1Ontable.GetComponent<CustomerView>().isCustomerEating && tableView.isTableBooked) { tableView.Customer1Ontable.GetComponent<CustomerView>().Eat(); }
                
                    }
                }

            }
        });
    }

    public void MoveJeansToTable()
    {
        TableView tableView = targetTable.GetComponent<TableView>();
        if (tableView.friesOnFirstTableCount == tableView.table1FriesDummy.Length) 
        { 
            if (tableView.customerCount == 1)
            {
                if (!tableView.Customer1Ontable.GetComponent<CustomerView>().isCustomerEating && tableView.isTableBooked) { tableView.Customer1Ontable.GetComponent<CustomerView>().Eat(); }
            }
            if (tableView.customerCount > 1)
            {
                if (tableView.isTableBooked)
                {
                    if (!tableView.Customer1Ontable.GetComponent<CustomerView>().isCustomerEating && tableView.isTableBooked) { tableView.Customer1Ontable.GetComponent<CustomerView>().Eat(); }
                
                }
            }
            Invoke(nameof(ResetAndBackToCurt), 0.5f);
            return; 
        }
        if (secondFoodOnhandCount == 0) return;

        //tableView.isFriesOnTable = true;
        tableView.Customer1Ontable.GetComponent<CustomerView>().isCustomerEating = false;
        GameObject firstFood = LeanPool.Spawn(CoockerView.instance.secondFoodPrefab, dummyFries[secondFoodOnhandCount - 1].transform.position, dummyFries[secondFoodOnhandCount - 1].transform.rotation);
        dummyFries[secondFoodOnhandCount - 1].enabled = false;

        firstFood.transform.DOMove(tableView.table1FriesDummy[tableView.friesOnFirstTableCount].transform.position, 0.04f).SetEase(Ease.InQuint).OnComplete(() =>
        {
            tableView.table1FriesDummy[tableView.friesOnFirstTableCount].SetActive(true);

            tableView.friesOnFirstTableCount++;
            secondFoodOnhandCount--;

            LeanPool.Despawn(firstFood);
            if (tableView.friesOnFirstTableCount == tableView.table1FriesDummy.Length) 
            { 
                if (tableView.customerCount == 1)
                {
                    if (!tableView.Customer1Ontable.GetComponent<CustomerView>().isCustomerEating && tableView.isTableBooked) { tableView.Customer1Ontable.GetComponent<CustomerView>().Eat(); }
                }
                if (tableView.customerCount > 1)
                {
                    if (tableView.isTableBooked)
                    {
                        if (!tableView.Customer1Ontable.GetComponent<CustomerView>().isCustomerEating && tableView.isTableBooked) { tableView.Customer1Ontable.GetComponent<CustomerView>().Eat(); }
                    
                    }
                }

                Invoke(nameof(ResetAndBackToCurt), 0.5f); 
                return; 
            }


            if (secondFoodOnhandCount != 0 && !tableView.table1FriesDummy[tableView.table1FriesDummy.Length - 1].activeInHierarchy)
            {
                Invoke(nameof(MoveJeansToTable), 0.05f);
            }


            if (secondFoodOnhandCount == 0)
            {
                animator.SetInteger("Base", (int)State.idle);
                tableView.isFriesOnTable = true;
                Invoke(nameof(MoveWaiterToCurt), 3f);

                if (tableView.customerCount == 1)
                {
                    if (!tableView.Customer1Ontable.GetComponent<CustomerView>().isCustomerEating && tableView.isTableBooked) { tableView.Customer1Ontable.GetComponent<CustomerView>().Eat(); }
                }
                if (tableView.customerCount > 1)
                {
                    if (tableView.isTableBooked)
                    {
                        if (!tableView.Customer1Ontable.GetComponent<CustomerView>().isCustomerEating && tableView.isTableBooked) { tableView.Customer1Ontable.GetComponent<CustomerView>().Eat(); }
                    
                    }
                }

            }
        });
    }
    public void MoveFrocksToTable()
    {
        TableView tableView = targetTable.GetComponent<TableView>();
        if (tableView.cokeOnFirstTableCount == tableView.table1CokeDummy.Length) 
        { 
            if (tableView.customerCount == 1)
            {
                if (!tableView.Customer1Ontable.GetComponent<CustomerView>().isCustomerEating && tableView.isTableBooked) { tableView.Customer1Ontable.GetComponent<CustomerView>().Eat(); }
            }
            if (tableView.customerCount > 1)
            {
                if (tableView.isTableBooked)
                {
                    if (!tableView.Customer1Ontable.GetComponent<CustomerView>().isCustomerEating && tableView.isTableBooked) { tableView.Customer1Ontable.GetComponent<CustomerView>().Eat(); }
                }
            }
            Invoke(nameof(ResetAndBackToCurt), 0.5f); 
            return; 
        }
        if (thirdFoodOnhandCount == 0) return;

        //tableView.isFriesOnTable = true;
        tableView.Customer1Ontable.GetComponent<CustomerView>().isCustomerEating = false;
        GameObject firstFood = LeanPool.Spawn(CoockerView.instance.thirdFoodPrefab, dummyCoke[thirdFoodOnhandCount - 1].transform.position, dummyCoke[thirdFoodOnhandCount - 1].transform.rotation);
        dummyCoke[thirdFoodOnhandCount - 1].enabled = false;

        firstFood.transform.DOMove(tableView.table1CokeDummy[tableView.cokeOnFirstTableCount].transform.position, 0.04f).SetEase(Ease.InQuint).OnComplete(() =>
        {
            tableView.table1CokeDummy[tableView.cokeOnFirstTableCount].SetActive(true);

            tableView.cokeOnFirstTableCount++;
            thirdFoodOnhandCount--;

            LeanPool.Despawn(firstFood);
            if (tableView.cokeOnFirstTableCount == tableView.table1CokeDummy.Length) 
            { 
                if (tableView.customerCount == 1)
                {
                    if (!tableView.Customer1Ontable.GetComponent<CustomerView>().isCustomerEating && tableView.isTableBooked) { tableView.Customer1Ontable.GetComponent<CustomerView>().Eat(); }
                }
                if (tableView.customerCount > 1)
                {
                    if (tableView.isTableBooked)
                    {
                        if (!tableView.Customer1Ontable.GetComponent<CustomerView>().isCustomerEating && tableView.isTableBooked) { tableView.Customer1Ontable.GetComponent<CustomerView>().Eat(); }
                    }
                }

                Invoke(nameof(ResetAndBackToCurt), 0.5f); 
                return; 
            }


            if (thirdFoodOnhandCount != 0 && !tableView.table1CokeDummy[tableView.table1CokeDummy.Length - 1].activeInHierarchy)
            {
                Invoke(nameof(MoveFrocksToTable), 0.05f);
            }
            if (thirdFoodOnhandCount == 0)
            {
                animator.SetInteger("Base", (int)State.idle);
                tableView.isCokeOnTable = true;
                Invoke(nameof(MoveWaiterToCurt), 3f);

                if (tableView.customerCount == 1)
                {
                    if (!tableView.Customer1Ontable.GetComponent<CustomerView>().isCustomerEating && tableView.isTableBooked) { tableView.Customer1Ontable.GetComponent<CustomerView>().Eat(); }
                }
                if (tableView.customerCount > 1)
                {
                    if (tableView.isTableBooked)
                    {
                        if (!tableView.Customer1Ontable.GetComponent<CustomerView>().isCustomerEating && tableView.isTableBooked) { tableView.Customer1Ontable.GetComponent<CustomerView>().Eat(); }
                    }
                }

            }
        });
    }

    public void MoveBurgerToDeliveryTable(GameObject table)
    {
        PackagingView packagingView = table.GetComponent<PackagingView>();
        if(packagingView.burgrOnTableCount >= packagingView.dummyBurgers.Length - 1)
        {
            ResetAndBackToCurt();
            return;
        }
        
        GlobalData.instance.isFoodMovingToTable = true;
        GameObject firstFood = LeanPool.Spawn(CoockerView.instance.firstFoodPrefab, dummyBurgers[firstFoodOnhandCount -1].transform.position, dummyBurgers[firstFoodOnhandCount -1].transform.rotation);
        dummyBurgers[firstFoodOnhandCount -1].enabled = false;

        firstFood.transform.DOMove(packagingView.dummyBurgers[packagingView.burgrOnTableCount].transform.position, 0.04f).SetEase(Ease.InQuint).OnComplete(()=>
        {
            GlobalData.instance.isFoodMovingToTable = false;
            packagingView.dummyBurgers[packagingView.burgrOnTableCount].SetActive(true);

            packagingView.burgrOnTableCount++;
            firstFoodOnhandCount--;

            LeanPool.Despawn(firstFood);
            if(firstFoodOnhandCount > 0)
            {
                MoveBurgerToDeliveryTable(table);
            }
            if(firstFoodOnhandCount == 0)
            {
                if(!packagingView.isPacking)
                {
                    if(packagingView.boxOnTableCount < packagingView.dummyBoxes.Length - 1)
                    {
                        packagingView.InstantiateBox();
                    }
                }
                //packagingView.isPacking = true;
                MoveWaiterToCurt();
            }
            if(packagingView.burgrOnTableCount >= packagingView.dummyBurgers.Length - 1)
            {
                ResetAndBackToCurt();
            }
        });
    }
    public void MoveFriesToDeliveryTable(GameObject table)
    {
        PackagingView packagingView = table.GetComponent<PackagingView>();
        if(packagingView.friesOnTableCount >= packagingView.dummyFries.Length - 1)
        {
            ResetAndBackToCurt();
            return;
        }
        
        GlobalData.instance.isFoodMovingToTable = true;
        GameObject firstFood = LeanPool.Spawn(CoockerView.instance.secondFoodPrefab, dummyFries[secondFoodOnhandCount -1].transform.position, dummyFries[secondFoodOnhandCount -1].transform.rotation);
        dummyFries[secondFoodOnhandCount -1].enabled = false;

        firstFood.transform.DOMove(packagingView.dummyFries[packagingView.friesOnTableCount].transform.position, 0.04f).SetEase(Ease.InQuint).OnComplete(()=>
        {
            GlobalData.instance.isFoodMovingToTable = false;
            packagingView.dummyFries[packagingView.friesOnTableCount].SetActive(true);

            packagingView.friesOnTableCount++;
            secondFoodOnhandCount--;

            LeanPool.Despawn(firstFood);
            if(secondFoodOnhandCount > 0)
            {
                MoveFriesToDeliveryTable(table);
            }
            if(secondFoodOnhandCount == 0)
            {
                if(!packagingView.isPacking)
                {
                    if(packagingView.boxOnTableCount < packagingView.dummyBoxes.Length - 1)
                    {
                        packagingView.InstantiateBox();
                    }
                }
                //packagingView.isPacking = true;
                MoveWaiterToCurt();
            }

            if(packagingView.friesOnTableCount >= packagingView.dummyFries.Length - 1)
            {
                ResetAndBackToCurt();
            }
        });
    }
    public void MoveCokeToDeliveryTable(GameObject table)
    {
        PackagingView packagingView = table.GetComponent<PackagingView>();
        if(packagingView.cokeOnTableCount >= packagingView.dummyCoke.Length - 1)
        {
            ResetAndBackToCurt();
            return;
        }
        
        GlobalData.instance.isFoodMovingToTable = true;
        GameObject firstFood = LeanPool.Spawn(CoockerView.instance.thirdFoodPrefab, dummyCoke[thirdFoodOnhandCount -1].transform.position, dummyCoke[thirdFoodOnhandCount -1].transform.rotation);
        dummyCoke[thirdFoodOnhandCount -1].enabled = false;

        firstFood.transform.DOMove(packagingView.dummyCoke[packagingView.cokeOnTableCount].transform.position, 0.04f).SetEase(Ease.InQuint).OnComplete(()=>
        {
            GlobalData.instance.isFoodMovingToTable = false;
            packagingView.dummyCoke[packagingView.cokeOnTableCount].SetActive(true);

            packagingView.cokeOnTableCount++;
            thirdFoodOnhandCount--;

            LeanPool.Despawn(firstFood);
            if(thirdFoodOnhandCount > 0)
            {
                MoveCokeToDeliveryTable(table);
            }
            if(thirdFoodOnhandCount == 0)
            {
                if(!packagingView.isPacking)
                {
                    if(packagingView.boxOnTableCount < packagingView.dummyBoxes.Length - 1)
                    {
                        packagingView.InstantiateBox();
                    }
                }
                //packagingView.isPacking = true;
                MoveWaiterToCurt();
            }
            if(packagingView.cokeOnTableCount >= packagingView.dummyCoke.Length - 1)
            {
                ResetAndBackToCurt();
            }
        });
    }

    public void MoveWaiterToCurt()
    {
        ResetFoodsBool();
        CheckFoods();
        ResetTablesBool();
        int pointNo;
        agent.enabled = true;

        if(AllLockAndUnlockView.instance.IsCokeUnlocked() && AllLockAndUnlockView.instance.IsFriesUnlocked())
        {
            pointNo = Random.Range(0, 3);
            if(pointNo == 0)
            {
                if (isShirtsOnHouse)
                {
                    agent.SetDestination(burgerPoint.position);
                    animator.SetInteger("Base", (int)State.walk);
                }
                else
                {
                    MoveWaiterToCurt();
                }
            }
            if(pointNo == 1)
            {
                if (isJeansOnHouse)
                {
                    agent.SetDestination(friesPoint.position);
                    animator.SetInteger("Base", (int)State.walk);
                }
                else
                {
                    MoveWaiterToCurt();
                }
            }
            if(pointNo == 2)
            {
                if (isFrocksOnHouse)
                {
                    agent.SetDestination(cokePoint.position);
                    animator.SetInteger("Base", (int)State.walk);
                }
                else
                {
                    MoveWaiterToCurt();
                }
            }
        }
        else if(!AllLockAndUnlockView.instance.IsCokeUnlocked() && AllLockAndUnlockView.instance.IsFriesUnlocked())
        {
            pointNo = Random.Range(0, 2);
            if(pointNo == 0)
            {
                if (isShirtsOnHouse)
                {
                    agent.SetDestination(burgerPoint.position);
                    animator.SetInteger("Base", (int)State.walk);
                }
                else
                {
                    MoveWaiterToCurt();
                }
            }
            if(pointNo == 1)
            {
                if (isJeansOnHouse)
                {
                    agent.SetDestination(friesPoint.position);
                    animator.SetInteger("Base", (int)State.walk);
                }
                else
                {
                    MoveWaiterToCurt();
                }
            }
        }
        else if(!AllLockAndUnlockView.instance.IsCokeUnlocked() && !AllLockAndUnlockView.instance.IsFriesUnlocked())
        {
            pointNo = 0;
            if(pointNo == 0)
            {
                agent.SetDestination(burgerPoint.position);
                animator.SetInteger("Base", (int)State.walk);
            }
        }

        for (int i = 0; i < dummyBurgers.Length; i++)
        {
            dummyBurgers[i].enabled = false;
        }
        for (int i = 0; i < dummyFries.Length; i++)
        {
            dummyFries[i].enabled = false;
        }
        for (int i = 0; i < dummyCoke.Length; i++)
        {
            dummyCoke[i].enabled = false;
        }

        animator.SetInteger("Base", (int)State.walk);

        firstFoodOnhandCount = 0;
        secondFoodOnhandCount = 0;
        thirdFoodOnhandCount = 0;
    }
}
