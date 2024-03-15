using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float time;
    private Tween tween;
    
    private void Start()
    {
        tween = transform.DOLocalMoveX(((RectTransform)transform).anchoredPosition.x + 1920f, time);
        tween.SetLoops(-1, LoopType.Restart);
        tween.SetEase(Ease.Linear);
    }

    private void OnDestroy()
    {
        tween?.Kill();
    }
}
