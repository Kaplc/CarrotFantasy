using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BeginPanel : BasePanel
{
    public Button btnAdventure;
    public Button btnBoss;
    public Button btnMonster;
    public Button btnSetting;
    public Button btnHelp;
    
    public Animator animator;
    // 作为子面板
    public SettingPanel settingPanel;

    protected override void Init()
    {
        btnAdventure.onClick.AddListener(() =>
        {
            // 发送打开选择大关卡的消息
            PanelMediator.SendNotification(NotificationName.LoadScene.LOADSCENE_BEGIN_TO_SELECTITEM);
            // 隐藏自己
            UIManager.Instance.Hide<BeginPanel>(false);
        });
        btnBoss.onClick.AddListener(() => { });
        btnMonster.onClick.AddListener(() => { });
        btnSetting.onClick.AddListener(() => { PanelMediator.SendNotification(NotificationName.UI.SHOW_SETTINGPANEL); });
        btnHelp.onClick.AddListener(() => { PanelMediator.SendNotification(NotificationName.UI.SHOW_HELPPANEL, true); });
    }
}

