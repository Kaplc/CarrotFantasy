using App.DataClass.Player;

namespace App.MVC.Model.PlayerData
{
    public interface IStatisticalDataManager
    {
        int BossMapCount { set; }
        int AdventureMapCount {  set; }
        int DestroyObstacleCount { set; }
        int HideMapCount { set; }
        int KillBossCount { set; }
        int KillMonsterCount { set; }
        int Money { set; }

        StatisticalData GetStatisticalData();
        void SaveStatisticalData();
    }
}