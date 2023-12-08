using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RoundData
{
    public MonsterData monsterData;
    public int waveCount; // 一波多少怪 
    public float intervalTimeEach; // 每个间隔时间
}
