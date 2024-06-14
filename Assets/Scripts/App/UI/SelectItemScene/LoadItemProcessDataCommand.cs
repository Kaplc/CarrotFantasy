using App.Manager.Data.NormalGame;
using App.MVC;
using App.MVC.Controller;
using App.MVC.Model.PlayerData;
using App.Static;
using App.UI.SelectItemScene;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;

namespace App.UI.SelectLevelScene
{
    public class LoadItemProcessDataCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            SelectItemPanelProxy proxy = GameFacade.Instance.RetrieveProxy(nameof(SelectItemPanelProxy)) as SelectItemPanelProxy;
            proxy.UpdateItemData();
        }
    }
}