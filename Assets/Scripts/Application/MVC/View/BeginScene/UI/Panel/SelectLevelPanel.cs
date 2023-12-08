using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevelPanel : BasePanel
{
    public Button btnBack;
    public Button btnHelp;
    public Button btnStart;
    public ScrollRect scrollRect;
    public Text teWavesCount;
    public Transform transformCreateTowerIcon;
    private List<Image> towerIcons = new List<Image>();
    private List<Button> btnsLevel = new List<Button>();
    private Button nowCenterButton; // 当前在中间的关卡按钮
    private LevelData nowCenterLevelData; // 当前中间的关卡数据

    public SelectLevelPanelPageFlipping pageFlipping;
    private BigLevelData bigLevelData;

    protected override void Init()
    {
        btnBack.onClick.AddListener(() =>
        {
            PanelMediator.SendNotification(NotificationName.SHOW_SELECTBIGLEVELPANEL);
            UIManager.Instance.Hide<SelectLevelPanel>(false);
        });
        btnHelp.onClick.AddListener(() =>
        {
            PanelMediator.SendNotification(NotificationName.SHOW_BEGINPANEL);
            PanelMediator.SendNotification(NotificationName.SHOW_HELPPANEL, false);
        });
        btnStart.onClick.AddListener(() =>
        {
            // 跳场景前显示LoadingPanel
            PanelMediator.SendNotification(NotificationName.SHOW_LOADINGPANEL);
            UIManager.Instance.Hide<SelectLevelPanel>(false);
            // 触发当前选中的Level按钮
            nowCenterButton.onClick.Invoke();
        });
        
        // 初始化更新
        PageFlippingCompleted();
    }

    /// <summary>
    /// 创建关卡按钮
    /// </summary>
    public void CreateLevelButton(BigLevelData data)
    {
        RectTransform content = scrollRect.content;
        // 设置滑动容器大小
        content.sizeDelta = new Vector2(1035 * (data.levels.Count - 1), content.sizeDelta.y);

        for (int i = 0; i < data.levels.Count; i++)
        {
            Button button = Instantiate(Resources.Load<GameObject>("UI/Button/ButtonLevel"), content).GetComponent<Button>();
            // 修改图片
            Image buttonImage = button.GetComponent<Image>();
            buttonImage.sprite = data.levels[i].image;
            // 添加事件
            LevelData levelData = data.levels[i];
            button.onClick.AddListener(() =>
            {
                GameFacade.Instance.SendNotification(NotificationName.LOAD_GAME, levelData);
            });

            btnsLevel.Add(button);
        }

        // 初始化翻页效果脚本
        pageFlipping.totalPageIndex = data.levels.Count;

        bigLevelData = data;
    }

    /// <summary>
    /// 更新选中关卡的可使用塔的图标
    /// </summary>
    public void UpdateTowerIcon(Sprite[] icons)
    {
        for (int i = 0; i < towerIcons.Count; i++)
        {
            Destroy(towerIcons[i].gameObject);
        }
        towerIcons.Clear();

        for (int i = 0; i < icons.Length; i++)
        {
            Image icon = Instantiate(Resources.Load<GameObject>("UI/Image/ImageTowerIcon"), transformCreateTowerIcon).GetComponent<Image>();
            icon.sprite = icons[i];
            towerIcons.Add(icon);
        }
    }

    /// <summary>
    /// 更新选中关卡的怪物波数
    /// </summary>
    private void UpdateWavesCount(int count)
    {
        teWavesCount.text = count.ToString();
    }

    /// <summary>
    /// 翻页完成的回调
    /// </summary>
    public void PageFlippingCompleted()
    {
        // 记录当前中间的关卡按钮
        nowCenterButton = btnsLevel[pageFlipping.pageIndex - 1];
        nowCenterLevelData = bigLevelData.levels[pageFlipping.pageIndex - 1];
        
        // 设置黑色遮罩
        for (int i = 0; i < btnsLevel.Count; i++)
        {
            // 未选中的按钮设置黑色遮罩
            Color color = btnsLevel[i].GetComponent<Image>().color;
            btnsLevel[i].GetComponent<Image>().color = new Color(100/255f, 100/255f, 100/255f, color.a);
        }

        nowCenterButton.GetComponent<Image>().color= new Color(1f, 1f, 1f, 1f);

        // 获取icons
        List<Sprite> towerIcons = new List<Sprite>();
        for (int i = 0; i < nowCenterLevelData.towersData.Count; i++)
        {
            towerIcons.Add(nowCenterLevelData.towersData[i].selectLevelIcon);
        }
        
        // 更新面板
        UpdateTowerIcon(towerIcons.ToArray());
        UpdateWavesCount(nowCenterLevelData.roundDataList.Count);
    }
}