using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class MovingFood2 : MonoBehaviour
{
    PLayerView playerView;
    public int targetBox;

    private void Start() 
    {
        playerView = PLayerView.instance;  
        targetBox = playerView.secondFoodOnhand;
    }
    void Update()
    {
        Vector3 pos = playerView.secondFoodDummys[targetBox].transform.position;
        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * 35);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("FoodCover"))
        {
            GameObject effect = LeanPool.Spawn(playerView.onHandEffect, playerView.secondFoodDummys[targetBox].transform.position, playerView.onHandEffect.transform.rotation);
            StartCoroutine(playerView.DestroyObject(effect, 2f));
            playerView.secondFoodDummys[playerView.secondFoodOnhand].enabled = true;

            playerView.secondFoodOnhand++;
            Controller.instance.gameController.PlayHaptic();

            if(playerView.secondFoodOnhand < playerView.maxFoodOnHand)
            {
                playerView.MoveJeansToHand();
            }
            if(playerView.secondFoodOnhand == playerView.maxFoodOnHand)
            {
                Controller.instance.uiController.playerMaxText2.SetActive(true);
                GlobalData.instance.isHandFull = true;
                if (!CoockerView.instance.isJeansSewing && CoockerView.instance.secondFoodFabricStored > 0) 
                { 
                    CoockerView.instance.SewJeansAndStack(); 
                    //CoockerView.instance.isJeansSewing = true;
                }
            }

            Destroy(transform.GetComponent<MovingFood2>());
            LeanPool.Despawn(this.gameObject);
        }    
    }
}
