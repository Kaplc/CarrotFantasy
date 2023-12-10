using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using UnityEngine;

public class LoadingPanel : BasePanel
{
    protected override void Init()
    {
        
    }
}

public class LoadingPanelMediator : Mediator
{
    public static new string NAME = "LoadingPanelMediator";

    public LoadingPanel Panel
    {
        get=>ViewComponent as LoadingPanel;
        set
        {
            ViewComponent = value;
            (ViewComponent as LoadingPanel)?.BindMediator(this);
        }
    }
    
    public LoadingPanelMediator() : base(NAME)
    {
    }

    public override string[] ListNotificationInterests()
    {
        return new string[]
        {
            NotificationName.SHOW_LOADINGPANEL,
            NotificationName.HIDE_LOADINGPANEL
        };

    }

    public override void HandleNotification(INotification notification)
    {
        base.HandleNotification(notification);

        switch (notification.Name)
        {
            case NotificationName.SHOW_LOADINGPANEL:
                Panel = UIManager.Instance.Show<LoadingPanel>(false);
                break;
            case NotificationName.HIDE_LOADINGPANEL:
                UIManager.Instance.Hide<LoadingPanel>(false);
                break;
        }
        
    }
}