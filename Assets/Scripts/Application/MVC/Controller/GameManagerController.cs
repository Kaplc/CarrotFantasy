using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityEngine;
using UnityEngine.SceneManagement;


public class InitGameManagerControllerCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        
        // 初始化GameController注册命令
        GameFacade.Instance.RegisterCommand(NotificationName.LOADED_LEVELDATA, ()=> new AcceptDataCommand());

        GameFacade.Instance.RegisterCommand(NotificationName.LOAD_GAME, () => new LoadGameCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.START_GAME, () => new StartGameCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.INIT_GAME, ()=> new InitGameCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.EXIT_GAME, () => new ExitGameCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.RESTART_GAME, ()=>new RestartGameCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.PAUSE_GAME, ()=>new PauseGameCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.CONTINUE_GAME, ()=>new ContinueGameCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.ALLOW_CLICKCELL, () => new AllowClickCellCommand());
    }
}

/// <summary>
/// 开始加载游戏
/// </summary>
public class LoadGameCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        
        // 加载场景
        ZFrameWorkSceneManager.Instance.LoadSceneAsync("3.GameScene", () =>
        {
            // 加载当前关卡数据
            GameDataProxy gameDataProxy = GameFacade.Instance.RetrieveProxy("GameDataProxy") as GameDataProxy;
            gameDataProxy?.LoadLevelData(GameManager.Instance.nowLevelId);
        });
    }
}

/// <summary>
/// 接受数据
/// </summary>
public class AcceptDataCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);

        switch (notification.Name)
        {
            case NotificationName.LOADED_LEVELDATA:
                LevelDataBody body = notification.Body as LevelDataBody;
                GameManager.Instance.nowLevelData = body?.levelData;
                GameManager.Instance.monstersData = body?.monstersData;
                GameManager.Instance.towersData = body?.towersData;
                break;
        }
        
        SendNotification(NotificationName.INIT_GAME);
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
        
        // 游戏初始化
        GameManager.Instance.GameInit();
        
        // 加载完成数据隐藏 LoadingPanel
        SendNotification(NotificationName.HIDE_LOADINGPANEL);
        // 显示游戏面板
        SendNotification(NotificationName.SHOW_GAMEPANEL);
    }
}

/// <summary>
/// 读秒结束开始游戏
/// </summary>
public class StartGameCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        GameManager.Instance.GameStart();
        // 开始出怪
        GameManager.Instance.EventCenter.TriggerEvent(NotificationName.START_SPAWN);
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
        GameManager.Instance.GameExit();
        // 销毁缓存池
        GameManager.Instance.PoolManager.Clear();
        // 关闭相关面板
        SendNotification(NotificationName.HIDE_BUILTPANEL);
        SendNotification(NotificationName.HIDE_MENUPANEL);
        // 进入选择面板
        SendNotification(NotificationName.SELECT_LEVEL);
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
        
        // 退出游戏
        GameManager.Instance.GameExit();
        // 关闭相关面板
        SendNotification(NotificationName.HIDE_BUILTPANEL);
        SendNotification(NotificationName.HIDE_MENUPANEL);
        // 重新加载游戏
        SendNotification(NotificationName.LOAD_GAME);
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
        GameManager.Instance.GamePause();
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
        GameManager.Instance.GameContinue();
    }
}

/// <summary>
/// 允许点击格子
/// </summary>
public class AllowClickCellCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        GameManager.Instance.allowClickCell = (bool)notification.Body;
    }
}