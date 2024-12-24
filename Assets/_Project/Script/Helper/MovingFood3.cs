using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class MovingFood3 : MonoBehaviour
{
    PLayerView playerView;
    public int targetBox;

    private void Start() 
    {
        playerView = PLayerView.instance;  
        targetBox = playerView.thirdFoodOnhand;
        // playerView.thirdFoodOnhand++;
    }
    void Update()
    {
        Vector3 pos = playerView.thirdFoodDummys[targetBox].transform.position;
        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * 35);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("FoodCover"))
        {
            GameObject effect = LeanPool.Spawn(playerView.onHandEffect, playerView.thirdFoodDummys[targetBox].transform.position, playerView.onHandEffect.transform.rotation);
            StartCoroutine(playerView.DestroyObject(effect, 2f));

            playerView.thirdFoodDummys[playerView.thirdFoodOnhand].enabled = true;
            playerView.thirdFoodOnhand++;
            Controller.instance.gameController.PlayHaptic();

            if(playerView.thirdFoodOnhand < playerView.maxFoodOnHand)
            {
                playerView.MoveFrocksToHand();
            }
            if(playerView.thirdFoodOnhand == playerView.maxFoodOnHand)
            {
                Controller.instance.uiController.playerMaxText3.SetActive(true);
                GlobalData.instance.isHandFull = true;
                if (!CoockerView.instance.isFrocksSewing && CoockerView.instance.thirdFoodFabricStored > 0) 
                { 
                    CoockerView.instance.SewFrocksAndStack(); 
                    //CoockerView.instance.isFrocksSewing = true; 
                }
            }
            
            Destroy(transform.GetComponent<MovingFood3>());
            LeanPool.Despawn(this.gameObject);
        }    
    }
}
