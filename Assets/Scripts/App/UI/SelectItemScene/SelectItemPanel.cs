using System.Collections.Generic;
using App.Data.DataClass.Player;
using App.Game;
using App.Game.SceneManager.NormalGame.interf;
using App.Static;
using App.UI.BaseControl;
using App.UI.SelectItemScene.Control;
using Library;
using UnityEngine.UI;

namespace App.UI.SelectItemScene
{
    public class SelectItemPanel : BasePanel
    {
        public Button btnBigLevel0;
        public Button btnBigLevel1;
        public Button btnBigLevel2;

        public Button btnHome;
        public Button btnHelp;

        public Button btnLeft;
        public Button btnRight;

        public BasePageFlipping pageFlipping; // 翻页效果脚本
        public ItemLockPanel itemLockPanel; // 提示主题锁定子面板

        private INormalSceneManager sceneManger => GameManager.Instance.sceneManger as INormalSceneManager;

        protected override void Init()
        {
            btnHome.onClick.AddListener(() =>
            {
                PanelMediator.SendNotification(NotificationName.LoadScene.LOADSCENE_SELECTITEM_TO_BEGIN);
                UIManager.Instance.Hide<SelectItemPanel>(false);
            });
            btnHelp.onClick.AddListener(() =>
            {
                // 跳转开始场景的HelpPanel
                PanelMediator.SendNotification(NotificationName.LoadScene.LOADSCENE_SELECTITEM_TO_HELP);
                UIManager.Instance.Hide<SelectItemPanel>(false);
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

        public void UpdateItem(ProcessData processData)
        {
            btnBigLevel0.onClick.AddListener(() =>
            {
                // 记录选择的大关卡索引
                sceneManger.NowItemID = 0;
                // 跳转场景
                GameFacade.Instance.SendNotification(NotificationName.LoadScene.LOADSCENE_SELECTITEM_TO_SELECTLEVEL);
                UIManager.Instance.Hide<SelectItemPanel>(false);
            });
            
            btnBigLevel1.onClick.AddListener(() =>
            {
                // 判断是否解锁
                if (!processData.passedItemsDic.ContainsKey(1))
                {
                    // 未解锁
                    itemLockPanel.gameObject.SetActive(true);
                    itemLockPanel.ShowItem0 = true;
                    return;
                }
            
                sceneManger.NowItemID = 1;
                GameFacade.Instance.SendNotification(NotificationName.LoadScene.LOADSCENE_SELECTITEM_TO_SELECTLEVEL);
                UIManager.Instance.Hide<SelectItemPanel>(false);
            });
            btnBigLevel2.onClick.AddListener(() =>
            {
                if (!processData.passedItemsDic.ContainsKey(2))
                {
                    // 未解锁
                    itemLockPanel.gameObject.SetActive(true);
                    itemLockPanel.ShowItem1 = true;
                    return;
                }
            
                sceneManger.NowItemID = 2;
                GameFacade.Instance.SendNotification(NotificationName.LoadScene.LOADSCENE_SELECTITEM_TO_SELECTLEVEL);
                UIManager.Instance.Hide<SelectItemPanel>(false);
            });
            // 获取关卡解锁数据
            if (processData.passedItemsDic.TryGetValue(0, out var value))
            {
                btnBigLevel0.GetComponent<ItemButton>().UpdateUnlockMapCount(value.passedLevelCount);
            }

            if (processData.passedItemsDic.TryGetValue(1, out var value1))
            {
                btnBigLevel1.GetComponent<ItemButton>().UpdateUnlockMapCount(value1.passedLevelCount);
            }

            if (processData.passedItemsDic.TryGetValue(2, out var value2))
            {
                btnBigLevel2.GetComponent<ItemButton>().UpdateUnlockMapCount(value2.passedLevelCount);
            }
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
}

