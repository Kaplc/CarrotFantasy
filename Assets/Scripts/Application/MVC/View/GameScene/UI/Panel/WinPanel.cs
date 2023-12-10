using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using UnityEngine;
using UnityEngine.UI;

public class WinPanel : BasePanel
{
    public Button btnSelect;
    public Button btnContinue;
    
    protected override void Init()
    {
        btnContinue.onClick.AddListener(() =>
        {
            PanelMediator.SendNotification(NotificationName.NEXT_LEVEL);
            UIManager.Instance.Hide<WinPanel>(false);
        });
        btnSelect.onClick.AddListener(() =>
        {
            PanelMediator.SendNotification(NotificationName.EXIT_GAME);
            UIManager.Instance.Hide<WinPanel>(false);
        });
    }
}

public class WinPanelMediator : Mediator
{
    public static new string NAME = "WinPanelMediator";

    public WinPanel Panel
    {
        get => ViewComponent as WinPanel;
        set
        {
            ViewComponent = value;
            (ViewComponent as WinPanel)?.BindMediator(this);
        }
    }

    public WinPanelMediator() : base(NAME)
    {
    }

    public override string[] ListNotificationInterests()
    {
        return new string[]
        {
            NotificationName.SHOW_WINPANEL,
        };
    }

    public override void HandleNotification(INotification notification)
    {
        base.HandleNotification(notification);

        switch (notification.Name)
        {
            case NotificationName.SHOW_WINPANEL:
                // 停止游戏
                SendNotification(NotificationName.STOP_GAME);
                
                Panel = UIManager.Instance.Show<WinPanel>(false);
                break;
        }
    }
}
