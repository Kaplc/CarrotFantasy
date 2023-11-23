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
            PanelMediator.SendNotification(NotificationName.CONTINUE_GAME);
            UIManager.Instance.Hide<MenuPanel>(false);
        });
        
        btnReStart.onClick.AddListener(() =>
        {
            PanelMediator.SendNotification(NotificationName.RESTART_GAME);
            UIManager.Instance.Hide<MenuPanel>(false);
        });
        
        btnSelect.onClick.AddListener(() =>
        {
            PanelMediator.SendNotification(NotificationName.EXIT_GAME);
            UIManager.Instance.Hide<MenuPanel>(false);
        });
    }
    
}
