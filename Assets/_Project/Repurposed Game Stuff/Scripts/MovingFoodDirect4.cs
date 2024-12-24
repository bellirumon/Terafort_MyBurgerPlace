using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class MovingFoodDirect4 : MonoBehaviour
{
    PLayerView playerView;
    public int targetBox;
    Vector3 targetPos;

    private void Start()
    {
        playerView = PLayerView.instance;
        targetBox = playerView.fabricOnHand;

        targetPos = playerView.fabricDummys[targetBox].transform.position;
    }
    void Update()
    {
        if (transform.position == targetPos) return;

        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * 10);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FoodCover"))
        { 
            // GameObject effect = LeanPool.Spawn(playerView.onHandEffect, playerView.firstFoodDummys[targetBox].transform.position, playerView.onHandEffect.transform.rotation);
            // StartCoroutine(playerView.DestroyObject(effect, 1f));
            playerView.fabricDummys[playerView.fabricOnHand].enabled = true;
            playerView.fabricOnHand++;
            Controller.instance.gameController.PlayHaptic();

            //gets called 16 times

            if (playerView.fabricOnHand < playerView.maxFoodOnHand)
            {
                CoockerView.instance.MoveFoodTohand();
            }
            else if (playerView.fabricOnHand == playerView.maxFoodOnHand)
            {
                Controller.instance.uiController.playerMaxText4.SetActive(true);
                GlobalData.instance.isHandFull = true;
                //CoockerView.instance.CoockThirdFoodAndStack();
            }
            Destroy(transform.GetComponent<MovingFoodDirect4>());
            LeanPool.Despawn(this.gameObject);
        }
    }

}
