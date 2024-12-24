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


        if (IsTable1Unlocked() && !IsNewLandUnlocked()) { newLandUnlockPoint.SetActive(true); }
        if (IsNewLandUnlocked() && !IsTable6Unlocked()) { table6UnlockPoint.SetActive(true); }
        if (IsTable6Unlocked() && !IsHRUnlocked()) { hrUnlockPoint.SetActive(true); }
        if (IsHRUnlocked() && !IsDeliverLandUnlocked()) { deliverLandUnlockPoint.SetActive(true); }
        if (IsDeliverLandUnlocked() && !IsFriesUnlocked()) { friesUnlockPoint.SetActive(true); }
        if (IsFriesUnlocked() && !IsTable2Unlocked()) { table2UnlockPoint.SetActive(true); }
        if (IsTable2Unlocked() && !IsUpgradeUnlocked()) { upgradeUnlockPoint.SetActive(true); }
        if (IsUpgradeUnlocked() && !IsSpecialTableUnlocked()) { specialTableUnlockPoint.SetActive(true); }
        if (IsSpecialTableUnlocked() && !IsTable7Unlocked()) { table7UnlockPoint.SetActive(true); }
        if (IsTable7Unlocked() && !IsCokeUnlocked()) { cokeUnlockPoint.SetActive(true); }
        if (IsCokeUnlocked() && !IsTable5Unlocked()) { table5UnlockPoint.SetActive(true); }
        if (IsTable5Unlocked() && !IsTable8Unlocked()) { table8UnlockPoint.SetActive(true); }
        if (IsTable8Unlocked() && !IsTable3Unlocked()) { table3UnlockPoint.SetActive(true); }
        if (IsTable3Unlocked() && !IsTable4Unlocked()) { table4UnlockPoint.SetActive(true); }

    }
    public bool IsBurgerUnlocked()
    {
        if (PlayerPrefs.GetString("BurgerUnlocked") == "true") { return true; }
        else{ return false; }
    }
    public void UnlockBurger()
    {
        PlayerPrefs.SetString("BurgerUnlocked", "true");
    }


    public bool IsFriesUnlocked()
    {
        if (PlayerPrefs.GetString("FriesUnlocked") == "true") {return true;}
        else { return false; }
    }
    public void UnlockFries()
    {
        PlayerPrefs.SetString("FriesUnlocked", "true");
    }


    public bool IsCokeUnlocked()
    {
        if (PlayerPrefs.GetString("CokeUnlocked") == "true") { return true; }
        else{ return false; }
    }
    public void UnlockCoke()
    {
        PlayerPrefs.SetString("CokeUnlocked", "true");
    }


    public bool IsSpecialTableUnlocked()
    {
        if (PlayerPrefs.GetString("SpecialTableUnlocked") == "true") { return true; }
        else{ return false; }
    }
    public void UnlockSpecialTable()
    {
        PlayerPrefs.SetString("SpecialTableUnlocked", "true");
    }


    public bool IsTable1Unlocked()
    {
        if (PlayerPrefs.GetString("Table1Unlocked") == "true") { return true; }
        else{ return false; }
    }
    public void UnlockTable1()
    {
        PlayerPrefs.SetString("Table1Unlocked", "true");
    }


    public bool IsTable2Unlocked()
    {
        if (PlayerPrefs.GetString("Table2Unlocked") == "true") { return true; }
        else{ return false; }
    }
    public void UnlockTable2()
    {
        PlayerPrefs.SetString("Table2Unlocked", "true");
    }


    public bool IsTable3Unlocked()
    {
        if (PlayerPrefs.GetString("Table3Unlocked") == "true") { return true; }
        else{ return false; }
    }
    public void UnlockTable3()
    {
        PlayerPrefs.SetString("Table3Unlocked", "true");
    }


    public bool IsTable4Unlocked()
    {
        if (PlayerPrefs.GetString("Table4Unlocked") == "true") { return true; }
        else{ return false; }
    }
    public void UnlockTable4()
    {
        PlayerPrefs.SetString("Table4Unlocked", "true");
    }

    public bool IsTable5Unlocked()
    {
        if (PlayerPrefs.GetString("Table5Unlocked") == "true") { return true; }
        else{ return false; }
    }
    public void UnlockTable5()
    {
        PlayerPrefs.SetString("Table5Unlocked", "true");
    }

    public bool IsTable6Unlocked()
    {
        if (PlayerPrefs.GetString("Table6Unlocked") == "true") { return true; }
        else{ return false; }
    }
    public void UnlockTable6()
    {
        PlayerPrefs.SetString("Table6Unlocked", "true");
    }

    public bool IsTable7Unlocked()
    {
        if (PlayerPrefs.GetString("Table7Unlocked") == "true") { return true; }
        else{ return false; }
    }
    public void UnlockTable7()
    {
        PlayerPrefs.SetString("Table7Unlocked", "true");
    }

    public bool IsTable8Unlocked()
    {
        if (PlayerPrefs.GetString("Table8Unlocked") == "true") { return true; }
        else { return false; }
    }
    public void UnlockTable8()
    {
        PlayerPrefs.SetString("Table8Unlocked", "true");
    }


    public bool IsHRUnlocked()
    {
        if (PlayerPrefs.GetString("HRUnlocked") == "true") { return true; }
        else{ return false; }
    }
    public void UnlockHR()
    {
        PlayerPrefs.SetString("HRUnlocked", "true");
    }


    public bool IsUpgradeUnlocked()
    {
        if (PlayerPrefs.GetString("UpgradeUnlocked") == "true") { return true; }
        else{ return false; }
    }
    public void UnlockUpgrade()
    {
        PlayerPrefs.SetString("UpgradeUnlocked", "true");
    }


    public bool IsDeliverLandUnlocked()
    {
        if (PlayerPrefs.GetString("DeliverLandUnlocked") == "true") { return true; }
        else{ return false; }
    }
    public bool IsPackTable2Unlocked()
    {
        if (PlayerPrefs.GetString("DeliverTable2Unlocked") == "true") { return true; }
        else{ return false; }
    }
    public void UnlockPackTable2()
    {
        PlayerPrefs.SetString("DeliverTable2Unlocked","true");
    }
    public void UnlockDeliverLand()
    {
        PlayerPrefs.SetString("DeliverLandUnlocked", "true");
    }
    public void UnlockNewLand()
    {
        PlayerPrefs.SetString("NewLandUnlocked", "true");
    }
    public bool IsNewLandUnlocked()
    {
        if (PlayerPrefs.GetString("NewLandUnlocked") == "true") { return true; }
        else{ return false; } 
    }
}
