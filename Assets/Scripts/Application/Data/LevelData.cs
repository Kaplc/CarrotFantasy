using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class LevelData: ScriptableObject
{
    public int levelId;
    public MapData mapData;
    public int totalWaves; // 共多少波
    public float intervalTimePerWave; // 每波间隔时间
    public int eachWaveCount; // 一波多少怪 
    public float intervalTimeEach; // 每个间隔时间
    public List<int> eachWaveMonsterId; // 每波怪id

}
