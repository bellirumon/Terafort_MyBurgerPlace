using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DailyTaskView : MonoBehaviour
{
    public static DailyTaskView instance;
    public GameObject firstWave;
    public GameObject SecondWave;
    public GameObject ThirdWave;
    public GameObject FourthdWave;
    [Header("Wave1")]
    public TextMeshProUGUI wave1task1ProgressText;
    public TextMeshProUGUI wave1task2ProgressText;
    public TextMeshProUGUI wave1task3ProgressText;
    public TextMeshProUGUI wave1task4ProgressText;
    [Space]
    public GameObject wave1task1ProgressPannel;
    public GameObject wave1task2ProgressPannel;
    public GameObject wave1task3ProgressPannel;
    public GameObject wave1task4ProgressPannel;
    [Space]
    public GameObject wave1task1ClaimButton;
    public GameObject wave1task2ClaimButton;
    public GameObject wave1task3ClaimButton;
    public GameObject wave1task4ClaimButton;
    [Header("Wave2")]
    public TextMeshProUGUI wave2task1ProgressText;
    public TextMeshProUGUI wave2task2ProgressText;
    public TextMeshProUGUI wave2task3ProgressText;
    public TextMeshProUGUI wave2task4ProgressText;
    [Space]
    public GameObject wave2task1ProgressPannel;
    public GameObject wave2task2ProgressPannel;
    public GameObject wave2task3ProgressPannel;
    public GameObject wave2task4ProgressPannel;
    [Space]
    public GameObject wave2task1ClaimButton;
    public GameObject wave2task2ClaimButton;
    public GameObject wave2task3ClaimButton;
    public GameObject wave2task4ClaimButton;
    [Header("Wave3")]
    public TextMeshProUGUI wave3task1ProgressText;
    public TextMeshProUGUI wave3task2ProgressText;
    public TextMeshProUGUI wave3task3ProgressText;
    public TextMeshProUGUI wave3task4ProgressText;
    [Space]
    public GameObject wave3task1ProgressPannel;
    public GameObject wave3task2ProgressPannel;
    public GameObject wave3task3ProgressPannel;
    public GameObject wave3task4ProgressPannel;
    [Space]
    public GameObject wave3task1ClaimButton;
    public GameObject wave3task2ClaimButton;
    public GameObject wave3task3ClaimButton;
    public GameObject wave3task4ClaimButton;
    [Header("Wave4")]
    public TextMeshProUGUI wave4task1ProgressText;
    public TextMeshProUGUI wave4task2ProgressText;
    public TextMeshProUGUI wave4task3ProgressText;
    public TextMeshProUGUI wave4task4ProgressText;
    [Space]
    public GameObject wave4task1ProgressPannel;
    public GameObject wave4task2ProgressPannel;
    public GameObject wave4task3ProgressPannel;
    public GameObject wave4task4ProgressPannel;
    [Space]
    public GameObject wave4task1ClaimButton;
    public GameObject wave4task2ClaimButton;
    public GameObject wave4task3ClaimButton;
    public GameObject wave4task4ClaimButton;

    private void Awake() 
    {
        instance = this;
    }
    private void Start() 
    {
        UpdateWave();
    }

    private void OnEnable() 
    {
        UpdateWave();
    }

    public void UpdateWave()
    {
        if(!PlayerPrefs.HasKey("WaveRunning") || PlayerPrefs.GetInt("WaveRunning") == 1)
        {
            firstWave.SetActive(true);
            SecondWave.SetActive(false);
            ThirdWave.SetActive(false);
            FourthdWave.SetActive(false);
            UpdateWave1Ui();
        }
        if(PlayerPrefs.GetInt("WaveRunning") == 2)
        {
            firstWave.SetActive(false);
            SecondWave.SetActive(true);
            ThirdWave.SetActive(false);
            FourthdWave.SetActive(false);
            UpdateWave2Ui();
        }
        if(PlayerPrefs.GetInt("WaveRunning") == 3)
        {
            firstWave.SetActive(false);
            SecondWave.SetActive(false);
            ThirdWave.SetActive(true);
            FourthdWave.SetActive(false);
            UpdateWave3Ui();
        }
        if(PlayerPrefs.GetInt("WaveRunning") == 4)
        {
            firstWave.SetActive(false);
            SecondWave.SetActive(false);
            ThirdWave.SetActive(false);
            FourthdWave.SetActive(true);
            UpdateWave4Ui();
        }
    }

    public void SetWaveValues()
    {
        if(!PlayerPrefs.HasKey("WaveRunning") || PlayerPrefs.GetInt("WaveRunning") == 1)
        {
            PlayerPrefs.SetInt("WaveRunning",2);
            UpdateWave();
            return;
        }
        if(PlayerPrefs.GetInt("WaveRunning") == 2)
        {
            PlayerPrefs.SetInt("WaveRunning",3);
            UpdateWave();
            return;
        }
        if(PlayerPrefs.GetInt("WaveRunning") == 3)
        {
            PlayerPrefs.SetInt("WaveRunning",4);
            UpdateWave();
            return;
        }
        if(PlayerPrefs.GetInt("WaveRunning") == 4)
        {
            PlayerPrefs.SetInt("WaveRunning",1);
            UpdateWave();
            return;
        }
    }

    public void UpdateWave1Ui()
    {
        //firstWave task....
        
        wave1task1ProgressText.text = PlayerPrefs.GetInt("CustomerServed").ToString();
        if(PlayerPrefs.GetInt("CustomerServed") > 4)
        {
            wave1task1ProgressPannel.SetActive(false);
            wave1task1ClaimButton.SetActive(true);
        }
        if(PlayerPrefs.GetString("Wave1Task1Claimed") == "true")
        {
            wave1task1ClaimButton.GetComponent<Button>().interactable = false;
            wave1task1ClaimButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "CLAIMED";
        }
        //Second task....

        wave1task2ProgressText.text = PlayerPrefs.GetInt("CustomerServed").ToString();
        if(PlayerPrefs.GetInt("CustomerServed") > 9)
        {
            wave1task2ProgressPannel.SetActive(false);
            wave1task2ClaimButton.SetActive(true);
        }
        if(PlayerPrefs.GetString("Wave1Task2Claimed") == "true")
        {
            wave1task2ClaimButton.GetComponent<Button>().interactable = false;
            wave1task2ClaimButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "CLAIMED";
        }
        //Third task....

        wave1task3ProgressText.text = PlayerPrefs.GetInt("BurgerServed").ToString();
        if(PlayerPrefs.GetInt("BurgerServed") > 49)
        {
            wave1task3ProgressPannel.SetActive(false);
            wave1task3ClaimButton.SetActive(true);
        }
        if(PlayerPrefs.GetString("Wave1Task3Claimed") == "true")
        {
            wave1task3ClaimButton.GetComponent<Button>().interactable = false;
            wave1task3ClaimButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "CLAIMED";
        }
         //Fourth task....

        wave1task4ProgressText.text = PlayerPrefs.GetInt("CustomerServed").ToString();
        if(PlayerPrefs.GetInt("CustomerServed") > 19)
        {
            wave1task4ProgressPannel.SetActive(false);
            wave1task4ClaimButton.SetActive(true);
        }
        if(PlayerPrefs.GetString("Wave1Task4Claimed") == "true")
        {
            wave1task4ClaimButton.GetComponent<Button>().interactable = false;
            wave1task4ClaimButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "CLAIMED";
        }
    }

    public void UpdateWave2Ui()
    {
        //firstWave task....
        
        wave2task1ProgressText.text = PlayerPrefs.GetInt("ColaServed").ToString();
        if(PlayerPrefs.GetInt("ColaServed") > 19)
        {
            wave2task1ProgressPannel.SetActive(false);
            wave2task1ClaimButton.SetActive(true);
        }
        if(PlayerPrefs.GetString("Wave2Task1Claimed") == "true")
        {
            wave2task1ClaimButton.GetComponent<Button>().interactable = false;
            wave2task1ClaimButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "CLAIMED";
        }
        //Second task....

        wave2task2ProgressText.text = PlayerPrefs.GetInt("FrieServed").ToString();
        if(PlayerPrefs.GetInt("FrieServed") > 50)
        {
            wave2task2ProgressPannel.SetActive(false);
            wave2task2ClaimButton.SetActive(true);
        }
        if(PlayerPrefs.GetString("Wave2Task2Claimed") == "true")
        {
            wave2task2ClaimButton.GetComponent<Button>().interactable = false;
            wave2task2ClaimButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "CLAIMED";
        }
        //Third task....

        wave2task3ProgressText.text = PlayerPrefs.GetInt("FrieServed").ToString();
        if(PlayerPrefs.GetInt("FrieServed") > 99)
        {
            wave2task3ProgressPannel.SetActive(false);
            wave2task3ClaimButton.SetActive(true);
        }
        if(PlayerPrefs.GetString("Wave2Task3Claimed") == "true")
        {
            wave2task3ClaimButton.GetComponent<Button>().interactable = false;
            wave2task3ClaimButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "CLAIMED";
        }
         //Fourth task....

        wave2task4ProgressText.text = PlayerPrefs.GetInt("BurgerServed").ToString();
        if(PlayerPrefs.GetInt("BurgerServed") > 149)
        {
            wave2task4ProgressPannel.SetActive(false);
            wave2task4ClaimButton.SetActive(true);
        }
        if(PlayerPrefs.GetString("Wave2Task4Claimed") == "true")
        {
            wave2task4ClaimButton.GetComponent<Button>().interactable = false;
            wave2task4ClaimButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "CLAIMED";
        }
    }
    public void UpdateWave3Ui()
    {
        //firstWave task....
        
        wave3task1ProgressText.text = PlayerPrefs.GetInt("ColaServed").ToString();
        if(PlayerPrefs.GetInt("ColaServed") > 49)
        {
            wave3task1ProgressPannel.SetActive(false);
            wave3task1ClaimButton.SetActive(true);
        }
        if(PlayerPrefs.GetString("Wave3Task1Claimed") == "true")
        {
            wave3task1ClaimButton.GetComponent<Button>().interactable = false;
            wave3task1ClaimButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "CLAIMED";
        }
        //Second task....

        wave3task2ProgressText.text = PlayerPrefs.GetInt("FrieServed").ToString();
        if(PlayerPrefs.GetInt("FrieServed") > 149)
        {
            wave3task2ProgressPannel.SetActive(false);
            wave3task2ClaimButton.SetActive(true);
        }
        if(PlayerPrefs.GetString("Wave3Task2Claimed") == "true")
        {
            wave3task2ClaimButton.GetComponent<Button>().interactable = false;
            wave3task2ClaimButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "CLAIMED";
        }
        //Third task....

        wave3task3ProgressText.text = PlayerPrefs.GetInt("BurgerServed").ToString();
        if(PlayerPrefs.GetInt("BurgerServed") > 199)
        {
            wave3task3ProgressPannel.SetActive(false);
            wave3task3ClaimButton.SetActive(true);
        }
        if(PlayerPrefs.GetString("Wave3Task3Claimed") == "true")
        {
            wave3task3ClaimButton.GetComponent<Button>().interactable = false;
            wave3task3ClaimButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "CLAIMED";
        }
         //Fourth task....

        wave3task4ProgressText.text = PlayerPrefs.GetInt("CustomerServed").ToString();
        if(PlayerPrefs.GetInt("CustomerServed") > 49)
        {
            wave3task4ProgressPannel.SetActive(false);
            wave3task4ClaimButton.SetActive(true);
        }
        if(PlayerPrefs.GetString("Wave3Task4Claimed") == "true")
        {
            wave3task4ClaimButton.GetComponent<Button>().interactable = false;
            wave3task4ClaimButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "CLAIMED";
        }
    }
    public void UpdateWave4Ui()
    {
        //firstWave task....
        
        wave4task1ProgressText.text = PlayerPrefs.GetInt("BurgerServed").ToString();
        if(PlayerPrefs.GetInt("BurgerServed") > 59)
        {
            wave4task1ProgressPannel.SetActive(false);
            wave4task1ClaimButton.SetActive(true);
        }
        if(PlayerPrefs.GetString("Wave4Task1Claimed") == "true")
        {
            wave4task1ClaimButton.GetComponent<Button>().interactable = false;
            wave4task1ClaimButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "CLAIMED";
        }
        //Second task....

        wave4task2ProgressText.text = PlayerPrefs.GetInt("FrieServed").ToString();
        if(PlayerPrefs.GetInt("FrieServed") > 199)
        {
            wave4task2ProgressPannel.SetActive(false);
            wave4task2ClaimButton.SetActive(true);
        }
        if(PlayerPrefs.GetString("Wave4Task2Claimed") == "true")
        {
            wave4task2ClaimButton.GetComponent<Button>().interactable = false;
            wave4task2ClaimButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "CLAIMED";
        }
        //Third task....

        wave4task3ProgressText.text = PlayerPrefs.GetInt("CustomerServed").ToString();
        if(PlayerPrefs.GetInt("CustomerServed") > 74)
        {
            wave4task3ProgressPannel.SetActive(false);
            wave4task3ClaimButton.SetActive(true);
        }
        if(PlayerPrefs.GetString("Wave4Task3Claimed") == "true")
        {
            wave4task3ClaimButton.GetComponent<Button>().interactable = false;
            wave4task3ClaimButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "CLAIMED";
        }
         //Fourth task....

        wave4task4ProgressText.text = PlayerPrefs.GetInt("BurgerServed").ToString();
        if(PlayerPrefs.GetInt("BurgerServed") > 349)
        {
            wave4task4ProgressPannel.SetActive(false);
            wave4task4ClaimButton.SetActive(true);
        }
        if(PlayerPrefs.GetString("Wave4Task4Claimed") == "true")
        {
            wave4task4ClaimButton.GetComponent<Button>().interactable = false;
            wave4task4ClaimButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "CLAIMED";
        }
    }


    public void CustomerServedCount(int count)
    {
        int amount = PlayerPrefs.GetInt("CustomerServed");
        amount = amount + count;
        PlayerPrefs.SetInt("CustomerServed", amount);
    }
    public void BurgerServedCount(int count)
    {
        int amount = PlayerPrefs.GetInt("BurgerServed");
        amount = amount + count;
        PlayerPrefs.SetInt("BurgerServed", amount);
    }
    public void ColaServedCount(int count)
    {
        int amount = PlayerPrefs.GetInt("ColaServed");
        amount = amount + count;
        PlayerPrefs.SetInt("ColaServed", amount);
    }
    public void FrieServedCount(int count)
    {
        int amount = PlayerPrefs.GetInt("FrieServed");
        amount = amount + count;
        PlayerPrefs.SetInt("FrieServed", amount);
    }

    public void ResetValues()
    {
        PlayerPrefs.SetInt("FrieServed", 0);
        PlayerPrefs.SetInt("ColaServed", 0);
        PlayerPrefs.SetInt("BurgerServed", 0);
        PlayerPrefs.SetInt("CustomerServed", 0);
    }


    public void ClaimButtonWave1(int taskNo)
    {
        if(taskNo == 1)
        {
            Controller.instance.currencyController.AddMoney(30);
            wave1task1ClaimButton.GetComponent<Button>().interactable = false;
            wave1task1ClaimButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "CLAIMED";
            PlayerPrefs.SetString("Wave1Task1Claimed", "true");
        }
        if(taskNo == 2)
        {
            Controller.instance.currencyController.AddMoney(60);
            wave1task2ClaimButton.GetComponent<Button>().interactable = false;
            wave1task2ClaimButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "CLAIMED";
            PlayerPrefs.SetString("Wave1Task2Claimed", "true");
        }
        if(taskNo == 3)
        {
            Controller.instance.currencyController.AddMoney(50);
            wave1task3ClaimButton.GetComponent<Button>().interactable = false;
            wave1task3ClaimButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "CLAIMED";
            PlayerPrefs.SetString("Wave1Task3Claimed", "true");
        }
        if(taskNo == 4)
        {
            Controller.instance.currencyController.AddMoney(100);
            wave1task4ClaimButton.GetComponent<Button>().interactable = false;
            wave1task4ClaimButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "CLAIMED";
            PlayerPrefs.SetString("Wave1Task4Claimed", "true");
        }
    }
    public void ClaimButtonWave2(int taskNo)
    {
        if(taskNo == 1)
        {
            Controller.instance.currencyController.AddMoney(40);
            wave2task1ClaimButton.GetComponent<Button>().interactable = false;
            wave2task1ClaimButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "CLAIMED";
            PlayerPrefs.SetString("Wave2Task1Claimed", "true");
        }
        if(taskNo == 2)
        {
            Controller.instance.currencyController.AddMoney(60);
            wave2task2ClaimButton.GetComponent<Button>().interactable = false;
            wave2task2ClaimButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "CLAIMED";
            PlayerPrefs.SetString("Wave2Task2Claimed", "true");
        }
        if(taskNo == 3)
        {
            Controller.instance.currencyController.AddMoney(120);
            wave2task3ClaimButton.GetComponent<Button>().interactable = false;
            wave2task3ClaimButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "CLAIMED";
            PlayerPrefs.SetString("Wave2Task3Claimed", "true");
        }
        if(taskNo == 4)
        {
            Controller.instance.currencyController.AddMoney(160);
            wave2task4ClaimButton.GetComponent<Button>().interactable = false;
            wave2task4ClaimButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "CLAIMED";
            PlayerPrefs.SetString("Wave2Task4Claimed", "true");
        }
    }
    public void ClaimButtonWave3(int taskNo)
    {
        if(taskNo == 1)
        {
            Controller.instance.currencyController.AddMoney(70);
            wave3task1ClaimButton.GetComponent<Button>().interactable = false;
            wave3task1ClaimButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "CLAIMED";
            PlayerPrefs.SetString("Wave3Task1Claimed", "true");
        }
        if(taskNo == 2)
        {
            Controller.instance.currencyController.AddMoney(180);
            wave3task2ClaimButton.GetComponent<Button>().interactable = false;
            wave3task2ClaimButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "CLAIMED";
            PlayerPrefs.SetString("Wave3Task2Claimed", "true");
        }
        if(taskNo == 3)
        {
            Controller.instance.currencyController.AddMoney(220);
            wave3task3ClaimButton.GetComponent<Button>().interactable = false;
            wave3task3ClaimButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "CLAIMED";
            PlayerPrefs.SetString("Wave3Task3Claimed", "true");
        }
        if(taskNo == 4)
        {
            Controller.instance.currencyController.AddMoney(250);
            wave3task4ClaimButton.GetComponent<Button>().interactable = false;
            wave3task4ClaimButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "CLAIMED";
            PlayerPrefs.SetString("Wave3Task4Claimed", "true");
        }
    }
    public void ClaimButtonWave4(int taskNo)
    {
        if(taskNo == 1)
        {
            Controller.instance.currencyController.AddMoney(80);
            wave4task1ClaimButton.GetComponent<Button>().interactable = false;
            wave4task1ClaimButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "CLAIMED";
            PlayerPrefs.SetString("Wave4Task1Claimed", "true");
        }
        if(taskNo == 2)
        {
            Controller.instance.currencyController.AddMoney(240);
            wave4task2ClaimButton.GetComponent<Button>().interactable = false;
            wave4task2ClaimButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "CLAIMED";
            PlayerPrefs.SetString("Wave4Task2Claimed", "true");
        }
        if(taskNo == 3)
        {
            Controller.instance.currencyController.AddMoney(350);
            wave4task3ClaimButton.GetComponent<Button>().interactable = false;
            wave4task3ClaimButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "CLAIMED";
            PlayerPrefs.SetString("Wave4Task3Claimed", "true");
        }
        if(taskNo == 4)
        {
            Controller.instance.currencyController.AddMoney(400);
            wave4task4ClaimButton.GetComponent<Button>().interactable = false;
            wave4task4ClaimButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "CLAIMED";
            PlayerPrefs.SetString("Wave4Task4Claimed", "true");
        }
    }
}
