using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class MovingFood : MonoBehaviour
{
    PLayerView playerView;
    public int targetBox;

    private void Start() 
    {
        playerView = PLayerView.instance;  
        targetBox = playerView.firstFoodOnhand;
    }
    void Update()
    {
        Vector3 pos = playerView.firstFoodDummys[targetBox].transform.position;
        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * 35);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("FoodCover"))
        {
            GameObject effect = LeanPool.Spawn(playerView.onHandEffect, playerView.firstFoodDummys[targetBox].transform.position, playerView.onHandEffect.transform.rotation);
            StartCoroutine(playerView.DestroyObject(effect, 1f));

            playerView.firstFoodDummys[playerView.firstFoodOnhand].enabled = true;
            playerView.firstFoodOnhand++;
            Controller.instance.gameController.PlayHaptic();

            if(playerView.firstFoodOnhand < playerView.maxFoodOnHand)
            {
                playerView.MoveShirtsToHand();
            }
            if(playerView.firstFoodOnhand == playerView.maxFoodOnHand)
            {
                Controller.instance.uiController.playerMaxText.SetActive(true);
                GlobalData.instance.isHandFull = true;
                //if (!CoockerView.instance.isShirtSewing) { CoockerView.instance.SewShirtsAndStack(); CoockerView.instance.isShirtSewing = true; }
                if (!CoockerView.instance.isShirtSewing && CoockerView.instance.firstFoodFabricStored > 0) 
                { 
                    CoockerView.instance.SewShirtsAndStack(); 
                    //CoockerView.instance.isShirtSewing = true; 
                }
            }
            Destroy(transform.GetComponent<MovingFood>());
            LeanPool.Despawn(this.gameObject);
        }    
    }
}
