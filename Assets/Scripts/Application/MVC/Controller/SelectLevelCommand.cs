using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// 菜单点击选择关卡上执行的控制器
/// </summary>
public class SelectLevelCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        
        ZFrameWorkSceneManager.Instance.LoadSceneAsync("2.BeginScene", () =>
        {
            SendNotification(NotificationName.SHOW_SELECTLEVELPANEL);
        });
    }
}
