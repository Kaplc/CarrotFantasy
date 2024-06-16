using System.Collections.Generic;
using App.Data.DataClass.Game.Level;
using App.Data.DataClass.Player;
using App.Game;
using App.Game.SceneManager.NormalGame;
using App.Static;
using PureMVC.Patterns.Proxy;

namespace App.UI.SelectLevelScene
{
    public class SelectLevelPanelProxy : Proxy
    {
        private NormalSceneDataManager SceneDataManager => GameManager.Instance.sceneManger.SceneDataManager as NormalSceneDataManager;
        private NormalSceneManager SceneManger =>GameManager.Instance.sceneManger as NormalSceneManager;
        private ItemData itemData;
        private ProcessData processData;

        public SelectLevelPanelProxy() : base(nameof(SelectLevelPanelProxy))
        {
        }

        public void UpdateLevelData()
        {
            itemData = SceneDataManager?.LevelDataManager.GetItemLevelData(SceneManger.NowItemID);
            processData = SceneDataManager?.ProcessDataManager.GetProcessData();
            
            SendNotification(NotificationName.UI.LEVEL_DATA_UPDATED, new LevelDataUpdatedArgs()
            {
                itemData = itemData,
                processData = processData
            });
        }
    }
}