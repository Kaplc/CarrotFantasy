using System;
using System.Collections.Generic;

namespace App.Data.DataClass.Player
{
    [Serializable]
    public class ProcessData
    {
        // 数据结构 bigLevelID - levelID - Grade
        public Dictionary<int, PassedLevelData> passedItemsDic = new Dictionary<int, PassedLevelData>(); // 各大关卡下已通关的关卡

        public ProcessData()
        {
            // 默认解锁的关卡
            var passedLevelData = new PassedLevelData();
            passedLevelData.passedLevelDic.Add(0, EPassedGrade.None);
            passedItemsDic.Add(0, passedLevelData);
        }
    }
}