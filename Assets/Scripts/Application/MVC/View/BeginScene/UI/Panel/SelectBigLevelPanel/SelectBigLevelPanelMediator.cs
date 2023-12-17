using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;

public class SelectBigLevelPanelMediator : Mediator
{
    public static new string NAME = "SelectBigLevelPanelMediator";

    public SelectBigLevelPanel Panel
    {
        get => ViewComponent as SelectBigLevelPanel;
        set
        {
            ViewComponent = value;
            (ViewComponent as SelectBigLevelPanel)?.BindMediator(this);
        }
    }

    public SelectBigLevelPanelMediator() : base(NAME)
    {
    }

    public override string[] ListNotificationInterests()
    {
        return new string[]
        {
            NotificationName.SHOW_SELECTBIGLEVELPANEL,
            NotificationName.LOADED_PROCESSDATA
        };
    }

    public override void HandleNotification(INotification notification)
    {
        base.HandleNotification(notification);

        switch (notification.Name)
        {
            case NotificationName.SHOW_SELECTBIGLEVELPANEL:
                Panel = UIManager.Instance.Show<SelectBigLevelPanel>(false);
                SendNotification(NotificationName.LOAD_PROCESSDATA);
                break;
            case NotificationName.LOADED_PROCESSDATA:
                Panel.processData = notification.Body as ProcessData;
                break;
        }
    }
}