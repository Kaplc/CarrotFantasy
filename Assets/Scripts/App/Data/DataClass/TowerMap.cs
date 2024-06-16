using System;
using System.Collections.Generic;
using App.Data.DataClass.Game.Object;
using App.Static.Enum;
using UnityEngine;

namespace App.Data.DataClass
{
    [CreateAssetMenu(fileName = "TowerMap", menuName = "TowerMap", order = 0)]
    public class TowerMap: ScriptableObject
    {
        public List<TowerMapItem> towerMapDic = new List<TowerMapItem>();

        private void OnEnable()
        {
            HashSet<ETowerType> seenTowerTypes = new HashSet<ETowerType>();
            foreach (var item in towerMapDic)
            {
                if (!seenTowerTypes.Add(item.towerType))
                {
                    Debug.LogWarning($"Duplicate towerType found: {item.towerType}");
                }
            }
        }
    }

    [Serializable]
    public class TowerMapItem
    {
        public ETowerType towerType;
        public TowerData towerData;
    }
}