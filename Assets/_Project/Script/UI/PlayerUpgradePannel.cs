using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUpgradePannel : MonoBehaviour
{
    public static PlayerUpgradePannel instance;
    public Button speedPurchaseButton;
    public TextMeshProUGUI speedPriceText;
    public int[] speedPrices;
    public float[] speeds;
    public Image[] speedProgressBars;
    public Button capacityPurchaseButton;
    public TextMeshProUGUI capacityPriceText;
    public int[] capacityPrices;
    public int[] capacities;
    public Image[] capacityProgressBars;
    public Button pricePurchaseButton;
    public TextMeshProUGUI pricePriceText;
    public int[] pricePrices;
    public int[] prices;
    public Image[] priceProgressBars;


    private void Awake() 
    {
        instance = this;
    }

    private void Start() 
    {
        UpdateSpeedPriceTexts();
        UpdateCapacityPriceTexts();
        UpdateBundlePriceTexts();

    }

    public void UpgradeSpeed()
    {
        if(PlayerPrefs.GetFloat("PlayerSpeed") < speeds[0] || !PlayerPrefs.HasKey("PlayerSpeed"))
        {
            if(Controller.instance.currencyController.GetCurrentAmount() < speedPrices[0]) return;
            PlayerPrefs.SetFloat("PlayerSpeed", speeds[0]);
            Controller.instance.currencyController.CutMoney(speedPrices[0]);
            Controller.instance.gameController.PlayHaptic();

            PlayerMovement.instance.tempSpeed = speeds[0];

            UpdateSpeedPriceTexts();
            return;
        }
        if(PlayerPrefs.GetFloat("PlayerSpeed") >= speeds[0] && PlayerPrefs.GetFloat("PlayerSpeed") < speeds[1])
        {
            if(Controller.instance.currencyController.GetCurrentAmount() < speedPrices[1]) return;
            PlayerPrefs.SetFloat("PlayerSpeed", speeds[1]);
            Controller.instance.currencyController.CutMoney(speedPrices[1]);
            Controller.instance.gameController.PlayHaptic();

            PlayerMovement.instance.tempSpeed = speeds[1];

            UpdateSpeedPriceTexts();
            return;
        }
        if(PlayerPrefs.GetFloat("PlayerSpeed") >= speeds[1] && PlayerPrefs.GetFloat("PlayerSpeed") < speeds[2])
        {
            if(Controller.instance.currencyController.GetCurrentAmount() < speedPrices[2]) return;
            PlayerPrefs.SetFloat("PlayerSpeed", speeds[2]);
            Controller.instance.currencyController.CutMoney(speedPrices[2]);
            Controller.instance.gameController.PlayHaptic();

            PlayerMovement.instance.tempSpeed = speeds[2];

            UpdateSpeedPriceTexts();
            return;
        }
        if(PlayerPrefs.GetFloat("PlayerSpeed") >= speeds[2] && PlayerPrefs.GetFloat("PlayerSpeed") < speeds[3])
        {
            if(Controller.instance.currencyController.GetCurrentAmount() < speedPrices[3]) return;
            PlayerPrefs.SetFloat("PlayerSpeed", speeds[3]);
            Controller.instance.currencyController.CutMoney(speedPrices[3]);
            Controller.instance.gameController.PlayHaptic();

            PlayerMovement.instance.tempSpeed = speeds[3];

            UpdateSpeedPriceTexts();
        }
    }


     public void UpgradeCapacity()
    {
        if(PlayerPrefs.GetInt("PlayerCapacity") < capacities[0] || !PlayerPrefs.HasKey("PlayerCapacity"))
        {
            if(Controller.instance.currencyController.GetCurrentAmount() < capacityPrices[0]) return;
            PlayerPrefs.SetInt("PlayerCapacity", capacities[0]);
            Controller.instance.currencyController.CutMoney(capacityPrices[0]);
            Controller.instance.gameController.PlayHaptic();

            PLayerView.instance.maxFoodOnHand = capacities[0];

            UpdateCapacityPriceTexts();
            return;
        }
        if(PlayerPrefs.GetInt("PlayerCapacity") >= capacities[0] && PlayerPrefs.GetInt("PlayerCapacity") < capacities[1])
        {
            if(Controller.instance.currencyController.GetCurrentAmount() < capacityPrices[1]) return;
            PlayerPrefs.SetInt("PlayerCapacity", capacities[1]);
            Controller.instance.currencyController.CutMoney(capacityPrices[1]);
            Controller.instance.gameController.PlayHaptic();

            PLayerView.instance.maxFoodOnHand = capacities[1];

            UpdateCapacityPriceTexts();
            return;
        }
        if(PlayerPrefs.GetInt("PlayerCapacity") >= capacities[1] && PlayerPrefs.GetInt("PlayerCapacity") < capacities[2])
        {
            if(Controller.instance.currencyController.GetCurrentAmount() < capacityPrices[2]) return;
            PlayerPrefs.SetInt("PlayerCapacity", capacities[2]);
            Controller.instance.currencyController.CutMoney(capacityPrices[2]);
            Controller.instance.gameController.PlayHaptic();

            PLayerView.instance.maxFoodOnHand = capacities[2];

            UpdateCapacityPriceTexts();
            return;
        }
        if(PlayerPrefs.GetInt("PlayerCapacity") >= capacities[2] && PlayerPrefs.GetInt("PlayerCapacity") < capacities[3])
        {
            if(Controller.instance.currencyController.GetCurrentAmount() < capacityPrices[3]) return;
            PlayerPrefs.SetInt("PlayerCapacity", capacities[3]);
            Controller.instance.currencyController.CutMoney(capacityPrices[3]);
            Controller.instance.gameController.PlayHaptic();

            PLayerView.instance.maxFoodOnHand = capacities[3];

            UpdateCapacityPriceTexts();
        }
    }
    public void UpgradeBundleCapacity()
    {
        if(PlayerPrefs.GetInt("BundleCapacity") < prices[0] || !PlayerPrefs.HasKey("BundleCapacity"))
        {
            if(Controller.instance.currencyController.GetCurrentAmount() < pricePrices[0]) return;
            PlayerPrefs.SetInt("BundleCapacity", prices[0]);
            Controller.instance.currencyController.CutMoney(pricePrices[0]);
            Controller.instance.gameController.PlayHaptic();

            Controller.instance.currencyController.perBundelMoney = prices[0];

            UpdateBundlePriceTexts();
            return;
        }
        if(PlayerPrefs.GetInt("BundleCapacity") >= prices[0] && PlayerPrefs.GetInt("BundleCapacity") < prices[1])
        {
            if(Controller.instance.currencyController.GetCurrentAmount() < pricePrices[1]) return;
            PlayerPrefs.SetInt("BundleCapacity", prices[1]);
            Controller.instance.currencyController.CutMoney(pricePrices[1]);
            Controller.instance.gameController.PlayHaptic();

            Controller.instance.currencyController.perBundelMoney = prices[1];

            UpdateBundlePriceTexts();
            return;
        }
        if(PlayerPrefs.GetInt("BundleCapacity") >= prices[1] && PlayerPrefs.GetInt("BundleCapacity") < prices[2])
        {
            if(Controller.instance.currencyController.GetCurrentAmount() < pricePrices[2]) return;
            PlayerPrefs.SetInt("BundleCapacity", prices[2]);
            Controller.instance.currencyController.CutMoney(pricePrices[2]);
            Controller.instance.gameController.PlayHaptic();

            Controller.instance.currencyController.perBundelMoney = prices[2];

            UpdateBundlePriceTexts();
            return;
        }
        if(PlayerPrefs.GetInt("BundleCapacity") >= prices[2] && PlayerPrefs.GetInt("BundleCapacity") < prices[3])
        {
            if(Controller.instance.currencyController.GetCurrentAmount() < pricePrices[3]) return;
            PlayerPrefs.SetInt("BundleCapacity", prices[3]);
            Controller.instance.currencyController.CutMoney(pricePrices[3]);
            Controller.instance.gameController.PlayHaptic();

            Controller.instance.currencyController.perBundelMoney = prices[3];

            UpdateBundlePriceTexts();
        }
    }


    public void UpdateSpeedPriceTexts()
    {
        if(PlayerPrefs.GetFloat("PlayerSpeed") < speeds[0] || !PlayerPrefs.HasKey("PlayerSpeed"))
        {
            speedPriceText.text = speedPrices[0].ToString();
            return;
        }
        if(PlayerPrefs.GetFloat("PlayerSpeed") >= speeds[0] && PlayerPrefs.GetFloat("PlayerSpeed") < speeds[1])
        {
            speedPriceText.text = speedPrices[1].ToString();
            speedProgressBars[0].color = Color.red;
            return;
        }
        if(PlayerPrefs.GetFloat("PlayerSpeed") >= speeds[1] && PlayerPrefs.GetFloat("PlayerSpeed") < speeds[2])
        {
            speedPriceText.text = speedPrices[2].ToString();
            speedProgressBars[0].color = Color.red;
            speedProgressBars[1].color = Color.red;
            return;
        }
        if(PlayerPrefs.GetFloat("PlayerSpeed") >= speeds[2] && PlayerPrefs.GetFloat("PlayerSpeed") < speeds[3])
        {
            speedPriceText.text = speedPrices[3].ToString();
            speedProgressBars[0].color = Color.red;
            speedProgressBars[1].color = Color.red;
            speedProgressBars[2].color = Color.red;
            return;
        }
        if(PlayerPrefs.GetFloat("PlayerSpeed") == speeds[3])
        {
            speedPriceText.text = "Max";
            speedProgressBars[0].color = Color.red;
            speedProgressBars[1].color = Color.red;
            speedProgressBars[2].color = Color.red;
            speedProgressBars[3].color = Color.red;
            speedPurchaseButton.interactable = false;
        }
    }

    public void UpdateCapacityPriceTexts()
    {
        Controller.instance.uiController.UpdatePlayerMaxText();
        if(PlayerPrefs.GetInt("PlayerCapacity") < capacities[0] || !PlayerPrefs.HasKey("PlayerCapacity"))
        {
            capacityPriceText.text = capacityPrices[0].ToString();
            return;
        }
        if(PlayerPrefs.GetInt("PlayerCapacity") >= capacities[0] && PlayerPrefs.GetInt("PlayerCapacity") < capacities[1])
        {
            capacityPriceText.text = capacityPrices[1].ToString();
            capacityProgressBars[0].color = Color.red;
            return;
        }
        if(PlayerPrefs.GetInt("PlayerCapacity") >= capacities[1] && PlayerPrefs.GetInt("PlayerCapacity") < capacities[2])
        {
            capacityPriceText.text = capacityPrices[2].ToString();
            capacityProgressBars[0].color = Color.red;
            capacityProgressBars[1].color = Color.red;
            return;
        }
        if(PlayerPrefs.GetInt("PlayerCapacity") >= capacities[2] && PlayerPrefs.GetInt("PlayerCapacity") < capacities[3])
        {
            capacityPriceText.text = capacityPrices[3].ToString();
            capacityProgressBars[0].color = Color.red;
            capacityProgressBars[1].color = Color.red;
            capacityProgressBars[2].color = Color.red;
            return;
        }
        if(PlayerPrefs.GetInt("PlayerCapacity") == capacities[3])
        {
            capacityPriceText.text = "Max";
            capacityProgressBars[0].color = Color.red;
            capacityProgressBars[1].color = Color.red;
            capacityProgressBars[2].color = Color.red;
            capacityProgressBars[3].color = Color.red;
            capacityPurchaseButton.interactable = false;
        }
    }
    public void UpdateBundlePriceTexts()
    {
        if(PlayerPrefs.GetInt("BundleCapacity") < prices[0] || !PlayerPrefs.HasKey("BundleCapacity"))
        {
            pricePriceText.text = pricePrices[0].ToString();
            return;
        }
        if(PlayerPrefs.GetInt("BundleCapacity") >= prices[0] && PlayerPrefs.GetInt("BundleCapacity") < prices[1])
        {
            pricePriceText.text = pricePrices[1].ToString();
            priceProgressBars[0].color = Color.red;
            return;
        }
        if(PlayerPrefs.GetInt("BundleCapacity") >= prices[1] && PlayerPrefs.GetInt("BundleCapacity") < prices[2])
        {
            pricePriceText.text = pricePrices[2].ToString();
            priceProgressBars[0].color = Color.red;
            priceProgressBars[1].color = Color.red;
            return;
        }
        if(PlayerPrefs.GetInt("BundleCapacity") >= prices[2] && PlayerPrefs.GetInt("BundleCapacity") < prices[3])
        {
            pricePriceText.text = pricePrices[3].ToString();
            priceProgressBars[0].color = Color.red;
            priceProgressBars[1].color = Color.red;
            priceProgressBars[2].color = Color.red;
            return;
        }
        if(PlayerPrefs.GetInt("BundleCapacity") == prices[3])
        {
            pricePriceText.text = "Max";
            priceProgressBars[0].color = Color.red;
            priceProgressBars[1].color = Color.red;
            priceProgressBars[2].color = Color.red;
            priceProgressBars[3].color = Color.red;
            pricePurchaseButton.interactable = false;
        }
    }
}
