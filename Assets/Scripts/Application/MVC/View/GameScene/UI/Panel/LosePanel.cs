using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using UnityEngine;
using UnityEngine.UI;

public class LosePanel : BasePanel
{
    public Button btnSelect;
    public Button btnReStart;
    
    protected override void Init()
    {
        btnReStart.onClick.AddListener(() =>
        {
            PanelMediator.SendNotification(NotificationName.RESTART_GAME);
            UIManager.Instance.Hide<LosePanel>(false);
        });
        btnSelect.onClick.AddListener(() =>
        {
            PanelMediator.SendNotification(NotificationName.EXIT_GAME);
            UIManager.Instance.Hide<LosePanel>(false);
        });
    }
}

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
            NotificationName.SHOW_LOSEPANEL
        };
    }

    public override void HandleNotification(INotification notification)
    {
        base.HandleNotification(notification);

        switch (notification.Name)
        {
            case NotificationName.SHOW_LOSEPANEL:
                // 停止游戏
                SendNotification(NotificationName.STOP_GAME);
                Panel = UIManager.Instance.Show<LosePanel>(false);
                
                // 先关闭菜单面板
                SendNotification(NotificationName.HIDE_MENUPANEL);
                
                break;
        }
    }
}