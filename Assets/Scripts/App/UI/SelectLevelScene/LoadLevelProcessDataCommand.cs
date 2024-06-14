using App.DataClass.Player;
using App.Manager.Data.NormalGame;
using App.MVC;
using App.MVC.Controller;
using App.Static;
using App.UI.SelectLevelScene;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;

namespace App.UI.SelectItemScene
{
    public class LoadLevelProcessDataCommand: SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            SelectLevelPanelProxy proxy = GameFacade.Instance.RetrieveProxy(nameof(SelectLevelPanelProxy)) as SelectLevelPanelProxy;
            proxy.UpdateLevelData();
        }
    }
}