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
            PanelMediator.SendNotification(NotificationName.LOADSCENE_GAME_TO_SELECTLEVEL);
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

