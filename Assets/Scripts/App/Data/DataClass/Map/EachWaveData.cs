using System;
using App.Static.Enum;

namespace App.Data.DataClass.Map
{
    [Serializable]
    public class EachWaveData
    {
        public float hard;
        public EMonsterType monsterType;
        public int monsterCount;
        public float monsterDuration;
    }
}