using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevelPanel : BasePanel
{
    public Button btnBack;
    public Button btnHelp;
    public Button btnStart;
    
    protected override void Init()
    {
        btnBack.onClick.AddListener(() =>
        {
            PanelMediator.SendNotification(NotificationName.SHOW_SELECTBIGLEVELPANEL);
            UIManager.Instance.Hide<SelectLevelPanel>(false);
        });
        btnHelp.onClick.AddListener(() =>
        {
            PanelMediator.SendNotification(NotificationName.SHOW_BEGINPANEL);
            PanelMediator.SendNotification(NotificationName.SHOW_HELPPANEL, false);
        });
        btnStart.onClick.AddListener(() =>
        {
            // 跳场景前显示LoadingPanel
            PanelMediator.SendNotification(NotificationName.SHOW_LOADINGPANEL);
            PanelMediator.SendNotification(NotificationName.START_GAME);
            UIManager.Instance.Hide<SelectLevelPanel>(false);
        });
    }
}
