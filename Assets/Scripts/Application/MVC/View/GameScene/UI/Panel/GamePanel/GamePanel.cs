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
        });
        btnSpeed2.onClick.AddListener(() =>
        {
            btnSpeed1.gameObject.SetActive(true);
            btnSpeed2.gameObject.SetActive(false);
        });
        tgPause.onValueChanged.AddListener(isOn =>
        {
            if (isOn)
            {
                PanelMediator.SendNotification(NotificationName.CONTINUE_GAME);
            }
            else
            {
                PanelMediator.SendNotification(NotificationName.PAUSE_GAME);
            }
            
        });
        btnMenu.onClick.AddListener(() =>
        {
            PanelMediator.SendNotification(NotificationName.SHOW_MENUPANEL);
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
        PanelMediator.SendNotification(NotificationName.START_GAME);
    }
}

public class GamePanelMediator : Mediator
{
    public static new string NAME = "GamePanelMediator";

    public GamePanel Panel
    {
        get=>ViewComponent as GamePanel;
        set
        {
            ViewComponent = value;
            (ViewComponent as GamePanel)?.BindMediator(this);
        }
    }
    
    public GamePanelMediator() : base(NAME)
    {
        
    }

    public override string[] ListNotificationInterests()
    {
        return new string[]
        {
            NotificationName.SHOW_GAMEPANEL,
            NotificationName.HIDE_GAMEPANEL,
            NotificationName.UPDATE_MONEY,
            NotificationName.UPDATE_WAVESCOUNT
        };
    }

    public override void HandleNotification(INotification notification)
    {
        base.HandleNotification(notification);

        switch (notification.Name)
        {
            case NotificationName.SHOW_GAMEPANEL:
                // 判断是否重新开始, 清空面板并重新生成, 让倒计时面板重新显示
                if (Panel!=null)
                {
                    UIManager.Instance.Hide<GamePanel>(false);
                }
                Panel = UIManager.Instance.Show<GamePanel>(false);
                
                break;
            case NotificationName.HIDE_GAMEPANEL:
                UIManager.Instance.Hide<GamePanel>(false);
                
                break;
            case NotificationName.UPDATE_MONEY:
                Panel.UpdateMoney((int)notification.Body);
                break;
            case NotificationName.UPDATE_WAVESCOUNT:
                Panel.UpdateWavesCount(((int, int))notification.Body);
                break;
        }
    }
}
