using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NotificationName
{
    #region 游戏逻辑相关

    public const string INIT = "Init"; // 游戏初始化
    public const string INIT_GAMEMANAGERCONTROLLER = "InitGameManagerController";
    public const string INIT_GAMEDATAPROXY = "InitGameDataProxy";
    public const string INIT_SPAWNERCONTROLLER = "InitSpawnerController";
    public const string INIT_LOADSCENECONTROLLER = "InitLoadSceneController";
    public const string INIT_GAMEDATA = "InitGameData"; // 初始化数据
    public const string INIT_END = "InitEnd"; // 游戏初始化结束
    
    public const string LOAD_GAME = "LoadGame"; // 开始加载游戏
    public const string START_GAME = "StartGame"; // 开始游戏
    public const string RESTART_GAME = "RestartGame"; // 重新开始
    public const string EXIT_GAME = "ExitGame"; // 退出游戏
    public const string INIT_GAME = "InitGame"; // 初始化游戏
    public const string PAUSE_GAME = "PauseGame"; // 暂停游戏
    public const string CONTINUE_GAME = "ContinueGame"; // 继续游戏
    public const string NEXT_LEVEL = "NextLevel"; // 下一关
    public const string STOP_GAME = "StopGame";
    public const string GAME_WIN = "GameWin";
    
    public const string START_SPAWN = "StartSpawn"; // 开始出怪
    public const string STOP_SPAWN = "StopSpawn"; // 开始出怪
    public const string REACH_ENDPOINT = "ReachEndPoint"; // 怪物到达终点
    public const string MONSTER_DEAD = "MonsterDead"; // 怪物死亡
    public const string CARROT_DEAD = "CarrotDead"; // 萝卜死亡
    public const string OPENED_BUILTPANEL = "OpenBuiltPanel"; // 建造面板已打开
    public const string SET_COLLECTINGFIRES = "CollectingFires"; // 设置集火目标
    public const string CANEL_COLLECTINGFIRES = "CanelCollectingFires"; // 取消集火

    #endregion
    

    #region 面板事件

    public const string SHOW_INITPANEL = "ShowInitPanel"; // 初始化界面
    public const string HIDE_INIPANEL = "HideInitPanel";
    
    public const string SHOW_SELECTITEMPANEL = "ShowSelectBigLevelPanel"; // 选择大关卡
    public const string SHOW_SELECTLEVELPANEL = "ShowSelectLevelPanel"; // 选择小关卡
    public const string SHOW_HELPPANEL = "ShowHelpPanel"; // 帮助面板
    public const string SHOW_BEGINPANEL = "ShowBeginPanel"; // 开始面板
    
    public const string SHOW_GAMEPANEL = "ShowGamePanel"; // 游戏面板
    public const string HIDE_GAMEPANEL = "HideGamePaenl"; 
    
    public const string SHOW_MENUPANEL = "ShowMenuPanel"; // 菜单
    public const string HIDE_MENUPANEL = "HideMenuPanel";
    
    public const string SHOW_SETTINGPANEL = "ShowSettingPanel"; // 设置
    public const string SHOW_LOSEPANEL = "ShowLosePanel"; // 失败
    public const string SHOW_WINPANEL = "ShowWinPanel"; // 胜利
    public const string SHOW_LOADINGPANEL = "ShowLoadingPanel"; // 加载面板
    
    public const string SHOW_CREATEPANEL = "ShowCreatePanel"; // 建造面板
    public const string SHOW_UPGRADEPANEL = "ShowUpGradePanel"; // 升级面板
    public const string HIDE_BUILTPANEL = "HideBuiltPanel"; // 隐藏所有建造面板
    
    public const string HIDE_LOADINGPANEL = "HideLoadingPanel"; // 隐藏加载面板

    public const string SHOW_ENDPANEL = "ShowEndPanel"; // 显示通关面板

    #endregion

    #region 控件事件

    public const string SELECT_LEVEL = "SelectLevel"; // 重新选择关卡
    public const string CREATE_TOWER = "CreateTower"; // 创建塔
    public const string SELL_TOWER = "SellTower"; // 出售塔
    public const string UPGRADE_TOWER = "UpGradeTower"; // 升级塔
    
    // GamePanel
    public const string UPDATE_MONEY = "UpdateMoney";
    public const string UPDATE_WAVESCOUNT = "UpdateWavesCount";

    #endregion

    #region 数据相关
    // 关卡数据
    public const string LOAD_LEVELDATA = "LoadLevelData";
    public const string LOADED_LEVELDATA = "LoadedLevelMapData"; // 加载完成关卡
    // 大关卡数据
    public const string LOAD_ITEMDATA = "LoadItemData";
    public const string LOADED_ITEMDATA = "LoadedItemData";
    // 音乐设置
    public const string LOAD_MUSICSETTINGDATA = "LoadMusciSettingData";
    public const string LOADED_MUSICSETTINGDATA = "LoadedMusicSettingData";
    public const string SAVE_MUSCISETTINGDATA = "SaveMusicSettingData";
    // 游戏进程数据
    public const string LOAD_PROCESSDATA = "LoadProcessData";
    public const string LOADED_PROCESSDATA = "LoadedProcessData";
    // 统计数据
    public const string LOAD_STATISTICALDATA = "LoadStatisticalData";
    public const string LOADED_STATISTICALDATA = "LoadedStatisticalData";
    // 图集
    public const string LOAD_ATLAS = "LoadAtlas"; 
    public const string LOADED_ATLAS = "LoadedAtlas"; 
    // 保存游戏进度数据
    public const string SAVE_PROCESSDATA = "SaveProcessData";

    #endregion

    #region 场景状态相关
    public static class LoadScene
    {
        // 加载场景
        public const string BEGINSCENE = "LoadScne.BeginScene";
        public const string SELECTITEMSCENE = "LoadScne.SelectItemScene";
        public const string SELECTLEVELSCENE = "LoadScne.SelectLevelScene";
        public const string GAMESCENE = "LoadScne.GameScene";
        public const string ENDSCENE = "LoadScne.EndScene";

        // 场景跳转
        public const string INIT_TO_BEGIN = "LoadScene.InitToBegin";
        public const string BEGIN_TO_SELECTITEM = "LoadScene.BeginToSelectItem";
        public const string SELECTITEM_TO_SELECTLEVEL = "LoadScene.SelectItemToSelectLevel";
        public const string SELECTITEM_TO_HELP = "LoadScene.SelectItemToHelp";
        public const string SELECTITEM_TO_BEGIN = "LoadScene.SelectItemToBegin";
        public const string SELECTLEVEL_TO_GAME = "LoadScene.SelectLevelToGame";
        public const string SELECTLEVEL_TO_SELECTITEM = "LoadScene.SelectLevelToSelectItem";
        public const string SELECTLEVEL_TO_HELP = "LoadScene.SelectLevelToHelp";
        public const string GAME_TO_END = "LoadScene.GameToEnd";
        public const string GAME_TO_SELECTLEVEL = "LoadScene.GameToSelect";
        public const string GAME_TO_GAME = "LoadScene.GameToGame";
    }
    
    // 加载场景
    public const string LOADSCENE_BEGIN = "LoadScne_Begin";
    public const string LOADSCENE_SELECTITEM = "LoadScne_SelectItem";
    public const string LOADSCENE_SELECTLEVEL = "LoadScne_SelectLevel";
    public const string LOADSCENE_GAME = "LoadScne_Game";
    public const string LOADSCENE_END = "LoadScne_End";
    
    // 场景跳转
    public const string LOADSCENE_INIT_TO_BEGIN = "LoadScene_InitToBegin";
    public const string LOADSCENE_BEGIN_TO_SELECTITEM = "LoadScene_BeginToSelectItem";
    public const string LOADSCENE_SELECTITEM_TO_SELECTLEVEL = "LoadScene_SelectItemToSelectLevel";
    public const string LOADSCENE_SELECTITEM_TO_HELP = "LoadScene_SelectItemToHelp";
    public const string LOADSCENE_SELECTITEM_TO_BEGIN = "LoadScene_SelectItemToBegin";
    public const string LOADSCENE_SELECTLEVEL_TO_GAME = "LoadScene_SelectLevelToGame";
    public const string LOADSCENE_SELECTLEVEL_TO_SELECTITEM = "LoadScene_SelectLevelToSelectItem";
    public const string LOADSCENE_SELECTLEVEL_TO_HELP = "LoadScene_SelectLevelToHelp";
    public const string LOADSCENE_GAME_TO_END = "LoadScene_GameToEnd";
    public const string LOADSCENE_GAME_TO_SELECTLEVEL = "LoadScene_GameToSelect";
    public const string LOADSCENE_GAME_TO_GAME = "LoadScene_GameToGame";

    #endregion
}
