using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyController : MonoBehaviour
{
    public GameObject moneyPrefab;
    public int perBundelMoney;

    private void Start() 
    {
        if (PlayerPrefs.HasKey("BundleCapacity"))
        {
            perBundelMoney = PlayerPrefs.GetInt("BundleCapacity");
        }
        else
        {
            perBundelMoney = 10;
        }
    }
    public void AddMoney(int amount)
    {
        int current = GetCurrentAmount();
        current = amount + current;
        PlayerPrefs.SetInt("Virtual_Currency", current);
        Controller.instance.uiController.UpdateMoneyTexts();
    }

    public int GetCurrentAmount()
    {
        return PlayerPrefs.GetInt("Virtual_Currency");
    }

    public void CutMoney(int amount)
    {
        int current = GetCurrentAmount();
        current = current - amount;
        PlayerPrefs.SetInt("Virtual_Currency", current);
        Controller.instance.uiController.UpdateMoneyTexts();
    }
}
