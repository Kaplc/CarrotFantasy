using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;

public class InitGameDataProxyCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        GameFacade.Instance.RegisterCommand(NotificationName.Init.INIT_GAMEDATA, () => new LoadInitGameDataCommand());
        
        GameFacade.Instance.RegisterCommand(NotificationName.Data.LOAD_STATISTICALDATA, () => new GetStatisticalDataCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.Data.LOAD_MUSICSETTINGDATA, () => new GetMusicSettingDataCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.Data.LOAD_PROCESSDATA, () => new GetProcessDataCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.Data.LOAD_ITEMDATA, () => new GetBigLevelDataCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.Data.LOAD_LEVELDATA, () => new LoadLevelDataCommand());
        
        GameFacade.Instance.RegisterCommand(NotificationName.Data.SAVE_PROCESSDATA, ()=> new SaveProcessDataCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.Data.SAVE_MUSCISETTINGDATA, () => new SaveMusicSettingDataCommand());
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

public class SaveProcessDataCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        GameDataProxy proxy = GameFacade.Instance.RetrieveProxy("GameDataProxy") as GameDataProxy;
        proxy?.SaveProcessData(((int,EPassedGrade))notification.Body);
       
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