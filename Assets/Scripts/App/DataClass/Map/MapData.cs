using System.Collections.Generic;
using App.DataClass.Game.Object;
using App.Generic;
using App.Generic.Map;
using App.Static.Enum;
using UnityEngine;

namespace App.DataClass.Map
{
    [CreateAssetMenu(fileName = "MapData", menuName = "MapData", order = 0)]
    public class MapData: ScriptableObject
    {
        [Header("地图数据")] 
        public int money;
        public Sprite mapBgTexture;
        public Sprite mapFgTexture;
        public List<PointClass> pathList = new List<PointClass>();
        public List<PointClass> towerList = new List<PointClass>();
        public List<ETowerType> towerTypeList = new List<ETowerType>();
        public List<ObstaclePointClass> obstacleList = new List<ObstaclePointClass>();
        
        [Header("出怪数据")]
        public List<WaveData> waveDataList = new List<WaveData>();

        public int GetWaveCount()
        {
            return waveDataList.Count;
        }
        
        public TowerData GetTowerData(int index)
        {
            // 从TowerMap获取
            TowerMap towerMap = Resources.Load<TowerMap>("Data/Tower/TowerMap");

            foreach (var item in towerMap.towerMapItems)
            {
                if (item.towerType == towerTypeList[index])
                {
                    return item.towerData;
                }
            }

            return null;
        }
    }
}