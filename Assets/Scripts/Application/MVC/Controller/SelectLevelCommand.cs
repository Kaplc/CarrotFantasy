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

        SceneManager.LoadScene("2.BeginScene");
        
        // 重新选择关卡则清空缓存池
        GameManager.Instance.PoolManager.Clear();
        // 清空事件中心
        GameManager.Instance.EventCenter.ClearAllEvent();
        
        SendNotification(NotificationName.SHOW_SELECTLEVELPANEL);
    }
}
