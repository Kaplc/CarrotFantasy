using App.MVC.Model.PlayerData;
using App.Static;
using App.UI.BeginScene.BeginPanel;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;

namespace App.MVC.Controller.Commands
{
    public class InitStaticalDataProxyControllerCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            GameFacade.Instance.RegisterCommand(NotificationName.Data.CHANGE_MONEY_COUNT, () => new ChangeMoneyCommand());
            GameFacade.Instance.RegisterCommand(NotificationName.Data.CHANGE_DESTROYOBSTACLE_COUNT, () => new ChangeDestroyObstacleCountCommand());
            GameFacade.Instance.RegisterCommand(NotificationName.Data.CHANGE_KILLMONSTER_COUNT, () => new ChangeKillMonsterCountCommand());
        }
    }

    public class GetStatisticalDataCommand : SimpleCommand
    {
        StatisticalDataManager manager = GameFacade.Instance.RetrieveProxy(nameof(StatisticalDataManager)) as StatisticalDataManager;

        public override void Execute(INotification notification)
        {
            manager.GetStatisticalData();
        }
    }

    public class SaveStaticalDataCommand : SimpleCommand
    {
        StatisticalDataManager manager = GameFacade.Instance.RetrieveProxy(nameof(StatisticalDataManager)) as StatisticalDataManager;

        public override void Execute(INotification notification)
        {
            manager.SaveStatisticalData();
        }
    }

    public class ChangeMoneyCommand : SimpleCommand
    {
        StatisticalDataManager manager = GameFacade.Instance.RetrieveProxy(nameof(StatisticalDataManager)) as StatisticalDataManager;

        public override void Execute(INotification notification)
        {
            manager.Money = (int)notification.Body;
        }
    }

    public class ChangeKillMonsterCountCommand : SimpleCommand
    {
        StatisticalDataManager manager = GameFacade.Instance.RetrieveProxy(nameof(StatisticalDataManager)) as StatisticalDataManager;

        public override void Execute(INotification notification)
        {
            manager.KillMonsterCount = (int)notification.Body;
        }
    }

    public class ChangeDestroyObstacleCountCommand : SimpleCommand
    {
        StatisticalDataManager manager = GameFacade.Instance.RetrieveProxy(nameof(StatisticalDataManager)) as StatisticalDataManager;

        public override void Execute(INotification notification)
        {
            manager.DestroyObstacleCount = (int)notification.Body;
        }
    }
}