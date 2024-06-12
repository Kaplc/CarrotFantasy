using App.DataClass.Player;
using App.MVC.Model.PlayerData;
using App.Static;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;

namespace App.MVC.Controller.Commands
{
    public class InitProcessDataProxyControllerCommand: SimpleCommand
    {
        ProcessDataManager manager = GameFacade.Instance.RetrieveProxy(nameof(ProcessDataManager)) as ProcessDataManager;
    
        public override void Execute(INotification notification)
        {
            GameFacade.Instance.RegisterCommand(NotificationName.Data.SAVE_PROCESS_DATA, ()=> new SaveProcessDataCommand()
            {
                manager = manager
            });
            GameFacade.Instance.RegisterCommand(NotificationName.Data.LOAD_PROCESSDATA, () => new GetProcessDataCommand()
            {
                manager = manager
            });
        }
    }

    public class GetProcessDataCommand : SimpleCommand
    {
        public ProcessDataManager manager;
    
        public override void Execute(INotification notification)
        {
            manager?.GetProcessData();
        }
    }

    public class SaveProcessDataCommand : SimpleCommand
    {
        public ProcessDataManager manager;
    
        public override void Execute(INotification notification)
        {
            // proxy?.SaveProcessData(((int,int,EPassedGrade))notification.Body);
        }
    }
}