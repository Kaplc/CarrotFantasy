using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


[CreateAssetMenu]
[Serializable]
public class LevelData : ScriptableObject
{
    public int levelID; // 关卡id
    public int itemID; // 属于哪个主题ID
    public int money; // 初始的钱
    public string mapDataPath; // 加载的地图数据的文件名
    public float intervalTimePerWave; // 每波间隔时间
    
    public List<TowerData> towersData; // 该关卡所有塔数据
    public List<RoundData> roundDataList;
    [HideInInspector] public MapData mapData; // 地图数据
    public Sprite image; // 关卡缩略图
}