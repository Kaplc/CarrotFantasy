﻿using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using UnityEngine;

public class GamePanelMediator : Mediator
{
    public static new string NAME = "GamePanelMediator";

    public GamePanel Panel
    {
        get=>ViewComponent as GamePanel;
        set
        {
            ViewComponent = value;
            (ViewComponent as GamePanel)?.BindMediator(this);
        }
    }
    
    public GamePanelMediator() : base(NAME)
    {
        
    }

    public override string[] ListNotificationInterests()
    {
        return new string[]
        {
            NotificationName.START_GAME,
            NotificationName.SELECT_LEVEL
        };
    }

    public override void HandleNotification(INotification notification)
    {
        base.HandleNotification(notification);

        switch (notification.Name)
        {
            case NotificationName.START_GAME:
                Panel = UIManager.Instance.Show<GamePanel>(false);
                break;
            case NotificationName.SELECT_LEVEL:
                UIManager.Instance.Hide<GamePanel>(false);
                break;
        }
        
        

    }
}
