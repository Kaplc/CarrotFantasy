using System.Collections;
using System.Collections.Generic;
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
            PanelMediator.SendNotification(NotificationName.START_GAME);
            UIManager.Instance.Hide<LosePanel>(false);
        });
        btnSelect.onClick.AddListener(() =>
        {
            PanelMediator.SendNotification(NotificationName.SHOW_SELECTLEVELPANEL);
            UIManager.Instance.Hide<LosePanel>(false);
        });
    }
}
