using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Pool;
using DG.Tweening;
using TMPro;

public class RedialProgressBar : MonoBehaviour
{
    public GameObject cash;
    public GameObject effectImage;
    public Image fillImage;
    public GameObject unlockObject;
    public TextMeshProUGUI priceText;
    public GameObject effect;
    public GameObject[] blackBordersAndColliders;
    public bool onBar;
    public int price;
    float increseAmount;
    public bool table;
    public bool burger, fries, coke;
    public bool table1, table2, table3, table4, table5, table6, table7, table8, specialTable;
    public bool hr, upgrade;
    public bool deliverLand,packtable2;
    public bool newland;
    public int fillSpeed;

    private void Start() 
    {
        Invoke(nameof(EffectStart), 0f);

        float calculate = price / fillSpeed;
        increseAmount = (1 / calculate);
        if(price >= 1000)
        {
            float value = price / 1000;
            value = Mathf.Round(value * 100.0f) * 0.01f;
            priceText.text = value.ToString() + "K";
        }
        else
        {
            priceText.text = price.ToString();
        }
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            onBar = true;
            Invoke(nameof(InstanceMoneyAndMove), 0.05f);
        }
    }
    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            onBar = false;
            CancelInvoke(nameof(InstanceMoneyAndMove));
        }
    }

    public void InstanceMoneyAndMove()
    {
        if(!onBar) return;
        if(Controller.instance.currencyController.GetCurrentAmount() < fillSpeed) return;

        Controller.instance.currencyController.CutMoney(fillSpeed);
        Controller.instance.uiController.UpdateMoneyTexts();

        GameObject money = LeanPool.Spawn(cash, PLayerView.instance.moneyMovePos.position, Controller.instance.currencyController.moneyPrefab.transform.rotation);
        money.transform.DOMove(this.transform.position, 0.07f).SetEase(Ease.InOutQuad).OnComplete(() => 
        {
            Destroy(money);
            Controller.instance.gameController.PlayHaptic();

            fillImage.fillAmount += increseAmount;
            if(fillImage.fillAmount < 1)
            {
                Invoke(nameof(InstanceMoneyAndMove), 0f);
            }
            if(fillImage.fillAmount == 1)
            {
                CancelInvoke(nameof(InstanceMoneyAndMove));
                Destroy(gameObject);
                unlockObject.transform.DOScale(Vector3.zero, 0).OnComplete(()=>
                {
                    unlockObject.SetActive(true);
                    LeanPool.Spawn(effect, unlockObject.transform.position, effect.transform.rotation);
                    if(!table){unlockObject.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutExpo);}
                    if(table){unlockObject.transform.DOScale(new Vector3(2,2,2), 0.5f).SetEase(Ease.InOutExpo);}
                });

                DoSpecificOperations();
            }
        });
    }

    public void EffectStart()
    {
        effectImage.transform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.3f).SetEase(Ease.InQuint).OnComplete(() =>
           {
               Invoke(nameof(EffectEnd), 0.1f);
           });
    }
    public void EffectEnd()
    {
        effectImage.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutQuint).OnComplete(() =>
           {
               Invoke(nameof(EffectStart), 0.1f);
           });
    }

    public void DoSpecificOperations()
    {
        //if (burger) 
        //{ 
        //    CoockerView.instance.SewShirtsAndStack();
        //    AllLockAndUnlockView.instance.table1UnlockPoint.SetActive(true);
        //    LeanPool.Spawn(effect, AllLockAndUnlockView.instance.table1UnlockPoint.transform.position, effect.transform.rotation);
        //}
        //if (fries) 
        //{ 
        //    CoockerView.instance.SewJeansAndStack();
        //    AllLockAndUnlockView.instance.UnlockFries();
        //    AllLockAndUnlockView.instance.deliverLandUnlockPoint.SetActive(true);
        //    LeanPool.Spawn(effect, AllLockAndUnlockView.instance.deliverLandUnlockPoint.transform.position, effect.transform.rotation);

        //    for (int i = 0; i < Controller.instance.customersController.waitingCustomers.Count; i++)
        //    {
        //        Controller.instance.customersController.waitingCustomers[i].GetComponent<CustomerView>().ResetOrders();
        //    }
        //}
        //if (coke) 
        //{ 
        //    CoockerView.instance.SewFrocksAndStack();
        //    AllLockAndUnlockView.instance.UnlockCoke();
        //    for (int i = 0; i < Controller.instance.customersController.waitingCustomers.Count; i++)
        //    {
        //        Controller.instance.customersController.waitingCustomers[i].GetComponent<CustomerView>().ResetOrders();
        //    }
        //    AllLockAndUnlockView.instance.newLandUnlockPoint.SetActive(true);
        //    LeanPool.Spawn(effect, AllLockAndUnlockView.instance.newLandUnlockPoint.transform.position, effect.transform.rotation);
        //}

        //if(deliverLand)
        //{
        //    AllLockAndUnlockView.instance.UnlockDeliverLand();
        //    AllLockAndUnlockView.instance.deliverLandLocked.SetActive(false);
        //    AllLockAndUnlockView.instance.upgradeUnlockPoint.SetActive(true);
        //    LeanPool.Spawn(effect, AllLockAndUnlockView.instance.upgradeUnlockPoint.transform.position, effect.transform.rotation);
        //}
        //if(newland)
        //{
        //    AllLockAndUnlockView.instance.UnlockNewLand();
        //    AllLockAndUnlockView.instance.newLandLocked.SetActive(false);
        //    AllLockAndUnlockView.instance.table5UnlockPoint.SetActive(true);
        //    LeanPool.Spawn(effect, AllLockAndUnlockView.instance.table5UnlockPoint.transform.position, effect.transform.rotation);
        //}
        //if(packtable2)
        //{
        //    AllLockAndUnlockView.instance.UnlockPackTable2();
        //}

        //if (hr) 
        //{ 
        //    AllLockAndUnlockView.instance.UnlockHR();
        //    AllLockAndUnlockView.instance.friesUnlockPoint.SetActive(true);
        //    Controller.instance.waiterController.waiter1.SetActive(true);
        //    LeanPool.Spawn(effect, AllLockAndUnlockView.instance.friesUnlockPoint.transform.position, effect.transform.rotation);
        //}
        //if (upgrade) 
        //{ 
        //    AllLockAndUnlockView.instance.UnlockUpgrade();
        //    AllLockAndUnlockView.instance.specialTableUnlockPoint.SetActive(true);
        //    LeanPool.Spawn(effect, AllLockAndUnlockView.instance.specialTableUnlockPoint.transform.position, effect.transform.rotation);
        //}

        //if(specialTable)
        //{
        //    //Controller.instance.customersController.specialCustomer.SetActive(true);
        //    //Controller.instance.customersController.EatFoodBySpecialCustomer();
        //    AllLockAndUnlockView.instance.UnlockSpecialTable();
        //    AllLockAndUnlockView.instance.table3UnlockPoint.SetActive(true);
        //    LeanPool.Spawn(effect, AllLockAndUnlockView.instance.table3UnlockPoint.transform.position, effect.transform.rotation);
        //}

        //if(table1)
        //{
        //    for (int i = 0; i < blackBordersAndColliders.Length; i++)
        //    {
        //        blackBordersAndColliders[i].SetActive(false);
        //    }

        //    Tutorial.instance.TutorialComplete();
        //    AllLockAndUnlockView.instance.UnlockBurger();
        //    AllLockAndUnlockView.instance.UnlockTable1();

        //    Controller.instance.cameraController.MoveCamera();

        //    AllLockAndUnlockView.instance.table2UnlockPoint.SetActive(true);
        //    LeanPool.Spawn(effect, AllLockAndUnlockView.instance.table2UnlockPoint.transform.position, effect.transform.rotation);

        //    Controller.instance.customersController.CallInstantiateCustomerAndWait();
        //}
        //if(table2)
        //{
        //    AllLockAndUnlockView.instance.UnlockTable2();
        //    AllLockAndUnlockView.instance.hrUnlockPoint.SetActive(true);
        //    LeanPool.Spawn(effect, AllLockAndUnlockView.instance.hrUnlockPoint.transform.position, effect.transform.rotation);
        //}
        //if(table3)
        //{
        //    AllLockAndUnlockView.instance.UnlockTable3();
        //    AllLockAndUnlockView.instance.table4UnlockPoint.SetActive(true);
        //    Controller.instance.tableController.DisableTable3Collider();
        //    LeanPool.Spawn(effect, AllLockAndUnlockView.instance.table4UnlockPoint.transform.position, effect.transform.rotation);
        //}
        //if(table4)
        //{
        //    AllLockAndUnlockView.instance.UnlockTable4();
        //    AllLockAndUnlockView.instance.cokeUnlockPoint.SetActive(true);
        //    Controller.instance.tableController.DisableTable4Collider();
        //    LeanPool.Spawn(effect, AllLockAndUnlockView.instance.cokeUnlockPoint.transform.position, effect.transform.rotation);
        //}
        //if(table5)
        //{
        //    AllLockAndUnlockView.instance.UnlockTable5();
        //    AllLockAndUnlockView.instance.table6UnlockPoint.SetActive(true);
        //    LeanPool.Spawn(effect, AllLockAndUnlockView.instance.table6UnlockPoint.transform.position, effect.transform.rotation);
        //}
        //if(table6)
        //{
        //    AllLockAndUnlockView.instance.UnlockTable6();
        //    AllLockAndUnlockView.instance.table7UnlockPoint.SetActive(true);
        //    LeanPool.Spawn(effect, AllLockAndUnlockView.instance.table7UnlockPoint.transform.position, effect.transform.rotation);
        //}
        //if(table7)
        //{
        //    AllLockAndUnlockView.instance.UnlockTable7();
        //    AllLockAndUnlockView.instance.table8UnlockPoint.SetActive(true);
        //    LeanPool.Spawn(effect, AllLockAndUnlockView.instance.table8UnlockPoint.transform.position, effect.transform.rotation);
        //    //AllLockAndUnlockView.instance.UnlockTable7();
        //}
        //if (table8)
        //{
        //    AllLockAndUnlockView.instance.UnlockTable8();
        //}



        if (burger)
        {
            CoockerView.instance.SewShirtsAndStack();
            AllLockAndUnlockView.instance.table1UnlockPoint.SetActive(true);
            LeanPool.Spawn(effect, AllLockAndUnlockView.instance.table1UnlockPoint.transform.position, effect.transform.rotation);
        }
        if (fries)
        {
            CoockerView.instance.SewJeansAndStack();
            AllLockAndUnlockView.instance.UnlockFries();
            AllLockAndUnlockView.instance.table2UnlockPoint.SetActive(true);
            LeanPool.Spawn(effect, AllLockAndUnlockView.instance.table2UnlockPoint.transform.position, effect.transform.rotation);

            for (int i = 0; i < Controller.instance.customersController.waitingCustomers.Count; i++)
            {
                Controller.instance.customersController.waitingCustomers[i].GetComponent<CustomerView>().ResetOrders();
            }
        }
        if (coke)
        {
            CoockerView.instance.SewFrocksAndStack();
            AllLockAndUnlockView.instance.UnlockCoke();
            for (int i = 0; i < Controller.instance.customersController.waitingCustomers.Count; i++)
            {
                Controller.instance.customersController.waitingCustomers[i].GetComponent<CustomerView>().ResetOrders();
            }
            AllLockAndUnlockView.instance.newLandUnlockPoint.SetActive(true);
            LeanPool.Spawn(effect, AllLockAndUnlockView.instance.newLandUnlockPoint.transform.position, effect.transform.rotation);
        }

        if (deliverLand)
        {
            AllLockAndUnlockView.instance.UnlockDeliverLand();
            AllLockAndUnlockView.instance.deliverLandLocked.SetActive(false);
            AllLockAndUnlockView.instance.friesUnlockPoint.SetActive(true);
            LeanPool.Spawn(effect, AllLockAndUnlockView.instance.friesUnlockPoint.transform.position, effect.transform.rotation);
        }
        if (newland)
        {
            AllLockAndUnlockView.instance.UnlockNewLand();
            AllLockAndUnlockView.instance.newLandLocked.SetActive(false);
            AllLockAndUnlockView.instance.table6UnlockPoint.SetActive(true);
            LeanPool.Spawn(effect, AllLockAndUnlockView.instance.table6UnlockPoint.transform.position, effect.transform.rotation);
        }
        if (packtable2)
        {
            AllLockAndUnlockView.instance.UnlockPackTable2();
        }

        if (hr)
        {
            AllLockAndUnlockView.instance.UnlockHR();
            Controller.instance.waiterController.waiter1.SetActive(true);
            AllLockAndUnlockView.instance.deliverLandUnlockPoint.SetActive(true);
            LeanPool.Spawn(effect, AllLockAndUnlockView.instance.deliverLandUnlockPoint.transform.position, effect.transform.rotation);
        }
        if (upgrade)
        {
            AllLockAndUnlockView.instance.UnlockUpgrade();
            AllLockAndUnlockView.instance.specialTableUnlockPoint.SetActive(true);
            LeanPool.Spawn(effect, AllLockAndUnlockView.instance.specialTableUnlockPoint.transform.position, effect.transform.rotation);
        }

        if (specialTable)
        {
            AllLockAndUnlockView.instance.UnlockSpecialTable();
            AllLockAndUnlockView.instance.table7UnlockPoint.SetActive(true);
            LeanPool.Spawn(effect, AllLockAndUnlockView.instance.table7UnlockPoint.transform.position, effect.transform.rotation);
        }

        if (table1)
        {
            for (int i = 0; i < blackBordersAndColliders.Length; i++)
            {
                blackBordersAndColliders[i].SetActive(false);
            }

            Tutorial.instance.TutorialComplete();
            AllLockAndUnlockView.instance.UnlockBurger();
            AllLockAndUnlockView.instance.UnlockTable1();

            Controller.instance.cameraController.MoveCamera();

            AllLockAndUnlockView.instance.newLandUnlockPoint.SetActive(true);
            LeanPool.Spawn(effect, AllLockAndUnlockView.instance.newLandUnlockPoint.transform.position, effect.transform.rotation);

            Controller.instance.customersController.CallInstantiateCustomerAndWait();
        }
        if (table2)
        {
            AllLockAndUnlockView.instance.UnlockTable2();
            AllLockAndUnlockView.instance.upgradeUnlockPoint.SetActive(true);
            LeanPool.Spawn(effect, AllLockAndUnlockView.instance.upgradeUnlockPoint.transform.position, effect.transform.rotation);
        }
        if (table3)
        {
            AllLockAndUnlockView.instance.UnlockTable3();
            AllLockAndUnlockView.instance.table4UnlockPoint.SetActive(true);
            Controller.instance.tableController.DisableTable3Collider();
            LeanPool.Spawn(effect, AllLockAndUnlockView.instance.table4UnlockPoint.transform.position, effect.transform.rotation);
        }
        if (table4)
        {
            AllLockAndUnlockView.instance.UnlockTable4();
            Controller.instance.tableController.DisableTable4Collider();
        }
        if (table5)
        {
            AllLockAndUnlockView.instance.UnlockTable5();
            AllLockAndUnlockView.instance.table8UnlockPoint.SetActive(true);
            LeanPool.Spawn(effect, AllLockAndUnlockView.instance.table8UnlockPoint.transform.position, effect.transform.rotation);
        }
        if (table6)
        {
            AllLockAndUnlockView.instance.UnlockTable6();
            AllLockAndUnlockView.instance.hrUnlockPoint.SetActive(true);
            LeanPool.Spawn(effect, AllLockAndUnlockView.instance.hrUnlockPoint.transform.position, effect.transform.rotation);
        }
        if (table7)
        {
            AllLockAndUnlockView.instance.UnlockTable7();
            AllLockAndUnlockView.instance.friesUnlockPoint.SetActive(true);
            LeanPool.Spawn(effect, AllLockAndUnlockView.instance.friesUnlockPoint.transform.position, effect.transform.rotation);
        }
        if (table8)
        {
            AllLockAndUnlockView.instance.UnlockTable8();
            AllLockAndUnlockView.instance.table3UnlockPoint.SetActive(true);
            LeanPool.Spawn(effect, AllLockAndUnlockView.instance.table3UnlockPoint.transform.position, effect.transform.rotation);
        }
    }
}
