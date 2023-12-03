using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ProcessData
{
    public List<PassedLevelData> passedLevels = new List<PassedLevelData>(){new PassedLevelData(){id = 0, grade = -1}}; // 已通关的关卡
}
