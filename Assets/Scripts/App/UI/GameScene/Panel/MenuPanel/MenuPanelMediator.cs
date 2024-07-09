using App.Static;
using GameFramework;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;

namespace App.UI.GameScene.Panel.MenuPanel
{
    public class MenuPanelMediator : Mediator
    {
        public new static string NAME = "MenuPanelMediator";

        public MenuPanelMediator() : base(NAME)
        {
        }

        public MenuPanel Panel
        {
            get => ViewComponent as MenuPanel;
            set
            {
                ViewComponent = value;
                (ViewComponent as MenuPanel)?.BindMediator(this);
            }
        }

        public override string[] ListNotificationInterests()
        {
            return new[]
            {
                NotificationName.UI.SHOW_MENU_PANEL,
                NotificationName.UI.HIDE_MENU_PANEL
            };
        }

        public override void HandleNotification(INotification notification)
        {
            base.HandleNotification(notification);
            switch (notification.Name)
            {
                case NotificationName.UI.SHOW_MENU_PANEL:
                    Panel = UIManager.Instance.Show<MenuPanel>(false);
                    // 停止游戏
                    SendNotification(NotificationName.Game.STOP_GAME);

                    break;
                case NotificationName.UI.HIDE_MENU_PANEL:
                    UIManager.Instance.Hide<MenuPanel>(false);
                    break;
            }
        }
    }
}