using App.MVC.Model.GameData;
using App.Static;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;

namespace App.MVC.Controller.Commands
{
    public class InitGameDataProxyCommand : SimpleCommand
    {
        LevelDataManager manager = GameFacade.Instance.RetrieveProxy("GameDataProxy") as LevelDataManager;

        public override void Execute(INotification notification)
        {
            GameFacade.Instance.RegisterCommand(NotificationName.Data.LOAD_ITEMDATA, () => new GetBigLevelDataCommand()
            {
                manager = manager
            });
            GameFacade.Instance.RegisterCommand(NotificationName.Data.LOAD_LEVELDATA, () => new LoadLevelDataCommand()
            {
                manager = manager
            });
        }
    }


    public class GetBigLevelDataCommand : SimpleCommand
    {
        public LevelDataManager manager;

        public override void Execute(INotification notification)
        {
            manager.GetBigLevelData((int)notification.Body);
        }
    }

    public class LoadLevelDataCommand : SimpleCommand
    {
        public LevelDataManager manager;

        public override void Execute(INotification notification)
        {
            manager.LoadLevelData((int)notification.Body);
        }
    }
}