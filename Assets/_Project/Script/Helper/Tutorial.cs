using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public static Tutorial instance;

    private void Awake() 
    {
        instance = this;
    }
    public void TutorialComplete()
    {
        PlayerPrefs.SetString("IsTutorialComplete", "true");
    }

    public bool IsTutorialComplete()
    {
        if (PlayerPrefs.GetString("IsTutorialComplete") == "true") { return true; }
        else
        {
            return false;
        }
    }
}
