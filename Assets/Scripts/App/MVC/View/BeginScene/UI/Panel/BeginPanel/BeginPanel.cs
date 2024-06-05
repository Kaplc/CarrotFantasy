using App.MVC.Controller;
using App.Static;
using Library.UIManager;
using UnityEngine;
using UnityEngine.UI;
using XLua;

namespace App.MVC.View.BeginScene.UI.Panel.BeginPanel
{
    [Hotfix()]
    public class BeginPanel : BasePanel
    {
        public Button btnAdventure;
        public Button btnBoss;
        public Button btnMonster;
        public Button btnSetting;
        public Button btnHelp;

        public Button btnPositioning;
    
        public Animator animator;
        // 作为子面板
        public SettingPanel.SettingPanel settingPanel;

        private AndroidJavaClass javaClass;
        private AndroidJavaObject javaObject;

        protected override void Init()
        {
            btnAdventure.onClick.AddListener(() =>
            {
                // 发送打开选择大关卡的消息
                PanelMediator.SendNotification(NotificationName.LoadScene.LOADSCENE_BEGIN_TO_SELECTITEM);
                // 隐藏自己
                UIManager.Instance.Hide<BeginPanel>(false);
            });
            btnBoss.onClick.AddListener(() => { 
                // 热更新Boss模式
                ShowBossPanel();
            });
            btnMonster.onClick.AddListener(() => { });
            btnSetting.onClick.AddListener(() =>
            {
                PanelMediator.SendNotification(NotificationName.UI.SHOW_SETTINGPANEL);
            
            });
            btnHelp.onClick.AddListener(() => { PanelMediator.SendNotification(NotificationName.UI.SHOW_HELPPANEL, true); });

            btnPositioning.onClick.AddListener(() =>
            {
                GameManager.Instance.sdkManager.StartPositioning();
            });
        }
    
        public void ShowBossPanel()
        {
            GameManager.Instance.XLuaManager.DoString("UIManager:ShowPanel('BossPanel', EUILayers.Middle)");
        }

        private void OnDestroy() {
            GameManager.Instance.sdkManager.Dispose();
        }
    }
}

