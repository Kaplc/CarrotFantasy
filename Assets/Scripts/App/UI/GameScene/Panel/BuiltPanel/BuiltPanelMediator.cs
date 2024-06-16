using App.Game.Generic.NotificationBody;
using App.Static;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using UnityEngine;

namespace App.UI.GameScene.Panel.BuiltPanel
{
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
                NotificationName.UI.SHOW_CREATE_PANEL,
                NotificationName.UI.SHOW_UPGRADE_PANEL,
                NotificationName.UI.HIDE_BUILT_PANEL,
                NotificationName.UI.SHOW_CANT_BUILT_ICON
            };
        }
        
        public override void HandleNotification(INotification notification)
        {
            base.HandleNotification(notification);

            switch (notification.Name)
            {
                case NotificationName.UI.SHOW_CREATE_PANEL:

                    ViewComponent = UIManager.Instance.Show<BuiltPanel>();
                
                    CreatePanelArgsBody createPanelArgsBody = notification.Body as CreatePanelArgsBody;
                    Panel.ShowCreatePanel(
                        createPanelArgsBody.createPos, 
                        createPanelArgsBody.towersDataDic, 
                        createPanelArgsBody.showDir
                    );
                    break;
                case NotificationName.UI.SHOW_UPGRADE_PANEL:

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
                case NotificationName.UI.HIDE_BUILT_PANEL:
                    UIManager.Instance.Hide<BuiltPanel>(false);
                    ViewComponent = null;
                    break;
                case NotificationName.UI.SHOW_CANT_BUILT_ICON:
                    ViewComponent = UIManager.Instance.Show<BuiltPanel>();
                    Panel.ShowCantBuiltIcon((Vector3)notification.Body);
                    break;
            }
        }
    }
}