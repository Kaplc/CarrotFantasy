using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityEngine;


/// <summary>
/// 加载LevelData
/// </summary>
public class LoadDataCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        
        // 注入数据
        LevelDataBody body = notification.Body as LevelDataBody;
        GameManager.Instance.monsterData = body?.monsterData;
        GameManager.Instance.nowLevelData = body?.levelData;
        
        // 加载完成数据隐藏LoadingPanel
        SendNotification(NotificationName.HIDE_LOADINGPANEL);
        SendNotification(NotificationName.SHOW_GAMEPANEL);
    }
}

