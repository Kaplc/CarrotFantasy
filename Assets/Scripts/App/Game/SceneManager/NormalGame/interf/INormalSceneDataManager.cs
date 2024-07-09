namespace App.Game.SceneManager.NormalGame.interf
{
    public interface INormalSceneDataManager : ISceneDataManager
    {
        ILevelDataManager LevelDataManager { get; }
        IProcessDataManager ProcessDataManager { get; }
    }
}