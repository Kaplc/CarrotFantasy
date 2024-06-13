using App.MVC.Model.GameData;
using App.MVC.Model.PlayerData;

namespace App.MVC.Model
{
    public interface IDataManager
    {
        IGameDataManager GameDataManager { get; set; }
        IMusicDataManager MusicDataManager { get; set; }
        IStatisticalDataManager StatisticalDataManager { get; set; }
    }
}