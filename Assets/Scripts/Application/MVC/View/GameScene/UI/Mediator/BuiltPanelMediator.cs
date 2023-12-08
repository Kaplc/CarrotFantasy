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
            NotificationName.SHOW_CREATEPANEL,
            NotificationName.SHOW_UPGRADEPANEL,
            NotificationName.HIDE_BUILTPANEL,
        };
    }

    public override void HandleNotification(INotification notification)
    {
        base.HandleNotification(notification);

        switch (notification.Name)
        {
            case NotificationName.SHOW_CREATEPANEL:

                ViewComponent = UIManager.Instance.Show<BuiltPanel>();
                
                CreatePanelArgsBody createPanelArgsBody = notification.Body as CreatePanelArgsBody;
                Panel.ShowCreatePanel(
                    createPanelArgsBody.createPos, 
                    createPanelArgsBody.towersDataDic, 
                    createPanelArgsBody.showDir
                    );
                
                break;
            case NotificationName.SHOW_UPGRADEPANEL:

                ViewComponent = UIManager.Instance.Show<BuiltPanel>();
                UpGradeTowerArgsBody upGradeTowerArgsBody = notification.Body as UpGradeTowerArgsBody;
                Panel.ShowUpGradePanel(
                    upGradeTowerArgsBody.createPos,
                    upGradeTowerArgsBody.icon, 
                    upGradeTowerArgsBody.upGradeMoney, 
                    upGradeTowerArgsBody.sellMoney,
                    upGradeTowerArgsBody.attackRange, 
                    upGradeTowerArgsBody.showDir
                    );
                break;
            case NotificationName.HIDE_BUILTPANEL:
                UIManager.Instance.Hide<BuiltPanel>(false);
                ViewComponent = null;
                break;
        }
    }
}