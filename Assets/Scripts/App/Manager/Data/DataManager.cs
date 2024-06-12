using App.Manager.Data.NormalGame;
using App.MVC.Model.GameData;
using App.MVC.Model.PlayerData;

namespace App.MVC.Model
{
    public class DataManager: IDataManager
    {
        private IGameDataManager gameDataManager;
        private IMusicDataManager musicDataManager;
        private IStatisticalDataManager statisticalDataManager;
        
        public IGameDataManager GameDataManager { get => gameDataManager; }
        public IMusicDataManager MusicDataManager { get => musicDataManager; }
        public IStatisticalDataManager StatisticalDataManager { get => statisticalDataManager; }

        public DataManager()
        {
            gameDataManager = new NormalGameDataManager();
            musicDataManager = new MusicDataManager();
            statisticalDataManager = new StatisticalDataManager();
        }
    }
}