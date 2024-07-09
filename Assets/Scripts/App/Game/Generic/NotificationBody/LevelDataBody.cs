using System.Collections.Generic;
using App.Data.DataClass.Game.Level;
using App.Data.DataClass.Game.Object;

namespace App.Game.Generic.NotificationBody
{
    public class LevelDataBody
    {
        public LevelData levelData;
        public Dictionary<int, MonsterData> monstersData;
        public Dictionary<int, TowerData> towersData;
    }
}