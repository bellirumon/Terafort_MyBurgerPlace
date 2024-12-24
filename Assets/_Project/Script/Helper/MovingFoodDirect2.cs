using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class MovingFoodDirect2 : MonoBehaviour
{

    PLayerView playerView;
    public int targetBox;

    private void Start() 
    {
        playerView = PLayerView.instance;  
        targetBox = playerView.secondFoodOnhand;
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
            playerView.secondFoodDummys[playerView.secondFoodOnhand].enabled = true;
            playerView.secondFoodOnhand++;
            Controller.instance.gameController.PlayHaptic();

            if(playerView.secondFoodOnhand < playerView.maxFoodOnHand)
            {
                CoockerView.instance.MoveFoodTohand();
            }
            if(playerView.secondFoodOnhand == playerView.maxFoodOnHand)
            {
                Controller.instance.uiController.playerMaxText2.SetActive(true);
                GlobalData.instance.isHandFull = true;
                CoockerView.instance.SewJeansAndStack();
            }
            Destroy(transform.GetComponent<MovingFoodDirect2>());
            LeanPool.Despawn(this.gameObject);
        }    
    }
}
