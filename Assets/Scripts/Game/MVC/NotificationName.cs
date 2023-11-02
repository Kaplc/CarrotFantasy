using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationName
{
    public const string INIT_END = "InitEnd"; // 游戏初始化结束
    public const string ENTER_BEGIN_SCENE = "EnterBeginScene"; // 进入BeginScene场景

    #region UI控件事件
    // BeginPanel
    public const string PRESS_ADVENTURE = "PressAdventure"; // 选择冒险模式
    public const string PRESS_HELP = "PressHelp"; // 按下帮助按钮
    
    // SelectPanel
    public const string PRESS_BACK = "PressBack"; // 按下返回
    public const string PRESS_START = "PressStart"; // 按下开始

    #endregion

}
