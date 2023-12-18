using System;
using System.Collections;
using System.Collections.Generic;
using Script.FrameWork.MusicManager;
using UnityEngine;

public class GameManager : BaseMonoSingleton<GameManager>
{
    public PoolManager PoolManager => PoolManager.Instance;
    public BinaryManager BinaryManager => BinaryManager.Instance;
    public PlayerData playerData;
    public FactoryManager FactoryManager => FactoryManager.Instance;

    private bool pause; // 暂停标识
    private bool stop; // 停止标识
    private float pauseTime; // 暂停时间
    public int nowBigLevelId; // 大关卡id
    public int money; // 金钱
    public bool openedBuiltPanel; // 建造面板已打开

    public bool Pause
    {
        get => pause;
        private set
        {
            pause = value;
            if (value)
            {
                // 记录暂停时间
                pauseTime = Time.realtimeSinceStartup;
            }
        }
    }
    public float PauseTime => pauseTime;
    public bool Stop => stop;

    public LevelData nowLevelData; // 当前Level数据
    public Map map;
    public Spawner spawner;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        GameFacade.Instance.SendNotification(NotificationName.INIT);
    }

    #region 游戏相关

    /// <summary>
    /// 初始化游戏
    /// </summary>
    public void InitGame()
    {
        stop = true;
        Pause = true;
        // 创建地图
        map = Instantiate(Resources.Load<GameObject>("Prefabs/Map")).GetComponent<Map>();
        // 地图初始化
        map.InitMap();
        // 创建出怪器
        spawner = Instantiate(Resources.Load<GameObject>("Prefabs/Spawner")).GetComponent<Spawner>();
        // 创建萝卜
        spawner.CreateCarrot();
        // 创建起点路牌
        spawner.CreateStartBrand();
        // 刷新钱
        money = nowLevelData.money;
    }

    /// <summary>
    /// 读秒结束真正开始游戏
    /// </summary>
    public void StartGame()
    {
        Pause = false;
        stop = false;
    }

    /// <summary>
    /// 游戏退出
    /// </summary>
    public void ExitGame()
    {
        // 回收萝卜和怪物
        PoolManager.PushObject(spawner.carrot.gameObject);
        spawner.OnPushAllMonster();
    }

    /// <summary>
    /// 游戏暂停
    /// </summary>
    public void PauseGame()
    {
        Pause = true;
    }

    /// <summary>
    /// 停止游戏
    /// </summary>
    public void StopGame()
    {
        stop = true;
        // 暂停且禁止鼠标检测
        Pause = true;
    }

    /// <summary>
    /// 游戏继续
    /// </summary>
    public void ContinueGame()
    {
        Pause = false;
        stop = false;
    }

    #endregion

    /// <summary>
    /// 判断是否胜利
    /// </summary>
    public void JudgingWin()
    {
        // 1.出怪完成
        if (!spawner.spawnedComplete) return;

        // 2.萝卜没死
        if (spawner.carrot.isDead) return;

        // 3.怪物全部死亡
        for (int i = 0; i < spawner.monsters.Count; i++)
        {
            // 有一个没死亡都无效
            if (spawner.monsters[i].isDead == false)
            {
                return;
            }
        }

        // 执行游戏胜利逻辑
        GameWin();
    }

    /// <summary>
    /// 游戏胜利
    /// </summary>
    private void GameWin()
    {
        int hp = spawner.carrot.Hp;
        // 结算通关等级
        EPassedGrade grade;
        if (1 <= hp && hp <= 3)
        {
            // 铜
            grade = EPassedGrade.Copper;
        }else if (4 <= hp && hp <= 6)
        {
            // 银
            grade = EPassedGrade.Sliver;
        }
        else
        {
            // 金
            grade = EPassedGrade.Gold;
        }
        // 保存游戏进度
        GameFacade.Instance.SendNotification(NotificationName.SAVE_PROCESSDATA, (nowLevelData.levelID, grade));
        // 显示胜利面板
        GameFacade.Instance.SendNotification(NotificationName.SHOW_WINPANEL,
            (
                spawner.nowWavesCount,
                nowLevelData.roundDataList.Count,
                nowLevelData.levelID,
                grade
            )
        );
    }

    /// <summary>
    /// 游戏失败
    /// </summary>
    public void GameOver()
    {
        // 停止出怪
        GameFacade.Instance.SendNotification(NotificationName.STOP_SPAWN);
        // 显示失败面板
        GameFacade.Instance.SendNotification(NotificationName.SHOW_LOSEPANEL,
            (
                spawner.nowWavesCount,
                nowLevelData.roundDataList.Count,
                nowLevelData.levelID
            )
        );
    }
}