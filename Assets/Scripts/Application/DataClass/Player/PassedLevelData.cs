using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPassedGrade
{
    Copper,
    Sliver,
    Gold,
    None
}

[Serializable]
public class PassedLevelData
{
    // ID - Grade
    public Dictionary<int, EPassedGrade> passedLevelDic = new Dictionary<int, EPassedGrade>();
}
