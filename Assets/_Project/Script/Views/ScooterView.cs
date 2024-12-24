using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Lean.Pool;

public class ScooterView : MonoBehaviour
{
    public static ScooterView instance;
    private void Awake() {
        instance = this;
    }
    public GameObject[] dummyBoxes;
    public GameObject[] moneys;

    public int boxStoredInScooty;
    public Transform scoterEndPos;
    public Transform scoterStartPos;
    public Vector3 originalPos;
    public int moneyStored;
    bool called;

    private void Start() 
    {
        //originalPos = this.transform.position;
    }

    public IEnumerator MoveScooterOut(float waitTime)
    {
        this.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(waitTime);
        this.transform.DOMove(scoterEndPos.position, 1.5f).SetEase(Ease.InQuint).OnComplete(() => 
        {
            boxStoredInScooty = 0;
            for (int i = 0; i < dummyBoxes.Length; i++)
            {
                dummyBoxes[i].SetActive(false);
            }
            this.transform.position = scoterStartPos.position;
            called = false;
            Invoke(nameof(MoveScooterIn), 1f);
        });
        InstanceMoney();
    }

    public void MoveScooterIn()
    {
        this.transform.DOMove(originalPos, 2.4f).SetEase(Ease.InOutCubic).OnComplete(()=>
        {
            this.GetComponent<BoxCollider>().enabled = true;
        });
    }

    public void InstanceMoney()
    {
        if(called) return;
        called = true;
        if(moneyStored == moneys.Length) return;
        int index = moneyStored;
        if(boxStoredInScooty < 3)
        {
            for (int i = 0; i < 6; i++)
            {
                if(moneyStored == moneys.Length) return;
                moneys[index + i].SetActive(true);
                moneyStored++;
            }
        }
        if(boxStoredInScooty >= 3 && boxStoredInScooty < 5)
        {
            for (int i = 0; i < 14; i++)
            {
                if(moneyStored == moneys.Length) return;
                moneys[index + i].SetActive(true);
                moneyStored++;
            }
        }
        if(boxStoredInScooty >= 5 )
        {
            for (int i = 0; i < 20; i++)
            {
                if(moneyStored == moneys.Length) return;
                moneys[index + i].SetActive(true);
                moneyStored++;
            }
        }
    }

    public void CollectMoney()
    {
        if(moneyStored > 0)
        {
            int a = moneyStored;
            Controller.instance.currencyController.AddMoney(a * 5);
            Controller.instance.uiController.UpdateMoneyTexts();
            moneyStored = 0;
            Controller.instance.gameController.PlayHaptic();
            for (int i = 0; i < a; i++)
            {
                GameObject m = LeanPool.Spawn(Controller.instance.currencyController.moneyPrefab, moneys[i].transform.position, moneys[i].transform.rotation);
                m.transform.DOMove(PLayerView.instance.moneyMovePos.position, 1f).OnComplete(() => { LeanPool.Despawn(m); });
                m.transform.DOScale(Vector3.zero, 0.7f);
                moneys[i].SetActive(false);
            }
        }
    }
}
