using PureMVC.Interfaces;
using PureMVC.Patterns.Command;

namespace App.UI.SelectItemScene
{
    public class LoadItemProcessDataCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            var proxy = GameFacade.Instance.RetrieveProxy<SelectItemPanelProxy>();
            proxy.UpdateItemData();
        }
    }
}