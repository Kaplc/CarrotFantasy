using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;

public class MenuPanelMediator : Mediator
{
    public static new string NAME = "MenuPanelMediator";

    public MenuPanel Panel
    {
        get=>ViewComponent as MenuPanel;
        set
        {
            ViewComponent = value;
            (ViewComponent as MenuPanel)?.BindMediator(this);
        }
    }
    
    public MenuPanelMediator() : base(NAME)
    {
    }

    public override string[] ListNotificationInterests()
    {
        return new string[]
        {
            NotificationName.SHOW_MENUPANEL,
            NotificationName.HIDE_MENUPANEL
        };
    }

    public override void HandleNotification(INotification notification)
    {
        base.HandleNotification(notification);
        switch (notification.Name)
        {
            case NotificationName.SHOW_MENUPANEL:
                Panel = UIManager.Instance.Show<MenuPanel>(false);
                // 停止游戏
                SendNotification(NotificationName.STOP_GAME);
                
                break;
            case NotificationName.HIDE_MENUPANEL:
                UIManager.Instance.Hide<MenuPanel>(false);
                break;
        }
        
    }
}