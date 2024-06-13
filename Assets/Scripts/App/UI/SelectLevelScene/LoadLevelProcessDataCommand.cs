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
            INormalGameDataManager nr = GameManager.Instance.dataManager.GameDataManager as INormalGameDataManager;
            
            SelectLevelPanelProxy proxy = GameFacade.Instance.RetrieveProxy(nameof(SelectLevelPanelProxy)) as SelectLevelPanelProxy;
            ProcessData processData = nr.ProcessDataManager.GetProcessData();

            NormalSceneManager normalSceneManager = GameManager.Instance.sceneManger as NormalSceneManager;
            int nowItemID = normalSceneManager.NowItemID;
            proxy.UpdateLevelData(nr.LevelDataManager.GetItemLevelData(nowItemID));
        }
    }
}