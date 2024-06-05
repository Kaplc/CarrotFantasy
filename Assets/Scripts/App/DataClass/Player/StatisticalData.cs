using System;

namespace App.DataClass.Player
{
    [Serializable]
    public class StatisticalData
    {
        public int adventureMapCount = 5;
        public int hideMapCount = 0;
        public int bossMapCount = 0;
        public int money;
        public int killMonsterCount;
        public int killBossCount;
        public int destroyObstacleCount;
    }
}
