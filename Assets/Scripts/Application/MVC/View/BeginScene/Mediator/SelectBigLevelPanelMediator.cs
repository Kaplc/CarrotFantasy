using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using UnityEngine;

public class SelectBigLevelPanelMediator : Mediator
{
    public static new string NAME = "SelectBigLevelPanelMediator";

    public SelectBigLevelPanel Panel
    {
        get => ViewComponent as SelectBigLevelPanel;
        set
        {
            ViewComponent = value;
            (ViewComponent as SelectBigLevelPanel)?.BindMediator(this);
        }
    }

    public SelectBigLevelPanelMediator() : base(NAME)
    {
    }

    public override string[] ListNotificationInterests()
    {
        return new string[]
        {
            NotificationName.SHOW_SELECTBIGLEVELPANEL
        };

    }

    public override void HandleNotification(INotification notification)
    {
        base.HandleNotification(notification);

        Panel = UIManager.Instance.Show<SelectBigLevelPanel>(false);
    }
}
