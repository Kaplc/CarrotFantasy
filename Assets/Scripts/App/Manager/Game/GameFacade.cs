using App.MVC.Controller.Commands;
using App.MVC.Model.GameData;
using App.MVC.Model.PlayerData;
using App.Static;
using App.UI.BeginScene.BeginPanel;
using App.UI.BeginScene.SettingPanel;
using App.UI.GameScene.Panel.BuiltPanel;
using App.UI.GameScene.Panel.GamePanel;
using App.UI.GameScene.Panel.LosePanel;
using App.UI.GameScene.Panel.MenuPanel;
using App.UI.GameScene.Panel.WinPanel;
using App.UI.Generic.LoadingPanel;
using App.UI.Generic.TipsPanel;
using App.UI.SelectItemScene;
using App.UI.SelectLevelScene;
using PureMVC.Patterns.Facade;
using XLua;

namespace App.MVC
{
    [LuaCallCSharp()]
    public class GameFacade : Facade
    {
        public static GameFacade Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameFacade();
                }

                return instance as GameFacade;
            }
        }

        // controller注册绑定通知
        protected override void InitializeController()
        {
            base.InitializeController();

            // 初始化通知
            RegisterCommand(NotificationName.Init.INIT, () => new InitCommand());
            RegisterCommand(NotificationName.Init.INIT_GAME_COMMAND, () => new InitGameCommand());
            RegisterCommand(NotificationName.Init.INIT_SPAWNER_COMMAND, () => new InitSpawnerCommand());
            RegisterCommand(NotificationName.Init.INIT_LOADSCENE_COMMAND, () => new InitLoadSceneController());
            RegisterCommand(NotificationName.Init.INIT_BUFFMANAGER_COMMAND, () => new InitBuffManagerControllerCommand());
            // ModelController
            RegisterCommand(NotificationName.Init.INIT_GAMEDATAPROXY_COMMAND, () => new InitGameDataProxyCommand());
            RegisterCommand(NotificationName.Init.INIT_MUSICDATAPROXY_COMMAND, () => new InitMusicDataProxyControllerCommand());
            RegisterCommand(NotificationName.Init.INIT_STATICALDATAPROXY_COMMAND, () => new InitStaticalDataProxyControllerCommand());
            RegisterCommand(NotificationName.Init.INIT_PROCESSDATAPROXY_COMMAND, () => new InitProcessDataProxyControllerCommand());


            RegisterCommand(NotificationName.Init.INIT_END, () => new InitEndCommand());
            RegisterCommand(NotificationName.Data.LOAD_ATLAS, () => new LoadAtlasCommand());


            #region 统计面板

            RegisterCommand(NotificationName.Data.LOAD_MUSIC_SETTING_DATA, () => new LoadMusicDataCommand());
            RegisterCommand(NotificationName.Data.LOAD_STATISTICAL_DATA, () => new LoadStatisticalDataCommand());
            RegisterCommand(NotificationName.Data.SAVE_MUSCISETTING_DATA, () => new SaveMusicSettingDataCommand());
            RegisterCommand(NotificationName.Data.SAVE_STATISTICAL_DATA, () => new SaveStaticalDataCommand());

            #endregion

            RegisterCommand(NotificationName.Data.REQUEST_UPDATE_ITEM_PROCESS_DATA, () => new LoadItemProcessDataCommand());
            RegisterCommand(NotificationName.Data.REQUEST_UPDATE_LEVEL_PROCESS_DATA, () => new LoadLevelProcessDataCommand());
        }

        protected override void InitializeView()
        {
            base.InitializeView();
            // 注册View
            RegisterMediator(new LoadingPanelMediator());
            RegisterMediator(new TipsPanelMediator());
            RegisterMediator(new GamePanelMediator());
            RegisterMediator(new BuiltPanelMediator());
            RegisterMediator(new MenuPanelMediator());
            RegisterMediator(new WinPanelMediator());
            RegisterMediator(new LosePanelMediator());
        }

        protected override void InitializeModel()
        {
            base.InitializeModel();

            RegisterProxy(new BeginPanelProxy());
            RegisterProxy(new SelectItemPanelProxy());
            RegisterProxy(new SelectLevelPanelProxy());
        }
    }
}