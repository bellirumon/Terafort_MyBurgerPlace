using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllLockAndUnlockView : MonoBehaviour
{
    public static AllLockAndUnlockView instance;
    public GameObject[] blackBordersAndColliders;

    public GameObject burgerCurt;
    public GameObject friesCurt;
    public GameObject cokeCurt;
    public GameObject burgerUnlockPoint;
    public GameObject friesUnlockPoint;
    public GameObject cokeUnlockPoint;

    public GameObject hr;
    public GameObject hrUnlockPoint;
    public GameObject upgrade;
    public GameObject upgradeUnlockPoint;

    public GameObject deliverLandLocked;
    public GameObject deliverLandUnlocked;
    public GameObject deliverLandUnlockPoint;
    public GameObject packtable2UnlockPoint;

    public GameObject table1;
    public GameObject table2;
    public GameObject table3;
    public GameObject table4;
    public GameObject table5;
    public GameObject table6;
    public GameObject table7;
    public GameObject table8;
    public GameObject specialTable;
    public GameObject packingTable1;
    public GameObject packingTable2;
    public GameObject table1UnlockPoint;
    public GameObject table2UnlockPoint;
    public GameObject table3UnlockPoint;
    public GameObject table4UnlockPoint;
    public GameObject table5UnlockPoint;
    public GameObject table6UnlockPoint;
    public GameObject table7UnlockPoint;
    public GameObject table8UnlockPoint;
    public GameObject specialTableUnlockPoint;

    [Space]
    public GameObject newLandLocked;
    public GameObject newLandUnLocked;
    public GameObject newLandUnlockPoint;
    private void Awake() 
    {
        instance = this;
    }
    private void Start() 
    {
        LoadData();
        LoadData2();
    }

    public void LoadData()
    {
        if(Tutorial.instance.IsTutorialComplete())
        {
            for (int i = 0; i < blackBordersAndColliders.Length; i++)
            {
                blackBordersAndColliders[i].SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < blackBordersAndColliders.Length; i++)
            {
                blackBordersAndColliders[i].SetActive(true);
            }
        }

        if (IsBurgerUnlocked()) { burgerCurt.SetActive(true); burgerUnlockPoint.SetActive(false); }
        if (IsFriesUnlocked()) { friesCurt.SetActive(true); }
        if (IsCokeUnlocked()) { cokeCurt.SetActive(true); }

        if (IsTable1Unlocked()) { table1.SetActive(true); }
        if (IsTable2Unlocked()) { table2.SetActive(true); }
        if (IsTable3Unlocked()) { table3.SetActive(true); }
        if (IsTable4Unlocked()) { table4.SetActive(true); }
        if (IsTable5Unlocked()) { table5.SetActive(true); }
        if (IsTable6Unlocked()) { table6.SetActive(true); }
        if (IsTable7Unlocked()) { table7.SetActive(true); /*table7UnlockPoint.SetActive(false);*/}
        if (IsTable8Unlocked()) { table8.SetActive(true); table8UnlockPoint.SetActive(false);}
        if (IsSpecialTableUnlocked()) { specialTable.SetActive(true); Controller.instance.customersController.specialCustomer.SetActive(true); }

        if (IsHRUnlocked()) 
        { 
            hr.SetActive(true);
            Controller.instance.waiterController.waiter1.SetActive(true);
        }
        if (IsUpgradeUnlocked()) { upgrade.SetActive(true); }

        if (IsDeliverLandUnlocked()) { deliverLandLocked.SetActive(false); deliverLandUnlocked.SetActive(true);}
        if (IsNewLandUnlocked()) 
        { 
            newLandLocked.SetActive(false); newLandUnLocked.SetActive(true);
            newLandUnlockPoint.SetActive(false);
        }
        if (IsPackTable2Unlocked()) { packingTable2.SetActive(true); packtable2UnlockPoint.SetActive(false); }
    }

    public void LoadData2()
    {
        //if (IsTable1Unlocked() && !IsTable2Unlocked()) { table2UnlockPoint.SetActive(true); }
        //if (IsTable2Unlocked() && !IsHRUnlocked()) { hrUnlockPoint.SetActive(true); }
        //if (IsHRUnlocked() && !IsFriesUnlocked()) { friesUnlockPoint.SetActive(true); }
        //if (IsFriesUnlocked() && !IsDeliverLandUnlocked()) { deliverLandUnlockPoint.SetActive(true); }
        //if (IsDeliverLandUnlocked() && !IsUpgradeUnlocked()) { upgradeUnlockPoint.SetActive(true); }
        //if (IsUpgradeUnlocked() && !IsSpecialTableUnlocked()) { specialTableUnlockPoint.SetActive(true); }
        //if (IsSpecialTableUnlocked() && !IsTable3Unlocked()) { table3UnlockPoint.SetActive(true); }
        //if (IsTable3Unlocked() && !IsTable4Unlocked()) { table4UnlockPoint.SetActive(true); }
        //if (IsTable4Unlocked() && !IsCokeUnlocked()) { cokeUnlockPoint.SetActive(true); }
        //if (IsCokeUnlocked() && !IsNewLandUnlocked()) { newLandUnlockPoint.SetActive(true); }
        //if (IsNewLandUnlocked() && !IsTable5Unlocked()) { table5UnlockPoint.SetActive(true); }
        //if (IsTable5Unlocked() && !IsTable6Unlocked()) { table6UnlockPoint.SetActive(true); }
        //if (IsTable6Unlocked() && !IsTable7Unlocked()) { table7UnlockPoint.SetActive(true); }
        //if (IsTable7Unlocked() && !IsTable8Unlocked()) { table8UnlockPoint.SetActive(true); }


        if (IsTable1Unlocked() && !IsTable6Unlocked()) { table6UnlockPoint.SetActive(true); }
        if (IsTable6Unlocked() && !IsHRUnlocked()) { hrUnlockPoint.SetActive(true); }
        if (IsHRUnlocked() && !IsDeliverLandUnlocked()) { deliverLandUnlockPoint.SetActive(true); }
        if (IsDeliverLandUnlocked() && !IsFriesUnlocked()) { friesUnlockPoint.SetActive(true); }
        if (IsFriesUnlocked() && !IsTable2Unlocked()) { table2UnlockPoint.SetActive(true); }
        if (IsTable2Unlocked() && !IsUpgradeUnlocked()) { upgradeUnlockPoint.SetActive(true); }
        if (IsUpgradeUnlocked() && !IsTable7Unlocked()) { table7UnlockPoint.SetActive(true); }
        if (IsTable7Unlocked() && !IsNewLandUnlocked()) { newLandUnlockPoint.SetActive(true); }
        if (IsNewLandUnlocked() && !IsSpecialTableUnlocked()) { specialTableUnlockPoint.SetActive(true); }
        if (IsSpecialTableUnlocked() && !IsCokeUnlocked()) { cokeUnlockPoint.SetActive(true); }
        if (IsCokeUnlocked() && !IsTable5Unlocked()) { table5UnlockPoint.SetActive(true); }
        if (IsTable5Unlocked() && !IsTable8Unlocked()) { table8UnlockPoint.SetActive(true); }
        if (IsTable8Unlocked() && !IsTable3Unlocked()) { table3UnlockPoint.SetActive(true); }
        if (IsTable3Unlocked() && !IsTable4Unlocked()) { table4UnlockPoint.SetActive(true); }

    }
    public bool IsBurgerUnlocked()
    {
        if (PlayerPrefs.GetInt("BurgerUnlocked") == 1) { return true; }
        else{ return false; }
    }
    public void UnlockBurger()
    {
        PlayerPrefs.SetInt("BurgerUnlocked", 1);
    }


    public bool IsFriesUnlocked()
    {
        if (PlayerPrefs.GetInt("FriesUnlocked") == 1) {return true;}
        else { return false; }
    }
    public void UnlockFries()
    {
        PlayerPrefs.SetInt("FriesUnlocked", 1);
    }


    public bool IsCokeUnlocked()
    {
        if (PlayerPrefs.GetInt("CokeUnlocked") == 1) { return true; }
        else{ return false; }
    }
    public void UnlockCoke()
    {
        PlayerPrefs.SetInt("CokeUnlocked", 1);
    }


    public bool IsSpecialTableUnlocked()
    {
        if (PlayerPrefs.GetInt("SpecialTableUnlocked") == 1) { return true; }
        else{ return false; }
    }
    public void UnlockSpecialTable()
    {
        PlayerPrefs.SetInt("SpecialTableUnlocked", 1);
    }


    public bool IsTable1Unlocked()
    {
        if (PlayerPrefs.GetInt("Table1Unlocked") == 1) { return true; }
        else{ return false; }
    }
    public void UnlockTable1()
    {
        PlayerPrefs.SetInt("Table1Unlocked", 1);
    }


    public bool IsTable2Unlocked()
    {
        if (PlayerPrefs.GetInt("Table2Unlocked") == 1) { return true; }
        else{ return false; }
    }
    public void UnlockTable2()
    {
        PlayerPrefs.SetInt("Table2Unlocked", 1);
    }


    public bool IsTable3Unlocked()
    {
        if (PlayerPrefs.GetInt("Table3Unlocked") == 1) { return true; }
        else{ return false; }
    }
    public void UnlockTable3()
    {
        PlayerPrefs.SetInt("Table3Unlocked", 1);
    }


    public bool IsTable4Unlocked()
    {
        if (PlayerPrefs.GetInt("Table4Unlocked") == 1) { return true; }
        else{ return false; }
    }
    public void UnlockTable4()
    {
        PlayerPrefs.SetInt("Table4Unlocked", 1);
    }

    public bool IsTable5Unlocked()
    {
        if (PlayerPrefs.GetInt("Table5Unlocked") == 1) { return true; }
        else{ return false; }
    }
    public void UnlockTable5()
    {
        PlayerPrefs.SetInt("Table5Unlocked", 1);
    }

    public bool IsTable6Unlocked()
    {
        if (PlayerPrefs.GetInt("Table6Unlocked") == 1) { return true; }
        else{ return false; }
    }
    public void UnlockTable6()
    {
        PlayerPrefs.SetInt("Table6Unlocked", 1);
    }

    public bool IsTable7Unlocked()
    {
        if (PlayerPrefs.GetInt("Table7Unlocked") == 1) { return true; }
        else{ return false; }
    }
    public void UnlockTable7()
    {
        PlayerPrefs.SetInt("Table7Unlocked", 1);
    }

    public bool IsTable8Unlocked()
    {
        if (PlayerPrefs.GetInt("Table8Unlocked") == 1) { return true; }
        else { return false; }
    }
    public void UnlockTable8()
    {
        PlayerPrefs.SetInt("Table8Unlocked", 1);
    }


    public bool IsHRUnlocked()
    {
        if (PlayerPrefs.GetInt("HRUnlocked") == 1) { return true; }
        else{ return false; }
    }
    public void UnlockHR()
    {
        PlayerPrefs.SetInt("HRUnlocked", 1);
    }


    public bool IsUpgradeUnlocked()
    {
        if (PlayerPrefs.GetInt("UpgradeUnlocked") == 1) { return true; }
        else{ return false; }
    }
    public void UnlockUpgrade()
    {
        PlayerPrefs.SetInt("UpgradeUnlocked", 1);
    }


    public bool IsDeliverLandUnlocked()
    {
        if (PlayerPrefs.GetInt("DeliverLandUnlocked") == 1) { return true; }
        else{ return false; }
    }
    public bool IsPackTable2Unlocked()
    {
        if (PlayerPrefs.GetInt("DeliverTable2Unlocked") == 1) { return true; }
        else{ return false; }
    }
    public void UnlockPackTable2()
    {
        PlayerPrefs.SetInt("DeliverTable2Unlocked",1);
    }
    public void UnlockDeliverLand()
    {
        PlayerPrefs.SetInt("DeliverLandUnlocked", 1);
    }
    public void UnlockNewLand()
    {
        PlayerPrefs.SetInt("NewLandUnlocked", 1);
    }
    public bool IsNewLandUnlocked()
    {
        if (PlayerPrefs.GetInt("NewLandUnlocked") == 1) { return true; }
        else{ return false; } 
    }
}
