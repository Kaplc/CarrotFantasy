using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : BasePanel
{
    public Button btnHome;
    public Toggle tgSelect;
    public Toggle tgData;
    public Toggle tgMaker;
    public Image imgSelectPage;
    public Image imgDataPage;
    public Image imgMakerPage;
    public StatisticalPage statisticalPage;
    public SelectPage selectPage;

    private bool ShowSelectPage
    {
        set
        {
            imgSelectPage.gameObject.SetActive(value);
            imgDataPage.gameObject.SetActive(!value);
            imgMakerPage.gameObject.SetActive(!value);
        }
    }

    private bool ShowDataPage
    {
        set
        {
            imgSelectPage.gameObject.SetActive(!value);
            imgDataPage.gameObject.SetActive(value);
            imgMakerPage.gameObject.SetActive(!value);
        }
    }

    private bool ShowMakerPage
    {
        set
        {
            imgSelectPage.gameObject.SetActive(!value);
            imgDataPage.gameObject.SetActive(!value);
            imgMakerPage.gameObject.SetActive(value);
        }
    }

    protected override void Init()
    {
        btnHome.onClick.AddListener(() =>
        {
            // 通过MVC管理器发送显示BeginPanel的消息
            GameFacade.Instance.SendNotification(NotificationName.UI.SHOW_BEGINPANEL);
        });

        tgSelect.onValueChanged.AddListener((isOn) => { ShowSelectPage = isOn; });

        tgData.onValueChanged.AddListener((isOn) => { ShowDataPage = isOn; });

        tgMaker.onValueChanged.AddListener((isOn) => { ShowMakerPage = isOn; });

        // 默认是选择界面
        ShowSelectPage = true;
    }

    public void UpdateSelectPage(MusicSettingData data)
    {
        selectPage.UpdateMusicSetting(data);
    }

    public void UpdateStatisticalPage(StatisticalData data)
    {
        statisticalPage.UpdateDate(data);
    }
}