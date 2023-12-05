using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class BaseTower : MonoBehaviour, IPoolObject
{
    public TowerData data;
    public int ID => data.id;
    public int Atk => data.atkList[level];
    public int RotaSpeed => data.rotaSpeed;
    public float lastAtkTime;
    public float AtkCd => data.atkCd;
    public int level;

    public Animator animator;
    public List<RuntimeAnimatorController> controllers;
    public Monster target; // 当前目标

    protected virtual  void Update()
    {
        if (GameManager.Instance.Pause)
        {
            // 游戏暂停停止炮塔动画
            animator.SetBool("Attack", false);
            return;
        }
        
        // 查找目标
        if (target is null)
        {
            FindTargets();
        }
        
        
        
        if (target != null)
        {
            // 攻击
            animator.SetBool("Attack", true);
            
            // 大于攻击距离解除锁定或打死怪物
            if (Vector3.Distance(transform.position, target.transform.position) > data.attackRange || target.isDead)
            {
                animator.SetBool("Attack", false);
                target = null;
            }

            
            // if (Time.time > lastAtkTime + AtkCd)
            // {
            //     lastAtkTime = Time.time;
            //     // Attack();
            //     if (!animator.GetBool("Attack"))
            //     {
            //         
            //     }
            // }
        }
    }

    protected void FindTargets()
    {
        float closestDistance = 0f;
        // 查找目标
        for (int i = 0; i < GameManager.Instance.spawner.monsters.Count; i++)
        {
            Monster monster = GameManager.Instance.spawner.monsters[i];
            float distance = Vector3.Distance(transform.position, monster.transform.position);

            // 处于攻击范围
            if (distance < data.attackRange && !monster.isDead)
            {
                if (closestDistance == 0f) closestDistance = distance;

                if (distance <= closestDistance)
                {
                    closestDistance = distance;
                    target = monster;
                }
            }
        }
    }

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