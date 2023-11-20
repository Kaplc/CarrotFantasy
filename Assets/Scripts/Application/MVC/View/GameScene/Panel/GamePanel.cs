using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : BasePanel
{
    public Button btnSpeed1;
    public Button btnSpeed2;
    public Toggle tgPause;
    public Button btnMenu;
    public Text txGem;
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
            
        });
        btnMenu.onClick.AddListener(() =>
        {
            PanelMediator.SendNotification(NotificationName.SHOW_MENUPANEL);
        });
        btnSpeed2.gameObject.SetActive(false);
        imgPause.gameObject.SetActive(false);
    }
    
}
