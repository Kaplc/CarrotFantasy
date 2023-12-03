﻿using System.Collections;
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
            NotificationName.SHOW_INITPANEL,
            NotificationName.HIDE_INIPANEL
        };
    }

    public override void HandleNotification(INotification notification)
    {
        base.HandleNotification(notification);

        switch (notification.Name)
        {
            case NotificationName.SHOW_INITPANEL:
                Panel = UIManager.Instance.Show<InitPanel>(false);
                break;
            case NotificationName.HIDE_INIPANEL:
                UIManager.Instance.Hide<InitPanel>(false);
                break;
        }
        
    }
}
