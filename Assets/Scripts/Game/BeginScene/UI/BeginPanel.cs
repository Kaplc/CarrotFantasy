using System.Collections;
using System.Collections.Generic;
using PureMVC.Patterns.Mediator;
using UnityEngine;
using UnityEngine.UI;

public class BeginPanel : BasePanel
{
    public Button btnAdventure;
    public Button btnBoss;
    public Button btnMonster;
    public Button btnSetting;
    public Button btnHelp;

    protected override void Init()
    {
        btnAdventure.onClick.AddListener(() =>
        {
            PanelMediator.SendNotification(NotificationName.PRESS_ADVENTURE);
            UIManager.Instance.Hide<BeginPanel>();
        });
        btnBoss.onClick.AddListener(() =>
        {
            
        });
        btnMonster.onClick.AddListener(() =>
        {
            
        });
        btnSetting.onClick.AddListener(() =>
        {
            
        });
        btnHelp.onClick.AddListener(() =>
        {
            PanelMediator.SendNotification(NotificationName.PRESS_HELP);
        });
    }
}
