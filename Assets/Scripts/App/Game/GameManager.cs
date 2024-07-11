using System;
using App.Data;
using App.Data.DataClass.Player;
using App.Game.Buff;
using App.Game.Factory;
using App.Game.SceneManager;
using App.Game.SDK;
using App.Static;
using DG.Tweening;
using GameFramework;
using UnityEngine;
using XLua;

namespace App.Game
{
    [LuaCallCSharp]
    public class GameManager : BaseMonoSingleton<GameManager>
    {
        public IDataManager dataManager;

        public ISceneManger sceneManager;

        #region 底层框架

        public PoolManager poolManager;
        public BinaryManager binaryManager;
        public FactoryManager factoryManager;
        public MusicManger musicManger;
        public BuffManager buffManager;
        public XLuaManager xLuaManager;
        public UIManager uiManager;
        public EventCenter eventCenter;
        public SDKManager sdkManager;

        public GameFramework.SceneManager loadSceneManager;

        public AddressablesManager addressablesManager;

        #endregion
        protected override void Awake()
        {
            base.Awake();
            GameFacade.Instance.SendNotification(NotificationName.UI.SHOW_INIT_PANEL);

            #region 初始化底层框架

            poolManager = PoolManager.Instance;
            binaryManager = BinaryManager.Instance;
            factoryManager = FactoryManager.Instance;
            musicManger = MusicManger.Instance;
            buffManager = BuffManager.Instance;
            xLuaManager = XLuaManager.Instance;
            uiManager = UIManager.Instance;
            eventCenter = EventCenter.Instance;
            loadSceneManager = GameFramework.SceneManager.Instance;
            addressablesManager = AddressablesManager.Instance;
            sdkManager = SDKManager.Instance;
            dataManager = new DataManager();
            DOTween.Init();
            LoggerManager.Init();

            #endregion

            DontDestroyOnLoad(gameObject);
            InitLua();

            // 初始化完成跳转开始场景
            LoadScene("2.BeginScene", success =>
            {
                GameFacade.Instance.SendNotification(NotificationName.UI.HIDE_INIT_PANEL);
                GameFacade.Instance.SendNotification(NotificationName.UI.SHOW_BEGIN_PANEL);
                // 播放音乐
                PlayMusic();
            });
        }


        #region Unity回调

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                // 清空addressables缓存
                Caching.ClearCache();
            }
        }

        #endregion

        #region lua相关

        private void InitLua()
        {
            // 自定义lua解析路径 
            // string path = Application.dataPath + "/Scripts/App/Lua/";
            // xLuaManager.AddLuaFilePath(path);
        }

        #endregion


        #region 游戏相关

        /// <summary>
        ///     初始化游戏场景管理器
        /// </summary>
        public void SetSceneManager(ISceneManger manger)
        {
            if (sceneManager != null) sceneManager.ExitScene();
            sceneManager = manger;
        }

        public void LoadGameScene(string sceneName, int levelID)
        {
            // 加载场景
            loadSceneManager.LoadSceneAsync(sceneName, success =>
            {
                if (!success)
                {
                    Debug.LogError("场景加载失败");
                    return;
                }

                // 初始化场景
                sceneManager.InitGame(levelID);
            });
        }

        public void LoadScene(string sceneName, Action<bool> callBack)
        {
            loadSceneManager.LoadSceneAsync(sceneName, callBack);
        }

        public void SaveStatisticalData(StatisticalData data)
        {
            dataManager.StatisticalDataManager.SaveStatisticalData(data);
        }

        #endregion

        #region 音乐相关

        public void StopMusic()
        {
            musicManger.StopMusic();
        }

        public void PlayMusic()
        {
            musicManger.PlayMusic("Music/BGMusic", 1, true);
            musicManger.MuteMusic(!dataManager.MusicDataManager.MusicOpen);
        }

        public void PlaySound(string path, float volume, bool loop)
        {
            if (dataManager.MusicDataManager.SoundOpen)
                musicManger.PlaySound(path, 1, loop);
            else
                musicManger.PlaySound(path, 0, loop);
        }

        #endregion
    }
}