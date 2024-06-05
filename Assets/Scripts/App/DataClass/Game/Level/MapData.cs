using System;
using System.Collections.Generic;
using App.Generic;
using UnityEngine;

namespace App.DataClass.Game.Level
{
    [Serializable]
    public class MapData
    {
        public string mapBgSpritePath; // 地图背景Sprite路径
        public string roadSpritePath; // 路径Sprite路径
        [HideInInspector] public List<Cell> pathList = new List<Cell>();
        [HideInInspector] public List<Cell> towerList = new List<Cell>();
        [HideInInspector] public List<Cell> obstacleList = new List<Cell>();
    }
}