using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using UnityEngine;

public class SelectPanelMediator : Mediator
{
    public static string Name = "SelectPanelMediator";

    public SelectPanel Panel
    {
        get => ViewComponent as SelectPanel;
        set
        {
            ViewComponent = value;
            (ViewComponent as SelectPanel)?.BindMediator(this);
        }
    }

    public SelectPanelMediator() : base(Name)
    {
    }

    public override string[] ListNotificationInterests()
    {
        return new string[]
        {
            NotificationName.PRESS_ADVENTURE
        };
    }

    public override void HandleNotification(INotification notification)
    {
        base.HandleNotification(notification);

        switch (notification.Name)
        {
            case NotificationName.PRESS_ADVENTURE:
                Panel = UIManager.Instance.Show<SelectPanel>();
                break;
        }
    }
}