using App.Static;
using GameFramework;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;

namespace App.UI.InitScene
{
    public class InitPanelMediator : Mediator
    {
        public new static string NAME = "InitPanelMediator";

        public InitPanelMediator() : base(NAME)
        {
        }

        public InitPanel Panel
        {
            get => ViewComponent as InitPanel;
            set
            {
                ViewComponent = value;
                (ViewComponent as InitPanel)?.BindMediator(this);
            }
        }

        public override string[] ListNotificationInterests()
        {
            return new[]
            {
                NotificationName.UI.SHOW_INIT_PANEL,
                NotificationName.UI.HIDE_INIT_PANEL
            };
        }

        public override void HandleNotification(INotification notification)
        {
            base.HandleNotification(notification);

            switch (notification.Name)
            {
                case NotificationName.UI.SHOW_INIT_PANEL:
                    Panel = UIManager.Instance.Show<InitPanel>(false);
                    break;
                case NotificationName.UI.HIDE_INIT_PANEL:
                    UIManager.Instance.Hide<InitPanel>(false);
                    break;
            }
        }
    }
}