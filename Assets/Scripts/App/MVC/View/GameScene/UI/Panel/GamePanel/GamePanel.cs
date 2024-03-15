using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : BasePanel
{
    public Button btnSpeed1;
    public Button btnSpeed2;
    public Toggle tgPause;
    public Button btnMenu;
    public Text txMoney;
    public Text txNowWave;
    public Text txTotalWaves;
    public Image imgPause;
    
    protected override void Init()
    {
        btnSpeed1.onClick.AddListener(() =>
        {
            btnSpeed2.gameObject.SetActive(true);
            btnSpeed1.gameObject.SetActive(false);
            GameFacade.Instance.SendNotification(NotificationName.Game.TWOSPEED, true);
        });
        btnSpeed2.onClick.AddListener(() =>
        {
            btnSpeed1.gameObject.SetActive(true);
            btnSpeed2.gameObject.SetActive(false);
            GameFacade.Instance.SendNotification(NotificationName.Game.TWOSPEED, false);
        });
        tgPause.onValueChanged.AddListener(isOn =>
        {
            if (isOn)
            {
                PanelMediator.SendNotification(NotificationName.Game.CONTINUE_GAME);
            }
            else
            {
                PanelMediator.SendNotification(NotificationName.Game.PAUSE_GAME);
            }
            
        });
        btnMenu.onClick.AddListener(() =>
        {
            PanelMediator.SendNotification(NotificationName.UI.SHOW_MENUPANEL);
        });
        btnSpeed2.gameObject.SetActive(false);
        imgPause.gameObject.SetActive(false);
    }


    public void UpdateMoney(int num)
    {
        txMoney.text = num.ToString();
    }

    public void UpdateWavesCount((int nowWave, int totalWavesCount) data)
    {
        txNowWave.text = $"{data.nowWave / 10}  {data.nowWave % 10}";
        txTotalWaves.text = data.totalWavesCount.ToString();
    }
    
    /// <summary>
    /// 广播接受子类信息
    /// </summary>
    public void SendStartGameNotification()
    {
        PanelMediator.SendNotification(NotificationName.Game.START_GAME);
    }
}

