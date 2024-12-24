using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public static Controller instance;
    public WaiterController waiterController;
    public GameController gameController;
    public UiController uiController;
    public CustomersController customersController;
    public TableController tableController;
    public CurrencyController currencyController;
    public CameraController cameraController;
    private void Awake() 
    {
        instance = this;
    }
}
