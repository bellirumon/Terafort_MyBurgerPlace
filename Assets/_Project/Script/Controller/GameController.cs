using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float startTime;
    public float endTime;
    void Start()
    {
        startTime = Time.time;
    }

    private void OnApplicationPause() 
    {
        endTime = Time.time;
        int playTime = (int)(endTime - startTime);
        AnalyticsService.LevelFinish(playTime, "TotalPlayTime", 0);
    }

    private void OnApplicationQuit() 
    {
        endTime = Time.time;
        int playTime = (int)(endTime - startTime);
        AnalyticsService.LevelFinish(playTime, "TotalPlayTime", 0);
    }
    
    public void PlayHaptic()
    {
        if(PlayerPrefs.GetString("HapticOn") == "true" || !PlayerPrefs.HasKey("HapticOn"))
        {
            AndroidManager.HapticFeedback();
        }
    }
}
