using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;

public class GamePanelMediator : Mediator
{
    public static new string NAME = "GamePanelMediator";

    public GamePanel Panel
    {
        get=>ViewComponent as GamePanel;
        set
        {
            ViewComponent = value;
            (ViewComponent as GamePanel)?.BindMediator(this);
        }
    }
    
    public GamePanelMediator() : base(NAME)
    {
        
    }

    public override string[] ListNotificationInterests()
    {
        return new string[]
        {
            NotificationName.UI.SHOW_GAMEPANEL,
            NotificationName.UI.HIDE_GAMEPANEL,
            NotificationName.UIEvent.GAMEPANEL_UPDATE_MONEY,
            NotificationName.UIEvent.GAMEPANEL_UPDATE_WAVESCOUNT
        };
    }

    public override void HandleNotification(INotification notification)
    {
        base.HandleNotification(notification);

        switch (notification.Name)
        {
            case NotificationName.UI.SHOW_GAMEPANEL:
                // 判断是否重新开始, 清空面板并重新生成, 让倒计时面板重新显示
                if (Panel!=null)
                {
                    UIManager.Instance.Hide<GamePanel>(false);
                }
                Panel = UIManager.Instance.Show<GamePanel>(false);
                
                break;
            case NotificationName.UI.HIDE_GAMEPANEL:
                UIManager.Instance.Hide<GamePanel>(false);
                
                break;
            case NotificationName.UIEvent.GAMEPANEL_UPDATE_MONEY:
                Panel.UpdateMoney((int)notification.Body);
                break;
            case NotificationName.UIEvent.GAMEPANEL_UPDATE_WAVESCOUNT:
                Panel.UpdateWavesCount(((int, int))notification.Body);
                break;
        }
    }
}