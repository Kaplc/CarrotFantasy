using System;
using System.Collections;
using System.Collections.Generic;
using Script.FrameWork.MusicManager;
using UnityEngine;

public class GameManager : BaseMonoSingleton<GameManager>
{
    public PoolManager PoolManager => PoolManager.Instance;
    public BinaryManager BinaryManager => BinaryManager.Instance;
    public MusicManger MusicManager => MusicManger.Instance;
    public EventCenter EventCenter => EventCenter.Instance;

    [HideInInspector] public int nowBigLevelId; // 大关卡id
    [HideInInspector] public int nowLevelId; // 小关卡id

    public LevelData nowLevelData; // 当前Level数据
    public Dictionary<int, MonsterData> monsterData = new Dictionary<int, MonsterData>(); // 当前关卡所有怪物数据
    public Map map;
    public Spawner spawner;
    public bool isPause; // 暂停标识

    protected override void Awake()
    {
        base.Awake();
        GameFacade.Instance.SendNotification(NotificationName.INIT);
        DontDestroyOnLoad(gameObject);

        // 发送初始化完成的通知
        GameFacade.Instance.SendNotification(NotificationName.INIT_END);
    }

    #region 重新封装map的方法

    /// <summary>
    /// 索引获取格子
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public Cell GetCell(int x, int y)
    {
        return map.GetCell(x,y);
    }

    /// <summary>
    /// 世界坐标获取格子
    /// </summary>
    /// <param name="worldPos"></param>
    /// <returns></returns>
    public Cell GetCell(Vector3 worldPos)
    {
        return map.GetCell(worldPos);
    }

    /// <summary>
    /// 返回格子中心点坐标坐标
    /// </summary>
    /// <param name="cell"></param>
    /// <returns></returns>
    public Vector3 GetCellCenterPos(Cell cell)
    {
        return map.GetCellCenterPos(cell);
    }
    #endregion

    #region 游戏相关
    
    /// <summary>
    /// 初始化游戏
    /// </summary>
    public void GameInit()
    {
        isPause = false;
        // 创建地图
        map = Instantiate(Resources.Load<GameObject>("Prefabs/Map")).GetComponent<Map>();
        // 地图初始化
        map.InitMap();
        // 创建出怪器
        spawner = Instantiate(Resources.Load<GameObject>("Prefabs/Spawner")).GetComponent<Spawner>();
        // 注册事件
        EventCenter.AddEventListener(NotificationName.JUDGING_WIN, JudgingWin);
        EventCenter.AddEventListener(NotificationName.GAME_OVER, GameOver);
    }
    
    /// <summary>
    /// 游戏退出
    /// </summary>
    public void GameExit()
    {
        // 回收萝卜和怪物
        map.carrot.OnPush();
        spawner.OnPushAllMonster();
        // 清空事件中心
        EventCenter.ClearAllEvent();
    }

    #endregion

    #region 事件相关
    
    /// <summary>
    /// 判断是否胜利
    /// </summary>
    private void JudgingWin()
    {
        // 1.出怪完成 2.萝卜没死 3.怪物全部死亡
        if (map.carrot.isDead == false)
        {
            GameFacade.Instance.SendNotification(NotificationName.SHOW_WINPANEL);
        }
    }

    private void GameOver()
    {
        // 显示失败面板
        GameFacade.Instance.SendNotification(NotificationName.SHOW_LOSEPANEL);
    }

    #endregion
    
}