using App.Data.DataClass.Player;

namespace App.Data
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
        void SaveStatisticalData(StatisticalData data);
    }
}