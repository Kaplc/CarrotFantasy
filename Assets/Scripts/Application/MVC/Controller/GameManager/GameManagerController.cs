using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;

public class InitGameManagerControllerCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        // 初始化GameController注册命令
        GameFacade.Instance.RegisterCommand(NotificationName.Data.LOADED_LEVELDATA, () => new AcceptLevelDataCommand());

        GameFacade.Instance.RegisterCommand(NotificationName.Game.LOAD_GAME, () => new LoadGameCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.Game.START_GAME, () => new StartGameCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.Game.INIT_GAME, () => new InitGameCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.Game.EXIT_GAME, () => new ExitGameCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.Game.RESTART_GAME, () => new RestartGameCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.Game.PAUSE_GAME, () => new PauseGameCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.Game.CONTINUE_GAME, () => new ContinueGameCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.UIEvent.SELECT_LEVEL, () => new SelectLevelCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.Game.NEXT_LEVEL, () => new NextLevelCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.Game.OPENED_BUILTPANEL, () => new OpenedBuiltPanelCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.Game.STOP_GAME, () => new StopGameCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.Game.GAME_WIN, () => new GameWinCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.Game.CARROT_DEAD, () => new GameOverCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.Game.UPDATE_MONEY, () => new UpdateMoneyCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.Game.TWOSPEED, () => new TwoSpeedCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.Game.PLAY_MUSIC, () => new PlayMusicCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.Game.MUTE_MUSIC, () => new MuteMusicCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.Game.STOP_MUSIC, () => new StopMusicCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.Game.PLAY_SOUND, () => new PlaySoundCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.Game.MUTE_SOUND, () => new MuteSoundCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.Data.LOADED_MUSICSETTINGDATA, () => new AcceptMusicSettingDataCommand());
    }
}

#region 关卡选择相关

/// <summary>
/// 菜单点击选择关卡
/// </summary>
public class SelectLevelCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        // 回到选择界面完全退出游戏 销毁缓存池
        GameManager.Instance.PoolManager.Clear();

        SendNotification(NotificationName.LoadScene.LOADSCENE_GAME_TO_SELECTLEVEL);
    }
}

/// <summary>
/// 下一关
/// </summary>
public class NextLevelCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        SendNotification(NotificationName.Game.EXIT_GAME);
        // 加载下一关
        SendNotification(NotificationName.LoadScene.LOADSCENE_GAME_TO_GAME, GameManager.Instance.nowLevelData.levelID + 1);
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
        // 加载当前关卡数据
        SendNotification(NotificationName.Data.LOAD_LEVELDATA, (int)notification.Body);
        SendNotification(NotificationName.Game.INIT_GAME);
    }
}

/// <summary>
/// 接受关卡数据
/// </summary>
public class AcceptLevelDataCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        GameManager.Instance.nowLevelData = notification.Body as LevelData;
    }
}

/// <summary>
/// 接受音乐数据
/// </summary>
public class AcceptMusicSettingDataCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        GameManager.Instance.musicSettingData = notification.Body as MusicSettingData;
    }
}

/// <summary>
/// 初始化游戏
/// </summary>
public class InitGameCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        // 显示游戏面板
        SendNotification(NotificationName.UI.SHOW_GAMEPANEL);
        // 游戏初始化
        GameManager.Instance.InitGame();
        // 更新面板
        SendNotification(NotificationName.UIEvent.GAMEPANEL_UPDATE_MONEY, GameManager.Instance.money);
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
        GameManager.Instance.StartGame();
        // 开始出怪
        SendNotification(NotificationName.Game.START_SPAWN);
    }
}

/// <summary>
/// 退出游戏命令
/// </summary>
public class ExitGameCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        // 退出游戏
        GameManager.Instance.ExitGame();

        // 关闭相关面板
        SendNotification(NotificationName.UI.HIDE_BUILTPANEL);
        SendNotification(NotificationName.UI.HIDE_MENUPANEL);
        SendNotification(NotificationName.UI.HIDE_GAMEPANEL);
    }
}

/// <summary>
/// 重新开始游戏命令
/// </summary>
public class RestartGameCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        SendNotification(NotificationName.Game.EXIT_GAME);
        // 重新加载游戏
        SendNotification(NotificationName.LoadScene.LOADSCENE_GAME_TO_GAME, GameManager.Instance.nowLevelData.levelID);
    }
}

/// <summary>
/// 暂停游戏
/// </summary>
public class PauseGameCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
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
        GameManager.Instance.ContinueGame();
    }
}

/// <summary>
/// 游戏胜利
/// </summary>
public class GameWinCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        GameManager.Instance.GameWin();
    }
}

public class GameOverCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        GameManager.Instance.GameOver();
    }
}

#endregion

#region 游戏内逻辑相关

/// <summary>
/// 打开建造面板
/// </summary>
public class OpenedBuiltPanelCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        GameManager.Instance.openedBuiltPanel = (bool)notification.Body;
    }
}

public class UpdateMoneyCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        int num = (int)notification.Body;
        GameManager.Instance.money += num;
        // 更新面板
        SendNotification(NotificationName.UIEvent.GAMEPANEL_UPDATE_MONEY, GameManager.Instance.money);
        if (num > 0)
        {
            // 记录到统计信息
            SendNotification(NotificationName.Data.CHANGE_MONEY_COUNT, +num);
        }
        
    }
}

/// <summary>
/// 两倍速
/// </summary>
public class TwoSpeedCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        GameManager.Instance.TwoSpeed = (bool)notification.Body;
    }
}

#endregion

#region 声音相关

/// <summary>
/// 播放背景音乐
/// </summary>
public class PlayMusicCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        GameManager.Instance.MusicManger.PlayMusic("Music/BGMusic", 1, true);

        SendNotification(NotificationName.Game.MUTE_MUSIC, !GameManager.Instance.musicSettingData.musicOpen);
    }
}

/// <summary>
/// 停止背景音乐
/// </summary>
public class StopMusicCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        GameManager.Instance.MusicManger.StopMusic();
    }
}

/// <summary>
/// 静音背景音乐
/// </summary>
public class MuteMusicCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        GameManager.Instance.MusicManger.MuteMusic((bool)notification.Body);
    }
}

/// <summary>
/// 播放音效
/// </summary>
public class PlaySoundCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        (string path, float volume, bool loop) data = ((string, float, bool))notification.Body;

        if (GameManager.Instance.musicSettingData.soundOpen)
        {
            GameManager.Instance.MusicManger.PlaySound(data.path, 1, data.loop);
        }
        else
        {
            GameManager.Instance.MusicManger.PlaySound(data.path, 0, data.loop);
        }
        
        SendNotification(NotificationName.Game.MUTE_SOUND, !GameManager.Instance.musicSettingData.soundOpen);
    }
}

/// <summary>
/// 静音音效
/// </summary>
public class MuteSoundCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        GameManager.Instance.MusicManger.MuteSound((bool)notification.Body);
    }
}

#endregion