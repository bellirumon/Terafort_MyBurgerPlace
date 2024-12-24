using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class CustomerDestroyTrigger : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Customer"))
        {
            LeanPool.Despawn(other.gameObject);
        }    
    }
}
