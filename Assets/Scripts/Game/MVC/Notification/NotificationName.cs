using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationName
{
    public const string INIT = "Init"; // 游戏初始化
    public const string INIT_END = "InitEnd"; // 游戏初始化结束
    public const string LOADING_SCENE = "LoadScene"; // 进入场景
    public const string START_GAME = "StartGame"; // 开始游戏

    #region 打开面板事件
    
    public const string SHOW_SELECTBIGLEVELPANEL = "ShowSelectBigLevelPanel"; // 选择大关卡
    public const string SHOW_SELECTLEVELPANEL = "ShowSelectLevelPanel"; // 选择小关卡
    public const string SHOW_HELPPANEL = "ShowHelpPanel"; // 帮助面板
    public const string SHOW_BEGINPANEL = "ShowBeginPanel"; // 开始面板
    public const string SHOW_MENUPANEL = "ShowMenuPanel"; // 菜单
    public const string SHOW_SETTINGPANEL = "ShowSettingPanel"; // 设置
    public const string SHOW_LOSEPANEL = "ShowLosePanel"; // 失败
    public const string SHOW_WINPANEL = "ShowWinPanel"; // 胜利
    public const string SHOW_LOADINGPANEL = "ShowLoadingPanel"; // 加载面板
    public const string HIDE_LOADINGPANEL = "HideLoadingPanel"; // 隐藏加载面板
    
    #endregion

    #region 控件事件

    public const string SELECT_LEVEL = "SelectLevel"; // 重新选择关卡

    #endregion
}
