using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Timer : MonoBehaviour
{
    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    public TextMeshProUGUI timeText;
    float tempTime;
    private void Start()
    {
        // Starts the timer automatically
        if(!PlayerPrefs.HasKey("TimeRemaining"))
        {
            tempTime = timeRemaining;
        }
        else
        {
            tempTime = PlayerPrefs.GetFloat("TimeRemaining");
        }
        timerIsRunning = true;
    }

    private void OnApplicationQuit() 
    {
        PlayerPrefs.SetFloat("TimeRemaining", tempTime);
    }
    
    void Update()
    {
        if (timerIsRunning)
        {
            if (tempTime > 0)
            {
                tempTime -= Time.deltaTime;
                DisplayTime(tempTime);
            }
            else
            {
                Debug.Log("Time has run out!");
                DailyTaskView.instance.ResetValues();
                DailyTaskView.instance.SetWaveValues();
                tempTime += timeRemaining;
                //timerIsRunning = false;
            }
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}",minutes, seconds);
    }
}