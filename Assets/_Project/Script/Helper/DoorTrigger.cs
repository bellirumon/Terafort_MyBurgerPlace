using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorTrigger : MonoBehaviour
{
    public GameObject door;
    public Vector3 targetRotation;
    public Vector3 origin;
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            door.transform.DORotate(targetRotation, 1f);
        }
    }
    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            door.transform.DORotate(origin, 1f);
        }
    }
}
