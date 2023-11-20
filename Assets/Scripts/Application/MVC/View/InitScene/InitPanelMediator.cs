using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using UnityEngine;

public class InitPanelMediator : Mediator
{
    public new static string NAME = "InitPanelMediator";
    
    public InitPanel Panel
    {
        get=>ViewComponent as InitPanel;
        set
        {
            ViewComponent = value;
            (ViewComponent as InitPanel)?.BindMediator(this);
        }
    }
    
    public InitPanelMediator() : base(NAME)
    {
        
    }

    public override string[] ListNotificationInterests()
    {
        return new string[]
        {
            NotificationName.INIT,
            NotificationName.INIT_END
        };
    }

    public override void HandleNotification(INotification notification)
    {
        base.HandleNotification(notification);

        switch (notification.Name)
        {
            case NotificationName.INIT:
                Panel = UIManager.Instance.Show<InitPanel>(false);
                break;
            case NotificationName.INIT_END:
                UIManager.Instance.Hide<InitPanel>(false);
                break;
        }
        
    }
}
