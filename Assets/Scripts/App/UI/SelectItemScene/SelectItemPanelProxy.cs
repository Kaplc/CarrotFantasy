using System.Collections.Generic;
using App.Data.DataClass.Player;
using App.Game;
using App.Game.SceneManager.NormalGame.interf;
using App.Static;
using PureMVC.Patterns.Proxy;

namespace App.UI.SelectItemScene
{
    public class SelectItemPanelProxy: Proxy
    {
        private INormalSceneDataManager SceneDataManager => GameManager.Instance.sceneManger.SceneDataManager as INormalSceneDataManager;
        private ProcessData processData;
        
        public SelectItemPanelProxy() : base(nameof(SelectItemPanelProxy))
        {
        }

        public void UpdateItemData()
        {
            processData = SceneDataManager.ProcessDataManager.GetProcessData();
            SendNotification(NotificationName.UI.ITEM_DATA_UPDATED, processData);
        }
    }
}