using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;

public class InitGameDataProxyCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        GameFacade.Instance.RegisterCommand(NotificationName.INIT_GAMEDATA, () => new LoadInitGameDataCommand());
        
        GameFacade.Instance.RegisterCommand(NotificationName.LOAD_STATISTICALDATA, () => new GetStatisticalDataCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.LOAD_MUSICSETTINGDATA, () => new GetMusicSettingDataCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.SAVE_MUSCISETTINGDATA, () => new SaveMusicSettingDataCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.LOAD_PROCESSDATA, () => new GetProcessDataCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.LOAD_BIGLEVELDATA, () => new GetBigLevelDataCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.LOAD_LEVELDATA, () => new LoadLevelDataCommand());
    }
}

public class LoadInitGameDataCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        GameDataProxy proxy = GameFacade.Instance.RetrieveProxy("GameDataProxy") as GameDataProxy;
        proxy.LoadInitGameData();
    }
}

public class GetStatisticalDataCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        GameDataProxy proxy = GameFacade.Instance.RetrieveProxy("GameDataProxy") as GameDataProxy;
        proxy.GetStatisticalData();
    }
}

public class GetMusicSettingDataCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        GameDataProxy proxy = GameFacade.Instance.RetrieveProxy("GameDataProxy") as GameDataProxy;
        proxy.GetMusicSettingData();
    }
}

public class SaveMusicSettingDataCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        GameDataProxy proxy = GameFacade.Instance.RetrieveProxy("GameDataProxy") as GameDataProxy;
        proxy.SaveMusicSettingData(notification.Body as MusicSettingData);
    }
}

public class GetProcessDataCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        GameDataProxy proxy = GameFacade.Instance.RetrieveProxy("GameDataProxy") as GameDataProxy;
        proxy.GetProcessData();
    }
}

public class GetBigLevelDataCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        GameDataProxy proxy = GameFacade.Instance.RetrieveProxy("GameDataProxy") as GameDataProxy;
        proxy.GetBigLevelData((int)notification.Body);
    }
}

public class LoadLevelDataCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        
        GameDataProxy proxy = GameFacade.Instance.RetrieveProxy("GameDataProxy") as GameDataProxy;
        proxy.LoadLevelData((int)notification.Body);
    }
    
}