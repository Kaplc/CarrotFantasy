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

    public Animator animator;

    protected override void Init()
    {
        btnAdventure.onClick.AddListener(() =>
        {
            // 发送打开选择大关卡的消息
            PanelMediator.SendNotification(NotificationName.SHOW_SELECTBIGLEVELPANEL);
            // 隐藏自己
            UIManager.Instance.Hide<BeginPanel>(false);
        });
        btnBoss.onClick.AddListener(() =>
        {
            
        });
        btnMonster.onClick.AddListener(() =>
        {
            
        });
        btnSetting.onClick.AddListener(() =>
        {
            PanelMediator.SendNotification(NotificationName.SHOW_SETTINGPANEL);
        });
        btnHelp.onClick.AddListener(() =>
        {
            PanelMediator.SendNotification(NotificationName.SHOW_HELPPANEL);
        });
    }
}
