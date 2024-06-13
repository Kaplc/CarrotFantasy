using System.Collections.Generic;
using App.DataClass.Game.Level;
using App.DataClass.Player;
using App.MVC.Controller;
using App.Static;
using PureMVC.Patterns.Proxy;

namespace App.UI.SelectLevelScene
{
    public class SelectLevelPanelProxy: Proxy
    {
        private ItemData itemData;
        public SelectLevelPanelProxy() : base(nameof(SelectLevelPanelProxy))
        {
        }

        public void UpdateLevelData(ItemData data)
        {
            itemData = data;
            SendNotification(NotificationName.UI.LEVEL_DATA_UPDATED, itemData);
        }
    }
}