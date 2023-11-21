using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class LevelData : ScriptableObject
{
    public int levelId; // 关卡id
    [HideInInspector] public MapData mapData; // 地图数据
    public List<int> monsterIds; // 该关卡所有怪id
    public float intervalTimePerWave; // 每波间隔时间
    public List<RoundData> roundDataList;
}