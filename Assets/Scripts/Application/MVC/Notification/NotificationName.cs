using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationName
{
    #region 游戏进程相关

    public const string INIT = "Init"; // 游戏初始化
    public const string INIT_END = "InitEnd"; // 游戏初始化结束
    public const string START_GAME = "StartGame"; // 开始游戏
    public const string START_SPAWN = "StartSpawn"; // 开始出怪
    public const string REACH_ENDPOINT = "ReachEndPoint"; // 怪物到达终点
    public const string CARROT_DEAD = "CarrotDead"; // 萝卜死亡
    #endregion
    

    #region 打开面板事件
    
    public const string SHOW_SELECTBIGLEVELPANEL = "ShowSelectBigLevelPanel"; // 选择大关卡
    public const string SHOW_SELECTLEVELPANEL = "ShowSelectLevelPanel"; // 选择小关卡
    public const string SHOW_HELPPANEL = "ShowHelpPanel"; // 帮助面板
    public const string SHOW_BEGINPANEL = "ShowBeginPanel"; // 开始面板
    public const string SHOW_GAMEPANEL = "ShowGamePanel"; // 游戏面板
    public const string SHOW_MENUPANEL = "ShowMenuPanel"; // 菜单
    public const string SHOW_SETTINGPANEL = "ShowSettingPanel"; // 设置
    public const string SHOW_LOSEPANEL = "ShowLosePanel"; // 失败
    public const string SHOW_WINPANEL = "ShowWinPanel"; // 胜利
    public const string SHOW_LOADINGPANEL = "ShowLoadingPanel"; // 加载面板
    
    public const string HIDE_LOADINGPANEL = "HideLoadingPanel"; // 隐藏加载面板
    public const string HIDE_MENUPANEL = "HideMenuPanel"; // 关闭菜单面板
    
    #endregion

    #region 控件事件

    public const string SELECT_LEVEL = "SelectLevel"; // 重新选择关卡

    #endregion

    #region 数据相关
    public const string LOADED_LEVELDATA = "LoadedLevelData"; // 加载完成关卡数据
    public const string LOADED_PLAYERDATA = "LoadedPlayerData"; // 加载完成玩家数据
    public const string LOADED_MUSICSETTINGDATA = "LoadedMusicSettingData"; // 加载完成音乐设置数据

    #endregion
}
