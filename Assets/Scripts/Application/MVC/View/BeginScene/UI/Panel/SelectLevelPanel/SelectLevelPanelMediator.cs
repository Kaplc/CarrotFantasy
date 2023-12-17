using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;

public class SelectLevelPanelMediator : Mediator
{
    public static new string NAME = "SelectLevelPanelMediator";

    public SelectLevelPanel Panel
    {
        get => ViewComponent as SelectLevelPanel;
        set
        {
            ViewComponent = value;
            (ViewComponent as SelectLevelPanel)?.BindMediator(this);
        }
    }

    public SelectLevelPanelMediator() : base(NAME)
    {
    }

    public override string[] ListNotificationInterests()
    {
        return new string[]
        {
            NotificationName.SHOW_SELECTLEVELPANEL,
            NotificationName.LOADED_BIGLEVELDATA,
            NotificationName.LOADED_PROCESSDATA
        };
    }

    public override void HandleNotification(INotification notification)
    {
        base.HandleNotification(notification);

        switch (notification.Name)
        {
            case NotificationName.SHOW_SELECTLEVELPANEL:
                Panel = UIManager.Instance.Show<SelectLevelPanel>(false);
                // 获取游戏进度数据
                SendNotification(NotificationName.LOAD_PROCESSDATA);
                // 获取当前选择的大关卡数据
                SendNotification(NotificationName.LOAD_BIGLEVELDATA, notification.Body);
                break;
            case NotificationName.LOADED_BIGLEVELDATA:
                Panel.CreateLevelButton(notification.Body as BigLevelData);
                break;
            case NotificationName.LOADED_PROCESSDATA:
                if (!Panel) break;

                Panel.processData = notification.Body as ProcessData;
                break;
        }
    }
}