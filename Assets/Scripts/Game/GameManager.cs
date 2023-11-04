using System;
using System.Collections;
using System.Collections.Generic;
using Script.FrameWork.MusicManager;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PoolManager poolManager;
    public BinaryManager binaryManager;
    public MusicManger musicManger;

    private void Awake()
    {
        GameFacade.Instance.SendNotification(NotificationName.INIT);
        
        poolManager = PoolManager.Instance;
        binaryManager = BinaryManager.Instance;
        musicManger = MusicManger.Instance;
        
        DontDestroyOnLoad(gameObject);
        
        // 发送初始化完成的通知
        GameFacade.Instance.SendNotification(NotificationName.INIT_END);
    }
}
