using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : BaseRole, IPoolObject
{
    public MonsterData data;
    
    private int hp;
    public int pathIndex = 0;
    
    public Cell nextCell;
    private Animator animator;

    #region 属性

    public int ID => data.id;
    private float Speed => data.speed;
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
        }
    }

    #endregion
    
    
    private void Update()
    {
        Move();
        
        // 判断是否到达目标格子
        if (Vector3.Distance(GameManager.Instance.GetCellCenterPos(nextCell), transform.position) < 0.1f && isDead == false)
        {
            // 到达终点格子, 触发死亡方法
            if (pathIndex == GameManager.Instance.nowLevelData.mapData.pathList.Count-1)
            {
                // 触发怪物到达终点事件
                GameManager.Instance.EventCenter.TriggerEvent<int>(NotificationName.REACH_ENDPOINT, data.atk);
                Dead();
                return;
            }
            
            // 到达换下个目标格子
            pathIndex++;
            pathIndex = Mathf.Clamp(pathIndex, 0, GameManager.Instance.nowLevelData.mapData.pathList.Count-1);
            nextCell = GameManager.Instance.nowLevelData.mapData.pathList[pathIndex];
        }
    }

    private void Move()
    {
        Vector3 dir = GameManager.Instance.GetCellCenterPos(nextCell) - transform.position;
        dir.Normalize();
        // 移动
        transform.Translate(dir * (Time.deltaTime * Speed));
    }

    protected override void Wound(int woundHp)
    {
        Hp -= woundHp;
    }

    protected override void Dead()
    {
        // 回收
        OnPush();
    }

    public override void OnPush()
    {
        // 清空数据
        nextCell = null;
        pathIndex = 0;
        isDead = false;
        
        // 回收
        GameManager.Instance.PoolManager.PushObject(gameObject);
    }
    
    /// <summary>
    /// // 每次从缓存池取出初始化数据
    /// </summary>
    public override void OnGet()
    {
        // 位置设置在起点
        transform.position = GameManager.Instance.GetCellCenterPos(GameManager.Instance.nowLevelData.mapData.pathList[0]);
        // 设置第一个目标格子
        nextCell = GameManager.Instance.nowLevelData.mapData.pathList[0];
        // 刷新血
        hp = data.maxHp;
    }
}
