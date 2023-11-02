using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using UnityEngine;

public class BeginPanelMediator : Mediator
{
    public static string Name = "BeginPanelVIew";

    // 命名
    public BeginPanelMediator() : base(Name)
    {
        // // 与view绑定
        // ViewComponent = UIManager.Instance.GetPanel<BeginPanel>();
    }
    
    // view要监听的事件列表
    public override string[] ListNotificationInterests()
    {
        // 返回事件名数组表示要监听的事件
        return new string[]
        {
            NotificationName.ENTER_BEGIN_SCENE
        };
    }
    
    // 执行监听的事件
    public override void HandleNotification(INotification notification)
    {
        // 根据不同的事件执行不同的逻辑
        switch (notification.Name)
        {
            case NotificationName.ENTER_BEGIN_SCENE:
                // 显示ui并绑定view
                ViewComponent = UIManager.Instance.Show<BeginPanel>(EUILayerType.Bottom, false);
                break;
        }
    }
}
