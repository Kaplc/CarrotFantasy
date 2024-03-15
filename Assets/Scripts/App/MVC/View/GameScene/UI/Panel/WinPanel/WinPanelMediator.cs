using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;

public class WinPanelMediator : Mediator
{
    public static new string NAME = "WinPanelMediator";

    public WinPanel Panel
    {
        get => ViewComponent as WinPanel;
        set
        {
            ViewComponent = value;
            (ViewComponent as WinPanel)?.BindMediator(this);
        }
    }

    public WinPanelMediator() : base(NAME)
    {
    }

    public override string[] ListNotificationInterests()
    {
        return new string[]
        {
            NotificationName.UI.SHOW_WINPANEL,
        };
    }

    public override void HandleNotification(INotification notification)
    {
        base.HandleNotification(notification);

        switch (notification.Name)
        {
            case NotificationName.UI.SHOW_WINPANEL:
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