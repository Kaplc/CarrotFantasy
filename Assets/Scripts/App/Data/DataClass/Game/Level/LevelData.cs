using System;
using App.Data.DataClass.Map;
using UnityEngine;

namespace App.Data.DataClass.Game.Level
{
    [CreateAssetMenu]
    [Serializable]
    public class LevelData : ScriptableObject
    {
        public int levelID; // 关卡id
        public int itemID; // 属于哪个主题ID
        public Sprite image; // 关卡缩略图
        public MapData mapData; // 地图数据
    }
}