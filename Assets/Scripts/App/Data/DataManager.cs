using App.Game.SceneManager;
using App.Game.SceneManager.NormalGame;

namespace App.Data
{
    public class DataManager : IDataManager
    {
        public DataManager()
        {
            SceneDataManager = new NormalSceneDataManager();
            MusicDataManager = new MusicDataManager();
            StatisticalDataManager = new StatisticalDataManager();
        }

        public ISceneDataManager SceneDataManager { get; private set; }

        public IMusicDataManager MusicDataManager { get; private set; }

        public IStatisticalDataManager StatisticalDataManager { get; private set; }

        public void SetMusicDataManager(IMusicDataManager musicDataManager)
        {
            MusicDataManager = musicDataManager;
        }

        public void SetStatisticalDataManager(IStatisticalDataManager statisticalDataManager)
        {
            StatisticalDataManager = statisticalDataManager;
        }

        public void SetGameDataManager(ISceneDataManager sceneDataManager)
        {
            SceneDataManager = sceneDataManager;
        }
    }
}