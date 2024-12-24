using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
using DG.Tweening;

public class TutorialMoney : MonoBehaviour
{
    public GameObject[] money;
    public GameObject moneyPrefab;

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Controller.instance.gameController.PlayHaptic();
            for (int i = 0; i < money.Length; i++)
            {
                GameObject m = LeanPool.Spawn(moneyPrefab, money[i].transform.position, money[i].transform.rotation);
                m.transform.DOMove(PLayerView.instance.moneyMovePos.position, 0.3f).OnComplete(() => { LeanPool.Despawn(m); });
                m.transform.DOScale(Vector3.zero, 0.3f);
                money[i].SetActive(false);
            }

            Controller.instance.currencyController.AddMoney(5000);
            Controller.instance.uiController.UpdateMoneyTexts();
            Destroy(gameObject);
        }
    }
}
