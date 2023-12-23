using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : BaseRole, IPoolObject
{
    public MonsterData data;
    
    private int hp;
    public int pathIndex;
    
    public Cell nextCell;
    private Animator animator;

    #region 属性
    
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
                GameFacade.Instance.SendNotification(NotificationName.CANEL_COLLECTINGFIRES, this);
                // 播放死亡动画
                animator.SetBool("Dead", true);
            }
        }
    }

    #endregion

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        
        // 判断是否到达目标格子
        if (Vector3.Distance(Map.GetCellCenterPos(nextCell), transform.position) < 0.1f && isDead == false)
        {
            // 到达终点格子, 触发死亡方法
            if (pathIndex == GameManager.Instance.nowLevelData.mapData.pathList.Count-1)
            {
                // 触发怪物到达终点事件
                GameFacade.Instance.SendNotification(NotificationName.REACH_ENDPOINT, data.atk);
                // 怪物死亡
                Hp = 0;
                return;
            }
            
            // 到达换下个目标格子
            pathIndex++;
            pathIndex = Mathf.Clamp(pathIndex, 0, GameManager.Instance.nowLevelData.mapData.pathList.Count-1);
            nextCell = GameManager.Instance.nowLevelData.mapData.pathList[pathIndex];
        }
    }
    
    /// <summary>
    /// 点击触发集火
    /// </summary>
    private void OnMouseDown()
    {
        // 将自己的位置信息传出
        GameFacade.Instance.SendNotification(NotificationName.SET_COLLECTINGFIRES, this);
    }

    private void Move()
    {
        if(GameManager.Instance.Pause || isDead) return;
        
        Vector3 dir = Map.GetCellCenterPos(nextCell) - transform.position;
        dir.Normalize();
        // 移动
        transform.Translate(dir * (Time.deltaTime * Speed));
    }

    public override void Wound(int woundHp)
    {
        Hp -= woundHp;
    }

    protected override void Dead()
    {
        // 回收
        GameManager.Instance.PoolManager.PushObject(gameObject);
        // 触发怪物死亡
        GameFacade.Instance.SendNotification(NotificationName.MONSTER_DEAD);
    }

    public override void OnPush()
    {
        // 清空数据
        nextCell = null;
    }
    
    /// <summary>
    /// 每次从缓存池取出初始化数据
    /// </summary>
    public override void OnGet()
    {
        // 位置设置在起点
        transform.position = Map.GetCellCenterPos(GameManager.Instance.nowLevelData.mapData.pathList[0]);
        // 设置第一个目标格子
        nextCell = GameManager.Instance.nowLevelData.mapData.pathList[0];
        pathIndex = 0;
        // 刷新血
        hp = data.maxHp;
        // 还原动画
        animator.SetBool("Dead", false);
        
        isDead = false;
    }
}
