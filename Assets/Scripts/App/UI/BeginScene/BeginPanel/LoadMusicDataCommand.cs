using App.MVC;
using Library;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;

namespace App.UI.BeginScene.BeginPanel
{
    public class LoadMusicDataCommand: SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            var proxy = Facade.RetrieveProxy(nameof(BeginPanelProxy)) as BeginPanelProxy;
            proxy.UpdateMusicData();
        }
    }
}