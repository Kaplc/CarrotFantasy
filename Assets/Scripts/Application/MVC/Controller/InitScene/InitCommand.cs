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
        
        // 初始化GameMangerController
        GameFacade.Instance.RegisterCommand(NotificationName.INIT, () => new InitGameControllerCommand());
        // 初始化GameDataProxy
        GameFacade.Instance.RegisterCommand(NotificationName.INIT, () => new InitGameControllerCommand());
        
        SendNotification(NotificationName.INIT_END);
    }
}
