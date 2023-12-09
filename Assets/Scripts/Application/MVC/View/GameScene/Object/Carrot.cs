using System;
using System.Collections;
using System.Collections.Generic;
using PureMVC.Patterns.Mediator;
using UnityEngine;

public class Carrot : BaseRole, IPoolObject
{
    public int hp;

    public CarrotData data;
    public List<Sprite> sprites; // 各血量萝卜Sprite
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public Coroutine idleAnimaCoroutine; // 待机动画协程

    private int Hp
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
            
            // 更改萝卜图片
            spriteRenderer.sprite = sprites[hp];
        }
    }

    protected void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
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

        // 播放受伤动画
        animator.SetTrigger("Wound");
    }

    protected override void Dead()
    {
        // 萝卜死亡触发游戏结束
        GameManager.Instance.EventCenter.TriggerEvent(NotificationName.GAME_OVER);
        // 回收
        GameManager.Instance.PoolManager.PushObject(gameObject);
    }

    public override void OnPush()
    {
        // 移除监听
        GameManager.Instance.EventCenter.RemoveEventListener<int>(NotificationName.REACH_ENDPOINT, Wound);
        StopCoroutine(idleAnimaCoroutine);
        
    }

    public override void OnGet()
    {
        // 刷新血量
        Hp = data.maxHp;
        isDead = false;
        // 重新开启动画协程
        idleAnimaCoroutine = StartCoroutine(IdleAnimaCoroutine());
        // 监听怪物到达终点事件
        GameManager.Instance.EventCenter.AddEventListener<int>(NotificationName.REACH_ENDPOINT, Wound);
    }

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