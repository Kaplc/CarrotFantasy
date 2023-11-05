using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using UnityEngine;

public class LosePanelMediator : Mediator
{
    public static new string NAME = "LosePanelMediator";

    public LosePanel Panel
    {
        get => ViewComponent as LosePanel;
        set
        {
            ViewComponent = value;
            (ViewComponent as LosePanel)?.BindMediator(this);
        }
    }

    public LosePanelMediator() : base(NAME)
    {
    }

    public override string[] ListNotificationInterests()
    {
        return new string[]
        {
            NotificationName.SHOW_LOSEPANEL
        };
    }

    public override void HandleNotification(INotification notification)
    {
        base.HandleNotification(notification);

        switch (notification.Name)
        {
            case NotificationName.SHOW_LOSEPANEL:
                Panel = UIManager.Instance.Show<LosePanel>(false);
                break;
        }
    }
}
