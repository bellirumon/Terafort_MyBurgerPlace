using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyParentAndArrayManager : MonoBehaviour
{
    [Tooltip("The 'Moneys' gameobject in a Table that holds all the cash prefabs")]
    public GameObject ParentMoneyGO;

    [Space]

    [Tooltip("The table whose TableView 'Moneys' array field needs the references to all the cash prefabs")]
    public TableView TableToAssignMoneysTo; 
}
