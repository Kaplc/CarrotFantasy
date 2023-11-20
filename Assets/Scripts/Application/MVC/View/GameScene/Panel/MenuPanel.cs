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
        });
        btnReStart.onClick.AddListener(() =>
        {
            PanelMediator.SendNotification(NotificationName.START_GAME);
            UIManager.Instance.Hide<MenuPanel>(false);
        });
        btnSelect.onClick.AddListener(() =>
        {
            PanelMediator.SendNotification(NotificationName.SELECT_LEVEL);
            UIManager.Instance.Hide<MenuPanel>(false);
        });
    }
    
}
