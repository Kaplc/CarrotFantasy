using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class LevelData: ScriptableObject
{
    public int levelId;
    public MapData mapData;
    public int totalWaves; // 共多少波
    public float intervalTime; // 间隔时间
    public int eachWaveCount; // 一波多少怪 
    public List<int> eachWaveMonsterId; // 每波怪id

}
