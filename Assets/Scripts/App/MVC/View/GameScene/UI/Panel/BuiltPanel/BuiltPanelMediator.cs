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
            NotificationName.UI.SHOW_CREATEPANEL,
            NotificationName.UI.SHOW_UPGRADEPANEL,
            NotificationName.UI.HIDE_BUILTPANEL,
            NotificationName.UI.SHOW_CANTBUILTICON
        };
    }

    public override void HandleNotification(INotification notification)
    {
        base.HandleNotification(notification);

        switch (notification.Name)
        {
            case NotificationName.UI.SHOW_CREATEPANEL:

                ViewComponent = UIManager.Instance.Show<BuiltPanel>();
                
                CreatePanelArgsBody createPanelArgsBody = notification.Body as CreatePanelArgsBody;
                Panel.ShowCreatePanel(
                    createPanelArgsBody.createPos, 
                    createPanelArgsBody.towersDataDic, 
                    createPanelArgsBody.showDir
                );
                SendNotification(NotificationName.Game.OPENED_BUILTPANEL, true);
                break;
            case NotificationName.UI.SHOW_UPGRADEPANEL:

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
                SendNotification(NotificationName.Game.OPENED_BUILTPANEL, true);
                break;
            case NotificationName.UI.HIDE_BUILTPANEL:
                UIManager.Instance.Hide<BuiltPanel>(false);
                ViewComponent = null;
                SendNotification(NotificationName.Game.OPENED_BUILTPANEL, false);
                break;
            case NotificationName.UI.SHOW_CANTBUILTICON:
                ViewComponent = UIManager.Instance.Show<BuiltPanel>();
                Panel.ShowCantBuiltIcon((Vector3)notification.Body);
                break;
        }
    }
}