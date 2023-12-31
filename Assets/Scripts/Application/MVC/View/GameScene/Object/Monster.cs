﻿using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Monster : BaseRole, IPoolObject
{
    public MonsterData data;

    public float hp;
    private int pathIndex;
    protected float lastWoundTime; // 上次扣血时间
    private float growth = 1.0f; // 成长系数
    public float speed;
        

    private Cell nextCell;
    public Animator animator;
    public Transform hpImageBg; // 血条背景图片
    public Transform hpImageFg; // 血条前景图片
    public Transform signFather; // 集火标记父对象

    #region 属性

    private float Hp
    {
        get => hp;
        set
        {
            hp = value;
            if (hp <= 0)
            {
                hp = 0;
                isDead = true;
                // 取消集火
                GameFacade.Instance.SendNotification(NotificationName.Game.CANEL_COLLECTINGFIRES, this);
                // 加钱
                GameFacade.Instance.SendNotification(NotificationName.Game.UPDATE_MONEY, +(int)(data.baseMoney * growth));
                // 生成加钱UI
                AddMoneyTips addMoneyTips = GameManager.Instance.FactoryManager.UIControlFactory.CreateControl("AddMoneyTips")
                    .GetComponent<AddMoneyTips>();
                addMoneyTips.textMeshPro.text = "+" + (int)(data.baseMoney * growth);
                addMoneyTips.transform.position = transform.position;
                addMoneyTips.transform.DOMoveY(addMoneyTips.transform.position.y + 2f, 0.5f); // 上移动画
                
                // 移除所有Buff
                ClearAllBuffs();
                
                // 播放死亡动画
                animator.SetBool("Dead", true);
                // 播放死亡音效
                (string, float, bool) soundData;
                soundData.Item1 = "Music/MonsterDead";
                soundData.Item2 = 1;
                soundData.Item3 = false;
                GameFacade.Instance.SendNotification(NotificationName.Game.PLAY_SOUND, soundData);
            }
            
            // 更新血条图片
            hpImageBg.gameObject.SetActive(true);
            hpImageFg.localScale = new Vector3(hp / (data.maxHp * (growth == 0 ? 1 : growth) ), 1, 1);
            // 记录显示血条的时间
            lastWoundTime = Time.time;
        }
    }

    public float Growth
    {
        get => growth;
        set
        {
            growth = value;
            // 修改成长系数时自动修改属性值
            hp *= growth;
        }
    }

    #endregion

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        Move();

        // 判断是否到达目标格子
        if (Vector3.Distance(Map.GetCellCenterPos(nextCell), transform.position) < 0.1f && isDead == false)
        {
            // 到达终点格子, 触发死亡方法
            if (pathIndex == GameManager.Instance.nowLevelData.mapData.pathList.Count - 1)
            {
                // 触发怪物到达终点事件
                GameFacade.Instance.SendNotification(NotificationName.Game.REACH_ENDPOINT, data.atk);
                // 怪物死亡
                Hp = 0;
                return;
            }

            // 到达换下个目标格子
            pathIndex++;
            pathIndex = Mathf.Clamp(pathIndex, 0, GameManager.Instance.nowLevelData.mapData.pathList.Count - 1);
            nextCell = GameManager.Instance.nowLevelData.mapData.pathList[pathIndex];
        }

        // 超过2秒没受到伤害或怪物死亡隐藏血条
        if (Time.time - lastWoundTime > 2 || isDead)
        {
            hpImageBg.gameObject.SetActive(false);
        }
    }

    private void ClearAllBuffs()
    {
        // 移除身上所有Buff
        BaseBuffEffect[] buffEffects = transform.GetComponentsInChildren<BaseBuffEffect>();
        for (int i = 0; i < buffEffects.Length; i++)
        {
            GameManager.Instance.PoolManager.PushObject(buffEffects[i].gameObject); ;
        }
        GameFacade.Instance.SendNotification(NotificationName.Game.REMOVE_BUFFS, this);
    }

    /// <summary>
    /// 点击触发集火
    /// </summary>
    private void OnMouseDown()
    {
        // 射线检测判断是否被UI遮挡
        GraphicRaycaster gr = UIManager.Instance.canvas.GetComponent<GraphicRaycaster>();
        PointerEventData eventData = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
        List<RaycastResult> results = new List<RaycastResult>();
        gr.Raycast(eventData, results);
        // 被显示范围的Ui遮挡除外
        if (results.Count > 0 && results[0].gameObject.name != "ImageAttackRange") return;
        // 将自己的位置信息传出
        GameFacade.Instance.SendNotification(NotificationName.Game.SET_COLLECTINGFIRES, this);
    }

    private void Move()
    {
        if (GameManager.Instance.Pause || isDead) return;

        Vector3 dir = Map.GetCellCenterPos(nextCell) - transform.position;
        dir.Normalize();
        // 移动
        transform.Translate(dir * (Time.deltaTime * speed));
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
        GameFacade.Instance.SendNotification(NotificationName.Game.MONSTER_DEAD);
        // 记录到统计信息
        GameFacade.Instance.SendNotification(NotificationName.Data.CHANGE_KILLMONSTER_COUNT, +1);
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
        // 刷新属性
        hp = data.maxHp;
        speed = data.speed;
        
        // 还原动画
        animator.SetBool("Dead", false);

        isDead = false;
    }
}