using App.Data.DataClass.Player;
using App.Game;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;

namespace App.UI.BeginScene.BeginPanel
{
    public class LoadMusicDataCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            var data = new MusicSettingData
            {
                musicOpen = GameManager.Instance.dataManager.MusicDataManager.MusicOpen,
                soundOpen = GameManager.Instance.dataManager.MusicDataManager.SoundOpen
            };

            var proxy = Facade.RetrieveProxy(nameof(BeginPanelProxy)) as BeginPanelProxy;
            proxy.UpdateMusicData(data);
        }
    }
}