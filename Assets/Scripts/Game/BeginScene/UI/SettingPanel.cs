using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : BasePanel
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
