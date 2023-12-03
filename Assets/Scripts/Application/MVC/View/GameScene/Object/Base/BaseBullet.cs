using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBullet : MonoBehaviour, IPoolObject
{
    public int atk;
    public bool active;
    
    public BulletData data;
    public Monster target;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Flying();
        
        // 根据距离判断是否击中
        if (Vector3.Distance(transform.position, target.transform.position) < 0.3f && active)
        {
            // 只扣血一次
            active = false;
            // 怪物扣血
            target.Wound(atk + data.baseAtk);
            // 播放爆炸动画
            animator.SetTrigger("Explode");
        }
    }

    protected virtual void Flying()
    {
        if (target)
        {
            transform.LookAt(target.transform);
            transform.Translate(transform.forward * (Time.deltaTime * data.speed), Space.World);
            
            // 目标死亡立刻回收
            if (target.isDead)
            {
                GameManager.Instance.PoolManager.PushObject(gameObject);
            }
        }
    }
    
    /// <summary>
    /// 爆炸动画完毕回调
    /// </summary>
    protected virtual void Explode()
    {
        // 回收子弹
        DontDestroyOnLoad(gameObject);
        GameManager.Instance.PoolManager.PushObject(gameObject);
    }
    
    public virtual void OnPush()
    {
        target = null;
    }

    public virtual void OnGet()
    {
        // 播放飞行动画
        animator.Play("Flying");
        // 重置
        active = true;
    }
}