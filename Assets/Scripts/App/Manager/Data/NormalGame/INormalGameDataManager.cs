using App.Manager.Data.NormalGame.@interface;
using App.MVC.Model.GameData;

namespace App.Manager.Data.NormalGame
{
    public interface INormalGameDataManager: IGameDataManager
    {
        ILevelDataManager LevelDataManager { get; }
        IProcessDataManager ProcessDataManager { get; }
    }
}