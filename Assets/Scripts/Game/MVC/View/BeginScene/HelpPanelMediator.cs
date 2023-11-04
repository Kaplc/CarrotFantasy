using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using UnityEngine;

public class HelpPanelMediator : Mediator
{
    public static new string NAME = "HelpPanelMediator";

    public HelpPanel Panel
    {
        get=>ViewComponent as HelpPanel;
        set
        {
            ViewComponent = value;
            (ViewComponent as HelpPanel)?.BindMediator(this);
        }
    }
    
    public HelpPanelMediator() : base(NAME)
    {
        
    }

    public override string[] ListNotificationInterests()
    {
        return new string[]
        {
            // NotificationName.SHOW_HELPPANEL
        };
    }

    public override void HandleNotification(INotification notification)
    {
        base.HandleNotification(notification);

        switch (notification.Name)
        {
            case NotificationName.SHOW_HELPPANEL:
                Panel = UIManager.Instance.Show<HelpPanel>(false);
                break;
        }
    }
}
