using System.Collections.Generic;
using App.Data.DataClass.Game.Level;
using App.Data.DataClass.Player;
using App.Game;
using App.Game.SceneManager.NormalGame.interf;
using App.Static;
using App.UI.SelectLevelScene.Control;
using GameFramework;
using UnityEngine;
using UnityEngine.UI;

namespace App.UI.SelectLevelScene
{
    public class SelectLevelPanel : BasePanel
    {
        private readonly List<Button> btnsLevel = new List<Button>();
        private readonly List<Image> towerIcons = new List<Image>();
        public Button btnBack;
        public Button btnHelp;
        public Button btnStart;
        private ItemData itemData;
        public LevelLockPanel levelLockPanel; // 提示关卡锁定的子面板
        private Button nowCenterButton; // 当前在中间的关卡按钮

        private LevelData nowCenterLevelData; // 当前中间的关卡数据
        public SelectLevelPanelPageFlipping pageFlipping;
        public int pageIndex; // 当前选择的页码
        public ScrollRect scrollRect;
        public Text teWavesCount;
        public Transform transformCreateTowerIcon;

        private INormalSceneManager normalSceneManager => GameManager.Instance.sceneManager as INormalSceneManager;

        protected override void Init()
        {
            btnBack.onClick.AddListener(() =>
            {
                UIManager.Instance.Hide<SelectLevelPanel>(false);
                PanelMediator.SendNotification(NotificationName.LoadScene.LOADSCENE_SELECTLEVEL_TO_SELECTITEM);
            });
            btnHelp.onClick.AddListener(() =>
            {
                UIManager.Instance.Hide<SelectLevelPanel>(false);
                PanelMediator.SendNotification(NotificationName.LoadScene.LOADSCENE_SELECTLEVEL_TO_HELP);
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
            for (var i = 0; i < itemData.levels.Count; i++)
                if (normalSceneManager.NowLevelID == itemData.levels[i].levelID)
                {
                    pageFlipping.ToPage(i + 1);
                    // 仅自动滑动一次退出选择关卡界面就无效
                    normalSceneManager.NowLevelID = 0;
                    return;
                }
        }

        /// <summary>
        ///     创建关卡按钮
        /// </summary>
        public void CreateLevelButton(ItemData data, ProcessData processData)
        {
            var content = scrollRect.content;
            // 设置滑动容器大小
            content.sizeDelta = new Vector2(534 * (data.levels.Count - 1) + 960, content.sizeDelta.y);

            for (var i = 0; i < data.levels.Count; i++)
            {
                var button = GameManager.Instance.factoryManager.UIControlFactory.CreateControl("ButtonLevel").GetComponent<Button>();
                button.transform.SetParent(content, false);
                btnsLevel.Add(button);
                // 获取脚本
                var buttonLevel = button.GetComponent<ButtonLevel>();
                // 设置信息
                buttonLevel.levelID = data.levels[i].levelID;
                // 修改图片
                buttonLevel.imgMap.sprite = data.levels[i].image;
                // 添加事件
                var levelData = data.levels[i];
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
                    GameFacade.Instance.SendNotification(NotificationName.UI.START_NORMAL_GAME, levelData.levelID);
                });
            }

            // 显隐锁定图标和更新通关等级体图片
            var passedLevelData = processData.passedItemsDic[normalSceneManager.NowItemID];
            for (var i = 0; i < btnsLevel.Count; i++)
            {
                var levelID = btnsLevel[i].GetComponent<ButtonLevel>().levelID;

                if (passedLevelData.passedLevelDic.TryGetValue(levelID, out var value))
                {
                    // 设置通关等级
                    btnsLevel[i].GetComponent<ButtonLevel>().passedGrade = value;
                    // 取消锁定
                    btnsLevel[i].GetComponent<ButtonLevel>().IsLock = false;
                }
            }


            // 初始化翻页效果脚本
            pageFlipping.totalPageIndex = data.levels.Count;

            itemData = data;
        }

        /// <summary>
        ///     更新选中关卡的可使用塔的图标
        /// </summary>
        public void UpdateTowerIcon(Sprite[] icons)
        {
            for (var i = 0; i < towerIcons.Count; i++) Destroy(towerIcons[i].gameObject);

            towerIcons.Clear();

            for (var i = 0; i < icons.Length; i++)
            {
                // Image icon = Instantiate(Resources.Load<GameObject>("UI/Image/ImageTowerIcon"), transformCreateTowerIcon).GetComponent<Image>();
                var icon = GameManager.Instance.factoryManager.UIControlFactory.CreateControl("ImageTowerIcon").GetComponent<Image>();
                icon.transform.SetParent(transformCreateTowerIcon, false);
                icon.sprite = icons[i];
                towerIcons.Add(icon);
            }
        }

        /// <summary>
        ///     更新选中关卡的怪物波数
        /// </summary>
        private void UpdateWavesCount(int count)
        {
            teWavesCount.text = count.ToString();
        }

        /// <summary>
        ///     翻页完成的回调
        /// </summary>
        public void PageFlippingCompleted()
        {
            if (btnsLevel.Count == 0) return;
            // 记录当前中间的关卡按钮
            nowCenterButton = btnsLevel[pageFlipping.pageIndex - 1];
            nowCenterLevelData = itemData.levels[pageFlipping.pageIndex - 1];

            // 设置黑色遮罩
            for (var i = 0; i < btnsLevel.Count; i++)
            {
                var buttonLevel = btnsLevel[i].GetComponent<ButtonLevel>();
                // 未选中的按钮设置黑色遮罩
                var imgMapColor = buttonLevel.imgMap.color;
                buttonLevel.imgMap.color = new Color(100 / 255f, 100 / 255f, 100 / 255f, imgMapColor.a);
                var imgGardeColor = buttonLevel.imgMap.color;
                buttonLevel.imgGarde.color = new Color(100 / 255f, 100 / 255f, 100 / 255f, imgGardeColor.a);
                var imgLockColor = buttonLevel.imgMap.color;
                buttonLevel.imgLock.color = new Color(100 / 255f, 100 / 255f, 100 / 255f, imgLockColor.a);
            }

            // 选中按钮为正常颜色
            var nowCenterButtonLevel = nowCenterButton.GetComponent<ButtonLevel>();
            nowCenterButtonLevel.imgMap.color = new Color(1f, 1f, 1f, 1f);
            nowCenterButtonLevel.imgGarde.color = new Color(1f, 1f, 1f, 1f);
            nowCenterButtonLevel.imgLock.color = new Color(1f, 1f, 1f, 1f);

            // 获取icons
            var towerIconSprites = new List<Sprite>();
            for (var i = 0; i < nowCenterLevelData.mapData.towerTypeList.Count; i++)
                towerIconSprites.Add(nowCenterLevelData.mapData.GetTowerData(i).selectLevelIcon);

            // 更新面板
            UpdateTowerIcon(towerIconSprites.ToArray());
            UpdateWavesCount(nowCenterLevelData.mapData.GetWaveCount());
        }
    }
}