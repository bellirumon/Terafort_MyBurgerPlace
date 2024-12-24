using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public GameObject playerMaxText;
    public GameObject playerMaxText2;
    public GameObject playerMaxText3;
    public GameObject playerMaxText4;
    public GameObject playerMaxBurger8FoodPos;
    public GameObject playerMaxBurger10FoodPos;
    public GameObject playerMaxFries8FoodPos;
    public GameObject playerMaxFries10FoodPos;
    public GameObject playerMaxCoke8FoodPos;
    public GameObject playerMaxCoke10FoodPos;
    public GameObject food1MaxText;
    public GameObject food2MaxText;
    public GameObject food3MaxText;
    [Space]
    public GameObject waiterUpgradePannel;
    public GameObject playerUpgradePannel;
    public GameObject dailyTaskPannel;

    public TextMeshProUGUI[] moneyTexts;

    public GameObject inputPannel;
    public TextMeshProUGUI inputFieldText;

    public GameObject incomePannel;
    public TextMeshProUGUI incomeText;
    public int offlineIncome;

    public Button hapticButton;
    public Sprite hapticOnImage;
    public Sprite hapticOffImage;

    private void Start() 
    {
        UpdateMoneyTexts();
        incomePannel.SetActive(true);
        if(Tutorial.instance.IsTutorialComplete()){offlineIncome = Random.Range(10, 500);}
        else
        {
            offlineIncome = 0;
        }
        incomeText.text = offlineIncome.ToString();
        AwakeTaskPannel();
        UpdatePlayerMaxText();
        UpdateButtonsUi();
    }

    public void UpdatePlayerMaxText()
    {
        if(PlayerPrefs.GetInt("PlayerCapacity") == 8)
        {
            playerMaxText.transform.position = playerMaxBurger8FoodPos.transform.position;
            playerMaxText2.transform.position = playerMaxFries8FoodPos.transform.position;
            playerMaxText3.transform.position = playerMaxCoke8FoodPos.transform.position;
        }
        if(PlayerPrefs.GetInt("PlayerCapacity") >= 10)
        {
            playerMaxText.transform.position = playerMaxBurger10FoodPos.transform.position;
            playerMaxText2.transform.position = playerMaxFries10FoodPos.transform.position;
            playerMaxText3.transform.position = playerMaxCoke10FoodPos.transform.position;
        }
    }

    public void AwakeTaskPannel()
    {
        dailyTaskPannel.transform.DOScale(Vector3.zero, 0).OnComplete(() => 
        {
            dailyTaskPannel.SetActive(true);
            CloseTaskPannel();
        });
    }
    public void CloseTaskPannel()
    {
        dailyTaskPannel.SetActive(false);
        dailyTaskPannel.transform.DOScale(Vector3.one, 0);
    }

    private void Update() 
    {
        playerMaxText.transform.LookAt(Camera.main.transform);
        playerMaxText2.transform.LookAt(Camera.main.transform);
        playerMaxText3.transform.LookAt(Camera.main.transform);
        playerMaxText4.transform.LookAt(Camera.main.transform);
    }

    public void UpdateMoneyTexts()
    {
        float money = Controller.instance.currencyController.GetCurrentAmount();
        for (int i = 0; i < moneyTexts.Length; i++)
        {
            if(money >= 1000)
            {
                float value = money / 1000;
                value = Mathf.Round(value * 100.0f) * 0.01f;
                moneyTexts[i].text = value.ToString() + "K";
            }
            else
            {
                moneyTexts[i].text = money.ToString();
            }
        }
    }

    public void UpdateButtonsUi()
    {
        if(PlayerPrefs.GetString("HapticOn") == "true" || !PlayerPrefs.HasKey("HapticOn"))
        {
            hapticButton.GetComponent<Image>().sprite = hapticOnImage;
            return;
        }
        if(PlayerPrefs.GetString("HapticOn") == "false")
        {
            hapticButton.GetComponent<Image>().sprite = hapticOffImage;
            return;
        }
    }
}
