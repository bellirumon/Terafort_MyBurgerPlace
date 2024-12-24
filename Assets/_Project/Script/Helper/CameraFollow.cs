using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance;
    public Transform player;
    public Vector3 offset;


    private void Awake() {
        instance = this;
    }

    private void Start() 
    {
        offset = transform.position - player.transform.position;
    }
    void LateUpdate()
    {
        transform.position = player.position + offset;
    }
}
