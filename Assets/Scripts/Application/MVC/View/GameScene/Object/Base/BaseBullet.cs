using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBullet : MonoBehaviour, IPoolObject
{
    public int atk;
    
    public BulletData data;
    public Monster target;

    private void Update()
    {
        Flying();
        
        // 根据距离判断是否击中
        if (Vector3.Distance(transform.position, target.transform.position) < 0.3f)
        {
            // 怪物扣血
            target.Wound(atk + data.baseAtk);
            // 回收子弹
            DontDestroyOnLoad(gameObject);
            GameManager.Instance.PoolManager.PushObject(gameObject);
        }
    }

    protected virtual void Flying()
    {
        transform.LookAt(target.transform);
        transform.Translate(transform.forward * (Time.deltaTime * data.speed), Space.World);
    }

    public virtual void OnPush()
    {
        target = null;
    }

    public abstract void OnGet();
}