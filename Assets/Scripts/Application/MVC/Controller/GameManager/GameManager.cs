using System;
using System.Collections;
using System.Collections.Generic;
using Script.FrameWork.MusicManager;
using UnityEngine;

public class GameManager : BaseMonoSingleton<GameManager>
{
    public PoolManager PoolManager => PoolManager.Instance;
    public BinaryManager BinaryManager => BinaryManager.Instance;
    public FactoryManager FactoryManager => FactoryManager.Instance;
    public MusicManger MusicManger => MusicManger.Instance;

    private bool pause; // 暂停标识
    public bool stop; // 停止标识
    public float pauseTime; // 暂停时间
    public int nowBigLevelId; // 大关卡id
    public int money; // 金钱
    public bool openedBuiltPanel; // 建造面板已打开
    public bool twoSpeed; // 开启两倍速标识

    public bool Pause
    {
        get => pause;
        private set
        {
            pause = value;
            if (value)
            {
                // 记录暂停时间
                pauseTime = Time.time;
            }
        }
    }
    public bool TwoSpeed
    {
        get => twoSpeed;
        set
        {
            twoSpeed = value;

            if (value)
                Time.timeScale = 2;
            else
                Time.timeScale = 1;
        }
    }

    public LevelData nowLevelData; // 当前Level数据
    public MusicSettingData musicSettingData; // 音乐数据
    public Map map;
    public Spawner spawner;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        GameFacade.Instance.SendNotification(NotificationName.Init.INIT);
        // 初始化音乐
        GameFacade.Instance.SendNotification(NotificationName.Data.LOAD_MUSICSETTINGDATA);
        GameFacade.Instance.SendNotification(NotificationName.Game.PLAY_MUSIC);
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
        // 创建障碍物
        spawner.CreateObstacles();
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
        // 速度恢复
        TwoSpeed = false;
        // 回收所有对象
        PoolManager.PushObject(spawner.carrot.gameObject);
        spawner.OnPushAllGameObject();
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
    /// 游戏胜利
    /// </summary>
    public void GameWin()
    {
        float hp = spawner.carrot.Hp;
        // 结算通关等级
        EPassedGrade grade;
        if (1 <= hp && hp <= 3)
        {
            // 铜
            grade = EPassedGrade.Copper;
        }
        else if (4 <= hp && hp <= 6)
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
        GameFacade.Instance.SendNotification(NotificationName.Data.SAVE_PROCESSDATA, (nowBigLevelId, nowLevelData.levelID, grade));
        // 显示胜利面板
        GameFacade.Instance.SendNotification(NotificationName.UI.SHOW_WINPANEL,
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
        GameFacade.Instance.SendNotification(NotificationName.Game.STOP_SPAWN);
        // 显示失败面板
        GameFacade.Instance.SendNotification(NotificationName.UI.SHOW_LOSEPANEL,
            (
                spawner.nowWavesCount,
                nowLevelData.roundDataList.Count,
                nowLevelData.levelID
            )
        );
    }
}