using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using UnityEngine;

public class SettingPanelMediator : Mediator
{
    public new static string NAME = "SettingPanelMediator";
    
    public SettingPanel Panel=>ViewComponent as SettingPanel;
    
    public SettingPanelMediator() : base(NAME)
    {
    }

    public override string[] ListNotificationInterests()
    {
        return new string[]
        {
            NotificationName.LOADED_MUSICSETTINGDATA
        };
    }

    public override void HandleNotification(INotification notification)
    {
        base.HandleNotification(notification);

        switch (notification.Name)
        {
            case NotificationName.LOADED_MUSICSETTINGDATA:
                
                break;
        }
    }
}
