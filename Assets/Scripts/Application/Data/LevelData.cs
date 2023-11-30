using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class LevelData : ScriptableObject
{
    public int levelId; // 关卡id
    public int money; // 初始的钱
    public int[] towersID; // 所有塔id
    public int[] monstersID; // 该关卡所有怪id
    
    public float intervalTimePerWave; // 每波间隔时间
    public List<RoundData> roundDataList;
    [HideInInspector] public MapData mapData; // 地图数据
    
}