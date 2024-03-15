using PureMVC.Interfaces;
using PureMVC.Patterns.Command;

public class InitProcessDataProxyControllerCommand: SimpleCommand
{
    ProcessDataProxy proxy = GameFacade.Instance.RetrieveProxy(nameof(ProcessDataProxy)) as ProcessDataProxy;
    
    public override void Execute(INotification notification)
    {
        GameFacade.Instance.RegisterCommand(NotificationName.Data.SAVE_PROCESSDATA, ()=> new SaveProcessDataCommand()
        {
            proxy = proxy
        });
        GameFacade.Instance.RegisterCommand(NotificationName.Data.LOAD_PROCESSDATA, () => new GetProcessDataCommand()
        {
            proxy = proxy
        });
    }
}

public class GetProcessDataCommand : SimpleCommand
{
    public ProcessDataProxy proxy;
    
    public override void Execute(INotification notification)
    {
        proxy?.GetProcessData();
    }
}

public class SaveProcessDataCommand : SimpleCommand
{
    public ProcessDataProxy proxy;
    
    public override void Execute(INotification notification)
    {
        proxy?.SaveProcessData(((int,int,EPassedGrade))notification.Body);
    }
}  