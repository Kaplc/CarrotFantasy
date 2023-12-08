using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
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
    public BasePageFlipping pageFlipping;

    protected override void Init()
    {
        //
        btnBigLevel0.onClick.AddListener(() =>
        {
            // 记录选择的大关卡索引
            GameManager.Instance.nowBigLevelId = 0;
            PanelMediator.SendNotification(NotificationName.SHOW_SELECTLEVELPANEL, GameManager.Instance.nowBigLevelId);

            UIManager.Instance.Hide<SelectBigLevelPanel>(false);
        });
        btnBigLevel1.onClick.AddListener(() =>
        {
            GameManager.Instance.nowBigLevelId = 1;
            PanelMediator.SendNotification(NotificationName.SHOW_SELECTLEVELPANEL, GameManager.Instance.nowBigLevelId);

            UIManager.Instance.Hide<SelectBigLevelPanel>(false);
        });
        btnBigLevel2.onClick.AddListener(() =>
        {
            GameManager.Instance.nowBigLevelId = 2;
            PanelMediator.SendNotification(NotificationName.SHOW_SELECTLEVELPANEL, GameManager.Instance.nowBigLevelId);

            UIManager.Instance.Hide<SelectBigLevelPanel>(false);
        });
        btnHome.onClick.AddListener(() =>
        {
            PanelMediator.SendNotification(NotificationName.SHOW_BEGINPANEL);
            UIManager.Instance.Hide<SelectBigLevelPanel>(false);
        });
        btnHelp.onClick.AddListener(() =>
        {
            // 显示HelpPanel前先显示BeginPanel
            PanelMediator.SendNotification(NotificationName.SHOW_BEGINPANEL);
            // false当消息体传递标识显示helpPanel无动画过渡
            PanelMediator.SendNotification(NotificationName.SHOW_HELPPANEL, false);
            UIManager.Instance.Hide<SelectBigLevelPanel>(false);
        });

        btnLeft.onClick.AddListener(() =>
        {
            pageFlipping.LastPage();

            if (pageFlipping.pageIndex == 1)
            {
                // 最小页码隐藏左边按钮
                btnLeft.gameObject.SetActive(false);
            }
            else
            {
                // 显示所有按钮
                btnLeft.gameObject.SetActive(true);
                btnRight.gameObject.SetActive(true);
            }
        });

        btnRight.onClick.AddListener(() =>
        {
            pageFlipping.NextPage();

            if (pageFlipping.pageIndex == pageFlipping.totalPageIndex)
            {
                // 最大页码隐藏右边按钮
                btnRight.gameObject.SetActive(false);
            }
            else
            {
                // 显示所有按钮
                btnLeft.gameObject.SetActive(true);
                btnRight.gameObject.SetActive(true);
            }
        });
        
        // 开始为第一页自动隐藏左边按钮
        btnLeft.gameObject.SetActive(false);
    }

    #region 接受ScrollView的消息

    public void FirstPage()
    {
        // 最小页码隐藏左边按钮
        btnLeft.gameObject.SetActive(false);
    }

    public void FinallyPage()
    {
        // 最大页码隐藏右边按钮
        btnRight.gameObject.SetActive(false);
    }

    public void NormalPage()
    {
        // 显示所有按钮
        btnLeft.gameObject.SetActive(true);
        btnRight.gameObject.SetActive(true);
    }
    #endregion
    
}