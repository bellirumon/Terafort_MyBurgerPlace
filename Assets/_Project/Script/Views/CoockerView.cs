using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Lean.Pool;
using UnityEngine.UI;
using UnityEngine.Serialization;

public class CoockerView : MonoBehaviour
{
    public static CoockerView instance;
    public Transform firstFoodInstancePos;
    public Transform secondFoodInstancePos;
    public Transform thirdFoodInstancePos;
    public Transform fabricInstancePos;

    [Space]
    [Header("--------------------First Food Config--------------------")]
    [Space]

    public GameObject firstFoodPrefab;
    public Animator shirtsTailor;
    public Image shirtsStall_Timer;
    [SerializeField] float shirtSewDuration = 1f;
    public int firstFoodStored;
    public int firstFoodMax;
    public GameObject[] firstFoodDummys;
    public int firstFoodFabricStored;
    public int firstFoodFabricMax;
    public GameObject[] firstFoodFabricDummys;

    [Space]
    [Header("--------------------Second Food Config--------------------")]
    [Space]

    public GameObject secondFoodPrefab;
    public Animator jeansTailor;
    public Image jeansStall_Timer;
    [SerializeField] float jeansSewDuration = 1.5f;
    public int secondFoodStored;
    public int secondFoodMax;
    public GameObject[] secondFoodDummys;
    public int secondFoodFabricStored;
    public int secondFoodFabricMax;
    public GameObject[] secondFoodFabricDummys;

    [Space]
    [Header("--------------------Third Food Config--------------------")]
    [Space]

    public GameObject thirdFoodPrefab;
    public Animator frocksTailor;
    public Image frocksStall_Timer;
    [SerializeField] float frockSewDuration = 2f;
    public int thirdFoodStored;
    public int thirdFoodMax;
    public GameObject[] thirdFoodDummys;
    public int thirdFoodFabricStored;
    public int thirdFoodFabricMax;
    public GameObject[] thirdFoodFabricDummys;

    [Space]
    [Header("--------------------Fabric Config--------------------")]
    [Space]

    public GameObject fabricPrefab;
    public GameObject[] moneyDeductedFromFabricGen;
    public GameObject[] moneyRefundedFromFabricRecycle;

    public bool isShirtSewing, isJeansSewing, isFrocksSewing;

    public bool isCalled;


    private void Awake() {
        instance = this;
    }
    private void Start() 
    {
        firstFoodMax = firstFoodDummys.Length;
        secondFoodMax = secondFoodDummys.Length;
        thirdFoodMax = thirdFoodDummys.Length;

        firstFoodFabricMax = firstFoodFabricDummys.Length;
        secondFoodFabricMax = secondFoodFabricDummys.Length;
        thirdFoodFabricMax = thirdFoodFabricDummys.Length;

        if(AllLockAndUnlockView.instance.IsBurgerUnlocked()) { Invoke(nameof(SewShirtsAndStack), 1.1f); }
        if(AllLockAndUnlockView.instance.IsFriesUnlocked()) { Invoke(nameof(SewJeansAndStack), 1.4f); }
        if(AllLockAndUnlockView.instance.IsCokeUnlocked()) { Invoke(nameof(SewFrocksAndStack), 1.8f); }
    }

    void Update()
    {
        shirtsStall_Timer.transform.LookAt(Camera.main.transform);    
        jeansStall_Timer.transform.LookAt(Camera.main.transform);    
        frocksStall_Timer.transform.LookAt(Camera.main.transform);    
    }


    public void SewShirtsAndStack()
    {
        if (firstFoodFabricStored <= 0 || firstFoodStored >= firstFoodMax) return;

        isShirtSewing = true;
        shirtsTailor.SetBool("isSewing", true);
        GameObject firstFood = LeanPool.Spawn(firstFoodPrefab, firstFoodInstancePos.transform.position, firstFoodDummys[0].transform.rotation);

        StartCoroutine(AnimateShirtTimer(shirtSewDuration));

        firstFood.transform.DOMove(firstFoodDummys[firstFoodStored].transform.position, shirtSewDuration).SetEase(Ease.OutSine).OnComplete(() =>
        {
            firstFoodFabricDummys[firstFoodFabricStored - 1].SetActive(false);
            firstFoodFabricStored--;

            firstFoodDummys[firstFoodStored].SetActive(true);
            firstFoodStored++;
            LeanPool.Despawn(firstFood);

            if (firstFoodStored >= firstFoodMax || firstFoodFabricStored <= 0)
            {
                isShirtSewing = false;
                shirtsTailor.SetBool("isSewing", false);
                Controller.instance.uiController.food1MaxText.SetActive(true);
                return;
            }
            else
            {
                Invoke(nameof(SewShirtsAndStack), 0.5f);
            }
            //if (firstFoodStored >= firstFoodMax) { Controller.instance.uiController.food1MaxText.SetActive(true); isBurgerCooking = false; }
        });

    }
    
    public void CookFirstFoodAgain(int time)
    {
        if (isCalled) return;
        isCalled = true;
        Invoke(nameof(SewShirtsAndStack), time);
    }
    public void StopMakingShirts()
    {
        CancelInvoke(nameof(SewShirtsAndStack));
        isShirtSewing = false;
        shirtsTailor.SetBool("isSewing", false);
    }

    IEnumerator AnimateShirtTimer(float duration)
    {
        shirtsStall_Timer.fillAmount = 0f;

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            shirtsStall_Timer.fillAmount += Mathf.Clamp01(elapsedTime / duration);
            yield return null;
        }

