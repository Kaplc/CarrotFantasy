using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityEngine;
using UnityEngine.Events;

public class InitLoadSceneController : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);

        #region 注册加载场景命令

        GameFacade.Instance.RegisterCommand(NotificationName.LoadScene.LOADSCENE_BEGIN, () => new LoadBeginSceneCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.LoadScene.LOADSCENE_SELECTITEM, () => new LoadSelectItemSceneCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.LoadScene.LOADSCENE_SELECTLEVEL, () => new LoadSelectLevelSceneCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.LoadScene.LOADSCENE_GAME, () => new LoadGameSceneCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.LoadScene.LOADSCENE_END, () => new LoadEndSceneCommand());

        #endregion

        #region 注册场景跳转命令

        GameFacade.Instance.RegisterCommand(NotificationName.LoadScene.LOADSCENE_INIT_TO_BEGIN, () => new LoadSceneInitToBeginCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.LoadScene.LOADSCENE_BEGIN_TO_SELECTITEM, () => new LoadSceneBeginToSelectItemCommand());
        // 选择主题场景
        GameFacade.Instance.RegisterCommand(NotificationName.LoadScene.LOADSCENE_SELECTITEM_TO_SELECTLEVEL,
            () => new LoadSceneSelectItemToSelectLevelCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.LoadScene.LOADSCENE_SELECTITEM_TO_HELP, () => new LoadSceneSelectItemToHelpPanelCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.LoadScene.LOADSCENE_SELECTITEM_TO_BEGIN, () => new LoadSceneSelectItemToBeginCommand());
        // 选择关卡场景
        GameFacade.Instance.RegisterCommand(NotificationName.LoadScene.LOADSCENE_SELECTLEVEL_TO_GAME, () => new LoadSceneSelectLevelToGameCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.LoadScene.LOADSCENE_SELECTLEVEL_TO_HELP, () => new LoadSceneSelectLevelToHelpPanelCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.LoadScene.LOADSCENE_SELECTLEVEL_TO_SELECTITEM,
            () => new LoadSceneSelectLevelToSelectItemCommand());
        // 游戏场景
        GameFacade.Instance.RegisterCommand(NotificationName.LoadScene.LOADSCENE_GAME_TO_SELECTLEVEL, () => new LoadSceneGameToSelectLevelCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.LoadScene.LOADSCENE_GAME_TO_END, () => new LoadSceneGameToEndCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.LoadScene.LOADSCENE_GAME_TO_GAME, () => new LoadSceneGameToGameCommand());

        #endregion
    }
}

#region 场景加载

public class LoadBeginSceneCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        GameFacade.Instance.RegisterMediator(new BeginPanelMediator());
        
        ZFrameWorkSceneManager.Instance.LoadSceneAsync("2.BeginScene", () =>
        {
            SendNotification(NotificationName.UI.SHOW_BEGINPANEL);
            // 执行回调
            (notification.Body as UnityAction)?.Invoke();
        });
    }
}

public class LoadSelectItemSceneCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        GameFacade.Instance.RegisterMediator(new SelectItemPanelMediator());
        ZFrameWorkSceneManager.Instance.LoadSceneAsync("3.SelectItemScene", () =>
        {
            SendNotification(NotificationName.UI.SHOW_SELECTITEMPANEL);
        });
    }
}

public class LoadSelectLevelSceneCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {

        GameFacade.Instance.RegisterMediator(new SelectLevelPanelMediator());
        ZFrameWorkSceneManager.Instance.LoadSceneAsync("4.SelectLevelScene", () =>
        {
            // 根据记录的ID打开对应主题
            SendNotification(NotificationName.UI.SHOW_SELECTLEVELPANEL, GameManager.Instance.nowBigLevelId);
        });
    }
}

public class LoadGameSceneCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        SendNotification(NotificationName.UI.SHOW_LOADINGPANEL);
        
        GameFacade.Instance.RegisterMediator(new GamePanelMediator());
        GameFacade.Instance.RegisterMediator(new BuiltPanelMediator());
        GameFacade.Instance.RegisterMediator(new MenuPanelMediator());
        GameFacade.Instance.RegisterMediator(new WinPanelMediator());
        GameFacade.Instance.RegisterMediator(new LosePanelMediator());
        // 停止背景音乐
        GameFacade.Instance.SendNotification(NotificationName.Game.STOP_MUSIC);
        
        ZFrameWorkSceneManager.Instance.LoadSceneAsync("5.GameScene", () =>
        {
            // 传递LevelID
            GameFacade.Instance.SendNotification(NotificationName.Game.LOAD_GAME, (int)notification.Body);
            SendNotification(NotificationName.UI.HIDE_LOADINGPANEL);
        });
    }
}

public class LoadEndSceneCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
       
    }
}

#endregion

#region 场景跳转

