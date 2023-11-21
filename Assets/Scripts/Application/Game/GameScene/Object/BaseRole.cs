using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseRole : MonoBehaviour, IPoolObject
{
    public RoleData data;
    public int Id => data.id;

    private int hp;

    public int Hp
    {
        get => hp;
        set
        {
            hp = value;
            
            // 每次改变hp判断是否死亡
            if (hp < 0)
            {
                hp = 0;
                Dead();
            }
        }
    }
    public int MaxHp => data.hp;

    public bool isDead;

    private void Awake()
    {
        hp = MaxHp;
    }

    public virtual void Wound(int woundHp)
    {
        Hp -= woundHp;
    } 

    public virtual void Dead()
    {
        isDead = true;
    }
    
    /// <summary>
    /// 适用于对象池回收时复原
    /// </summary>
    /// <param name="obj"></param>
    public abstract void OnPush();
    
    /// <summary>
    /// 使用对象池时初始化
    /// </summary>
    /// <returns></returns>
    public abstract void OnGet();
}
