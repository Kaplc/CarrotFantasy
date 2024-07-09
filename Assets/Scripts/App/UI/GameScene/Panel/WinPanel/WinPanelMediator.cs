using App.Data.DataClass.Player;
using App.Static;
using GameFramework;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;

namespace App.UI.GameScene.Panel.WinPanel
{
    public class WinPanelMediator : Mediator
    {
        public new static string NAME = "WinPanelMediator";

        public WinPanelMediator() : base(NAME)
        {
        }

        public WinPanel Panel
        {
            get => ViewComponent as WinPanel;
            set
            {
                ViewComponent = value;
                (ViewComponent as WinPanel)?.BindMediator(this);
            }
        }

        public override string[] ListNotificationInterests()
        {
            return new[]
            {
                NotificationName.UI.SHOW_WIN_PANEL
            };
        }

        public override void HandleNotification(INotification notification)
        {
            base.HandleNotification(notification);

            switch (notification.Name)
            {
                case NotificationName.UI.SHOW_WIN_PANEL:
                    // 停止游戏
                    SendNotification(NotificationName.Game.STOP_GAME);

                    Panel = UIManager.Instance.Show<WinPanel>(false);
                    // 更新数据
                    (int wavesCount, int totalWavesCount, int levelID, EPassedGrade grade) data = ((int, int, int, EPassedGrade))notification.Body;
                    Panel.UpdatePanelData(data.wavesCount, data.totalWavesCount, data.levelID);
                    Panel.UpdateGradeImage(data.grade);
                    break;
            }
        }
    }
}