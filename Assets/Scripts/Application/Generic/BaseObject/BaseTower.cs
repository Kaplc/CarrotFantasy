﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class BaseTower : MonoBehaviour, IPoolObject
{
    public TowerData data;
    public int Atk => data.atkList[level];
    public int level;
    public bool attacking; // 是否正在攻击标识

    public Animator animator;
    public List<RuntimeAnimatorController> controllers;
    public Monster target; // 当前目标
    private GameObject upGradeTips;

    protected virtual void Update()
    {
        if (GameManager.Instance.Pause)
        {
            // 游戏暂停停止炮塔动画
            animator.SetBool("Attack", false);
            attacking = false;
            return;
        }

        // 查找目标
        if (target is null)
        {
            FindTargets();
        }

        if (target)
        {
            // 大于攻击距离解除锁定或打死怪物
            if (Vector3.Distance(transform.position, target.transform.position) > data.attackRangesList[level] || target.isDead)
            {
                animator.SetBool("Attack", false);
                attacking = false;
                target = null;
            }
        }

        // 攻击
        if (target && !attacking)
        {
            animator.SetBool("Attack", true);
        }

        // 集火目标
        if (GameManager.Instance.spawner.collectingFiresTarget)
        {
            CollectingFiresTarget();
        }

        // 显示升级提醒
        if (level != 2 && GameManager.Instance.money > data.prices[level + 1])
        {
            if (upGradeTips) return;

            upGradeTips = GameManager.Instance.FactoryManager.UIControlFactory.CreateControl("UpGradeTips");
            upGradeTips.transform.position = transform.position + Vector3.up;
        }
        else
        {
            if (upGradeTips)
            {
                GameManager.Instance.FactoryManager.UIControlFactory.PushControl(upGradeTips);
                upGradeTips = null;
            }
        }
    }

    /// <summary>
    /// 集火目标
    /// </summary>
    private void CollectingFiresTarget()
    {
        // 有集火目标直接锁定
        Monster monster = GameManager.Instance.spawner.collectingFiresTarget;
        float distance = Vector3.Distance(transform.position, monster.transform.position);

        // 处于攻击范围
        if (distance < data.attackRangesList[level] && !monster.isDead)
        {
            target = monster;
        }
    }

    /// <summary>
    /// 查找目标
    /// </summary>
    protected void FindTargets()
    {
        float closestDistance = 0f;
        // 查找目标
        for (int i = 0; i < GameManager.Instance.spawner.monsters.Count; i++)
        {
            Monster monster = GameManager.Instance.spawner.monsters[i];
            float distance = Vector3.Distance(transform.position, monster.transform.position);

            // 处于攻击范围
            if (distance < data.attackRangesList[level] && !monster.isDead)
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
        // 升级动画
        animator.SetTrigger("UpGrade");
    }

    public virtual void OnGet()
    {
    }

    public virtual void OnPush()
    {
        // 复原数据
        target = null;
        level = 0;
        animator.runtimeAnimatorController = controllers[0];
        // 有升级标记同时回收
        if (upGradeTips)
        {
            GameManager.Instance.FactoryManager.UIControlFactory.PushControl(upGradeTips.gameObject);
            upGradeTips = null;
        }
    }
}