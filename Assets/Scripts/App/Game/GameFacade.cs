using App.Game.Commands;
using App.Static;
using App.UI.BeginScene.BeginPanel;
using App.UI.BeginScene.SettingPanel;
using App.UI.GameScene.Panel.BuiltPanel;
using App.UI.GameScene.Panel.BuiltPanel.Command;
using App.UI.GameScene.Panel.GamePanel;
using App.UI.GameScene.Panel.LosePanel;
using App.UI.GameScene.Panel.MenuPanel;
using App.UI.GameScene.Panel.MenuPanel.Command;
using App.UI.GameScene.Panel.WinPanel;
using App.UI.Generic.LoadingPanel;
using App.UI.Generic.TipsPanel;
using App.UI.SelectItemScene;
using App.UI.SelectLevelScene;
using PureMVC.Patterns.Facade;
using PureMVC.Patterns.Proxy;
using XLua;

namespace App.Game
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
            
            #region 统计面板

            RegisterCommand(NotificationName.Data.LOAD_MUSIC_SETTING_DATA, () => new LoadMusicDataCommand());
            RegisterCommand(NotificationName.Data.LOAD_STATISTICAL_DATA, () => new LoadStatisticalDataCommand());
            RegisterCommand(NotificationName.Data.SAVE_MUSIC_SETTING_DATA, () => new SaveMusicSettingDataCommand());

            #endregion

            RegisterCommand(NotificationName.Data.REQUEST_UPDATE_ITEM_PROCESS_DATA, () => new LoadItemProcessDataCommand());
            RegisterCommand(NotificationName.Data.REQUEST_UPDATE_LEVEL_PROCESS_DATA, () => new LoadLevelProcessDataCommand());

            #region 选择面板

            RegisterCommand(NotificationName.UI.START_NORMAL_GAME, () => new StartNormalGameCommand());

            #endregion

            #region 设置面板

            RegisterCommand(NotificationName.Game.MUTE_MUSIC, () => new MuteMusicCommand());
            RegisterCommand(NotificationName.Game.MUTE_SOUND, () => new MuteSoundCommand());

            #endregion

            #region 注册场景跳转命令

            RegisterCommand(NotificationName.LoadScene.LOADSCENE_INIT_TO_BEGIN, () => new LoadSceneInitToBeginCommand());
            RegisterCommand(NotificationName.LoadScene.LOADSCENE_BEGIN_TO_SELECTITEM, () => new LoadSceneBeginToSelectItemCommand());
            RegisterCommand(NotificationName.LoadScene.LOADSCENE_SELECTITEM_TO_SELECTLEVEL, () => new LoadSceneSelectItemToSelectLevelCommand());
            RegisterCommand(NotificationName.LoadScene.LOADSCENE_SELECTITEM_TO_HELP, () => new LoadSceneSelectItemToHelpPanelCommand());
            RegisterCommand(NotificationName.LoadScene.LOADSCENE_SELECTITEM_TO_BEGIN, () => new LoadSceneSelectItemToBeginCommand());
            // 选择关卡场景
            RegisterCommand(NotificationName.LoadScene.LOADSCENE_SELECTLEVEL_TO_GAME, () => new LoadSceneSelectLevelToGameCommand());
            RegisterCommand(NotificationName.LoadScene.LOADSCENE_SELECTLEVEL_TO_HELP, () => new LoadSceneSelectLevelToHelpPanelCommand());
            RegisterCommand(NotificationName.LoadScene.LOADSCENE_SELECTLEVEL_TO_SELECTITEM, () => new LoadSceneSelectLevelToSelectItemCommand());
            // 游戏场景
            RegisterCommand(NotificationName.LoadScene.LOADSCENE_GAME_TO_SELECTLEVEL, () => new LoadSceneGameToSelectLevelCommand());
            RegisterCommand(NotificationName.LoadScene.LOADSCENE_GAME_TO_END, () => new LoadSceneGameToEndCommand());
            RegisterCommand(NotificationName.LoadScene.LOADSCENE_GAME_TO_GAME, () => new LoadSceneGameToGameCommand());

            #endregion

            #region 注册加载场景命令

            RegisterCommand(NotificationName.LoadScene.LOADSCENE_BEGIN, () => new LoadBeginSceneCommand());
            RegisterCommand(NotificationName.LoadScene.LOADSCENE_SELECTITEM, () => new LoadSelectItemSceneCommand());
            RegisterCommand(NotificationName.LoadScene.LOADSCENE_SELECTLEVEL, () => new LoadSelectLevelSceneCommand());
            RegisterCommand(NotificationName.LoadScene.LOADSCENE_GAME, () => new LoadGameSceneCommand());

            #endregion
            

            #region WinPanel
            
            RegisterCommand(NotificationName.UI.SELECT_LEVEL, () => new SelectLevelCommand());
            RegisterCommand(NotificationName.UI.NEXT_LEVEL, () => new NextLevelCommand());

            #endregion

            #region GamePanel

            RegisterCommand(NotificationName.Game.START_GAME, () => new StartGameCommand());
            RegisterCommand(NotificationName.Game.RESUME_GAME, () => new ResumeGameCommand());
            RegisterCommand(NotificationName.Game.PAUSE_GAME, () => new PauseGameCommand());
            RegisterCommand(NotificationName.Game.SET_SPEED_UP, () => new SetSpeedUpCommand());
            
            #endregion

            #region MenuPanel

            RegisterCommand(NotificationName.Game.RESTART_GAME, () => new RestartGameCommand());

            #endregion

            #region BuiltPanel

            RegisterCommand(NotificationName.UI.UPGRADE_TOWER, () => new UpGradTowerCommand());
            RegisterCommand(NotificationName.UI.CREATE_TOWER, () => new CreateTowerCommand());
            RegisterCommand(NotificationName.UI.SELL_TOWER, () => new SellTowerCommand());

            #endregion
        }

        protected override void InitializeView()
        {
            base.InitializeView();
            // 注册View
            RegisterMediator(new BeginPanelMediator());
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

        public T RetrieveProxy<T>() where T : Proxy
        {
            return RetrieveProxy(typeof(T).Name) as T;
        }
    }
}