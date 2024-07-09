using App.Static;
using GameFramework;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;

namespace App.UI.Generic.TipsPanel
{
    public class TipsPanelMediator : Mediator
    {
        public new const string NAME = "TipsPanelMediator";

        public TipsPanelMediator() : base(NAME)
        {
        }

        public override string[] ListNotificationInterests()
        {
            return new[]
            {
                NotificationName.UI.SHOW_TIPS_PANEL
            };
        }

        public override void HandleNotification(INotification notification)
        {
            switch (notification.Name)
            {
                case NotificationName.UI.SHOW_TIPS_PANEL:
                    UIManager.Instance.Show<TipsPanel>().SetInfo(notification.Body.ToString());
                    break;
            }
        }
    }
}