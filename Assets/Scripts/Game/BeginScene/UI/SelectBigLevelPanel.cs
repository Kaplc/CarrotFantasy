﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectBigLevelPanel : BasePanel
{
    public Button btnBigLevel0;
    public Button btnBigLevel1;
    public Button btnBigLevel2;

    public Button btnHome;
    public Button btnHelp;

    public Button btnLeft;
    public Button btnRight;

    protected override void Init()
    {
        btnBigLevel0.onClick.AddListener(() =>
        {
            
        });
        btnBigLevel1.onClick.AddListener(() =>
        {
            PanelMediator.SendNotification(NotificationName.SHOW_SELECTLEVELPANEL);
            UIManager.Instance.Hide<SelectBigLevelPanel>(false);
        });
        btnBigLevel2.onClick.AddListener(() =>
        {
            
        });
        btnHome.onClick.AddListener(() =>
        {
            PanelMediator.SendNotification(NotificationName.SHOW_BEGINPANEL);
            UIManager.Instance.Hide<SelectBigLevelPanel>(false);
        });
        btnHelp.onClick.AddListener(() =>
        {
            PanelMediator.SendNotification(NotificationName.SHOW_HELPPANEL);
            UIManager.Instance.Hide<SelectBigLevelPanel>(false);
        });
    }
}
