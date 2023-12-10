using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : BasePanel
{
    public Button btnContinue;
    public Button btnReStart;
    public Button btnSelect;

    protected override void Init()
    {
        btnContinue.onClick.AddListener(() =>
        {
            UIManager.Instance.Hide<MenuPanel>(false);
            PanelMediator.SendNotification(NotificationName.CONTINUE_GAME);
            PanelMediator.SendNotification(NotificationName.ALLOW_CLICKCELL, true);
        });
        
        btnReStart.onClick.AddListener(() =>
        {
            UIManager.Instance.Hide<MenuPanel>(false);
            PanelMediator.SendNotification(NotificationName.RESTART_GAME);
        });
        
        btnSelect.onClick.AddListener(() =>
        {
            UIManager.Instance.Hide<MenuPanel>(false);
            // 退出游戏
            PanelMediator.SendNotification(NotificationName.EXIT_GAME);
            // 进入选择面板
            PanelMediator.SendNotification(NotificationName.SELECT_LEVEL);;
        });
    }
    
}

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