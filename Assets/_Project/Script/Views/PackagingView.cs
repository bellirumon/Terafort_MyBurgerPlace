using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
using DG.Tweening;

public class PackagingView : MonoBehaviour
{
    public static PackagingView instance;
    public GameObject[] dummyBurgers;
    public GameObject[] dummyFries;
    public GameObject[] dummyCoke;
    public GameObject[] dummyBoxes;

    public Animator packAnimator;

    public Transform boxInstancePos;
    public GameObject boxPrefab;
    public GameObject boxAnimated;
    public GameObject boxAnimated2;
    public Animator boxAnimator;
    public int burgrOnTableCount;
    public int friesOnTableCount;
    public int cokeOnTableCount;
    public int boxOnTableCount;
    GameObject packedBox;
    GameObject foods;
    public bool test;
    public bool isPacking;

    public bool isCalledBurger, isCalledfries, isCalledCoke;
    private void Awake() 
    {
        instance = this;
    }


    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            PLayerView.instance.onDeliveryTable = true;
            // GlobalData.instance.isPacking = false;
            PLayerView.instance.DeliveryTableNo = this.gameObject;

            if(PLayerView.instance.firstFoodOnhand > 0)
            {
                PLayerView.instance.MoveShirtsToDeliveryTable();
            }
            if(PLayerView.instance.secondFoodOnhand > 0)
            {
                PLayerView.instance.MoveJeansToDeliveryTable();
            }
            if(PLayerView.instance.thirdFoodOnhand > 0)
            {
                PLayerView.instance.MoveFrocksToDeliveryTable();
            }
        }
    }
    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            PLayerView.instance.onDeliveryTable = false;
            if(burgrOnTableCount > 0 || friesOnTableCount > 0 || cokeOnTableCount > 0)
            {
                if(!isPacking)
                {
                    if(boxOnTableCount < dummyBoxes.Length)
                    {
                        InstantiateBox();
                        isPacking = true;
                    }
                }
            }
        }
    }


    public void InstantiateBox()
    {
        if(boxOnTableCount >= dummyBoxes.Length - 1)
        {
            packAnimator.SetBool("Pack", false);
            isPacking = false;
            return;
        }

        if(PLayerView.instance.onDeliveryTable && GlobalData.instance.isFoodOnHand) 
        {
            isPacking = false;

            if(PLayerView.instance.firstFoodOnhand > 0)
            {
                if(!isPacking){PLayerView.instance.MoveShirtsToDeliveryTable();}
            }
            if(PLayerView.instance.secondFoodOnhand > 0)
            {
                if(!isPacking){PLayerView.instance.MoveJeansToDeliveryTable();}
            }
            if(PLayerView.instance.thirdFoodOnhand > 0)
            {
                if(!isPacking){PLayerView.instance.MoveFrocksToDeliveryTable();}
            }

            return;
        }
        if(burgrOnTableCount == 0 && friesOnTableCount == 0 && cokeOnTableCount == 0) 
        {
            isPacking = false;
            return;
        }

        packAnimator.SetBool("Pack", true);
        if(this.gameObject.name != "SecondTable"){packedBox = Instantiate(boxAnimated, boxInstancePos.position, boxAnimated.transform.rotation);}
        if(this.gameObject.name == "SecondTable"){packedBox = Instantiate(boxAnimated2, boxInstancePos.position, boxAnimated.transform.rotation);}
        boxAnimator = packedBox.GetComponentInChildren<Animator>();
        boxAnimator.SetBool("Close", false);
        isPacking = true;
        packedBox.transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.6f).SetEase(Ease.InQuint).OnComplete(() => 
        {
            CallBurger();
        });
    }

    public void CallBurger()
    {
        if(burgrOnTableCount == 0 && friesOnTableCount == 0 && cokeOnTableCount == 0) 
        {
            isPacking = false;
            return;
        }
        if(burgrOnTableCount == 0)
        {
            CallFries();
            return;
        }

        if (burgrOnTableCount > 0)
        {
            GameObject food = LeanPool.Spawn(CoockerView.instance.firstFoodPrefab, dummyBurgers[burgrOnTableCount - 1].transform.position, dummyBurgers[burgrOnTableCount - 1].transform.rotation);
            dummyBurgers[burgrOnTableCount - 1].SetActive(false);
            burgrOnTableCount--;
            foods = food;

            Animator animator = boxAnimator;
            food.transform.DOMove(boxInstancePos.position, 0.4f).SetEase(Ease.InQuint).OnComplete(() => 
            {
                animator.SetBool("Close", true);
                if(burgrOnTableCount != 0 || friesOnTableCount != 0 || cokeOnTableCount != 0)
                {
                    Invoke(nameof(InstantiateBox), 0.3f);
                }
                else
                {
                    packAnimator.SetBool("Pack", false);
                    isPacking = false;
                }
                Invoke(nameof(MovePackedBox), 0.2f);
            });
        }
        
    }
    public void CallFries()
    {
        if(friesOnTableCount == 0)
        {
            CallCoke();
        }

        if (friesOnTableCount > 0)
        {
            GameObject food = LeanPool.Spawn(CoockerView.instance.secondFoodPrefab, dummyFries[friesOnTableCount - 1].transform.position, dummyFries[friesOnTableCount - 1].transform.rotation);
            dummyFries[friesOnTableCount - 1].SetActive(false);
            friesOnTableCount--;
            foods = food;

            Animator animator = boxAnimator;
            food.transform.DOMove(boxInstancePos.position, 0.4f).SetEase(Ease.InQuint).OnComplete(() => 
            {
                animator.SetBool("Close", true);
                if(burgrOnTableCount != 0 || friesOnTableCount != 0 || cokeOnTableCount != 0)
                {
                    Invoke(nameof(InstantiateBox), 0.3f);
                }
                else
                {
                    packAnimator.SetBool("Pack", false);
                    isPacking = false;
                }
                Invoke(nameof(MovePackedBox), 0.2f);
            });
        }
    }
    public void CallCoke()
    {
        if(cokeOnTableCount == 0)
        {
            CallBurger();
        }

        if (cokeOnTableCount > 0)
        {
            GameObject food = LeanPool.Spawn(CoockerView.instance.thirdFoodPrefab, dummyCoke[cokeOnTableCount - 1].transform.position, dummyCoke[cokeOnTableCount - 1].transform.rotation);
            dummyCoke[cokeOnTableCount - 1].SetActive(false);
            cokeOnTableCount--;
            foods = food;

            Animator animator = boxAnimator;
            food.transform.DOMove(boxInstancePos.position, 0.4f).SetEase(Ease.InQuint).OnComplete(() => 
            {
                animator.SetBool("Close", true);
                if(burgrOnTableCount != 0 || friesOnTableCount != 0 || cokeOnTableCount != 0)
                {
                    Invoke(nameof(InstantiateBox), 0.3f);
                }
                else
                {
                    packAnimator.SetBool("Pack", false);
                    isPacking = false;
                }
                Invoke(nameof(MovePackedBox), 0.2f);
            });
        }
    }

    public void MovePackedBox()
    {
        GameObject box = packedBox;
        packedBox = null;

        GameObject f = foods;
        LeanPool.Despawn(f);
        foods = null;
        box.transform.DOMove(dummyBoxes[boxOnTableCount].transform.position, 0.3f).SetEase(Ease.InQuint).OnComplete(() => 
        {
            dummyBoxes[boxOnTableCount].SetActive(true);
            boxOnTableCount++;
            Destroy(box);
        });
    }
}
