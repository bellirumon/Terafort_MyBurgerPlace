using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaiterController : MonoBehaviour
{
    public GameObject waiter1;
    public GameObject waiter2;
    public GameObject waiter3;
    public GameObject waiter4;

    private void Start() 
    {
        if(PlayerPrefs.GetInt("WaiterCount") == 1)
        {
            waiter2.SetActive(true);
        }    
        if(PlayerPrefs.GetInt("WaiterCount") == 2)
        {
            waiter2.SetActive(true);
            waiter3.SetActive(true);
        }    
        if(PlayerPrefs.GetInt("WaiterCount") == 3)
        {
            waiter2.SetActive(true);
            waiter3.SetActive(true);
            waiter4.SetActive(true);
        }    
    }
}
