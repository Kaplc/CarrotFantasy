using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        SendNotification(NotificationName.SHOW_INITPANEL);
        // 初始化Controller
        SendNotification(NotificationName.INIT_GAMEMANAGERCONTROLLER);
        SendNotification(NotificationName.INIT_GAMEDATAPROXY);
        SendNotification(NotificationName.INIT_SPAWNERCONTROLLER);
        // 初始化游戏数据
        SendNotification(NotificationName.INIT_GAMEDATA);
        
        SendNotification(NotificationName.INIT_END);
    }
}
