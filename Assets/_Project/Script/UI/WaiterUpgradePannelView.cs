using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaiterUpgradePannelView : MonoBehaviour
{
    public static WaiterUpgradePannelView instance;
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
    public Button countPurchaseButton;
    public TextMeshProUGUI countPriceText;
    public int[] countPrices;
    public Image[] countProgressBars;


    private void Awake() 
    {
        instance = this;
    }
    private void Start() 
    {
        UpdateSpeedPriceTexts();
        UpdateCapacityPriceTexts();
        UpdateCountPriceText();
    }

    public void UpgradeSpeed()
    {
        if(PlayerPrefs.GetFloat("WaiterSpeed") < speeds[0] || !PlayerPrefs.HasKey("WaiterSpeed"))
        {
            if(Controller.instance.currencyController.GetCurrentAmount() < speedPrices[0]) return;
            PlayerPrefs.SetFloat("WaiterSpeed", speeds[0]);
            Controller.instance.currencyController.CutMoney(speedPrices[0]);
            Controller.instance.gameController.PlayHaptic();

            Controller.instance.waiterController.waiter1.GetComponent<WaiterView>().agent.speed = speeds[0];
            Controller.instance.waiterController.waiter2.GetComponent<WaiterView>().agent.speed = speeds[0];
            Controller.instance.waiterController.waiter3.GetComponent<WaiterView>().agent.speed = speeds[0];
            Controller.instance.waiterController.waiter4.GetComponent<WaiterView>().agent.speed = speeds[0];

            UpdateSpeedPriceTexts();
            return;
        }
        if(PlayerPrefs.GetFloat("WaiterSpeed") >= speeds[0] && PlayerPrefs.GetFloat("WaiterSpeed") < speeds[1])
        {
            if(Controller.instance.currencyController.GetCurrentAmount() < speedPrices[1]) return;
            PlayerPrefs.SetFloat("WaiterSpeed", speeds[1]);
            Controller.instance.currencyController.CutMoney(speedPrices[1]);
            Controller.instance.gameController.PlayHaptic();

            Controller.instance.waiterController.waiter1.GetComponent<WaiterView>().agent.speed = speeds[1];
            Controller.instance.waiterController.waiter2.GetComponent<WaiterView>().agent.speed = speeds[1];
            Controller.instance.waiterController.waiter3.GetComponent<WaiterView>().agent.speed = speeds[1];
            Controller.instance.waiterController.waiter4.GetComponent<WaiterView>().agent.speed = speeds[1];

            UpdateSpeedPriceTexts();
            return;
        }
        if(PlayerPrefs.GetFloat("WaiterSpeed") >= speeds[1] && PlayerPrefs.GetFloat("WaiterSpeed") < speeds[2])
        {
            if(Controller.instance.currencyController.GetCurrentAmount() < speedPrices[2]) return;
            PlayerPrefs.SetFloat("WaiterSpeed", speeds[2]);
            Controller.instance.currencyController.CutMoney(speedPrices[2]);
            Controller.instance.gameController.PlayHaptic();

            Controller.instance.waiterController.waiter1.GetComponent<WaiterView>().agent.speed = speeds[2];
            Controller.instance.waiterController.waiter2.GetComponent<WaiterView>().agent.speed = speeds[2];
            Controller.instance.waiterController.waiter3.GetComponent<WaiterView>().agent.speed = speeds[2];
            Controller.instance.waiterController.waiter4.GetComponent<WaiterView>().agent.speed = speeds[2];

            UpdateSpeedPriceTexts();
            return;
        }
        if(PlayerPrefs.GetFloat("WaiterSpeed") >= speeds[2] && PlayerPrefs.GetFloat("WaiterSpeed") < speeds[3])
        {
            if(Controller.instance.currencyController.GetCurrentAmount() < speedPrices[3]) return;
            PlayerPrefs.SetFloat("WaiterSpeed", speeds[3]);
            Controller.instance.currencyController.CutMoney(speedPrices[3]);
            Controller.instance.gameController.PlayHaptic();

            Controller.instance.waiterController.waiter1.GetComponent<WaiterView>().agent.speed = speeds[3];
            Controller.instance.waiterController.waiter2.GetComponent<WaiterView>().agent.speed = speeds[3];
            Controller.instance.waiterController.waiter3.GetComponent<WaiterView>().agent.speed = speeds[3];
            Controller.instance.waiterController.waiter4.GetComponent<WaiterView>().agent.speed = speeds[3];

            UpdateSpeedPriceTexts();
        }
    }
    public void UpgradeCapacity()
    {
        if(PlayerPrefs.GetInt("WaiterCapacity") < capacities[0] || !PlayerPrefs.HasKey("WaiterCapacity"))
        {
            if(Controller.instance.currencyController.GetCurrentAmount() < capacityPrices[0]) return;
            PlayerPrefs.SetInt("WaiterCapacity", capacities[0]);
            Controller.instance.currencyController.CutMoney(capacityPrices[0]);
            Controller.instance.gameController.PlayHaptic();

            Controller.instance.waiterController.waiter1.GetComponent<WaiterView>().maxFoodOnHandCount = capacities[0];
            Controller.instance.waiterController.waiter2.GetComponent<WaiterView>().maxFoodOnHandCount = capacities[0];
            Controller.instance.waiterController.waiter3.GetComponent<WaiterView>().maxFoodOnHandCount = capacities[0];
            Controller.instance.waiterController.waiter4.GetComponent<WaiterView>().maxFoodOnHandCount = capacities[0];

            UpdateCapacityPriceTexts();
            return;
        }
        if(PlayerPrefs.GetInt("WaiterCapacity") >= capacities[0] && PlayerPrefs.GetInt("WaiterCapacity") < capacities[1])
        {
            if(Controller.instance.currencyController.GetCurrentAmount() < capacityPrices[1]) return;
            PlayerPrefs.SetInt("WaiterCapacity", capacities[1]);
            Controller.instance.currencyController.CutMoney(capacityPrices[1]);
            Controller.instance.gameController.PlayHaptic();

            Controller.instance.waiterController.waiter1.GetComponent<WaiterView>().maxFoodOnHandCount = capacities[1];
            Controller.instance.waiterController.waiter2.GetComponent<WaiterView>().maxFoodOnHandCount = capacities[1];
            Controller.instance.waiterController.waiter3.GetComponent<WaiterView>().maxFoodOnHandCount = capacities[1];
            Controller.instance.waiterController.waiter4.GetComponent<WaiterView>().maxFoodOnHandCount = capacities[1];

            UpdateCapacityPriceTexts();
            return;
        }
        if(PlayerPrefs.GetInt("WaiterCapacity") >= capacities[1] && PlayerPrefs.GetInt("WaiterCapacity") < capacities[2])
        {
            if(Controller.instance.currencyController.GetCurrentAmount() < capacityPrices[2]) return;
            PlayerPrefs.SetInt("WaiterCapacity", capacities[2]);
            Controller.instance.currencyController.CutMoney(capacityPrices[2]);
            Controller.instance.gameController.PlayHaptic();

            Controller.instance.waiterController.waiter1.GetComponent<WaiterView>().maxFoodOnHandCount = capacities[2];
            Controller.instance.waiterController.waiter2.GetComponent<WaiterView>().maxFoodOnHandCount = capacities[2];
            Controller.instance.waiterController.waiter3.GetComponent<WaiterView>().maxFoodOnHandCount = capacities[2];
            Controller.instance.waiterController.waiter4.GetComponent<WaiterView>().maxFoodOnHandCount = capacities[2];

            UpdateCapacityPriceTexts();
            return;
        }
        if(PlayerPrefs.GetInt("WaiterCapacity") >= capacities[2] && PlayerPrefs.GetInt("WaiterCapacity") < capacities[3])
        {
            if(Controller.instance.currencyController.GetCurrentAmount() < capacityPrices[3]) return;
            PlayerPrefs.SetInt("WaiterCapacity", capacities[3]);
            Controller.instance.currencyController.CutMoney(capacityPrices[3]);
            Controller.instance.gameController.PlayHaptic();

            Controller.instance.waiterController.waiter1.GetComponent<WaiterView>().maxFoodOnHandCount = capacities[3];
            Controller.instance.waiterController.waiter2.GetComponent<WaiterView>().maxFoodOnHandCount = capacities[3];
            Controller.instance.waiterController.waiter3.GetComponent<WaiterView>().maxFoodOnHandCount = capacities[3];
            Controller.instance.waiterController.waiter4.GetComponent<WaiterView>().maxFoodOnHandCount = capacities[3];

            UpdateCapacityPriceTexts();
        }
    }

    public void UnlockWaiter()
    {
        if (PlayerPrefs.GetInt("WaiterCount") == 0 || !PlayerPrefs.HasKey("WaiterCount"))
        { 
            if(Controller.instance.currencyController.GetCurrentAmount() < countPrices[0]) return;
            PlayerPrefs.SetInt("WaiterCount", 1);
            Controller.instance.currencyController.CutMoney(countPrices[0]);
            Controller.instance.gameController.PlayHaptic();
            Controller.instance.waiterController.waiter2.SetActive(true);
            UpdateCountPriceText();
            return;
        }
        if (PlayerPrefs.GetInt("WaiterCount") == 1)
        { 
            if(Controller.instance.currencyController.GetCurrentAmount() < countPrices[1]) return;
            PlayerPrefs.SetInt("WaiterCount", 2);
            Controller.instance.currencyController.CutMoney(countPrices[1]);
            Controller.instance.gameController.PlayHaptic();
            Controller.instance.waiterController.waiter3.SetActive(true);
            UpdateCountPriceText();
            return;
        }
        if (PlayerPrefs.GetInt("WaiterCount") == 2)
        { 
            if(Controller.instance.currencyController.GetCurrentAmount() < countPrices[2]) return;
            PlayerPrefs.SetInt("WaiterCount", 3);
            Controller.instance.currencyController.CutMoney(countPrices[2]);
            Controller.instance.gameController.PlayHaptic();
            Controller.instance.waiterController.waiter4.SetActive(true);
            UpdateCountPriceText();
        }
    }

    public void UpdateSpeedPriceTexts()
    {
        if(PlayerPrefs.GetFloat("WaiterSpeed") < speeds[0] || !PlayerPrefs.HasKey("WaiterSpeed"))
        {
            speedPriceText.text = speedPrices[0].ToString();
            return;
        }
        if(PlayerPrefs.GetFloat("WaiterSpeed") >= speeds[0] && PlayerPrefs.GetFloat("WaiterSpeed") < speeds[1])
        {
            speedPriceText.text = speedPrices[1].ToString();
            speedProgressBars[0].color = Color.red;
            return;
        }
        if(PlayerPrefs.GetFloat("WaiterSpeed") >= speeds[1] && PlayerPrefs.GetFloat("WaiterSpeed") < speeds[2])
        {
            speedPriceText.text = speedPrices[2].ToString();
            speedProgressBars[0].color = Color.red;
            speedProgressBars[1].color = Color.red;
            return;
        }
        if(PlayerPrefs.GetFloat("WaiterSpeed") >= speeds[2] && PlayerPrefs.GetFloat("WaiterSpeed") < speeds[3])
        {
            speedPriceText.text = speedPrices[3].ToString();
            speedProgressBars[0].color = Color.red;
            speedProgressBars[1].color = Color.red;
            speedProgressBars[2].color = Color.red;
            return;
        }
        if(PlayerPrefs.GetFloat("WaiterSpeed") == speeds[3])
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
        if(PlayerPrefs.GetInt("WaiterCapacity") < capacities[0] || !PlayerPrefs.HasKey("WaiterCapacity"))
        {
            capacityPriceText.text = capacityPrices[0].ToString();
            return;
        }
        if(PlayerPrefs.GetInt("WaiterCapacity") >= capacities[0] && PlayerPrefs.GetInt("WaiterCapacity") < capacities[1])
        {
            capacityPriceText.text = capacityPrices[1].ToString();
            capacityProgressBars[0].color = Color.red;
            return;
        }
        if(PlayerPrefs.GetInt("WaiterCapacity") >= capacities[1] && PlayerPrefs.GetInt("WaiterCapacity") < capacities[2])
        {
            capacityPriceText.text = capacityPrices[2].ToString();
            capacityProgressBars[0].color = Color.red;
            capacityProgressBars[1].color = Color.red;
            return;
        }
        if(PlayerPrefs.GetInt("WaiterCapacity") >= capacities[2] && PlayerPrefs.GetInt("WaiterCapacity") < capacities[3])
        {
            capacityPriceText.text = capacityPrices[3].ToString();
            capacityProgressBars[0].color = Color.red;
            capacityProgressBars[1].color = Color.red;
            capacityProgressBars[2].color = Color.red;
            return;
        }
        if(PlayerPrefs.GetInt("WaiterCapacity") == capacities[3])
        {
            capacityPriceText.text = "Max";
            capacityProgressBars[0].color = Color.red;
            capacityProgressBars[1].color = Color.red;
            capacityProgressBars[2].color = Color.red;
            capacityProgressBars[3].color = Color.red;
            capacityPurchaseButton.interactable = false;
        }
    }

    public void UpdateCountPriceText()
    {
        if (PlayerPrefs.GetInt("WaiterCount") == 0 || !PlayerPrefs.HasKey("WaiterCount"))
        {
            countPriceText.text = countPrices[0].ToString();
            return;
        }
        if (PlayerPrefs.GetInt("WaiterCount") == 1)
        { 
            countPriceText.text = countPrices[1].ToString();
            countProgressBars[0].color = Color.red;
            return;
        }
        if (PlayerPrefs.GetInt("WaiterCount") == 2)
        { 
            countPriceText.text = countPrices[2].ToString();
            countProgressBars[0].color = Color.red;
            countProgressBars[1].color = Color.red;
            return;
        }
        if (PlayerPrefs.GetInt("WaiterCount") == 3)
        { 
            countPriceText.text = "Max";
            countProgressBars[0].color = Color.red;
            countProgressBars[1].color = Color.red;
            countProgressBars[2].color = Color.red;
            countPurchaseButton.interactable = false;
        }
    }
}
