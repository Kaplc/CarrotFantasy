using System;
using System.Collections;
using System.Collections.Generic;
using Script.FrameWork.MusicManager;
using UnityEngine;

public class GameManager : BaseMonoSingleton<GameManager>
{
    [HideInInspector] public PoolManager poolManager;
    [HideInInspector] public BinaryManager binaryManager;
    [HideInInspector] public MusicManger musicManger;

    [HideInInspector] public int bigMapIndex; // 大关卡的索引
    
    protected override void Awake()
    {
        base.Awake();
        
        GameFacade.Instance.SendNotification(NotificationName.INIT);

        poolManager = PoolManager.Instance;
        binaryManager = BinaryManager.Instance;
        musicManger = MusicManger.Instance;

        DontDestroyOnLoad(gameObject);

        // 发送初始化完成的通知
        GameFacade.Instance.SendNotification(NotificationName.INIT_END);
    }
}