using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;

public class LoadingPanelMediator : Mediator
{
    public static new string NAME = "LoadingPanelMediator";

    public LoadingPanel Panel
    {
        get=>ViewComponent as LoadingPanel;
        set
        {
            ViewComponent = value;
            (ViewComponent as LoadingPanel)?.BindMediator(this);
        }
    }
    
    public LoadingPanelMediator() : base(NAME)
    {
    }

    public override string[] ListNotificationInterests()
    {
        return new string[]
        {
            NotificationName.UI.SHOW_LOADINGPANEL,
            NotificationName.UI.HIDE_LOADINGPANEL
        };

    }

    public override void HandleNotification(INotification notification)
    {
        base.HandleNotification(notification);

        switch (notification.Name)
        {
            case NotificationName.UI.SHOW_LOADINGPANEL:
                Panel = UIManager.Instance.Show<LoadingPanel>(false);
                break;
            case NotificationName.UI.HIDE_LOADINGPANEL:
                UIManager.Instance.Hide<LoadingPanel>(false);
                break;
        }
        
    }
}