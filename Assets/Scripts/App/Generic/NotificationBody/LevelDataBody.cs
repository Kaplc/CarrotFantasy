using System.Collections.Generic;
using App.DataClass.Game.Level;
using App.DataClass.Game.Object;

namespace App.Generic.NotificationBody
{
    public class LevelDataBody
    {
        public LevelData levelData;
        public Dictionary<int, MonsterData> monstersData;
        public Dictionary<int, TowerData> towersData;
    }
}
