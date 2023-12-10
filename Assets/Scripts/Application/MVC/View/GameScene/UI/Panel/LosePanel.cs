using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using UnityEngine;
using UnityEngine.UI;

public class LosePanel : BasePanel
{
    public Text txWavesCount;
    public Text txTotalWavesCount;
    public Text txLevel;
    public Button btnSelect;
    public Button btnReStart;
    
    protected override void Init()
    {
        btnReStart.onClick.AddListener(() =>
        {
            PanelMediator.SendNotification(NotificationName.RESTART_GAME);
            UIManager.Instance.Hide<LosePanel>(false);
        });
        btnSelect.onClick.AddListener(() =>
        {
            PanelMediator.SendNotification(NotificationName.EXIT_GAME);
            PanelMediator.SendNotification(NotificationName.SELECT_LEVEL);
            UIManager.Instance.Hide<LosePanel>(false);
        });
    }
    
    public void UpdatePanelData(int wavesCount, int totalWavesCount, int levelID)
    {
        txWavesCount.text = wavesCount / 10 + "  " + wavesCount % 10;
        txTotalWavesCount.text = totalWavesCount / 10 + "" + totalWavesCount % 10;
        txLevel.text = (levelID + 1) / 10 + "" + (levelID + 1) % 10;
    }
}

public class LosePanelMediator : Mediator
{
    public static new string NAME = "LosePanelMediator";

    public LosePanel Panel
    {
        get => ViewComponent as LosePanel;
        set
        {
            ViewComponent = value;
            (ViewComponent as LosePanel)?.BindMediator(this);
        }
    }

    public LosePanelMediator() : base(NAME)
    {
    }

    public override string[] ListNotificationInterests()
    {
        return new string[]
        {
            NotificationName.SHOW_LOSEPANEL
        };
    }

    public override void HandleNotification(INotification notification)
    {
        base.HandleNotification(notification);

        switch (notification.Name)
        {
            case NotificationName.SHOW_LOSEPANEL:
                // 先关闭菜单面板
                SendNotification(NotificationName.HIDE_MENUPANEL);
                // 停止游戏
                SendNotification(NotificationName.STOP_GAME);
                Panel = UIManager.Instance.Show<LosePanel>(false);
                
                // 更新数据
                (int wavesCount, int totalWavesCount, int levelID) data = ((int, int, int))notification.Body;
                Panel.UpdatePanelData(data.wavesCount, data.totalWavesCount, data.levelID);
                
                break;
        }
    }
}