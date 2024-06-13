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