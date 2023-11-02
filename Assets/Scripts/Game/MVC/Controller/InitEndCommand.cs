using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitEndCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        
        // 初始化结束加载场景
        SceneManager.LoadScene("2.BeginScene");
        
        SendNotification(NotificationName.LOAD_SCENE, new LoadSceneBody(2));
    }
}
