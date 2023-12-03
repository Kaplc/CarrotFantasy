using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;

public class InitGameDataProxyCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        
        GameFacade.Instance.RegisterCommand(NotificationName.LOAD_STATISTICALDATA, () => new LoadStatisticalData());

    }
    
}


public class LoadStatisticalData : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        GameDataProxy proxy = GameFacade.Instance.RetrieveProxy("GameDataProxy") as GameDataProxy;
        proxy.LoadMusicSettingData();
    }
}

public class LoadMusicSettingData : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        GameDataProxy proxy = GameFacade.Instance.RetrieveProxy("GameDataProxy") as GameDataProxy;
        proxy.LoadMusicSettingData();
    }
}