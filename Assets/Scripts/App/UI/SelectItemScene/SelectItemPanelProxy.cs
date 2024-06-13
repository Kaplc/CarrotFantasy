using System.Collections.Generic;
using App.DataClass.Game.Level;
using App.DataClass.Player;
using App.Static;
using PureMVC.Patterns.Proxy;

namespace App.UI.SelectItemScene
{
    public class SelectItemPanelProxy: Proxy
    {
        private ProcessData processData;
        
        public SelectItemPanelProxy() : base(nameof(SelectItemPanelProxy))
        {
        }

        public void UpdateItemData(ProcessData processData)
        {
            this.processData = processData;
            SendNotification(NotificationName.UI.ITEM_DATA_UPDATED, processData);
        }
    }
}