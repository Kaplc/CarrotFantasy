﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// BeginPanel的子Panel
/// </summary>
public class HelpPanel : BasePanel
{

    public Button btnHome;
    
    protected override void Init()
    {
        btnHome.onClick.AddListener(() =>
        {
            // 通过MVC管理器发送显示BeginPanel的消息
            GameFacade.Instance.SendNotification(NotificationName.SHOW_BEGINPANEL);
        });
    }
}
