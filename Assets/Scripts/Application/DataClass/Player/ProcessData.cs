using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ProcessData
{
    // 数据结构 bigLevelID - levelID - Grade
    public Dictionary<int, PassedLevelData> passedBigLevelsDic = new Dictionary<int, PassedLevelData>(); // 各大关卡下已通关的关卡

    public ProcessData()
    {
        // 默认解锁的关卡
        PassedLevelData passedLevelData = new PassedLevelData();
        passedLevelData.passedLevelDic.Add(0, EPassedGrade.None);
        passedBigLevelsDic.Add(0, passedLevelData);
    }
}