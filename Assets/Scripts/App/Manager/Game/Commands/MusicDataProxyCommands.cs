using App.DataClass.Player;
using App.MVC.Model.PlayerData;
using App.Static;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;

namespace App.MVC.Controller.Commands
{
    public class InitMusicDataProxyControllerCommand : SimpleCommand
    {
        MusicDataManager manager = GameFacade.Instance.RetrieveProxy(nameof(MusicDataManager)) as MusicDataManager;
    
        public override void Execute(INotification notification)
        {

        }
    }

    public class GetMusicSettingDataCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            bool musicOpen = GameManager.Instance.dataManager.MusicDataManager.MusicOpen;
            bool soundOpen = GameManager.Instance.dataManager.MusicDataManager.SoundOpen;
        }
    }
}