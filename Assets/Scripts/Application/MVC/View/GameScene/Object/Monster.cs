using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using DG.Tweening;
using UnityEngine;

public class Monster : BaseRole, IPoolObject
{
    public MonsterData data;

    private int hp;
    public int pathIndex;
    private float lastWoundTime; // 上次扣血时间

    public Cell nextCell;
    private Animator animator;
    public Transform signFather; // 集火标志父对象
    public Transform hpImageBg; // 血条背景图片
    public Transform hpImageFg; // 血条前景图片

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
                // 取消集火
                GameFacade.Instance.SendNotification(NotificationName.Game.CANEL_COLLECTINGFIRES, this);
                // 加钱
                GameFacade.Instance.SendNotification(NotificationName.Game.UPDATE_MONEY, +data.baseMoney);
                // 生成加钱UI
                AddMoneyTips addMoneyTips = GameManager.Instance.FactoryManager.UIControlFactory.CreateControl("AddMoneyTips").GetComponent<AddMoneyTips>();
                addMoneyTips.textMeshPro.text = "+" +data.baseMoney;
                addMoneyTips.transform.position = transform.position;
                addMoneyTips.transform.DOMoveY( addMoneyTips.transform.position.y + 2f, 0.5f); // 上移动画
                // 播放死亡动画
                animator.SetBool("Dead", true);
            }

            // 更新血条图片
            hpImageBg.gameObject.SetActive(true);
            hpImageFg.localScale = new Vector3(hp / (data.maxHp * 1f), 1, 1);

            lastWoundTime = Time.realtimeSinceStartup;
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
        if (Time.realtimeSinceStartup - lastWoundTime > 2 || isDead)
        {
            hpImageBg.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 点击触发集火
    /// </summary>
    private void OnMouseDown()
    {
        // 将自己的位置信息传出
        GameFacade.Instance.SendNotification(NotificationName.Game.SET_COLLECTINGFIRES, this);
    }

    private void Move()
    {
        if (GameManager.Instance.Pause || isDead) return;

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
        GameFacade.Instance.SendNotification(NotificationName.Game.MONSTER_DEAD);
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