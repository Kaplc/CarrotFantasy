using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// 开始游戏
/// </summary>
public class StartGameCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        
        // 加载场景
        ZFrameWorkSceneManager.Instance.LoadSceneAsync("3.GameScene", () =>
        {
            // 加载当前关卡数据
            GameDataProxy gameDataProxy = GameFacade.Instance.RetrieveProxy("GameDataProxy") as GameDataProxy;
            gameDataProxy?.LoadLevelData(GameManager.Instance.nowBigLevelId, GameManager.Instance.nowLevelId);
        });
    }
    
}

/// <summary>
/// 加载完成LevelData
/// </summary>
public class LoadedDataCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        
        // 注入数据
        LevelDataBody body = notification.Body as LevelDataBody;
        GameManager.Instance.monsterData = body?.monsterData;
        GameManager.Instance.nowLevelData = body?.levelData;
        
        // 加载完成数据隐藏LoadingPanel
        SendNotification(NotificationName.HIDE_LOADINGPANEL);
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
        // 显示游戏面板
        SendNotification(NotificationName.SHOW_GAMEPANEL);
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
        
        SendNotification(NotificationName.START_GAME);
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
        GameManager.Instance.isPause = true;
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
        GameManager.Instance.isPause = false;
    }
} 

