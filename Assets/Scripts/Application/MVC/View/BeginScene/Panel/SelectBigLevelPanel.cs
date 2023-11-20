using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public float speed;
    public List<RectTransform> posList;

    // 三个btn跟踪的位置下标
    private int btn0Index;
    private int btn1Index;
    private int btn2Index;

    protected override void Init()
    {
        btnBigLevel0.onClick.AddListener(() =>
        {
            // 记录选择的大关卡索引
            GameManager.Instance.nowBigLevelId = 0;
            PanelMediator.SendNotification(NotificationName.SHOW_SELECTLEVELPANEL);

            UIManager.Instance.Hide<SelectBigLevelPanel>(false);
        });
        btnBigLevel1.onClick.AddListener(() =>
        {
            GameManager.Instance.nowBigLevelId = 1;
            PanelMediator.SendNotification(NotificationName.SHOW_SELECTLEVELPANEL);
            
            UIManager.Instance.Hide<SelectBigLevelPanel>(false);
        });
        btnBigLevel2.onClick.AddListener(() =>
        {
            GameManager.Instance.nowBigLevelId = 2;
            PanelMediator.SendNotification(NotificationName.SHOW_SELECTLEVELPANEL);

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
            btn0Index++;
            btn1Index++;
            btn2Index++;
            
            if (btn0Index == 1)
            {
                // 到最左边时隐藏左按钮
                btnLeft.gameObject.SetActive(false);
            }
            else
            {
                // 其他情况右按钮都显示
                btnRight.gameObject.SetActive(true);
            }
        });
        btnRight.onClick.AddListener(() =>
        {
            btn0Index--;
            btn1Index--;
            btn2Index--;
            
            if (btn2Index == 1)
            {
                btnRight.gameObject.SetActive(false);
            }
            else
            {
                btnLeft.gameObject.SetActive(true);
            }
        });
    }

    protected override void Start()
    {
        base.Start();

        // 一开始跟踪的位置
        btn0Index = 1;
        btn1Index = 2;
        btn2Index = 3;
        
        // 开始左按钮隐藏
        btnLeft.gameObject.SetActive(false);
    }

    public override void Update()
    {
        base.Update();

        // 每个控件都跟踪固定的位置下标, 只要改变posList的内容控件就会跟踪动态调整位置
        // Mathf.Clamp(btn2Index, 0, posList.Count - 1)越界保持数组不越界
        ((RectTransform)btnBigLevel0.transform).anchoredPosition = Vector2.Lerp(((RectTransform)btnBigLevel0.transform).anchoredPosition,
            posList[Mathf.Clamp(btn0Index, 0, posList.Count)].anchoredPosition, Time.deltaTime * speed);
        ((RectTransform)btnBigLevel1.transform).anchoredPosition = Vector2.Lerp(((RectTransform)btnBigLevel1.transform).anchoredPosition,
            posList[Mathf.Clamp(btn1Index, 0, posList.Count - 1)].anchoredPosition, Time.deltaTime * speed);
        ((RectTransform)btnBigLevel2.transform).anchoredPosition = Vector2.Lerp(((RectTransform)btnBigLevel2.transform).anchoredPosition,
            posList[Mathf.Clamp(btn2Index, 0, posList.Count - 1)].anchoredPosition, Time.deltaTime * speed);
    }
}