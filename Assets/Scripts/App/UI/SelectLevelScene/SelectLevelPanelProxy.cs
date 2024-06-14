using System.Collections.Generic;
using App.DataClass.Game.Level;
using App.DataClass.Player;
using App.Manager.Data.NormalGame;
using App.MVC.Controller;
using App.Static;
using PureMVC.Patterns.Proxy;

namespace App.UI.SelectLevelScene
{
    public class SelectLevelPanelProxy : Proxy
    {
        private NormalGameDataManager GameDataManager => GameManager.Instance.dataManager.GameDataManager as NormalGameDataManager;
        private NormalSceneManager SceneManger =>GameManager.Instance.sceneManger as NormalSceneManager;
        private ItemData itemData;

        public SelectLevelPanelProxy() : base(nameof(SelectLevelPanelProxy))
        {
        }

        public void UpdateLevelData()
        {
            itemData = GameDataManager?.LevelDataManager.GetItemLevelData(SceneManger.NowItemID);
            SendNotification(NotificationName.UI.LEVEL_DATA_UPDATED, itemData);
        }
    }
}