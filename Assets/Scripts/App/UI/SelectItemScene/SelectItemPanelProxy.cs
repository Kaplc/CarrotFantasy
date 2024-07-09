using App.Data.DataClass.Player;
using App.Game;
using App.Game.SceneManager.NormalGame.interf;
using App.Static;
using PureMVC.Patterns.Proxy;

namespace App.UI.SelectItemScene
{
    public class SelectItemPanelProxy : Proxy
    {
        private ProcessData processData;

        public SelectItemPanelProxy() : base(nameof(SelectItemPanelProxy))
        {
        }

        private INormalSceneDataManager SceneDataManager => GameManager.Instance.sceneManager.SceneDataManager as INormalSceneDataManager;

        public void UpdateItemData()
        {
            processData = SceneDataManager.ProcessDataManager.GetProcessData();
            SendNotification(NotificationName.UI.ITEM_DATA_UPDATED, processData);
        }
    }
}