using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using UnityEngine;

public interface Bind
{
    T Panel<T>();
}

public class BeginPanelMediator : Mediator
{
    public static string Name = "BeginPanelVIew";

    public BeginPanel Panel
    {
        get => ViewComponent as BeginPanel;
        set
        {
            ViewComponent = value;
            (ViewComponent as BeginPanel)?.BindMediator(this);
        } 
    }

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
            NotificationName.INIT_END,
            NotificationName.PRESS_BACK
        };
    }
    
    // 执行监听的事件
    public override void HandleNotification(INotification notification)
    {
        // 根据不同的事件执行不同的逻辑
        switch (notification.Name)
        {
            case NotificationName.INIT_END:
                // 显示ui并双向绑定
                Panel = UIManager.Instance.Show<BeginPanel>(EUILayerType.Bottom, false);
                break;
            case NotificationName.PRESS_BACK:
                Panel = UIManager.Instance.Show<BeginPanel>();
                break;
        }
    }
}
