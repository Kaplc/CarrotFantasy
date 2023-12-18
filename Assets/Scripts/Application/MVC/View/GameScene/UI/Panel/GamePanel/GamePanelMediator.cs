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
            NotificationName.SHOW_GAMEPANEL,
            NotificationName.HIDE_GAMEPANEL,
            NotificationName.UPDATE_MONEY,
            NotificationName.UPDATE_WAVESCOUNT
        };
    }

    public override void HandleNotification(INotification notification)
    {
        base.HandleNotification(notification);

        switch (notification.Name)
        {
            case NotificationName.SHOW_GAMEPANEL:
                // 判断是否重新开始, 清空面板并重新生成, 让倒计时面板重新显示
                if (Panel!=null)
                {
                    UIManager.Instance.Hide<GamePanel>(false);
                }
                Panel = UIManager.Instance.Show<GamePanel>(false);
                
                break;
            case NotificationName.HIDE_GAMEPANEL:
                UIManager.Instance.Hide<GamePanel>(false);
                
                break;
            case NotificationName.UPDATE_MONEY:
                Panel.UpdateMoney((int)notification.Body);
                break;
            case NotificationName.UPDATE_WAVESCOUNT:
                Panel.UpdateWavesCount(((int, int))notification.Body);
                break;
        }
    }
}