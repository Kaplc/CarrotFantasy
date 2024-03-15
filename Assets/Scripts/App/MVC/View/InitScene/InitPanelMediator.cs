using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;

public class InitPanelMediator : Mediator
{
    public new static string NAME = "InitPanelMediator";

    public InitPanel Panel
    {
        get => ViewComponent as InitPanel;
        set
        {
            ViewComponent = value;
            (ViewComponent as InitPanel)?.BindMediator(this);
        }
    }

    public InitPanelMediator() : base(NAME)
    {
    }

    public override string[] ListNotificationInterests()
    {
        return new string[]
        {
            NotificationName.UI.SHOW_INITPANEL,
            NotificationName.UI.HIDE_INIPANEL
        };
    }

    public override void HandleNotification(INotification notification)
    {
        base.HandleNotification(notification);

        switch (notification.Name)
        {
            case NotificationName.UI.SHOW_INITPANEL:
                Panel = UIManager.Instance.Show<InitPanel>(false);
                break;
            case NotificationName.UI.HIDE_INIPANEL:
                UIManager.Instance.Hide<InitPanel>(false);
                break;
        }
    }
}