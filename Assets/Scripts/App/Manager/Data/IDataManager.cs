using App.MVC.Model.GameData;
using App.MVC.Model.PlayerData;

namespace App.MVC.Model
{
    public interface IDataManager
    {
        IGameDataManager GameDataManager { get; }
        IMusicDataManager MusicDataManager { get; }
        IStatisticalDataManager StatisticalDataManager { get; }
    }
}