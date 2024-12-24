using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IndicatorEffect : MonoBehaviour
{
    Vector3 originPos;
    Vector3 targetPos;
    void Start()
    {
        originPos = transform.position;
        targetPos = new Vector3(originPos.x, originPos.y + 0.5f, originPos.z);
        Invoke(nameof(MoveUp), 0);
    }

    void MoveUp()
    {
        transform.DOMove(targetPos, 0.3f).SetEase(Ease.InOutSine).OnComplete(() => { Invoke(nameof(MoveDown), 0);});
    }
    void MoveDown()
    {
        transform.DOMove(originPos, 0.3f).SetEase(Ease.InOutSine).OnComplete(() => { Invoke(nameof(MoveUp), 0);});
    }
}
