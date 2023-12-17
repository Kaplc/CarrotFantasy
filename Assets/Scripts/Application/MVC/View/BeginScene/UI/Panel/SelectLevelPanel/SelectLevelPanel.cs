using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevelPanel : BasePanel
{
    public int pageIndex; // 当前选择的页码

    public Button btnBack;
    public Button btnHelp;
    public Button btnStart;
    private Button nowCenterButton; // 当前在中间的关卡按钮
    public ScrollRect scrollRect;
    public Text teWavesCount;
    public Transform transformCreateTowerIcon;
    private List<Image> towerIcons = new List<Image>();
    private List<Button> btnsLevel = new List<Button>();

    private LevelData nowCenterLevelData; // 当前中间的关卡数据
    public SelectLevelPanelPageFlipping pageFlipping;
    private BigLevelData bigLevelData;
    public LevelLockPanel levelLockPanel; // 提示关卡锁定的子面板
    public ProcessData processData; // 游戏进度数据

    protected override void Init()
    {
        btnBack.onClick.AddListener(() =>
        {
            UIManager.Instance.Hide<SelectLevelPanel>(false);
            PanelMediator.SendNotification(NotificationName.SHOW_SELECTBIGLEVELPANEL);
        });
        btnHelp.onClick.AddListener(() =>
        {
            UIManager.Instance.Hide<SelectLevelPanel>(false);
            PanelMediator.SendNotification(NotificationName.SHOW_BEGINPANEL);
            PanelMediator.SendNotification(NotificationName.SHOW_HELPPANEL, false);
        });
        btnStart.onClick.AddListener(() =>
        {
            // 触发当前选中的Level按钮
            nowCenterButton.onClick.Invoke();
        });

        // 初始化更新
        PageFlippingCompleted();

        // 滑动到上一次打开的关卡
        ToPage();
    }

    private void ToPage()
    {
        if (!GameManager.Instance.nowLevelData) return;

        for (int i = 0; i < bigLevelData.levels.Count; i++)
        {
            if (GameManager.Instance.nowLevelData.levelID == bigLevelData.levels[i].levelID)
            {
                pageFlipping.ToPage(i + 1);
                // 仅自动滑动一次退出选择关卡界面就无效
                GameManager.Instance.nowLevelData = null;
                return;
            }
        }
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
            btnsLevel.Add(button);
            // 获取脚本
            ButtonLevel buttonLevel = button.GetComponent<ButtonLevel>();
            // 设置信息
            buttonLevel.levelID = data.levels[i].levelID;
            // 修改图片
            buttonLevel.imgMap.sprite = data.levels[i].image;
            // 添加事件
            LevelData levelData = data.levels[i];
            button.onClick.AddListener(() =>
            {
                // 如果点击时并不是在中间选中状态，则自动滑动到中间
                if (nowCenterButton != button)
                {
                    pageFlipping.ToPage(btnsLevel.IndexOf(button) + 1);
                    return;
                }

                if (button.GetComponent<ButtonLevel>().IsLock)
                {
                    // 关卡锁定状态显示提示面板
                    levelLockPanel.gameObject.SetActive(true);
                    return;
                }

                UIManager.Instance.Hide<SelectLevelPanel>(false);
                GameFacade.Instance.SendNotification(NotificationName.LOAD_GAME, levelData.levelID);
            });
        }

        // 显隐锁定图标和更新通关等级体图片
        PassedLevelData passedLevelData = processData.passedBigLevelsDic[GameManager.Instance.nowBigLevelId];
        for (int i = 0; i < btnsLevel.Count; i++)
        {
            int levelID = btnsLevel[i].GetComponent<ButtonLevel>().levelID;
            
            if (passedLevelData.passedLevelDic.ContainsKey(levelID))
            {
                // 设置通关等级
                btnsLevel[i].GetComponent<ButtonLevel>().passedGrade = passedLevelData.passedLevelDic[levelID];
                // 取消锁定
                btnsLevel[i].GetComponent<ButtonLevel>().IsLock = false;
            }
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
        if (btnsLevel.Count == 0) return;
        // 记录当前中间的关卡按钮
        nowCenterButton = btnsLevel[pageFlipping.pageIndex - 1];
        nowCenterLevelData = bigLevelData.levels[pageFlipping.pageIndex - 1];

        // 设置黑色遮罩
        for (int i = 0; i < btnsLevel.Count; i++)
        {
            ButtonLevel buttonLevel = btnsLevel[i].GetComponent<ButtonLevel>();
            // 未选中的按钮设置黑色遮罩
            Color imgMapColor = buttonLevel.imgMap.color;
            buttonLevel.imgMap.color = new Color(100 / 255f, 100 / 255f, 100 / 255f, imgMapColor.a);
            Color imgGardeColor = buttonLevel.imgMap.color;
            buttonLevel.imgGarde.color = new Color(100 / 255f, 100 / 255f, 100 / 255f, imgGardeColor.a);
            Color imgLockColor = buttonLevel.imgMap.color;
            buttonLevel.imgLock.color = new Color(100 / 255f, 100 / 255f, 100 / 255f, imgLockColor.a);
        }

        // 选中按钮为正常颜色
        ButtonLevel nowCenterButtonLevel = nowCenterButton.GetComponent<ButtonLevel>();
        nowCenterButtonLevel.imgMap.color = new Color(1f, 1f, 1f, 1f);
        nowCenterButtonLevel.imgGarde.color = new Color(1f, 1f, 1f, 1f);
        nowCenterButtonLevel.imgLock.color = new Color(1f, 1f, 1f, 1f);

        // 获取icons
        List<Sprite> towerIconSprites = new List<Sprite>();
        for (int i = 0; i < nowCenterLevelData.towersData.Count; i++)
        {
            towerIconSprites.Add(nowCenterLevelData.towersData[i].selectLevelIcon);
        }

        // 更新面板
        UpdateTowerIcon(towerIconSprites.ToArray());
        UpdateWavesCount(nowCenterLevelData.roundDataList.Count);
    }
}

