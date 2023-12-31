using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;

public class LosePanelMediator : Mediator
{
    public static new string NAME = "LosePanelMediator";

    public LosePanel Panel
    {
        get => ViewComponent as LosePanel;
        set
        {
            ViewComponent = value;
            (ViewComponent as LosePanel)?.BindMediator(this);
        }
    }

    public LosePanelMediator() : base(NAME)
    {
    }

    public override string[] ListNotificationInterests()
    {
        return new string[]
        {
            NotificationName.UI.SHOW_LOSEPANEL
        };
    }

    public override void HandleNotification(INotification notification)
    {
        base.HandleNotification(notification);

        switch (notification.Name)
        {
            case NotificationName.UI.SHOW_LOSEPANEL:
                // 先关闭菜单面板
                SendNotification(NotificationName.UI.HIDE_MENUPANEL);
                // 停止游戏
                SendNotification(NotificationName.Game.STOP_GAME);
                Panel = UIManager.Instance.Show<LosePanel>(false);
                
                // 更新数据
                (int wavesCount, int totalWavesCount, int levelID) data = ((int, int, int))notification.Body;
                Panel.UpdatePanelData(data.wavesCount, data.totalWavesCount, data.levelID);
                
                break;
        }
    }
}