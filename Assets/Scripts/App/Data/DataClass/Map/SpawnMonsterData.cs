using App.Static.Enum;
using XLua;

namespace App.Data.DataClass.Map
{
    [LuaCallCSharp]
    public class SpawnMonsterData
    {
        public float hard;
        public EMonsterType monsterType;
        public float nextSpawnTime;
    }
}