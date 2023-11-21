using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityEngine;


public class LoadDataCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);

        switch (notification.Name)
        {
            case NotificationName.LOADED_LEVELDATA:
                // 注入数据
                LevelDataBody body = notification.Body as LevelDataBody;
                GameManager.Instance.monsterData = body.monsterData;
                GameManager.Instance.nowLevelData = body.levelData;
                break;
        }
    }
}