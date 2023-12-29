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
        SendNotification(NotificationName.UI.SHOW_INITPANEL);
        
        // 初始化Controller
        SendNotification(NotificationName.Init.INIT_GAMEMANAGER_CONTROLLER);
        SendNotification(NotificationName.Init.INIT_SPAWNER_CONTROLLER);
        SendNotification(NotificationName.Init.INIT_LOADSCENE_CONTROLLER);
        // ModelController
        SendNotification(NotificationName.Init.INIT_GAMEDATAPROXY_CONTROLLER);
        SendNotification(NotificationName.Init.INIT_MUSICDATAPROXY_CONTROLLER);
        SendNotification(NotificationName.Init.INIT_PROCESSDATAPROXY_CONTROLLER);
        SendNotification(NotificationName.Init.INIT_STATICALDATAPROXY_CONTROLLER);
        
        // 初始化游戏数据
        SendNotification(NotificationName.Init.INIT_GAMEDATA);
        // 初始化完成跳转开始场景
        SendNotification(NotificationName.LoadScene.LOADSCENE_INIT_TO_BEGIN);
    }
}
