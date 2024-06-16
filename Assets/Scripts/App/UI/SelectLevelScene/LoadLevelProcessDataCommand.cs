using App.Game;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;

namespace App.UI.SelectLevelScene
{
    public class LoadLevelProcessDataCommand: SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            SelectLevelPanelProxy proxy = GameFacade.Instance.RetrieveProxy<SelectLevelPanelProxy>();
            proxy.UpdateLevelData();
        }
    }
}