        shirtsStall_Timer.fillAmount = 1f;
    }

    public void SewJeansAndStack()
    {
        if (secondFoodFabricStored <= 0 || secondFoodStored >= secondFoodMax) return;

        isJeansSewing = true;
        jeansTailor.SetBool("isSewing", true);
        GameObject secondFood = LeanPool.Spawn(secondFoodPrefab, secondFoodInstancePos.transform.position, secondFoodDummys[0].transform.rotation);

        StartCoroutine(AnimateJeansTimer(jeansSewDuration));

        secondFood.transform.DOMove(secondFoodDummys[secondFoodStored].transform.position, jeansSewDuration).SetEase(Ease.OutSine).OnComplete(() =>
        {
            secondFoodFabricDummys[secondFoodFabricStored - 1].SetActive(false);
            secondFoodFabricStored--;

            secondFoodDummys[secondFoodStored].SetActive(true);
            secondFoodStored++;
            LeanPool.Despawn(secondFood);

            if (secondFoodStored >= secondFoodMax || secondFoodFabricStored <= 0)
            {
                isJeansSewing = false;
                jeansTailor.SetBool("isSewing", false);
                Controller.instance.uiController.food2MaxText.SetActive(true);
                return;
            }
            else
            {
                Invoke(nameof(SewJeansAndStack), 0.5f); 
            }
            //if (secondFoodStored >= secondFoodMax) { Controller.instance.uiController.food2MaxText.SetActive(true); isFriesCooking = false; }
        });
    }
    public void CookSecondFoodAgain(float time)
    {
        if(isCalled) return;
        isCalled = true;
        Invoke(nameof(SewJeansAndStack), time);
    }
    public void StopMakingJeans()
    {
        CancelInvoke(nameof(SewJeansAndStack));
        isJeansSewing = false;
        jeansTailor.SetBool("isSewing", false);
    }
    IEnumerator AnimateJeansTimer(float duration)
    {
        jeansStall_Timer.fillAmount = 0f;

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            jeansStall_Timer.fillAmount += Mathf.Clamp01(elapsedTime / duration);
            yield return null;
        }

        jeansStall_Timer.fillAmount = 1f;
    }

    public void SewFrocksAndStack()
    {
        if (thirdFoodFabricStored <= 0 || thirdFoodStored >= thirdFoodMax) return;

        isFrocksSewing = true;
        frocksTailor.SetBool("isSewing", true);
        GameObject thirdFood = LeanPool.Spawn(thirdFoodPrefab, thirdFoodInstancePos.transform.position, thirdFoodDummys[0].transform.rotation);

        StartCoroutine(AnimateFrockTimer(frockSewDuration));

        thirdFood.transform.DOMove(thirdFoodDummys[thirdFoodStored].transform.position, frockSewDuration).SetEase(Ease.OutSine).OnComplete(() =>
        {
            thirdFoodFabricDummys[thirdFoodFabricStored - 1].SetActive(false);
            thirdFoodFabricStored--;

            thirdFoodDummys[thirdFoodStored].SetActive(true);
            thirdFoodStored++;
            LeanPool.Despawn(thirdFood);
            
            if (thirdFoodStored >= thirdFoodMax || thirdFoodFabricStored <= 0)
            {
                isFrocksSewing = false;
                frocksTailor.SetBool("isSewing", false);
                Controller.instance.uiController.food3MaxText.SetActive(true);
                return;
            }
            else
            {
                Invoke(nameof(SewFrocksAndStack), 0.5f);
            }
            //if (thirdFoodStored >= thirdFoodMax) { Controller.instance.uiController.food3MaxText.SetActive(true); isCokeCooking = false; }
        });
    }
    public void CookThirdFoodAgain(float time)
    {
        if(isCalled) return;
        isCalled = true;
        Invoke(nameof(SewFrocksAndStack), time);
    }

    public void StopMakingFrocks()
    {
        CancelInvoke(nameof(SewFrocksAndStack));
        isFrocksSewing = false;
        frocksTailor.SetBool("isSewing", false);
    }

    IEnumerator AnimateFrockTimer(float duration)
    {
        frocksStall_Timer.fillAmount = 0f;

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            frocksStall_Timer.fillAmount += Mathf.Clamp01(elapsedTime / duration);
            yield return null;
        }

        frocksStall_Timer.fillAmount = 1f;
    }




    public void MoveItemCall()
    {
        MoveFoodTohand();
    }
    public void RemoveMoveFoodCall()
    {
        CancelInvoke(nameof(MoveFoodTohand));
    }


    public void MoveFoodTohand()
    {
        if(PLayerView.instance.onCurt1)
        {
            if (PLayerView.instance.firstFoodOnhand >= PLayerView.instance.maxFoodOnHand) 
            {
                return;
            }

            GameObject food = LeanPool.Spawn(firstFoodPrefab, firstFoodInstancePos.transform.position, firstFoodPrefab.transform.rotation);

            GlobalData.instance.isFoodOnHand = true;
            food.AddComponent<MovingFoodDirect1>();
        }
        else if(PLayerView.instance.onCurt2)
        {
            if (PLayerView.instance.secondFoodOnhand >= PLayerView.instance.maxFoodOnHand) 
            {
                return;
            }

            GameObject food = LeanPool.Spawn(secondFoodPrefab, secondFoodInstancePos.transform.position, secondFoodPrefab.transform.rotation);

            GlobalData.instance.isFoodOnHand = true;
            food.AddComponent<MovingFoodDirect2>();
        }
        else if(PLayerView.instance.onCurt3)
        {
            if (PLayerView.instance.thirdFoodOnhand >= PLayerView.instance.maxFoodOnHand) 
            {
                return;
            }

            GameObject food = LeanPool.Spawn(thirdFoodPrefab, thirdFoodInstancePos.transform.position, thirdFoodPrefab.transform.rotation);

            GlobalData.instance.isFoodOnHand = true;
            food.AddComponent<MovingFoodDirect3>();
        }
    }

}
