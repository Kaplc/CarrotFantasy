using PureMVC.Interfaces;
using PureMVC.Patterns.Command;

public class InitStaticalDataProxyControllerCommand : SimpleCommand
{
    StatisticalDataProxy proxy = GameFacade.Instance.RetrieveProxy(nameof(StatisticalDataProxy)) as StatisticalDataProxy;

    public override void Execute(INotification notification)
    {
        GameFacade.Instance.RegisterCommand(NotificationName.Data.LOAD_STATISTICALDATA, () => new GetStatisticalDataCommand()
        {
            proxy = proxy
        });
    }
}

public class GetStatisticalDataCommand : SimpleCommand
{
    public StatisticalDataProxy proxy;

    public override void Execute(INotification notification)
    {
        proxy.GetStatisticalData();
    }
}