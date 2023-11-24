using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityEngine;
using UnityEngine.SceneManagement;


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
        GameManager.Instance.Pause = false;
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

