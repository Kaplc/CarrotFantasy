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
            PanelMediator.SendNotification(NotificationName.LOADSCENE_GAME_TO_SELECTLEVEL);;
        });
    }
    
}

