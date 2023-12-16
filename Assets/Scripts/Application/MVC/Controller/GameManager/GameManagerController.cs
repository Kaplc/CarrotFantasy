﻿using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;

public class InitGameManagerControllerCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);

        // 初始化GameController注册命令
        GameFacade.Instance.RegisterCommand(NotificationName.LOADED_LEVELDATA, () => new AcceptLevelDataCommand());

        GameFacade.Instance.RegisterCommand(NotificationName.SELECT_BIGLEVEL, () => new SelectBigLevelCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.LOAD_GAME, () => new LoadGameCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.START_GAME, () => new StartGameCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.INIT_GAME, () => new InitGameCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.EXIT_GAME, () => new ExitGameCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.RESTART_GAME, () => new RestartGameCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.PAUSE_GAME, () => new PauseGameCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.CONTINUE_GAME, () => new ContinueGameCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.SELECT_LEVEL, () => new SelectLevelCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.NEXT_LEVEL, () => new NextLevelCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.OPENED_BUILTPANEL, () => new OpenedBuiltPanelCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.STOP_GAME, () => new StopGameCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.MONSTER_DEAD, () => new JudgeWinCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.CARROT_DEAD, () => new GameOverCommand());
    }
}

#region 关卡选择相关

/// <summary>
/// 选择大关卡命令
/// </summary>
public class SelectBigLevelCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        GameManager.Instance.nowBigLevelId = (int)notification.Body;
        SendNotification(NotificationName.SHOW_SELECTLEVELPANEL, GameManager.Instance.nowBigLevelId);
    }
}

/// <summary>
/// 菜单点击选择关卡
/// </summary>
public class SelectLevelCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        // 回到选择界面完全退出游戏 销毁缓存池
        GameManager.Instance.PoolManager.Clear();

        ZFrameWorkSceneManager.Instance.LoadSceneAsync("2.BeginScene",
            () => { SendNotification(NotificationName.SHOW_SELECTLEVELPANEL, GameManager.Instance.nowBigLevelId); });
    }
}

/// <summary>
/// 下一关
/// </summary>
public class NextLevelCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        SendNotification(NotificationName.EXIT_GAME);
        // 加载下一关
        SendNotification(NotificationName.LOAD_GAME, GameManager.Instance.nowLevelData.levelId + 1);
    }
}

#endregion

#region 游戏初始化相关

/// <summary>
/// 开始加载游戏
/// </summary>
public class LoadGameCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        SendNotification(NotificationName.SHOW_LOADINGPANEL);
        // 加载场景
        ZFrameWorkSceneManager.Instance.LoadSceneAsync("3.GameScene", () =>
        {
            // 加载当前关卡数据
            SendNotification(NotificationName.LOAD_LEVELDATA, (int)notification.Body);
            SendNotification(NotificationName.INIT_GAME);
            SendNotification(NotificationName.HIDE_LOADINGPANEL);
        });
    }
}

/// <summary>
/// 接受数据到关卡数据
/// </summary>
public class AcceptLevelDataCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        GameManager.Instance.nowLevelData = notification.Body as LevelData;
    }
}

/// <summary>
/// 初始化游戏
/// </summary>
public class InitGameCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        // 显示游戏面板
        SendNotification(NotificationName.SHOW_GAMEPANEL);
        // 游戏初始化
        GameManager.Instance.InitGame();
        // 更新面板
        SendNotification(NotificationName.UPDATE_MONEY, GameManager.Instance.money);
    }
}

#endregion

#region 游戏进程相关

/// <summary>
/// 读秒结束开始游戏
/// </summary>
public class StartGameCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        GameManager.Instance.StartGame();
        // 开始出怪
        SendNotification(NotificationName.START_SPAWN);
    }
}

/// <summary>
/// 退出游戏命令
/// </summary>
public class ExitGameCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);

        // 退出游戏
        GameManager.Instance.ExitGame();

        // 关闭相关面板
        SendNotification(NotificationName.HIDE_BUILTPANEL);
        SendNotification(NotificationName.HIDE_MENUPANEL);
        SendNotification(NotificationName.HIDE_GAMEPANEL);
    }
}

/// <summary>
/// 重新开始游戏命令
/// </summary>
public class RestartGameCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);

        SendNotification(NotificationName.EXIT_GAME);
        // 重新加载游戏
        SendNotification(NotificationName.LOAD_GAME, GameManager.Instance.nowLevelData.levelId);
    }
}

/// <summary>
/// 暂停游戏
/// </summary>
public class PauseGameCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        GameManager.Instance.PauseGame();
    }
}

/// <summary>
/// 停止游戏
/// </summary>
public class StopGameCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        GameManager.Instance.StopGame();
    }
}

/// <summary>
/// 继续游戏
/// </summary>
public class ContinueGameCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        GameManager.Instance.ContinueGame();
    }
}

/// <summary>
/// 判断游戏胜利
/// </summary>
public class JudgeWinCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        GameManager.Instance.JudgingWin();
        // 保存游戏进度
        SendNotification(NotificationName.SAVE_PROCESSDATA, (GameManager.Instance.nowLevelData.levelId, EPassedGrade.Gold));
    }
}

public class GameOverCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);

        GameManager.Instance.GameOver();
    }
}

#endregion

#region 游戏内逻辑相关

/// <summary>
/// 允许点击格子
/// </summary>
public class OpenedBuiltPanelCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        GameManager.Instance.openedBuiltPanel = (bool)notification.Body;
    }
}

#endregion