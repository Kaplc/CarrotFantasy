using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;

public class InitGameDataProxyCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);

        GameFacade.Instance.RegisterCommand(NotificationName.LOAD_STATISTICALDATA, () => new StatisticalDataCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.LOAD_MUSICSETTINGDATA, () => new LoadMusicSettingDataCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.SAVE_MUSCISETTINGDATA, () => new SaveMusicSettingDataCommand());

        GameFacade.Instance.RegisterCommand(NotificationName.LOAD_PROCESSDATA, () => new LoadProcessDataCommand());
    }
}


public class StatisticalDataCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        GameDataProxy proxy = GameFacade.Instance.RetrieveProxy("GameDataProxy") as GameDataProxy;
        proxy.LoadStatisticalData();
    }
}

public class LoadMusicSettingDataCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        GameDataProxy proxy = GameFacade.Instance.RetrieveProxy("GameDataProxy") as GameDataProxy;
        proxy.LoadMusicSettingData();
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

public class LoadProcessDataCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        GameDataProxy proxy = GameFacade.Instance.RetrieveProxy("GameDataProxy") as GameDataProxy;
        proxy.LoadProcessData();
    }
}