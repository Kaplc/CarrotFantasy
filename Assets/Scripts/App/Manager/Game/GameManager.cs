using App.DataClass.Player;
using App.Manager.Game.SceneManager.Interf;
using App.MVC.Model;
using App.SDK;
using App.Static;
using Library;
using UnityEngine;
using XLua;

namespace App.MVC.Controller
{
    [LuaCallCSharp]
    public class GameManager : BaseMonoSingleton<GameManager>
    {
        #region 框架

        public PoolManager PoolManager => PoolManager.Instance;
        public BinaryManager BinaryManager => BinaryManager.Instance;
        public FactoryManager FactoryManager => FactoryManager.Instance;
        public MusicManger MusicManger => MusicManger.Instance;
        public BuffManager BuffManager => BuffManager.Instance;
        public XLuaManager XLuaManager => XLuaManager.Instance;
        public UIManager UIManager => UIManager.Instance;
        public SDKManager sdkManager;

        #endregion

        #region 数据模块

        public IDataManager dataManager;

        #endregion
        
        public ISceneManger sceneManger;
        
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
            GameFacade.Instance.SendNotification(NotificationName.Init.INIT);
            // 初始化数据
            dataManager = new DataManager();
            // 初始化音乐
            GameFacade.Instance.SendNotification(NotificationName.Data.LOAD_MUSIC_SETTING_DATA);
            GameFacade.Instance.SendNotification(NotificationName.Game.PLAY_MUSIC);
            
            // 初始化Sdk
            GameObject sdkManagerObj = new GameObject(name:"SDKManager");
            sdkManager = sdkManagerObj.AddComponent<SDKManager>();
            DontDestroyOnLoad(sdkManagerObj);
        
            // add lua loader 
            // custom loader
            string path = Application.dataPath + "/Scripts/App/Lua/"; 
            XLuaManager.AddLuaFilePath(path);
            XLuaManager.DoFile("Init");
        }

        #region 游戏相关

        /// <summary>
        /// 初始化游戏场景管理器
        /// </summary>
        public void SetSceneManager(ISceneManger manger)
        {
            sceneManger = manger;
        }
        
        public void SaveGameData()
        {
            
        }

        #endregion
    }
}