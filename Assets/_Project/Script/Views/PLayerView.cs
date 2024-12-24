using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Lean.Pool;
using System.Diagnostics.Contracts;

public class PLayerView : MonoBehaviour
{
    public static PLayerView instance;
    TableView tableView;
    public GameObject onHandEffect;
    public Transform moneyMovePos;
    public int maxFoodOnHand;
    public MeshRenderer[] firstFoodDummys;
    public int firstFoodOnhand;
    [Space]
    public MeshRenderer[] secondFoodDummys;
    public int secondFoodOnhand;
    [Space]
    public MeshRenderer[] thirdFoodDummys;
    public int thirdFoodOnhand;
    [Space]
    public MeshRenderer[] fabricDummys;
    public int fabricOnHand;
    [Space]
    public GameObject[] boxDummys;
    public int boxOnhandCount;
    public int maxBoxOnhandCount;

    public bool onTable;
    public bool onDeliveryTable;
    public bool onBoxPoint;
    public bool onScootrePoint;
    public bool isBoxMoving;
    public bool onCurt;
    public bool onCurt1, onCurt2, onCurt3, onCurt4;
    public bool isCookCalled;
    public bool isCalledBox;
    public GameObject DeliveryTableNo;

    private void Awake()
    {
        instance = this;
    }

    private void Start() 
    {
        if(PlayerPrefs.HasKey("PlayerCapacity"))
        {
            maxFoodOnHand = PlayerPrefs.GetInt("PlayerCapacity");
        }
    }