/// <summary>
/// 初始化 - 开始
/// </summary>
public class LoadSceneInitToBeginCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        SendNotification(NotificationName.UI.HIDE_INIPANEL);
        GameFacade.Instance.RemoveMediator(nameof(InitPanelMediator));
        SendNotification(NotificationName.LoadScene.LOADSCENE_BEGIN);
    }
}

/// <summary>
/// 开始 - 选择主题
/// </summary>
public class LoadSceneBeginToSelectItemCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        GameFacade.Instance.RemoveMediator(nameof(BeginPanelMediator));
        SendNotification(NotificationName.LoadScene.LOADSCENE_SELECTITEM);
    }
}

#region 选择主题场景

/// <summary>
/// 选择主题 - 开始
/// </summary>
public class LoadSceneSelectItemToBeginCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        GameFacade.Instance.RemoveMediator(nameof(SelectItemPanelMediator));
        SendNotification(NotificationName.LoadScene.LOADSCENE_BEGIN);
    }
}

/// <summary>
/// 选择主题 - 选择关卡
/// </summary>
public class LoadSceneSelectItemToSelectLevelCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        GameFacade.Instance.RemoveMediator(nameof(SelectItemPanelMediator));
        
        SendNotification(NotificationName.LoadScene.LOADSCENE_SELECTLEVEL);
    }
}

/// <summary>
/// 选择主题 - 帮助
/// </summary>
public class LoadSceneSelectItemToHelpPanelCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        GameFacade.Instance.RemoveMediator(nameof(SelectItemPanelMediator));
        UnityAction ac = () =>
        {
            // false当消息体传递标识显示helpPanel无动画过渡
            SendNotification(NotificationName.UI.SHOW_HELPPANEL, false);
        };
        SendNotification(NotificationName.LoadScene.LOADSCENE_BEGIN, ac);
        
    }
}

#endregion

#region 选择关卡场景

/// <summary>
/// 选择关卡 - 游戏场景
/// </summary>
public class LoadSceneSelectLevelToGameCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        GameFacade.Instance.RemoveMediator(nameof(SelectLevelPanelMediator));
        SendNotification(NotificationName.LoadScene.LOADSCENE_GAME, (int)notification.Body);
    }
}

/// <summary>
/// 选择关卡 - 帮助
/// </summary>
public class LoadSceneSelectLevelToHelpPanelCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        GameFacade.Instance.RemoveMediator(nameof(SelectLevelPanelMediator));
        UnityAction action = () =>
        {
            SendNotification(NotificationName.UI.SHOW_HELPPANEL, false);
        };

        SendNotification(NotificationName.LoadScene.LOADSCENE_BEGIN, action);
        
    }
}

/// <summary>
/// 选择关卡 - 选择主题
/// </summary>
public class LoadSceneSelectLevelToSelectItemCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        GameFacade.Instance.RemoveMediator(nameof(SelectLevelPanelMediator));
        SendNotification(NotificationName.LoadScene.LOADSCENE_SELECTITEM);
    }
}

#endregion

#region 游戏场景

/// <summary>
/// 游戏 - 选择关卡
/// </summary>
public class LoadSceneGameToSelectLevelCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        GameFacade.Instance.RemoveMediator(nameof(GamePanelMediator));
        GameFacade.Instance.RemoveMediator(nameof(BuiltPanelMediator));
        GameFacade.Instance.RemoveMediator(nameof(MenuPanelMediator));
        GameFacade.Instance.RemoveMediator(nameof(WinPanelMediator));
        GameFacade.Instance.RemoveMediator(nameof(LosePanelMediator));
        // 开启背景音乐
        SendNotification(NotificationName.Game.PLAY_MUSIC);
        
        SendNotification(NotificationName.LoadScene.LOADSCENE_SELECTLEVEL);
    }
}

/// <summary>
/// 游戏 - 游戏 重新开始或下一关
/// </summary>
public class LoadSceneGameToGameCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        GameFacade.Instance.RemoveMediator(nameof(GamePanelMediator));
        GameFacade.Instance.RemoveMediator(nameof(BuiltPanelMediator));
        GameFacade.Instance.RemoveMediator(nameof(MenuPanelMediator));
        GameFacade.Instance.RemoveMediator(nameof(WinPanelMediator));
        GameFacade.Instance.RemoveMediator(nameof(LosePanelMediator));

        SendNotification(NotificationName.LoadScene.LOADSCENE_GAME, (int)notification.Body);
    }
}

/// <summary>
/// 游戏 - 结束
/// </summary>
public class LoadSceneGameToEndCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        GameFacade.Instance.RemoveMediator(nameof(GamePanelMediator));
        GameFacade.Instance.RemoveMediator(nameof(BuiltPanelMediator));
        GameFacade.Instance.RemoveMediator(nameof(MenuPanelMediator));
        GameFacade.Instance.RemoveMediator(nameof(WinPanelMediator));
        GameFacade.Instance.RemoveMediator(nameof(LosePanelMediator));

        SendNotification(NotificationName.LoadScene.LOADSCENE_END);
    }
}

#endregion

#endregion