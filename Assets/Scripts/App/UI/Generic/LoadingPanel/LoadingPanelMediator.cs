using App.Static;
using GameFramework;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;

namespace App.UI.Generic.LoadingPanel
{
    public class LoadingPanelMediator : Mediator
    {
        public new static string NAME = "LoadingPanelMediator";

        public LoadingPanelMediator() : base(NAME)
        {
        }

        public LoadingPanel Panel
        {
            get => ViewComponent as LoadingPanel;
            set
            {
                ViewComponent = value;
                (ViewComponent as LoadingPanel)?.BindMediator(this);
            }
        }

        public override string[] ListNotificationInterests()
        {
            return new[]
            {
                NotificationName.UI.SHOW_LOADING_PANEL,
                NotificationName.UI.HIDE_LOADING_PANEL
            };
        }

        public override void HandleNotification(INotification notification)
        {
            base.HandleNotification(notification);

            switch (notification.Name)
            {
                case NotificationName.UI.SHOW_LOADING_PANEL:
                    Panel = UIManager.Instance.Show<LoadingPanel>(false);
                    break;
                case NotificationName.UI.HIDE_LOADING_PANEL:
                    UIManager.Instance.Hide<LoadingPanel>(false);
                    break;
            }
        }
    }
}