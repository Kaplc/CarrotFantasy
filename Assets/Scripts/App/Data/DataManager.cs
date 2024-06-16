using App.Game.SceneManager;
using App.Game.SceneManager.NormalGame;

namespace App.Data
{
    public class DataManager: IDataManager
    {
        private ISceneDataManager sceneDataManager;
        private IMusicDataManager musicDataManager;
        private IStatisticalDataManager statisticalDataManager;
        
        public ISceneDataManager SceneDataManager { get => sceneDataManager;}
        public IMusicDataManager MusicDataManager { get => musicDataManager;}
        public IStatisticalDataManager StatisticalDataManager { get => statisticalDataManager;}

        public DataManager()
        {
            sceneDataManager = new NormalSceneDataManager();
            musicDataManager = new MusicDataManager();
            statisticalDataManager = new StatisticalDataManager();
        }
        
        public void SetGameDataManager(ISceneDataManager sceneDataManager)
        {
            this.sceneDataManager = sceneDataManager;
        }
        
        public void SetMusicDataManager(IMusicDataManager musicDataManager)
        {
            this.musicDataManager = musicDataManager;
        }
        
        public void SetStatisticalDataManager(IStatisticalDataManager statisticalDataManager)
        {
            this.statisticalDataManager = statisticalDataManager;
        }
    }
}