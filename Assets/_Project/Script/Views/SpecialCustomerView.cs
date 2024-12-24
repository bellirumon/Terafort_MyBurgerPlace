using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
using DG.Tweening;

public class SpecialCustomerView : MonoBehaviour
{
    public static SpecialCustomerView instance;
    public Animator specialAnimator;
    public GameObject[] moneys;
    public GameObject foodPrefab;
    public GameObject moneyPrefab;
    public Transform moneyInstancePos;
    public Transform instancePos;
    public Transform endPos;
    public int moneyCount;
    int foodMoved;

    private void Awake() {
        instance = this;
    }
    void Start()
    {
        Invoke(nameof(EatFoodAgain), 20);
    }

    public IEnumerator EatFood()
    {
        ;
        for (int i = 0; i < 30; i++)
        {
            GameObject food = LeanPool.Spawn(foodPrefab, instancePos.position, foodPrefab.transform.rotation);
            food.transform.DOMove(endPos.position, 2f).OnComplete(() =>
            {
                foodMoved++;
                specialAnimator.SetBool("Eat", true);
                LeanPool.Despawn(food);
                if (foodMoved == 30) { specialAnimator.SetBool("Eat", false); foodMoved = 0; Invoke(nameof(EatFoodAgain), 200); }
                InstanceAndMoveMoney1Customer();
            });
            yield return new WaitForSeconds(0.3f);
        }
    }

    public void EatFoodAgain()
    {
        StartCoroutine(EatFood());
    }

    public void InstanceAndMoveMoney1Customer()
    {
        if(moneyCount == moneys.Length) return;
        GameObject money = LeanPool.Spawn(moneyPrefab, moneyInstancePos.position, moneyPrefab.transform.rotation);
        money.transform.DOMove(moneys[moneyCount].transform.position, 0.5f).SetEase(Ease.InExpo).OnComplete(() =>
         {
             moneys[moneyCount].SetActive(true);
             moneyCount++;
             LeanPool.Despawn(money);
         });
        GameObject money2 = LeanPool.Spawn(moneyPrefab, moneyInstancePos.position, moneyPrefab.transform.rotation);
        money2.transform.DOMove(moneys[moneyCount].transform.position, 0.5f).SetEase(Ease.InExpo).OnComplete(() =>
         {
             moneys[moneyCount].SetActive(true);
             moneyCount++;
             LeanPool.Despawn(money2);
         });
        GameObject money3 = LeanPool.Spawn(moneyPrefab, moneyInstancePos.position, moneyPrefab.transform.rotation);
        money2.transform.DOMove(moneys[moneyCount].transform.position, 0.5f).SetEase(Ease.InExpo).OnComplete(() =>
         {
             moneys[moneyCount].SetActive(true);
             moneyCount++;
             LeanPool.Despawn(money3);
         });

        Controller.instance.customersController.moneyTransfared = moneyCount;
    }

    public void CollectMoney()
    {
        if(Controller.instance.customersController.moneyTransfared > 0)
        {
            int a = Controller.instance.customersController.moneyTransfared;
            Controller.instance.currencyController.AddMoney(a * Controller.instance.currencyController.perBundelMoney);
            Controller.instance.uiController.UpdateMoneyTexts();
            moneyCount = 0;
            Controller.instance.gameController.PlayHaptic();
            Controller.instance.customersController.moneyTransfared = 0;
            for (int i = 0; i < a + 6; i++)
            {
                GameObject m = LeanPool.Spawn(Controller.instance.currencyController.moneyPrefab, moneys[i].transform.position, moneys[i].transform.rotation);
                m.transform.DOMove(PLayerView.instance.moneyMovePos.position, 0.7f).OnComplete(() => { LeanPool.Despawn(m); });
                m.transform.DOScale(Vector3.zero, 0.5f);

                moneys[i].SetActive(false);
            }
        }
    }
}