    bool fabricDropTriggerFlag1 = false;
    bool fabricDropTriggerFlag2 = false;
    bool fabricDropTriggerFlag3 = false;
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("1stFoodPickupPlace"))
        {
            onCurt1 = true;
            onCurt = true;
            isCookCalled = false;
            if (GlobalData.instance.isHandFull) return;
            if (secondFoodOnhand > 0) return;
            if (thirdFoodOnhand > 0) return;
            if (fabricOnHand > 0) return;
            if (CoockerView.instance.firstFoodStored <= 0) return; 

            MoveShirtsToHand();
        }
        else if(other.gameObject.CompareTag("2ndFoodPickupPlace"))
        {
            onCurt2 = true;
            onCurt = true;
            isCookCalled = false;
            if (GlobalData.instance.isHandFull) return;
            if (firstFoodOnhand > 0) return;
            if (thirdFoodOnhand > 0) return;
            if (fabricOnHand > 0) return;
            if (CoockerView.instance.secondFoodStored <= 0) return;

            MoveJeansToHand();
        }
        else if(other.gameObject.CompareTag("3rdFoodPickupPlace"))
        {
            onCurt3 = true;
            onCurt = true;
            isCookCalled = false;
            if (GlobalData.instance.isHandFull) return;
            if(firstFoodOnhand > 0) return;
            if(secondFoodOnhand > 0) return;
            if (fabricOnHand > 0) return;
            if (CoockerView.instance.thirdFoodStored <= 0) return;

            MoveFrocksToHand();
        }
        else if (other.gameObject.CompareTag("FabricPickupPlace"))
        {
            onCurt4 = true;
            onCurt = true;
            isCookCalled = false;
            if (GlobalData.instance.isHandFull) return;
            if (firstFoodOnhand > 0) return;
            if (secondFoodOnhand > 0) return;
            if (thirdFoodOnhand > 0) return;
            MoveFabricToHand();
        }

        if (other.CompareTag("1stFoodFabricDropPlace"))
        {
            if (fabricDropTriggerFlag1) return;

            fabricDropTriggerFlag1 = true;   
            if (fabricOnHand <= 0) return;
            if (CoockerView.instance.firstFoodFabricStored >= CoockerView.instance.firstFoodFabricMax) return;
            
            StartCoroutine(MoveFabricToFirstFoodStall());
        }
        else if (other.CompareTag("2ndFoodFabricDropPlace"))
        {
            if (fabricDropTriggerFlag2) return;

            fabricDropTriggerFlag2 = true;
            if (fabricOnHand <= 0) return;
            if (CoockerView.instance.secondFoodFabricStored >= CoockerView.instance.secondFoodFabricMax) return;

            StartCoroutine(MoveFabricToSecondFoodStall());
        }
        else if (other.CompareTag("3rdFoodFabricDropPlace"))
        {
            if (fabricDropTriggerFlag3) return;

            fabricDropTriggerFlag3 = true;
            if (fabricOnHand <= 0) return;
            if (CoockerView.instance.thirdFoodFabricStored >= CoockerView.instance.thirdFoodFabricMax) return;

            StartCoroutine(MoveFabricToThirdFoodStall());
        }

        if (other.gameObject.CompareTag("Money"))
        {
            other.gameObject.GetComponentInParent<TableView>().CollectMoney();
            for (int i = 0; i < other.gameObject.GetComponentInParent<TableView>().destroyableMoney.Count; i++)
            {
                if(other.gameObject.GetComponentInParent<TableView>().destroyableMoney[i].activeInHierarchy)
                {
                    other.gameObject.GetComponentInParent<TableView>().destroyableMoney[i].SetActive(false);
                }
                if(i == other.gameObject.GetComponentInParent<TableView>().destroyableMoney.Count - 1)
                {
                    other.gameObject.GetComponentInParent<TableView>().destroyableMoney.Clear();
                }
            }
        }
        else if(other.gameObject.CompareTag("SpecialTableMoney"))
        {
            SpecialCustomerView.instance.CollectMoney();
        }
        
        if(other.gameObject.CompareTag("BoxCollectPoint"))
        {
            onBoxPoint = true;
            DeliveryTableNo = other.transform.parent.gameObject;
            if(GlobalData.instance.isFoodOnHand) return;
            if (!isCalledBox) 
            {
                MoveBoxToHand();
                isCalledBox = true; 
            }
        }
        else if(other.gameObject.CompareTag("Scotty"))
        {
            onScootrePoint = true;
            MoveBoxToScooter();
        }
        
        if(other.gameObject.CompareTag("ScottyMoney"))
        {
            ScooterView.instance.CollectMoney();
        }
        
        if(other.gameObject.CompareTag("WaiterPannelTrigger"))
        {
            Controller.instance.uiController.waiterUpgradePannel.SetActive(true);
            Controller.instance.gameController.PlayHaptic();
        }
        else if(other.gameObject.CompareTag("PlayerPannelTrigger"))
        {
            Controller.instance.uiController.playerUpgradePannel.SetActive(true);
            Controller.instance.gameController.PlayHaptic();
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Table1"))
        {
            onTable = true;
            tableView = other.gameObject.GetComponent<TableView>();
            if(firstFoodOnhand != 0 && !tableView.table1FirstFoodDummy[tableView.table1FirstFoodDummy.Length - 1].activeInHierarchy)
            {
                Invoke(nameof(MoveShirtsToTable1), 0.1f);
                GlobalData.instance.isHandFull = false;
                SetAllPlayerMaxTextsToFalse();
            }
        }
        if(other.gameObject.CompareTag("Table2"))
        {
            onTable = true;
            tableView = other.gameObject.GetComponent<TableView>();
            if (secondFoodOnhand != 0 && !tableView.table1FriesDummy[tableView.table1FriesDummy.Length - 1].activeInHierarchy)
            {
                Invoke(nameof(MoveJeansToTable1), 0.1f);
                GlobalData.instance.isHandFull = false;
                SetAllPlayerMaxTextsToFalse();
            }
        }
        if(other.gameObject.CompareTag("Table3"))
        {
            onTable = true;
            tableView = other.gameObject.GetComponent<TableView>();
            if(firstFoodOnhand != 0 && !tableView.table1FirstFoodDummy[tableView.table1FirstFoodDummy.Length - 1].activeInHierarchy)
            {
                Invoke(nameof(MoveShirtsToTable1), 0.1f);
                GlobalData.instance.isHandFull = false;
                SetAllPlayerMaxTextsToFalse();
            }
            if (secondFoodOnhand != 0 && !tableView.table1FriesDummy[tableView.table1FriesDummy.Length - 1].activeInHierarchy)
            {
                Invoke(nameof(MoveJeansToTable1), 0.1f);
                GlobalData.instance.isHandFull = false;
                SetAllPlayerMaxTextsToFalse();
            }
            if (thirdFoodOnhand != 0 && !tableView.table1CokeDummy[tableView.table1CokeDummy.Length - 1].activeInHierarchy)
            {
                Invoke(nameof(MoveFrocksToTable1), 0.1f);
                SetAllPlayerMaxTextsToFalse();
            }
        }
        if(other.gameObject.CompareTag("Table4"))
        {
            onTable = true;
            tableView = other.gameObject.GetComponent<TableView>();
            if (firstFoodOnhand != 0 && !tableView.table1FirstFoodDummy[tableView.table1FirstFoodDummy.Length - 1].activeInHierarchy)
            {
                Invoke(nameof(MoveShirtsToTable1), 0.1f);
                GlobalData.instance.isHandFull = false;
                SetAllPlayerMaxTextsToFalse();
            }
            if (secondFoodOnhand != 0 && !tableView.table1FriesDummy[tableView.table1FriesDummy.Length - 1].activeInHierarchy)
            {
                Invoke(nameof(MoveJeansToTable1), 0.1f);
                GlobalData.instance.isHandFull = false;
                SetAllPlayerMaxTextsToFalse();
            }
            if (thirdFoodOnhand != 0 && !tableView.table1CokeDummy[tableView.table1CokeDummy.Length - 1].activeInHierarchy)
            {
                Invoke(nameof(MoveFrocksToTable1), 0.1f);
                GlobalData.instance.isHandFull = false;
                SetAllPlayerMaxTextsToFalse();
            }
        }
        if (other.gameObject.CompareTag("Table5"))
        {
            onTable = true;
            tableView = other.gameObject.GetComponent<TableView>();
            if (thirdFoodOnhand != 0 && !tableView.table1CokeDummy[tableView.table1CokeDummy.Length - 1].activeInHierarchy)
            {
                Invoke(nameof(MoveFrocksToTable1), 0.1f);
                GlobalData.instance.isHandFull = false;
                SetAllPlayerMaxTextsToFalse();
            }
        }
        if(other.gameObject.CompareTag("Table6"))
        {
            onTable = true;
            tableView = other.gameObject.GetComponent<TableView>();
            if(firstFoodOnhand != 0 && !tableView.table1FirstFoodDummy[tableView.table1FirstFoodDummy.Length - 1].activeInHierarchy)
            {
                Invoke(nameof(MoveShirtsToTable1), 0.1f);
                GlobalData.instance.isHandFull = false;
                SetAllPlayerMaxTextsToFalse();
            }
        }
        if(other.gameObject.CompareTag("Table7"))
        {
            onTable = true;
            tableView = other.gameObject.GetComponent<TableView>();
            if(secondFoodOnhand != 0 && !tableView.table1FriesDummy[tableView.table1FriesDummy.Length - 1].activeInHierarchy)
            {
                Invoke(nameof(MoveJeansToTable1),0.1f);
                GlobalData.instance.isHandFull = false;
                SetAllPlayerMaxTextsToFalse();
            }
        }
        if (other.gameObject.CompareTag("Table8"))
        {
            onTable = true;
            tableView = other.gameObject.GetComponent<TableView>();
            if (thirdFoodOnhand != 0 && !tableView.table1CokeDummy[tableView.table1CokeDummy.Length - 1].activeInHierarchy)
            {
                Invoke(nameof(MoveFrocksToTable1), 0.1f);
                GlobalData.instance.isHandFull = false;
                SetAllPlayerMaxTextsToFalse();
            }
        }

    }

    void SetAllPlayerMaxTextsToFalse()
    {
        Controller.instance.uiController.playerMaxText.SetActive(false);
        Controller.instance.uiController.playerMaxText2.SetActive(false);
        Controller.instance.uiController.playerMaxText3.SetActive(false);
        Controller.instance.uiController.playerMaxText4.SetActive(false);
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.CompareTag("1stFoodPickupPlace"))
        {
            onCurt1 = false;
            onCurt = false;
            CoockerView.instance.isCalled = false;
            CoockerView.instance.RemoveMoveFoodCall();
            if (!isCookCalled && !CoockerView.instance.isShirtSewing && !GlobalData.instance.isHandFull) 
            { 
                if(CoockerView.instance.firstFoodStored < CoockerView.instance.firstFoodMax)
                {
                    isCookCalled = true;
                    CoockerView.instance.SewShirtsAndStack(); 
                }
            }
        }
        else if(other.gameObject.CompareTag("2ndFoodPickupPlace"))
        {
            onCurt2 = false;
            onCurt = false;
            CoockerView.instance.isCalled = false;
            CoockerView.instance.RemoveMoveFoodCall();
            if (!isCookCalled && !CoockerView.instance.isJeansSewing && !GlobalData.instance.isHandFull)
            {
                if (CoockerView.instance.secondFoodStored < CoockerView.instance.secondFoodMax)
                {
                    CoockerView.instance.SewJeansAndStack(); isCookCalled = true;
                    CoockerView.instance.isJeansSewing = true;
                }
            }
        }
        else if(other.gameObject.CompareTag("3rdFoodPickupPlace"))
        {
            onCurt3 = false;
            onCurt = false;
            CoockerView.instance.isCalled = false;
            CoockerView.instance.RemoveMoveFoodCall();
            if (!isCookCalled && !CoockerView.instance.isFrocksSewing && !GlobalData.instance.isHandFull) 
            {
                if (CoockerView.instance.thirdFoodStored < CoockerView.instance.thirdFoodMax)
                {
                    CoockerView.instance.SewFrocksAndStack(); isCookCalled = true;
                    CoockerView.instance.isFrocksSewing = true;
                }
            }
        }
        else if (other.gameObject.CompareTag("FabricPickupPlace"))
        {
            onCurt4 = false;
            onCurt = false;
            CoockerView.instance.isCalled = false;
            CoockerView.instance.RemoveMoveFoodCall();
        }

        if (other.CompareTag("1stFoodFabricDropPlace"))
        {
            fabricDropTriggerFlag1 = false;
        }
        else if (other.CompareTag("2ndFoodFabricDropPlace"))
        {
            fabricDropTriggerFlag2 = false;
        }
        else if (other.CompareTag("3rdFoodFabricDropPlace"))
        {
            fabricDropTriggerFlag3 = false;
        }
        
        if (other.gameObject.CompareTag("BoxCollectPoint"))
        {
            onBoxPoint = false;

            DeliveryTableNo = other.transform.parent.gameObject;
            PackagingView packagingView = DeliveryTableNo.GetComponent<PackagingView>();

            if(packagingView.burgrOnTableCount > 0 || packagingView.friesOnTableCount > 0 || packagingView.cokeOnTableCount > 0)
            {
                if(!packagingView.isPacking)
                {
                    packagingView.InstantiateBox();
                }
            }
            isCalledBox = false;
        }
        
        if(other.gameObject.CompareTag("WaiterPannelTrigger"))
        {
            Controller.instance.uiController.waiterUpgradePannel.SetActive(false);
        }
        else if(other.gameObject.CompareTag("PlayerPannelTrigger"))
        {
            Controller.instance.uiController.playerUpgradePannel.SetActive(false);
        }
    }
    private void OnCollisionExit(Collision other) 
    {
        if (other.gameObject.CompareTag("Table1"))
        {
            onTable = false;
        }
        if (other.gameObject.CompareTag("Table2"))
        { 
            onTable = false;
        }
        if (other.gameObject.CompareTag("Table3"))
        { 
            onTable = false;
        }
        if (other.gameObject.CompareTag("Table4"))
        { 
            onTable = false;
        }
        if (other.gameObject.CompareTag("Table5"))
        { 
            onTable = false;
        }
        if (other.gameObject.CompareTag("Table6"))
        { 
            onTable = false;
        }
        if (other.gameObject.CompareTag("Table7"))
        { 
            onTable = false;
        }
        if (other.gameObject.CompareTag("Table8"))
        {
            onTable = false;
        }
    }

    public void MoveShirtsToHand()
    {
        if(!onCurt) { return;}
        if (firstFoodOnhand == maxFoodOnHand) 
        {
            return;
        }

        if (CoockerView.instance.firstFoodStored <= 0) //this is for directfoodmovement
        { 
            //CoockerView.instance.StopMakingShirts();
            //CoockerView.instance.MoveItemCall(); 
            return;
        }

        Controller.instance.uiController.food1MaxText.SetActive(false);
        GameObject firstFood = LeanPool.Spawn(CoockerView.instance.firstFoodPrefab, CoockerView.instance.firstFoodDummys[CoockerView.instance.firstFoodStored - 1].transform.position, CoockerView.instance.firstFoodDummys[CoockerView.instance.firstFoodStored - 1].transform.rotation);
        GlobalData.instance.isFoodOnHand = true;
        firstFood.AddComponent<MovingFood>();

        CoockerView.instance.firstFoodDummys[CoockerView.instance.firstFoodStored - 1].SetActive(false);
        CoockerView.instance.firstFoodStored--;
    }
    public void MoveJeansToHand()
    {
        if(!onCurt) { return;}
        if (secondFoodOnhand == maxFoodOnHand) 
        {
            return;
        };
        if (CoockerView.instance.secondFoodStored == 0) 
        { 
            //CoockerView.instance.StopMakingJeans();
            //CoockerView.instance.MoveItemCall(); 
            return;
        }
        Controller.instance.uiController.food2MaxText.SetActive(false);
        GameObject firstFood = LeanPool.Spawn(CoockerView.instance.secondFoodPrefab, CoockerView.instance.secondFoodDummys[CoockerView.instance.secondFoodStored - 1].transform.position, CoockerView.instance.secondFoodDummys[CoockerView.instance.secondFoodStored - 1].transform.rotation);
        GlobalData.instance.isFoodOnHand = true;
        firstFood.AddComponent<MovingFood2>();

        CoockerView.instance.secondFoodDummys[CoockerView.instance.secondFoodStored - 1].SetActive(false);
        CoockerView.instance.secondFoodStored--;
    }
    public void MoveFrocksToHand()
    {
        if(!onCurt) { return;}
        if (thirdFoodOnhand == maxFoodOnHand) 
        {
            return;
        };
        if (CoockerView.instance.thirdFoodStored == 0) 
        { 
            //CoockerView.instance.StopMakingFrocks();
            //CoockerView.instance.MoveItemCall(); 
            return;
        }
        Controller.instance.uiController.food3MaxText.SetActive(false);
        GameObject firstFood = LeanPool.Spawn(CoockerView.instance.thirdFoodPrefab, CoockerView.instance.thirdFoodDummys[CoockerView.instance.thirdFoodStored - 1].transform.position, CoockerView.instance.thirdFoodDummys[CoockerView.instance.thirdFoodStored - 1].transform.rotation);
        GlobalData.instance.isFoodOnHand = true;
        firstFood.AddComponent<MovingFood3>();

        CoockerView.instance.thirdFoodDummys[CoockerView.instance.thirdFoodStored - 1].SetActive(false);
        CoockerView.instance.thirdFoodStored--;
    }
    void MoveFabricToHand()
    {
        if (!onCurt) return; 
        if (fabricOnHand == maxFoodOnHand) return;

        //InvokeRepeating(nameof(ShowFabricOnHand), 0f, 0.2f);
        StartCoroutine(ShowFabricOnHand());
    }

    IEnumerator ShowFabricOnHand()
    {
        if (fabricOnHand >= maxFoodOnHand) yield break;

        GlobalData.instance.isFoodOnHand = true;

        while (fabricOnHand < maxFoodOnHand)
        {
            fabricDummys[fabricOnHand].enabled = true;
            fabricOnHand++;

            yield return new WaitForSeconds(0.1f);
        }
    }

    //void ShowFabricOnHand()
    //{
    //    if (fabricOnHand >= maxFoodOnHand)
    //    {
    //        CancelInvoke(nameof(ShowFabricOnHand));
    //        return;
    //    }

    //    fabricDummys[fabricOnHand].enabled = true; //index out of bounds exceptions here
    //    fabricOnHand++;
    //    GlobalData.instance.isFoodOnHand = true;
    //}


    IEnumerator MoveFabricToFirstFoodStall()
    {
        CoockerView cookcerViewInstance = CoockerView.instance;

        int numOfFabricsOnHand = fabricOnHand;
        for (int i = numOfFabricsOnHand - 1; i >= 0; i--)
        {
            if (cookcerViewInstance.firstFoodFabricStored < cookcerViewInstance.firstFoodFabricMax)
            {
                yield return new WaitForSeconds(0.1f);

                cookcerViewInstance.firstFoodFabricDummys[cookcerViewInstance.firstFoodFabricStored].SetActive(true);
                CoockerView.instance.firstFoodFabricStored++;

                fabricOnHand--;
                fabricDummys[i].enabled = false;
            }
        }

        if (fabricOnHand == 0)
        {
            GlobalData.instance.isFoodOnHand = false;
        }

        if (!CoockerView.instance.isShirtSewing)
        {
            CoockerView.instance.SewShirtsAndStack();
        }
    }

    IEnumerator MoveFabricToSecondFoodStall()
    {
        CoockerView cookcerViewInstance = CoockerView.instance;

        int numOfFabricsOnHand = fabricOnHand;
        for (int i = numOfFabricsOnHand - 1; i >= 0; i--)
        {
            if (cookcerViewInstance.secondFoodFabricStored < cookcerViewInstance.secondFoodFabricMax)
            {
                yield return new WaitForSeconds(0.1f);

                cookcerViewInstance.secondFoodFabricDummys[cookcerViewInstance.secondFoodFabricStored].SetActive(true);
                cookcerViewInstance.secondFoodFabricStored++;

                fabricOnHand--;
                fabricDummys[i].enabled = false;
            }
        }

        if (fabricOnHand == 0)
        {
            GlobalData.instance.isFoodOnHand = false;
        }

        if (!CoockerView.instance.isJeansSewing)
        {
            CoockerView.instance.SewJeansAndStack();
        }
    }

    IEnumerator MoveFabricToThirdFoodStall()
    {
        CoockerView cookcerViewInstance = CoockerView.instance;

        int numOfFabricsOnHand = fabricOnHand;
        for (int i = numOfFabricsOnHand - 1; i >= 0; i--)
        {
            if (cookcerViewInstance.thirdFoodFabricStored < cookcerViewInstance.thirdFoodFabricMax)
            {
                yield return new WaitForSeconds(0.1f);

                cookcerViewInstance.thirdFoodFabricDummys[cookcerViewInstance.thirdFoodFabricStored].SetActive(true);
                cookcerViewInstance.thirdFoodFabricStored++;

                fabricOnHand--;
                fabricDummys[i].enabled = false;
            }
        }

        if (fabricOnHand == 0)
        {
            GlobalData.instance.isFoodOnHand = false;
        }

        if (!CoockerView.instance.isFrocksSewing)
        {
            CoockerView.instance.SewFrocksAndStack();
        }
    }


    public void MoveShirtsToTable1()
    {
        if(!onTable) return;
        GlobalData.instance.isFoodMovingToTable = true;
        tableView.isBurgerOnTable = true;
        tableView.Customer1Ontable.GetComponent<CustomerView>().isCustomerEating = false;
        GameObject firstFood = LeanPool.Spawn(CoockerView.instance.firstFoodPrefab, firstFoodDummys[firstFoodOnhand -1].transform.position, firstFoodDummys[firstFoodOnhand -1].transform.rotation);
        firstFoodDummys[firstFoodOnhand -1].enabled = false;
        DailyTaskView.instance.BurgerServedCount(1);

        firstFood.transform.DOMove(tableView.table1FirstFoodDummy[tableView.firstFoodOnFirstTableCount].transform.position, 0.04f).SetEase(Ease.InQuint).OnComplete(()=>
        {
            GlobalData.instance.isFoodMovingToTable = false;
            tableView.table1FirstFoodDummy[tableView.firstFoodOnFirstTableCount].SetActive(true);

            GameObject effect = LeanPool.Spawn(onHandEffect, tableView.table1FirstFoodDummy[tableView.firstFoodOnFirstTableCount].transform.position, onHandEffect.transform.rotation);
            StartCoroutine(DestroyObject(effect, 2f));

            tableView.firstFoodOnFirstTableCount++;
            firstFoodOnhand--;
            Controller.instance.gameController.PlayHaptic();

            LeanPool.Despawn(firstFood);
            if(firstFoodOnhand != 0 && !tableView.table1FirstFoodDummy[tableView.table1FirstFoodDummy.Length - 1].activeInHierarchy)
            {
                Invoke(nameof(MoveShirtsToTable1), 0.05f);
            }
            if(firstFoodOnhand == 0)
            {
                GlobalData.instance.isFoodOnHand = false;
                // tableView.isBurgerOnTable = true;

                if (tableView.customerCount == 1) 
                {
                    if(!tableView.Customer1Ontable.GetComponent<CustomerView>().isCustomerEating && tableView.isTableBooked){tableView.Customer1Ontable.GetComponent<CustomerView>().Eat();}
                }
                if(tableView.customerCount > 1)
                {
                    if(tableView.isTableBooked)
                    {
                        if(!tableView.Customer1Ontable.GetComponent<CustomerView>().isCustomerEating && tableView.isTableBooked){tableView.Customer1Ontable.GetComponent<CustomerView>().Eat();}
                    }
                }
                
            }
        });
    }
    public void MoveJeansToTable1()
    {
        if (!onTable) return;
        GlobalData.instance.isFoodMovingToTable = true;
        tableView.isFriesOnTable = true;
        tableView.Customer1Ontable.GetComponent<CustomerView>().isCustomerEating = false;
        GameObject fries = LeanPool.Spawn(CoockerView.instance.secondFoodPrefab, secondFoodDummys[secondFoodOnhand - 1].transform.position, secondFoodDummys[secondFoodOnhand - 1].transform.rotation);
        secondFoodDummys[secondFoodOnhand - 1].enabled = false;
        secondFoodOnhand--;

        DailyTaskView.instance.FrieServedCount(1);

        int index = tableView.friesOnFirstTableCount;
        tableView.friesOnFirstTableCount++;

        fries.transform.DOMove(tableView.table1FriesDummy[index].transform.position, 0.04f).SetEase(Ease.InQuint).OnComplete(() =>
        {
            GlobalData.instance.isFoodMovingToTable = false;
            tableView.table1FriesDummy[index].SetActive(true);
            Controller.instance.gameController.PlayHaptic();

            GameObject effect = LeanPool.Spawn(onHandEffect, tableView.table1FriesDummy[index].transform.position, onHandEffect.transform.rotation);
            StartCoroutine(DestroyObject(effect, 2f));

            LeanPool.Despawn(fries);
            if (secondFoodOnhand != 0 && !tableView.table1FriesDummy[tableView.table1FriesDummy.Length - 1].activeInHierarchy)
            {
                Invoke(nameof(MoveJeansToTable1), 0.05f);
            }
            if (secondFoodOnhand == 0)
            {
                GlobalData.instance.isFoodOnHand = false;
                // tableView.isFriesOnTable = true;

                if (tableView.customerCount == 1)
                {
                    if (!tableView.Customer1Ontable.GetComponent<CustomerView>().isCustomerEating && tableView.isTableBooked) { tableView.Customer1Ontable.GetComponent<CustomerView>().Eat(); }
                }
                if (tableView.customerCount > 1)
                {
                    if (tableView.isTableBooked)
                    {
                        if (!tableView.Customer1Ontable.GetComponent<CustomerView>().isCustomerEating && tableView.isTableBooked) { tableView.Customer1Ontable.GetComponent<CustomerView>().Eat(); }
                        // tableView.Customer2Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("Eat", true);
                        // tableView.Customer3Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("Eat", true);
                        // tableView.Customer4Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("Eat", true);
                    }
                }
            }
        });
    }
    public void MoveFrocksToTable1()
    {
        if (!onTable) return;
        GlobalData.instance.isFoodMovingToTable = true;
        tableView.isCokeOnTable = true;
        tableView.Customer1Ontable.GetComponent<CustomerView>().isCustomerEating = false;
        GameObject coke = LeanPool.Spawn(CoockerView.instance.thirdFoodPrefab, thirdFoodDummys[thirdFoodOnhand - 1].transform.position, thirdFoodDummys[thirdFoodOnhand - 1].transform.rotation);
        thirdFoodDummys[thirdFoodOnhand - 1].enabled = false;
        thirdFoodOnhand--;
        DailyTaskView.instance.ColaServedCount(1);

        int index = tableView.cokeOnFirstTableCount;
        tableView.cokeOnFirstTableCount++;

        coke.transform.DOMove(tableView.table1CokeDummy[index].transform.position, 0.04f).SetEase(Ease.InQuint).OnComplete(() =>
        {
            GlobalData.instance.isFoodMovingToTable = false;
            tableView.table1CokeDummy[index].SetActive(true);
            Controller.instance.gameController.PlayHaptic();

            GameObject effect = LeanPool.Spawn(onHandEffect, tableView.table1CokeDummy[index].transform.position, onHandEffect.transform.rotation);
            StartCoroutine(DestroyObject(effect, 2f));

            LeanPool.Despawn(coke);
            if (thirdFoodOnhand != 0 && !tableView.table1CokeDummy[tableView.table1CokeDummy.Length - 1].activeInHierarchy)
            {
                Invoke(nameof(MoveFrocksToTable1), 0.05f);
            }
            if (thirdFoodOnhand == 0)
            {
                GlobalData.instance.isFoodOnHand = false;
                // tableView.isCokeOnTable = true;

                if (tableView.customerCount == 1)
                {
                    if (!tableView.Customer1Ontable.GetComponent<CustomerView>().isCustomerEating && tableView.isTableBooked) { tableView.Customer1Ontable.GetComponent<CustomerView>().Eat(); }
                }
                if (tableView.customerCount > 1)
                {
                    if (tableView.isTableBooked)
                    {
                        if (!tableView.Customer1Ontable.GetComponent<CustomerView>().isCustomerEating && tableView.isTableBooked) { tableView.Customer1Ontable.GetComponent<CustomerView>().Eat(); }
                        // tableView.Customer2Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("Eat", true);
                        // tableView.Customer3Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("Eat", true);
                        // tableView.Customer4Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("Eat", true);
                    }
                }
            }
        });
    }

    public void MoveFabricToTable1()
    {
        if (!onTable) return;
        GlobalData.instance.isFoodMovingToTable = true;
        tableView.isCokeOnTable = true;
        tableView.Customer1Ontable.GetComponent<CustomerView>().isCustomerEating = false;
        GameObject coke = LeanPool.Spawn(CoockerView.instance.thirdFoodPrefab, thirdFoodDummys[thirdFoodOnhand - 1].transform.position, thirdFoodDummys[thirdFoodOnhand - 1].transform.rotation);
        thirdFoodDummys[thirdFoodOnhand - 1].enabled = false;
        thirdFoodOnhand--;
        DailyTaskView.instance.ColaServedCount(1);

        int index = tableView.cokeOnFirstTableCount;
        tableView.cokeOnFirstTableCount++;

        coke.transform.DOMove(tableView.table1CokeDummy[index].transform.position, 0.04f).SetEase(Ease.InQuint).OnComplete(() =>
        {
            GlobalData.instance.isFoodMovingToTable = false;
            tableView.table1CokeDummy[index].SetActive(true);
            Controller.instance.gameController.PlayHaptic();

            GameObject effect = LeanPool.Spawn(onHandEffect, tableView.table1CokeDummy[index].transform.position, onHandEffect.transform.rotation);
            StartCoroutine(DestroyObject(effect, 2f));

            LeanPool.Despawn(coke);
            if (thirdFoodOnhand != 0 && !tableView.table1CokeDummy[tableView.table1CokeDummy.Length - 1].activeInHierarchy)
            {
                Invoke(nameof(MoveFrocksToTable1), 0.05f);
            }
            if (thirdFoodOnhand == 0)
            {
                GlobalData.instance.isFoodOnHand = false;
                // tableView.isCokeOnTable = true;

                if (tableView.customerCount == 1)
                {
                    if (!tableView.Customer1Ontable.GetComponent<CustomerView>().isCustomerEating && tableView.isTableBooked) { tableView.Customer1Ontable.GetComponent<CustomerView>().Eat(); }
                }
                if (tableView.customerCount > 1)
                {
                    if (tableView.isTableBooked)
                    {
                        if (!tableView.Customer1Ontable.GetComponent<CustomerView>().isCustomerEating && tableView.isTableBooked) { tableView.Customer1Ontable.GetComponent<CustomerView>().Eat(); }
                        // tableView.Customer2Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("Eat", true);
                        // tableView.Customer3Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("Eat", true);
                        // tableView.Customer4Ontable.GetComponent<CustomerView>().customerAnimator.SetBool("Eat", true);
                    }
                }
            }
        });
    }

    public void MoveShirtsToDeliveryTable()
    {
        PackagingView packagingView = DeliveryTableNo.GetComponent<PackagingView>();

        if(!onDeliveryTable) return;
        if(packagingView.burgrOnTableCount == packagingView.dummyFries.Length) return;

        GlobalData.instance.isHandFull = false;
        Controller.instance.uiController.playerMaxText.SetActive(false);
        Controller.instance.uiController.playerMaxText2.SetActive(false);
        Controller.instance.uiController.playerMaxText3.SetActive(false);
        Controller.instance.uiController.playerMaxText4.SetActive(false);

        GameObject secondFood = LeanPool.Spawn(CoockerView.instance.firstFoodPrefab, firstFoodDummys[firstFoodOnhand -1].transform.position, firstFoodDummys[firstFoodOnhand -1].transform.rotation);
        secondFood.transform.DOMove(packagingView.dummyBurgers[packagingView.burgrOnTableCount].transform.position, 0.04f).SetEase(Ease.InQuint).OnComplete(() =>
        {
            GameObject effect = LeanPool.Spawn(onHandEffect, packagingView.dummyBurgers[packagingView.burgrOnTableCount].transform.position, onHandEffect.transform.rotation);
            StartCoroutine(DestroyObject(effect, 2f));

            packagingView.dummyBurgers[packagingView.burgrOnTableCount].SetActive(true);
            packagingView.burgrOnTableCount++;
            Controller.instance.gameController.PlayHaptic();
            LeanPool.Despawn(secondFood);
        });

        firstFoodDummys[firstFoodOnhand -1].enabled = false;
        firstFoodOnhand--;
        if(firstFoodOnhand > 0)
        {
            Invoke(nameof(MoveShirtsToDeliveryTable), 0.06f);
        }
        if(firstFoodOnhand == 0)
        {
            GlobalData.instance.isFoodOnHand = false;
            if(packagingView.boxOnTableCount < packagingView.dummyBoxes.Length - 1)
            {
                if(!packagingView.isPacking)
                {
                    packagingView.InstantiateBox();
                }
            }
        }
    }
    public void MoveJeansToDeliveryTable()
    {
        PackagingView packagingView = DeliveryTableNo.GetComponent<PackagingView>();

        if(!onDeliveryTable) return;
        if(packagingView.friesOnTableCount == packagingView.dummyFries.Length) return;

        GlobalData.instance.isHandFull = false;
        Controller.instance.uiController.playerMaxText.SetActive(false);
        Controller.instance.uiController.playerMaxText2.SetActive(false);
        Controller.instance.uiController.playerMaxText3.SetActive(false);
        Controller.instance.uiController.playerMaxText4.SetActive(false);

        GameObject secondFood = LeanPool.Spawn(CoockerView.instance.secondFoodPrefab, secondFoodDummys[secondFoodOnhand -1].transform.position, secondFoodDummys[secondFoodOnhand -1].transform.rotation);
        secondFood.transform.DOMove(packagingView.dummyFries[packagingView.friesOnTableCount].transform.position, 0.04f).SetEase(Ease.InQuint).OnComplete(() =>
        {
            GameObject effect = LeanPool.Spawn(onHandEffect, packagingView.dummyFries[packagingView.friesOnTableCount].transform.position, onHandEffect.transform.rotation);
            StartCoroutine(DestroyObject(effect, 2f));

            packagingView.dummyFries[packagingView.friesOnTableCount].SetActive(true);
            packagingView.friesOnTableCount++;
            Controller.instance.gameController.PlayHaptic();
            LeanPool.Despawn(secondFood);
        });

        secondFoodDummys[secondFoodOnhand -1].enabled = false;
        secondFoodOnhand--;
        if(secondFoodOnhand > 0)
        {
            Invoke(nameof(MoveJeansToDeliveryTable), 0.06f);
        }
        if(secondFoodOnhand == 0)
        {
            GlobalData.instance.isFoodOnHand = false;
            if(packagingView.boxOnTableCount < packagingView.dummyBoxes.Length - 1)
            {
                if(!packagingView.isPacking)
                {
                    packagingView.InstantiateBox();
                }
            }
        }
    }
    public void MoveFrocksToDeliveryTable()
    {
        PackagingView packagingView = DeliveryTableNo.GetComponent<PackagingView>();

        if(!onDeliveryTable) return;
        if(packagingView.cokeOnTableCount == packagingView.dummyCoke.Length) return;

        GlobalData.instance.isHandFull = false;
        Controller.instance.uiController.playerMaxText.SetActive(false);
        Controller.instance.uiController.playerMaxText2.SetActive(false);
        Controller.instance.uiController.playerMaxText3.SetActive(false);
        Controller.instance.uiController.playerMaxText4.SetActive(false);

        GameObject secondFood = LeanPool.Spawn(CoockerView.instance.thirdFoodPrefab, thirdFoodDummys[thirdFoodOnhand -1].transform.position, thirdFoodDummys[thirdFoodOnhand -1].transform.rotation);
        secondFood.transform.DOMove(packagingView.dummyCoke[packagingView.cokeOnTableCount].transform.position, 0.04f).SetEase(Ease.InQuint).OnComplete(() =>
        {
            GameObject effect = LeanPool.Spawn(onHandEffect, packagingView.dummyCoke[packagingView.cokeOnTableCount].transform.position, onHandEffect.transform.rotation);
            StartCoroutine(DestroyObject(effect, 2f));

            packagingView.dummyCoke[packagingView.cokeOnTableCount].SetActive(true);
            packagingView.cokeOnTableCount++;
            Controller.instance.gameController.PlayHaptic();
            LeanPool.Despawn(secondFood);
        });

        thirdFoodDummys[thirdFoodOnhand -1].enabled = false;
        thirdFoodOnhand--;
        if(thirdFoodOnhand > 0)
        {
            Invoke(nameof(MoveFrocksToDeliveryTable), 0.06f);
        }
        if(thirdFoodOnhand == 0)
        {
            GlobalData.instance.isFoodOnHand = false;
            if(packagingView.boxOnTableCount < packagingView.dummyBoxes.Length - 1)
            {
                if(!packagingView.isPacking)
                {
                    packagingView.InstantiateBox();
                }
            }
        }
    }

    public void MoveBoxToHand()
    {
        if(onScootrePoint) return;

        PackagingView packagingView = DeliveryTableNo.GetComponent<PackagingView>();
        if(packagingView.boxOnTableCount == 0)return;
        if (boxOnhandCount < maxBoxOnhandCount)
        {
            if (!onBoxPoint) return;
            packagingView.dummyBoxes[packagingView.boxOnTableCount - 1].SetActive(false);

            GameObject box = LeanPool.Spawn(packagingView.boxPrefab, packagingView.dummyBoxes[packagingView.boxOnTableCount - 1].transform.position, packagingView.dummyBoxes[packagingView.boxOnTableCount - 1].transform.rotation);
            GlobalData.instance.isBoxOnHand = true;
            box.AddComponent<MovingBox>();

            packagingView.boxOnTableCount--;
        }
    }

    public void MoveBoxToScooter()
    {
        if (isBoxMoving) return;
        if(boxOnhandCount == 0 ) { onScootrePoint = false; return; }
        
        GlobalData.instance.isFoodMovingToTable = true;
        GameObject box = LeanPool.Spawn(PackagingView.instance.boxPrefab, boxDummys[boxOnhandCount - 1].transform.position, boxDummys[boxOnhandCount - 1].transform.rotation);
        boxDummys[boxOnhandCount - 1].SetActive(false);

        boxOnhandCount--;

        box.transform.DOMove(ScooterView.instance.dummyBoxes[ScooterView.instance.boxStoredInScooty].transform.position, 0.04f).SetEase(Ease.InQuint).OnComplete(()=>
        {
            GlobalData.instance.isFoodMovingToTable = false;
            ScooterView.instance.dummyBoxes[ScooterView.instance.boxStoredInScooty].SetActive(true);
            Controller.instance.gameController.PlayHaptic();

            ScooterView.instance.boxStoredInScooty++;
            //firstFoodOnhand--;

            LeanPool.Despawn(box);
            if(boxOnhandCount > 0)
            {
                Invoke(nameof(MoveBoxToScooter), 0.1f);
            }
            if(boxOnhandCount == 0)
            {
                onScootrePoint = false;
                GlobalData.instance.isBoxOnHand = false;
                StartCoroutine(ScooterView.instance.MoveScooterOut(0.5f));
            }
        });
    }

    public IEnumerator DestroyObject(GameObject gameObject,float time)
    {
        yield return new WaitForSeconds(time);
        LeanPool.Despawn(gameObject);
    }

}
