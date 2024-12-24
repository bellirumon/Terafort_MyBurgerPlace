using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData : MonoBehaviour
{
    public static GlobalData instance;
    public bool isFoodOnHand;
    public bool isBoxOnHand;
    public bool isHandFull;
    public bool isFoodMovingToTable;
    public bool isPacking;
    private void Awake() 
    {
        instance = this;
    }
}
