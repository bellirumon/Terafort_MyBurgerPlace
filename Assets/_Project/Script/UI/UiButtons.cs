using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiButtons : MonoBehaviour
{
    private void Start() 
    {
        if(PlayerPrefs.HasKey("OwnerName")){Controller.instance.uiController.inputPannel.SetActive(false);}
        if(!PlayerPrefs.HasKey("OwnerName")){Controller.instance.uiController.inputPannel.SetActive(true);}
    }

    public void HapticButton()
    {
        if(PlayerPrefs.GetString("HapticOn") == "true" || !PlayerPrefs.HasKey("HapticOn"))
        {
            PlayerPrefs.SetString("HapticOn", "false");
            Controller.instance.uiController.hapticButton.GetComponent<Image>().sprite = Controller.instance.uiController.hapticOffImage;
            return;
        }
        if(PlayerPrefs.GetString("HapticOn") == "false")
        {
            PlayerPrefs.SetString("HapticOn", "true");
            Controller.instance.uiController.hapticButton.GetComponent<Image>().sprite = Controller.instance.uiController.hapticOnImage;
            return;
        }
    }
    public void InputFieldOkButton()
    {
        PlayerPrefs.SetString("OwnerName", Controller.instance.uiController.inputFieldText.text);
        Controller.instance.uiController.inputPannel.SetActive(false);
    }

    public void IncomePannelOkButton()
    {
        Controller.instance.currencyController.AddMoney(Controller.instance.uiController.offlineIncome);
        Controller.instance.uiController.incomePannel.SetActive(false);
        AnalyticsService.LevelStart(0, "Level0", 0);
    }
}
