using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;

public class BeginPanelMediator : Mediator
{
    public static new string NAME = "BeginPanelMediator";

    // 相互绑定的Panel
    public BeginPanel Panel
    {
        get => ViewComponent as BeginPanel;
        set
        {
            ViewComponent = value;
            (ViewComponent as BeginPanel)?.BindMediator(this);
        }
    }

    // 命名
    public BeginPanelMediator() : base(NAME)
    {
    }

    // view要监听的事件列表
    public override string[] ListNotificationInterests()
    {
        // 返回事件名数组表示要监听的事件
        return new string[]
        {
            NotificationName.SHOW_BEGINPANEL,
            NotificationName.SHOW_HELPPANEL,
            NotificationName.SHOW_SETTINGPANEL,
            NotificationName.LOADED_MUSICSETTINGDATA,
            NotificationName.LOADED_STATISTICALDATA
        };
    }

    // 执行监听的事件
    public override void HandleNotification(INotification notification)
    {
        // 根据不同的事件执行不同的逻辑
        switch (notification.Name)
        {
            case NotificationName.SHOW_BEGINPANEL:
                Panel = UIManager.Instance.Show<BeginPanel>(false);
                // 每次显示时复原动画参数
                Panel.animator.SetBool("ShowHelpPanel", false);
                Panel.animator.SetBool("RawShowHelpPanel", false);
                Panel.animator.SetBool("ShowSettingPanel", false);
                break;
            case NotificationName.SHOW_HELPPANEL:
                // true为有动画过渡
                if ((bool)notification.Body)
                {
                    // 播放显示HelpPanel的动画
                    Panel.animator.SetBool("ShowHelpPanel", true);
                }
                else
                {
                    Panel.animator.SetBool("RawShowHelpPanel", true);
                }

                break;
            case NotificationName.SHOW_SETTINGPANEL:
                // 给设置面板刷获取数据
                SendNotification(NotificationName.LOAD_MUSICSETTINGDATA);
                SendNotification(NotificationName.LOAD_STATISTICALDATA);
                // 播放显示HelpPanel的动画
                Panel.animator.SetBool("ShowSettingPanel", true);
                break;
            case NotificationName.LOADED_MUSICSETTINGDATA:
                // 刷新音乐设置数据
                Panel.settingPanel.UpdateSelectPage(notification.Body as MusicSettingData);
                break;
            case NotificationName.LOADED_STATISTICALDATA:
                // 刷新统计数据
                Panel.settingPanel.UpdateStatisticalPage(notification.Body as StatisticalData);
                break;
        }
    }
}