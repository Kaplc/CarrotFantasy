using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : BaseRole, IPoolObject
{
    public int hp;

    public CarrotData data;
    public List<Sprite> sprites;
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    public int Hp
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

        // 监听怪物到达终点事件
        EventCenter.Instance.AddEventListener<int>(NotificationName.REACH_ENDPOINT, Wound);
    }

    private void Start()
    {
        StartCoroutine(TimedIdleCoroutine());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spriteRenderer.sprite = sprites[3];
        }
    }

    protected override void Wound(int woundHp)
    {
        Hp -= woundHp;

        // 播放受伤动画
        animator.SetTrigger("Wound");
    }

    protected override void Dead()
    {
        GameManager.Instance.EventCenter.TriggerEvent(NotificationName.CARROT_DEAD); // 触发萝卜死亡事件
        // 显示失败面板
        GameFacade.Instance.SendNotification(NotificationName.SHOW_LOSEPANEL);
        // 回收
        OnPush();
    }

    public override void OnPush()
    {
        GameManager.Instance.PoolManager.PushObject(gameObject);
    }

    public override void OnGet()
    {
        // 刷新血量
        Hp = data.maxHp;
        isDead = false;
        // 重新开启动画协程
        StartCoroutine(TimedIdleCoroutine());
    }

    /// <summary>
    /// 定时播放Idle动画协程
    /// </summary>
    /// <returns></returns>
    private IEnumerator TimedIdleCoroutine()
    {
        // 满血才播放动画
        while (Hp == data.maxHp)
        {
            yield return new WaitForSeconds(5f);
            if (Hp == data.maxHp)
            {
                animator.SetTrigger("Idle");
            }
        }
    }
}