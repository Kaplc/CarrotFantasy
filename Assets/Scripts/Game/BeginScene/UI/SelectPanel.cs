using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectPanel : BasePanel
{
    public Button btnBack;
    public Button btnHelp;
    public Button btnStart;
    
    protected override void Init()
    {
        btnBack.onClick.AddListener(() =>
        {
            PanelMediator.SendNotification(NotificationName.PRESS_BACK);
            UIManager.Instance.Hide<SelectPanel>(false);
        });
        btnHelp.onClick.AddListener(() =>
        {
            PanelMediator.SendNotification(NotificationName.PRESS_HELP);
        });
        btnStart.onClick.AddListener(() =>
        {
            PanelMediator.SendNotification(NotificationName.PRESS_START);
            UIManager.Instance.Hide<SelectPanel>(false);
        });
    }
}
