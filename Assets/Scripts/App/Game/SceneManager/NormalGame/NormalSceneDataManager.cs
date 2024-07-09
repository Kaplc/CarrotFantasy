using App.Game.SceneManager.NormalGame.interf;

namespace App.Game.SceneManager.NormalGame
{
    public class NormalSceneDataManager : INormalSceneDataManager
    {
        public NormalSceneDataManager()
        {
            LevelDataManager = new LevelDataManager();
            ProcessDataManager = new ProcessDataManager();
        }

        public IProcessDataManager ProcessDataManager { get; }

        public ILevelDataManager LevelDataManager { get; }
    }
}