using System;
using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
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
        // 回收
        GameManager.Instance.PoolManager.PushObject(gameObject);
        // 萝卜死亡触发游戏结束
        GameFacade.Instance.SendNotification(NotificationName.CARROT_DEAD);
    }

    public override void OnPush()
    {
        StopCoroutine(idleAnimaCoroutine);
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
        // 重新开启动画协程
        idleAnimaCoroutine = StartCoroutine(IdleAnimaCoroutine());
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

public class CarrotMediator : Mediator
{
    public new static string NAME = nameof(CarrotMediator);

    private Carrot carrot;
    
    public CarrotMediator(Carrot carrot) : base(NAME)
    {
        this.carrot = carrot;
    }

    public override string[] ListNotificationInterests()
    {
        return new string[]
        {
            NotificationName.REACH_ENDPOINT
        };
    }

    public override void HandleNotification(INotification notification)
    {
        base.HandleNotification(notification);
        
        carrot.Wound((int)notification.Body);
    }
}