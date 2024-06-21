using App.Data;
using App.Data.DataClass.Player;
using App.Game.Buff;
using App.Game.Factory;
using App.Game.SceneManager;
using App.Game.SDK;
using App.Static;
using Library;
using UnityEngine;
using UnityEngine.Events;
using XLua;

namespace App.Game
{
    [LuaCallCSharp]
    public class GameManager : BaseMonoSingleton<GameManager>
    {
        #region 底层框架

        public PoolManager poolManager;
        public BinaryManager binaryManager;
        public FactoryManager factoryManager ;
        public MusicManger musicManger;
        public BuffManager buffManager;
        public XLuaManager xLuaManager;
        public UIManager uiManager;
        public EventCenter eventCenter;
        public SDKManager sdkManager;

        #endregion
        
        public ISceneManger sceneManager;
        public IDataManager dataManager;
        
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

            // 初始化Sdk
            GameObject sdkManagerObj = new GameObject(name:"SDKManager");
            sdkManager = sdkManagerObj.AddComponent<SDKManager>();
            DontDestroyOnLoad(sdkManagerObj);
            // 自定义lua解析路径 
            string path = Application.dataPath + "/Scripts/App/Lua/"; 
            xLuaManager.AddLuaFilePath(path);
            xLuaManager.DoFile("Init");
            
            #endregion
            
            
            DontDestroyOnLoad(gameObject);

            // 初始化数据
            dataManager = new DataManager();
            // 初始化完成跳转开始场景
            LoadScene("2.BeginScene",() =>
            {
                GameFacade.Instance.SendNotification(NotificationName.UI.HIDE_INIT_PANEL);
                GameFacade.Instance.SendNotification(NotificationName.UI.SHOW_BEGIN_PANEL);
                // 播放音乐
                PlayMusic();
            });
        }

        #region 游戏相关

        /// <summary>
        /// 初始化游戏场景管理器
        /// </summary>
        public void SetSceneManager(ISceneManger manger)
        {
            sceneManager = manger;
        }

        public void LoadGameScene(string sceneName, int levelID)
        {
            // 加载场景
            ZFrameWorkSceneManager.Instance.LoadSceneAsync(sceneName, () =>
            {
                // 初始化场景
                sceneManager.InitGame(levelID);
            });
        }

        public void LoadScene(string sceneName, UnityAction callBack)
        {
            ZFrameWorkSceneManager.Instance.LoadSceneAsync(sceneName, callBack);
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
            {
                musicManger.PlaySound(path, 1, loop);
            }
            else
            {
                musicManger.PlaySound(path, 0, loop);
            }
        }

        #endregion
    }
}