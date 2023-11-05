using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// BeginPanel的子Panel
/// </summary>
public class HelpPanel : BasePanel
{
    public Button btnHome;
    public Button btnHelp;
    public Button btnMonster;
    public Button btnTower;

    public Transform helpPage;
    public Transform towerPage;
    public Transform monsterPage;
    public Transform bottomImage;
    public Text bottomPageNumber;
    
    private bool showHelpPage;
    private bool showMonsterPage;
    private bool showTowerPage;
    
    #region 页面属性

    // 显示HelpPage
    
    public bool ShowHelpPage
    {
        get => showHelpPage;
        set
        {
            showHelpPage = value;
            
            bottomImage.gameObject.SetActive(value); // 底部页码
            
            helpPage.gameObject.SetActive(value); // help页面
            monsterPage.gameObject.SetActive(!value); // 怪物页面
            towerPage.gameObject.SetActive(!value); // tower页面
        }
    }

    public bool ShowMonsterPage
    {
        get => showMonsterPage;
        set
        {
            showMonsterPage = value;
            
            bottomImage.gameObject.SetActive(!value);
            
            helpPage.gameObject.SetActive(!value);
            monsterPage.gameObject.SetActive(value);
            towerPage.gameObject.SetActive(!value);
        }
    }

    public bool ShowTowerPage
    {
        get => showTowerPage;
        set
        {
            showTowerPage = value;
            
            bottomImage.gameObject.SetActive(value);
            
            helpPage.gameObject.SetActive(!value);
            monsterPage.gameObject.SetActive(!value);
            towerPage.gameObject.SetActive(value);
        }
    }
    
    #endregion
    
    protected override void Init()
    {
        btnHome.onClick.AddListener(() =>
        {
            // 通过MVC管理器发送显示BeginPanel的消息
            GameFacade.Instance.SendNotification(NotificationName.SHOW_BEGINPANEL);
        });
        btnHelp.onClick.AddListener(() =>
        {
            ShowHelpPage = true;
        });
        btnMonster.onClick.AddListener(() =>
        {
            ShowMonsterPage = true;
        });
        btnTower.onClick.AddListener(() =>
        {
            ShowTowerPage = true;
        });
        
        // 初始隐藏怪物和塔页面
        monsterPage.gameObject.SetActive(false);
        towerPage.gameObject.SetActive(false);
    }
}
