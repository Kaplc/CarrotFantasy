using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;

public class SelectItemPanelMediator : Mediator
{
    public static new string NAME = "SelectItemPanelMediator";

    public SelectItemPanel Panel
    {
        get => ViewComponent as SelectItemPanel;
        set
        {
            ViewComponent = value;
            (ViewComponent as SelectItemPanel)?.BindMediator(this);
        }
    }

    public SelectItemPanelMediator() : base(NAME)
    {
    }

    public override string[] ListNotificationInterests()
    {
        return new string[]
        {
            NotificationName.UI.SHOW_SELECTITEMPANEL,
            NotificationName.Data.LOADED_PROCESSDATA
        };
    }

    public override void HandleNotification(INotification notification)
    {
        base.HandleNotification(notification);

        switch (notification.Name)
        {
            case NotificationName.UI.SHOW_SELECTITEMPANEL:
                Panel = UIManager.Instance.Show<SelectItemPanel>(false);
                SendNotification(NotificationName.Data.LOAD_PROCESSDATA);
                break;
            case NotificationName.Data.LOADED_PROCESSDATA:
                Panel.processData = notification.Body as ProcessData;
                break;
        }
    }
}