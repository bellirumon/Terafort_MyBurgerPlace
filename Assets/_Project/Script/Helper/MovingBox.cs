using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class MovingBox : MonoBehaviour
{
    PLayerView playerView;
    public int targetBox;

    private void Start() 
    {
        playerView = PLayerView.instance;  
        targetBox = playerView.boxOnhandCount;
        playerView.isBoxMoving = true;
    }
    void Update()
    {
        Vector3 pos = playerView.boxDummys[targetBox].transform.position;
        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * 45);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("FoodCover"))
        { 
            GameObject effect = LeanPool.Spawn(playerView.onHandEffect, playerView.boxDummys[targetBox].transform.position, playerView.onHandEffect.transform.rotation);
            StartCoroutine(playerView.DestroyObject(effect, 2f));

            playerView.boxDummys[targetBox].SetActive(true);
            PLayerView.instance.boxOnhandCount++;
            Controller.instance.gameController.PlayHaptic();
            PackagingView packagingView = PLayerView.instance.DeliveryTableNo.GetComponent<PackagingView>();
            if (packagingView.boxOnTableCount > 0 && playerView.boxOnhandCount < playerView.maxBoxOnhandCount && playerView.onBoxPoint)
            {
                PLayerView.instance.MoveBoxToHand();
            }

            if(packagingView.burgrOnTableCount > 0 || packagingView.friesOnTableCount > 0 || packagingView.cokeOnTableCount > 0)
            {
                if(!packagingView.isPacking)
                {
                    packagingView.InstantiateBox();
                    //packagingView.isPacking = true;
                }
            }

            

            playerView.isBoxMoving = false;
            Destroy(transform.GetComponent<MovingBox>());
            LeanPool.Despawn(this.gameObject);
        }    
    }
}
