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
        
        public IGameDataManager GameDataManager { get => gameDataManager; set => gameDataManager = value;}
        public IMusicDataManager MusicDataManager { get => musicDataManager; set => musicDataManager = value;}
        public IStatisticalDataManager StatisticalDataManager { get => statisticalDataManager; set => statisticalDataManager = value;}

        public DataManager()
        {
            gameDataManager = new NormalGameDataManager();
            musicDataManager = new MusicDataManager();
            statisticalDataManager = new StatisticalDataManager();
        }
    }
}