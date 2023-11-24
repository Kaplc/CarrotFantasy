using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using UnityEngine;

public class BuiltPanelMediator : Mediator
{
    public static new string NAME = "BuiltPanelMediator";

    public BuiltPanel Panel => ViewComponent as BuiltPanel;

    public BuiltPanelMediator() : base(NAME)
    {
        
    }

    public override string[] ListNotificationInterests()
    {
        return new string[]
        {
            NotificationName.SHOW_BUILTPANEL,
            NotificationName.SHOW_UPGRADEPANEl
        };
    }

    public override void HandleNotification(INotification notification)
    {
        base.HandleNotification(notification);

        switch (notification.Name)
        {
            case NotificationName.SHOW_BUILTPANEL:
                ViewComponent = UIManager.Instance.Show<BuiltPanel>(false);
                Panel.ShowBuiltPanel(((BuiltTowerArgsBody)notification.Body).cellCenterPos);
                break;
            case NotificationName.SHOW_UPGRADEPANEl:
                ViewComponent = UIManager.Instance.Show<BuiltPanel>(false);
                Panel.ShowUpGradePanel();
                break;
        }
    }
}