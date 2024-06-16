using System.Collections.Generic;
using App.Data.DataClass.Game.Object;
using App.Game.Generic.Map;
using App.Static.Enum;
using UnityEngine;

namespace App.Data.DataClass.Map
{
    [CreateAssetMenu(fileName = "MapData", menuName = "MapData", order = 0)]
    public class MapData: ScriptableObject, IMapData
    {
        [Header("地图数据")] 
        public int money;
        public Sprite mapBgTexture;
        public Sprite mapFgTexture;
        public List<PointClass> pathList = new List<PointClass>();
        public List<PointClass> towerPosList = new List<PointClass>();
        public List<ETowerType> towerTypeList = new List<ETowerType>();
        public List<ObjectPointClass> obstacleList = new List<ObjectPointClass>();
        public List<ObjectPointClass> towerList = new List<ObjectPointClass>();

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

            foreach (var item in towerMap.towerMapDic)
            {
                if (item.towerType == towerTypeList[index])
                {
                    return item.towerData;
                }
            }

            return null;
        }

        public List<Cell> GetPathList()
        {
            return PointClassToCell.ToCellList(pathList);
        }

        public List<WaveData> GetWaveData()
        {
            return waveDataList;
        }

        public List<Cell> GetObstacle()
        {
            return PointClassToCell.ToCellList(obstacleList);
        }
    }
}