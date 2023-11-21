using System;
using System.Collections;
using System.Collections.Generic;
using Script.FrameWork.MusicManager;
using UnityEngine;

public class GameManager : BaseMonoSingleton<GameManager>
{
    [HideInInspector] public PoolManager PoolManager => PoolManager.Instance;
    [HideInInspector] public BinaryManager BinaryManager => BinaryManager.Instance;
    [HideInInspector] public MusicManger MusicManager => MusicManger.Instance;
    public EventCenter EventCenter => EventCenter.Instance;

    [HideInInspector] public int nowBigLevelId; // 大关卡id
    [HideInInspector] public int nowLevelId; // 小关卡id

    public LevelData nowLevelData; // 当前Level数据
    public Dictionary<int, RoleData> monsterData = new Dictionary<int, RoleData>(); // 当前关卡所有怪物数据

    protected override void Awake()
    {
        base.Awake();
        
        GameFacade.Instance.SendNotification(NotificationName.INIT);
        

        DontDestroyOnLoad(gameObject);

        // 发送初始化完成的通知
        GameFacade.Instance.SendNotification(NotificationName.INIT_END);
    }
}