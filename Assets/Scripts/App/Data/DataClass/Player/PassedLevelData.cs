using System;
using System.Collections.Generic;

namespace App.Data.DataClass.Player
{
    public enum EPassedGrade
    {
        None,
        Copper,
        Sliver,
        Gold,
    }

    [Serializable]
    public class PassedLevelData
    {
        public Dictionary<int, EPassedGrade> passedLevelDic = new Dictionary<int, EPassedGrade>(); // 关卡ID和 通关等级
        public int passedLevelCount; // 已通关关卡数不包含已解锁
    }
}