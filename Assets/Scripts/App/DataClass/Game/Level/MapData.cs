using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MapData
{
    public string mapBgSpritePath; // 地图背景Sprite路径
    public string roadSpritePath; // 路径Sprite路径
    [HideInInspector] public List<Cell> pathList = new List<Cell>();
    [HideInInspector] public List<Cell> towerList = new List<Cell>();
    [HideInInspector] public List<Cell> obstacleList = new List<Cell>();
}