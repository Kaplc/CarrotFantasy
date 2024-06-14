using System.Collections.Generic;
using App.DataClass.Game.Level;
using App.DataClass.Player;
using App.Manager.Data.NormalGame;
using App.MVC.Controller;
using App.Static;
using PureMVC.Patterns.Proxy;

namespace App.UI.SelectItemScene
{
    public class SelectItemPanelProxy: Proxy
    {
        private NormalGameDataManager GameDataManager => GameManager.Instance.dataManager.GameDataManager as NormalGameDataManager;
        private ProcessData processData;
        
        public SelectItemPanelProxy() : base(nameof(SelectItemPanelProxy))
        {
        }

        public void UpdateItemData()
        {
            processData = GameDataManager.ProcessDataManager.GetProcessData();
            SendNotification(NotificationName.UI.ITEM_DATA_UPDATED, processData);
        }
    }
}