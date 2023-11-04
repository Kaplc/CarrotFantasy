using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using UnityEngine;

public class SelectLevelPanelMediator : Mediator
{
    public static new string NAME = "SelectLevelPanelMediator";

    public SelectLevelPanel LevelPanel
    {
        get => ViewComponent as SelectLevelPanel;
        set
        {
            ViewComponent = value;
            (ViewComponent as SelectLevelPanel)?.BindMediator(this);
        }
    }

    public SelectLevelPanelMediator() : base(NAME)
    {
    }

    public override string[] ListNotificationInterests()
    {
        return new string[]
        {
            NotificationName.SHOW_SELECTLEVELPANEL
        };
    }

    public override void HandleNotification(INotification notification)
    {
        base.HandleNotification(notification);

        switch (notification.Name)
        {
            case NotificationName.SHOW_SELECTLEVELPANEL:
                LevelPanel = UIManager.Instance.Show<SelectLevelPanel>(false);
                break;
        }
    }
}