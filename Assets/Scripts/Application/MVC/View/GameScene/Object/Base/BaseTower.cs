using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class BaseTower : MonoBehaviour, IPoolObject
{
    public TowerData data;
    public int ID => data.id;
    public int Atk => data.atk;
    public int RotaSpeed => data.rotaSpeed;
    public int level;
    
    public Animator animator;
    public List<RuntimeAnimatorController> controllers;
    public Monster target;

    public abstract void Attack();
    
    /// <summary>
    /// 炮塔升级
    /// </summary>
    public virtual void UpGrade()
    {
        level++;
        
        // 切换状态机
        animator.runtimeAnimatorController = controllers[level];
    }

    public abstract void OnGet();
    
    public abstract void OnPush();
}
