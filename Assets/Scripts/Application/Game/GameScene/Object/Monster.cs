using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : BaseRole, IPoolObject
{
    public float Speed => data.speed;

    public MapData mapData;
    public int pathIndex = 0;
    public Cell nextCell;

    private void Update()
    {
        Move();
        
        // 判断是否到达目标格子
        // if (Vector3.Distance(GameManager.Instance.map.GetCellCenterPos(nextCell), transform.position) < 0.1f && isDead == false)
        // {
        //     // 到达终点格子, 触发死亡方法
        //     if (pathIndex == mapData.pathList.Count-1)
        //     {
        //         Dead();
        //         return;
        //     }
        //     
        //     // 到达换下个目标格子
        //     pathIndex++;
        //     pathIndex = Mathf.Clamp(pathIndex, 0, mapData.pathList.Count-1);
        //     nextCell = mapData.pathList[pathIndex];
        // }
    }

    public void Move()
    {
        // Vector3 dir = (GameManager.Instance.map.GetCellCenterPos(nextCell) - transform.position);
        // dir.Normalize();
        // 移动
        // transform.Translate(dir * (Time.deltaTime * Speed));
    }

    public override void Dead()
    {
        base.Dead();
        

        // 回收
        OnPush();
    }

    public override void OnPush()
    {
        // 清空数据
        mapData = null;
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
        // 获取当前关卡的地图路径信息
        mapData = GameManager.Instance.nowLevelData.mapData;
        // 位置设置在起点
        // transform.position = GameManager.Instance.map.GetCellCenterPos(mapData.pathList[0]);
        // 设置第一个目标格子
        nextCell = mapData.pathList[0];
        // 刷新血
        Hp = MaxHp;
    }
}
