using App.DataClass.Game.Level;
using App.DataClass.Player;
using App.Static;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;

namespace App.UI.SelectLevelScene
{
    public class SelectLevelPanelMediator : Mediator
    {
        public static new string NAME = "SelectLevelPanelMediator";

        public SelectLevelPanel Panel
        {
            get => ViewComponent as SelectLevelPanel;
            set
            {
                ViewComponent = value;
                (ViewComponent as SelectLevelPanel)?.BindMediator(this);
            }
        }

        public SelectLevelPanelMediator() : base(NAME)
        {
        }

        public override string[] ListNotificationInterests()
        {
            return new string[]
            {
                NotificationName.UI.SHOW_SELECT_LEVEL_PANEL,
                NotificationName.UI.LEVEL_DATA_UPDATED,
            };
        }

        public override void HandleNotification(INotification notification)
        {
            base.HandleNotification(notification);

            switch (notification.Name)
            {
                case NotificationName.UI.SHOW_SELECT_LEVEL_PANEL:
                    Panel = UIManager.Instance.Show<SelectLevelPanel>(false);
                    // 获取游戏进度数据
                    SendNotification(NotificationName.Data.REQUEST_UPDATE_LEVEL_PROCESS_DATA);
                    break;
                case NotificationName.UI.LEVEL_DATA_UPDATED:
                    Panel.CreateLevelButton(notification.Body as ItemData);
                    break;
            }
        }
    }
}