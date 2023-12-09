using System.Collections;
using System.Collections.Generic;
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
