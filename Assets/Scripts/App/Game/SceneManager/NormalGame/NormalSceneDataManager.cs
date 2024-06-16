using App.Game.SceneManager.NormalGame.interf;

namespace App.Game.SceneManager.NormalGame
{
    public class NormalSceneDataManager: INormalSceneDataManager
    {
        private ILevelDataManager levelDataManager;
        private IProcessDataManager processDataManager;
        
        public IProcessDataManager ProcessDataManager { get=>processDataManager;}
        public ILevelDataManager LevelDataManager {  get=> levelDataManager; }
        
        public NormalSceneDataManager()
        {
            levelDataManager = new LevelDataManager();
            processDataManager = new ProcessDataManager();
        }
    }
}