using App.Static.Enum;
using XLua;

namespace App.Data.DataClass.Map
{
    [LuaCallCSharp]
    public class SpawnMonsterData
    {
        public EMonsterType monsterType;
        public float nextSpawnTime;
        public float hard;
    }
}