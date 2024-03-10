using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;

public class InitGameDataProxyCommand : SimpleCommand
{
    GameDataProxy proxy = GameFacade.Instance.RetrieveProxy("GameDataProxy") as GameDataProxy;

    public override void Execute(INotification notification)
    {
        GameFacade.Instance.RegisterCommand(NotificationName.Data.LOAD_ITEMDATA, () => new GetBigLevelDataCommand()
        {
            proxy = proxy
        });
        GameFacade.Instance.RegisterCommand(NotificationName.Data.LOAD_LEVELDATA, () => new LoadLevelDataCommand()
        {
            proxy = proxy
        });
    }
}


public class GetBigLevelDataCommand : SimpleCommand
{
    public GameDataProxy proxy;

    public override void Execute(INotification notification)
    {
        proxy.GetBigLevelData((int)notification.Body);
    }
}

public class LoadLevelDataCommand : SimpleCommand
{
    public GameDataProxy proxy;

    public override void Execute(INotification notification)
    {
        proxy.LoadLevelData((int)notification.Body);
    }
}