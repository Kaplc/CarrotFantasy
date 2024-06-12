using App.MVC.Model.GameData;

namespace App.Manager.Data.NormalGame.@interface
{
    public interface INormalGameDataManager: IGameDataManager
    {
        ILevelDataManager LevelDataManager { get; }
        IProcessDataManager ProcessDataManager { get; }
    }
}