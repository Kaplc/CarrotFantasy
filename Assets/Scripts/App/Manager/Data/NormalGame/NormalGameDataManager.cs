using App.Manager.Data.NormalGame.@interface;
using App.MVC.Model.GameData;
using App.MVC.Model.PlayerData;

namespace App.Manager.Data.NormalGame
{
    public class NormalGameDataManager: IGameDataManager
    {
        private ILevelDataManager levelDataManager;
        private IProcessDataManager processDataManager;
        
        public IProcessDataManager ProcessDataManager { get=>processDataManager;}
        public ILevelDataManager LevelDataManager {  get=> levelDataManager; }
        
        public NormalGameDataManager()
        {
            levelDataManager = new LevelDataManager();
            processDataManager = new ProcessDataManager();
        }
    }
}