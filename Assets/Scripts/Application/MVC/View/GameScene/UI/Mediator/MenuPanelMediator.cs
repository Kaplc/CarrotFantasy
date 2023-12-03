using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using UnityEngine;

public class MenuPanelMediator : Mediator
{
    public static new string NAME = "MenuPanelMediator";

    public MenuPanel Panel
    {
        get=>ViewComponent as MenuPanel;
        set
        {
            ViewComponent = value;
            (ViewComponent as MenuPanel)?.BindMediator(this);
        }
    }
    
    public MenuPanelMediator() : base(NAME)
    {
    }

    public override string[] ListNotificationInterests()
    {
        return new string[]
        {
            NotificationName.SHOW_MENUPANEL,
            NotificationName.HIDE_MENUPANEL
        };
    }

    public override void HandleNotification(INotification notification)
    {
        base.HandleNotification(notification);
        switch (notification.Name)
        {
            case NotificationName.SHOW_MENUPANEL:
                Panel = UIManager.Instance.Show<MenuPanel>(false);
                // 暂停游戏
                SendNotification(NotificationName.PAUSE_GAME);
                
                SendNotification(NotificationName.ALLOW_CLICKCELL, false);
                break;
            case NotificationName.HIDE_MENUPANEL:
                UIManager.Instance.Hide<MenuPanel>(false);
                break;
        }
        
    }
}
