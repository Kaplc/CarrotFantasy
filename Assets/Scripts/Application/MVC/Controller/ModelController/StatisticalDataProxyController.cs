using PureMVC.Interfaces;
using PureMVC.Patterns.Command;

public class InitStaticalDataProxyControllerCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        GameFacade.Instance.RegisterCommand(NotificationName.Data.LOAD_STATISTICALDATA, () => new GetStatisticalDataCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.Data.SAVE_STATISTICALDATA, () => new SaveStaticalDataCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.Data.CHANGE_MONEY_COUNT, () => new ChangeMoneyCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.Data.CHANGE_DESTROYOBSTACLE_COUNT, () => new ChangeDestroyObstacleCountCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.Data.CHANGE_KILLMONSTER_COUNT, () => new ChangeKillMonsterCountCommand());
    }
}

public class GetStatisticalDataCommand : SimpleCommand
{
    StatisticalDataProxy proxy = GameFacade.Instance.RetrieveProxy(nameof(StatisticalDataProxy)) as StatisticalDataProxy;

    public override void Execute(INotification notification)
    {
        proxy.GetStatisticalData();
    }
}

public class SaveStaticalDataCommand : SimpleCommand
{
    StatisticalDataProxy proxy = GameFacade.Instance.RetrieveProxy(nameof(StatisticalDataProxy)) as StatisticalDataProxy;

    public override void Execute(INotification notification)
    {
        proxy.SaveStatisticalData();
    }
}

public class ChangeMoneyCommand : SimpleCommand
{
    StatisticalDataProxy proxy = GameFacade.Instance.RetrieveProxy(nameof(StatisticalDataProxy)) as StatisticalDataProxy;

    public override void Execute(INotification notification)
    {
        proxy.ChangeMoneyCount((int)notification.Body);
    }
}

public class ChangeKillMonsterCountCommand : SimpleCommand
{
    StatisticalDataProxy proxy = GameFacade.Instance.RetrieveProxy(nameof(StatisticalDataProxy)) as StatisticalDataProxy;

    public override void Execute(INotification notification)
    {
        proxy.ChangeKillMonsterCount((int)notification.Body);
    }
}

public class ChangeDestroyObstacleCountCommand : SimpleCommand
{
    StatisticalDataProxy proxy = GameFacade.Instance.RetrieveProxy(nameof(StatisticalDataProxy)) as StatisticalDataProxy;

    public override void Execute(INotification notification)
    {
        proxy.ChangeDestroyObstacleCount((int)notification.Body);
    }
}