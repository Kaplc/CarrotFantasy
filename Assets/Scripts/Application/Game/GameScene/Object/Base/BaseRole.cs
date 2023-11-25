using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseRole : MonoBehaviour, IPoolObject
{
    public bool isDead;
    
    protected abstract void Wound(int woundHp);

    protected abstract void Dead();
    
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
