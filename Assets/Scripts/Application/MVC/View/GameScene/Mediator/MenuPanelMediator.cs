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
            NotificationName.SHOW_MENUPANEL
        };
    }

    public override void HandleNotification(INotification notification)
    {
        base.HandleNotification(notification);
        Panel = UIManager.Instance.Show<MenuPanel>(false);
    }
}
