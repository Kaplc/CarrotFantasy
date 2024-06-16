using System;
using System.Collections.Generic;

namespace App.Data.DataClass.Game.Level
{
    [Serializable]
    public class RoundData
    {
        public float growth; // 怪物属性成长系数
        public List<GroupData> group; // 一波中每组相同类型的怪
        public float intervalTimeEach; // 每个间隔时间
    }
}
