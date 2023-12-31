﻿using System;
using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using TMPro;
using UnityEngine;

public class Carrot : BaseRole, IPoolObject
{
    private float hp;

    public CarrotData data;
    public List<Sprite> sprites; // 各血量萝卜Sprite
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    private Coroutine idleAnimaCoroutine; // 待机动画协程
    public TextMeshPro textMeshPro;

    public float Hp
    {
        get => hp;
        set
        {
            hp = value;
            if (hp <= 0)
            {
                hp = 0;
                isDead = true;
                Dead();
            }
            
            // 关闭animator
            if (animator.enabled && hp != data.maxHp)
            {
                animator.enabled = false;
            }
            // 更改萝卜图片
            spriteRenderer.sprite = sprites[(int)hp];
            // 更改血量数值
            textMeshPro.text = hp.ToString();
        }
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spriteRenderer.sprite = sprites[3];
        }
    }

    public override void Wound(int woundHp)
    {
        Hp -= woundHp;
    }

    protected override void Dead()
    {
        // 回收
        GameManager.Instance.PoolManager.PushObject(gameObject);
        // 萝卜死亡触发游戏结束
        GameFacade.Instance.SendNotification(NotificationName.Game.CARROT_DEAD);
    }
    
    /// <summary>
    /// 被点击回调
    /// </summary>
    public void OnMouseDown()
    {
        // 播放Idle动画
        animator.SetTrigger("Idle");
    }

    #region 缓存池回收回调

    public override void OnPush()
    {
        StopCoroutine(idleAnimaCoroutine);
        // 停止所有动画
        animator.enabled = false;
        // 恢复萝卜的图片
        spriteRenderer.sprite = sprites[10];
        // 移除Mediator
        GameFacade.Instance.RemoveMediator(nameof(CarrotMediator));
    }

    public override void OnGet()
    {
        // 注册Mediator
        GameFacade.Instance.RegisterMediator(new CarrotMediator(this));
        // 刷新血量
        Hp = data.maxHp;
        isDead = false;
        // 
        animator.enabled = true;
        // 重新开启动画协程
        idleAnimaCoroutine = StartCoroutine(IdleAnimaCoroutine());
    }

    #endregion
    
    /// <summary>
    /// 定时播放Idle动画协程
    /// </summary>
    /// <returns></returns>
    private IEnumerator IdleAnimaCoroutine()
    {
        // 满血才播放动画
        while (true)
        {
            yield return new WaitForSeconds(5f);
            
            if (Hp == data.maxHp)
            {
                animator.SetTrigger("Idle");
            }
        }
    }
}

