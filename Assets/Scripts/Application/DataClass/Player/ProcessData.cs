using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ProcessData
{
    public Dictionary<int, List<PassedLevelData>> passedLevelsDic = new Dictionary<int, List<PassedLevelData>>(); // 各大关卡下已通关的关卡

    public ProcessData()
    {
        // 默认解锁的关卡
        List<PassedLevelData> passedLevel = new List<PassedLevelData>();
        passedLevel.Add(new PassedLevelData(){id = 0, grade = -1});
        passedLevelsDic.Add(0, passedLevel);
    }
}