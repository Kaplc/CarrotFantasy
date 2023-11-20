using System.Collections;
using System.Collections.Generic;
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
            UIManager.Instance.Hide<WinPanel>(false);
        });
        btnSelect.onClick.AddListener(() =>
        {
            PanelMediator.SendNotification(NotificationName.SHOW_SELECTLEVELPANEL);
            UIManager.Instance.Hide<WinPanel>(false);
        });
    }
}
