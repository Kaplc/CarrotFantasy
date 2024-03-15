using System;
using System.Collections;
using System.Collections.Generic;
using PureMVC.Patterns.Mediator;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

public abstract class BasePanel : MonoBehaviour
{
    private bool showFade;
    private bool hideFade;
    private CanvasGroup canvasGroup;
    public float fadeSpeed = 3f;
    public UnityAction hideCallBack;
    public UnityAction showCallBack;
    
    // 返回与自身绑定的Mediator
    private Mediator mediator;
    public Mediator PanelMediator => mediator;
    
    protected virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (!canvasGroup)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    protected virtual void Start(){
        Init();
    }

    protected abstract void Init();

    public virtual void Update()
    {
        if (showFade)
        {
            if (canvasGroup.alpha >=1)
            {
                canvasGroup.alpha = 1;
                showFade = false;
                // 淡入成功执行回调
                showCallBack?.Invoke();
                return;
            }
            // 淡入
            canvasGroup.alpha += Time.deltaTime * fadeSpeed;
        }

        if (hideFade)
        {
            // 淡出
            if (canvasGroup.alpha <=0)
            {
                canvasGroup.alpha = 0;
                hideFade = false;
                // 淡出成功执行回调
                hideCallBack?.Invoke();
                return;
            }
            // 淡入
            canvasGroup.alpha -= Time.deltaTime * fadeSpeed;
        }
    }

    public virtual void Show(bool isFade = false, UnityAction callBack = null)
    {
        if (isFade)
        {
            showFade = true;
            canvasGroup.alpha = 0;
        }
        else
        {
            showFade = false;
            canvasGroup.alpha = 1;
        }
        
        showCallBack += callBack;
    }
    
    public virtual void Hide(UnityAction callBack)
    {
        hideFade = true;
        canvasGroup.alpha = 1;
        hideCallBack += callBack; // 淡出成功的回调
    }

    public void BindMediator(Mediator m)
    {
        this.mediator = m;
    }
}
