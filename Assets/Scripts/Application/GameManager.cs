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
    
}