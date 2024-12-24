using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class MovingFoodDirect1 : MonoBehaviour
{

    PLayerView playerView;
    public int targetBox;

    private void Start() 
    {
        playerView = PLayerView.instance;  
        targetBox = playerView.firstFoodOnhand;
        // playerView.firstFoodOnhand++;
        // Debug.Log(targetBox);
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
            // GameObject effect = LeanPool.Spawn(playerView.onHandEffect, playerView.firstFoodDummys[targetBox].transform.position, playerView.onHandEffect.transform.rotation);
            // StartCoroutine(playerView.DestroyObject(effect, 1f));
            playerView.firstFoodDummys[playerView.firstFoodOnhand].enabled = true;
            playerView.firstFoodOnhand++;
            Controller.instance.gameController.PlayHaptic();

            if(playerView.firstFoodOnhand < playerView.maxFoodOnHand)
            {
                CoockerView.instance.MoveFoodTohand();
            }
            if(playerView.firstFoodOnhand == playerView.maxFoodOnHand)
            {
                Controller.instance.uiController.playerMaxText.SetActive(true);
                GlobalData.instance.isHandFull = true;
                CoockerView.instance.SewShirtsAndStack();
            }
            Destroy(transform.GetComponent<MovingFoodDirect1>());
            LeanPool.Despawn(this.gameObject);
        }    
    }
}
