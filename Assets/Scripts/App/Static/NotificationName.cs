namespace App.Static
{
    public static class NotificationName
    {
        public static class Game
        {
            public const string LOAD_GAME = "LOAD_GAME"; // 开始加载游戏
            public const string START_GAME = "START_GAME"; // 开始游戏
            public const string PAUSE_GAME = "PAUSE_GAME"; // 暂停游戏
            public const string RESTART_GAME = "RESTART_GAME"; // 重新开始
            public const string RESUME_GAME = "RESUME_GAME"; // 继续游戏

            public const string STOP_GAME = "StopGame";
            public const string REACH_ENDPOINT = "ReachEndPoint"; // 怪物到达终点
            public const string CARROT_DEAD = "CarrotDead"; // 萝卜死亡
            public const string UPDATE_MONEY = "UpdateMoney"; // 更新钱

            public const string SET_SPEED_UP = "TwoSpeed"; // 两倍速

            // 声音相关
            public const string PLAY_MUSIC = "PlayMusic";
            public const string MUTE_MUSIC = "MuteMusic";
            public const string STOP_MUSIC = "StopMusic";
            public const string PLAY_SOUND = "PlaySound";

            public const string MUTE_SOUND = "MuteSound";
            // Buff
        }

        public static class UI
        {
            public const string SHOW_INIT_PANEL = "SHOW_INIT_PANEL"; // 初始化界面
            public const string HIDE_INIT_PANEL = "HIDE_INIT_PANEL";

            public const string HIDE_LOADING_PANEL = "HIDE_LOADING_PANEL"; // 隐藏加载面板
            public const string SHOW_END_PANEL = "SHOW_END_PANEL"; // 显示通关面板
            public const string SHOW_TIPS_PANEL = "SHOW_TIPS_PANEL"; // 显示提示面板

            #region 选择界面

            public const string SHOW_SELECT_ITEM_PANEL = "SHOW_SELECT_ITEM_PANEL"; // 选择大关卡
            public const string ITEM_DATA_UPDATED = "ITEM_DATA_UPDATED";
            public const string SHOW_SELECT_LEVEL_PANEL = "SHOW_SELECT_LEVEL_PANEL"; // 选择小关卡
            public const string LEVEL_DATA_UPDATED = "LEVEL_DATA_UPDATED";
            public const string START_NORMAL_GAME = "START_NORMAL_GAME"; // 开始普通模式

            #endregion

            #region 开始界面

            public const string SHOW_BEGIN_PANEL = "SHOW_BEGIN_PANEL"; // 开始面板
            public const string MUSIC_SETTING_UPDATED = "MUSIC_SETTING_UPDATED"; // 更新音乐设置
            public const string STATISTICAL_DATA_UPDATED = "STATISTICAL_DATA_UPDATED"; // 更新统计数据
            public const string SHOW_HELP_PANEL = "SHOW_HELP_PANEL"; // 帮助面板

            #endregion

            #region 游戏面板

            public const string SHOW_GAME_PANEL = "SHOW_GAME_PANEL"; // 游戏面板
            public const string HIDE_GAME_PANEL = "HIDE_GAME_PANEL";
            public const string MONEY_UPDATED = "MONEY_UPDATED";
            public const string UPDATE_MONEY = "UPDATE_MONEY";
            public const string WAVES_COUNT_UPDATED = "WAVES_COUNT_UPDATED";
            public const string UPDATE_WAVES_COUNT = "UPDAT_WAVES_COUNT";
            public const string SELECT_LEVEL = "SELECT_LEVEL"; // 重新选择关卡
            public const string CREATE_TOWER = "CREATE_TOWER"; // 创建塔
            public const string SELL_TOWER = "SELL_TOWER"; // 出售塔
            public const string UPGRADE_TOWER = "UPGRADE_TOWER"; // 升级塔

            #endregion

            #region 设置面板

            public const string SHOW_SETTING_PANEL = "SHOW_SETTING_PANEL"; // 设置
            public const string SHOW_LOSE_PANEL = "SHOW_LOSE_PANEL"; // 失败
            public const string SHOW_WIN_PANEL = "SHOW_WIN_PANEL"; // 胜利
            public const string SHOW_LOADING_PANEL = "SHOW_LOADING_PANEL"; // 加载面板
            public const string NEXT_LEVEL = "NEXT_LEVEL";

            #endregion

            #region 菜单

            public const string SHOW_MENU_PANEL = "SHOW_MENU_PANEL"; // 菜单
            public const string HIDE_MENU_PANEL = "HIDE_MENU_PANEL";

            #endregion

            #region 建造面板

            public const string SHOW_CREATE_PANEL = "SHOW_CREATE_PANEL"; // 建造面板
            public const string SHOW_UPGRADE_PANEL = "SHOW_UPGRADE_PANEL"; // 升级面板
            public const string SHOW_CANT_BUILT_ICON = "SHOW_CANT_BUILT_ICON"; // 显示禁止建造图标
            public const string HIDE_BUILT_PANEL = "HIDE_BUILT_PANEL"; // 隐藏所有建造面板

            #endregion
        }

        public static class Data
        {
            // 音乐设置
            public const string LOAD_MUSIC_SETTING_DATA = "LoadMusciSettingData";

            public const string SAVE_MUSIC_SETTING_DATA = "SaveMusicSettingData";

            // 游戏进程数据
            public const string REQUEST_UPDATE_ITEM_PROCESS_DATA = "REQUEST_UPDATE_ITEM_DATA";

            public const string REQUEST_UPDATE_LEVEL_PROCESS_DATA = "REQUEST_UPDATE_LEVEL_DATA";

            // 统计数据
            public const string LOAD_STATISTICAL_DATA = "LoadStatisticalData";
            public const string SAVE_STATISTICAL_DATA = "SAVE_STATISTICALDATA";
            public const string CHANGE_DESTROYOBSTACLE_COUNT = "CHANGE_DESTROYOBSTACLE_COUNT";
        }

        #region 场景状态相关

        public static class LoadScene
        {
            // 加载场景
            public const string LOADSCENE_BEGIN = "LoadScne.BeginScene";
            public const string LOADSCENE_SELECTITEM = "LoadScne.SelectItemScene";
            public const string LOADSCENE_SELECTLEVEL = "LoadScne.SelectLevelScene";
            public const string LOADSCENE_GAME = "LoadScne.GameScene";
            public const string LOADSCENE_END = "LoadScne.EndScene";

            // 场景跳转
            public const string LOADSCENE_INIT_TO_BEGIN = "LoadScene.InitToBegin";
            public const string LOADSCENE_BEGIN_TO_SELECTITEM = "LoadScene.BeginToSelectItem";
            public const string LOADSCENE_SELECTITEM_TO_SELECTLEVEL = "LoadScene.SelectItemToSelectLevel";
            public const string LOADSCENE_SELECTITEM_TO_HELP = "LoadScene.SelectItemToHelp";
            public const string LOADSCENE_SELECTITEM_TO_BEGIN = "LoadScene.SelectItemToBegin";
            public const string LOADSCENE_SELECTLEVEL_TO_GAME = "LoadScene.SelectLevelToGame";
            public const string LOADSCENE_SELECTLEVEL_TO_SELECTITEM = "LoadScene.SelectLevelToSelectItem";
            public const string LOADSCENE_SELECTLEVEL_TO_HELP = "LoadScene.SelectLevelToHelp";
            public const string LOADSCENE_GAME_TO_END = "LoadScene.GameToEnd";
            public const string LOADSCENE_GAME_TO_SELECTLEVEL = "LoadScene.GameToSelect";
            public const string LOADSCENE_GAME_TO_GAME = "LoadScene.GameToGame";
        }

        #endregion
    }
}