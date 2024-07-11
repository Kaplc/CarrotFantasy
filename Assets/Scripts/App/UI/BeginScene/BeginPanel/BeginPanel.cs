using System.IO;
using App.Game;
using App.Static;
using DG.Tweening;
using GameFramework;
using UnityEngine;
using UnityEngine.UI;

namespace App.UI.BeginScene.BeginPanel
{
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

        // Update Game
        private Transform updatePanelTsf;
        private Button btnSureUpdate;
        private Button btnCancelUpdate;

        private Text txUpdateTips;

        // Clear
        private Button btnClear;

        private void OnDestroy()
        {
            GameManager.Instance.sdkManager.Dispose();
        }

        protected override void Init()
        {
            // UpdatePanel
            updatePanelTsf = transform.Find("UpdatePanel");
            btnSureUpdate = updatePanelTsf.Find("ButtonSureUpdate").GetComponent<Button>();
            btnCancelUpdate = updatePanelTsf.Find("ButtonCancelUpdate").GetComponent<Button>();
            txUpdateTips = updatePanelTsf.Find("TextUpdateTips").GetComponent<Text>();
            // 
            btnClear = transform.Find("ButtonClear").GetComponent<Button>();

            btnAdventure.onClick.AddListener(() =>
            {
                // 发送打开选择大关卡的消息
                PanelMediator.SendNotification(NotificationName.LoadScene.LOADSCENE_BEGIN_TO_SELECTITEM);
                // 隐藏自己
                UIManager.Instance.Hide<BeginPanel>(false);
            });
            btnBoss.onClick.AddListener(() =>
            {
                // 热更新Boss模式
                ShowBossPanel();
            });
            btnMonster.onClick.AddListener(() => { });
            btnSetting.onClick.AddListener(() => { PanelMediator.SendNotification(NotificationName.UI.SHOW_SETTING_PANEL); });
            btnHelp.onClick.AddListener(() => { PanelMediator.SendNotification(NotificationName.UI.SHOW_HELP_PANEL, true); });
            // SDK
            btnPositioning.onClick.AddListener(() => { GameManager.Instance.sdkManager.StartPositioning(); });
            // Claer
            btnClear.onClick.AddListener(() =>
            {
                Caching.ClearCache();

                string cachePath = Application.persistentDataPath + "/com.unity.addressables";
                if (Directory.Exists(cachePath))
                {
                    Directory.Delete(cachePath, true);
                    Debug.Log("Addressables cache directory deleted.");
                }
                else
                {
                    Debug.Log("Addressables cache directory does not exist.");
                }

                GameManager.Instance.addressablesManager.ReleaseAll();
            });
            // UpdatePanel
            btnSureUpdate.onClick.AddListener(() =>
            {
                GameManager.Instance.addressablesManager.UpdateCatalogs((process) =>
                {
                    txUpdateTips.text = $"正在更新...{1 / process}%";
                    if (process == -2)
                    {
                        updatePanelTsf.gameObject.SetActive(false);
                        // 更新完成
                        LoadAllLuaFile();
                    }
                });
                // 隐藏取消按钮
                btnCancelUpdate.gameObject.SetActive(false);
            });
            btnCancelUpdate.onClick.AddListener(() =>
            {
                updatePanelTsf.gameObject.SetActive(false);
            });

            // 默认更新面板隐藏
            updatePanelTsf.gameObject.SetActive(false);
        }

        private void ShowUpdatePanel()
        {
            updatePanelTsf.gameObject.SetActive(true);
            txUpdateTips.text = "检查到游戏有更新, 是否更新?";
        }

        public void ShowBossPanel()
        {
            // 检查是否有更新
            GameManager.Instance.addressablesManager.CheckCatalogs((exist) =>
            {
                if (exist)
                {
                    ShowUpdatePanel();
                }
                else
                {
                    LoadAllLuaFile();
                }

            });
        }

        private void LoadAllLuaFile()
        {
            // addressables提前加载lua文件
            GameManager.Instance.addressablesManager.PreloadAssetsAsync<TextAsset>(isDone =>
            {
                if (isDone)
                {
                    GameManager.Instance.xLuaManager.DoFile("Init");
                    GameManager.Instance.xLuaManager.DoString("Main:Main()");
                }
                else
                {
                    Debug.Log("Lua文件加载失败");
                }

            }, "Lua");
        }
    }
}