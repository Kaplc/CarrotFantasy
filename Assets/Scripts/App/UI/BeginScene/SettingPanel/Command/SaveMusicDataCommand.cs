using App.Data.DataClass.Player;
using App.Game;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;

namespace App.UI.BeginScene.SettingPanel
{
    public class SaveMusicSettingDataCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            GameManager.Instance.dataManager.MusicDataManager.Save(notification.Body as MusicSettingData);
        }
    }
}