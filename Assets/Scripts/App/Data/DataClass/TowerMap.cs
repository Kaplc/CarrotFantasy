using System;
using System.Collections.Generic;
using App.Data.DataClass.Game.Object;
using App.Static.Enum;
using UnityEngine;
using XLua;

namespace App.Data.DataClass
{
    [CreateAssetMenu(fileName = "TowerMap", menuName = "TowerMap", order = 0)]
    [LuaCallCSharp]
    public class TowerMap : ScriptableObject
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

        public TowerData GetData(ETowerType type)
        {
            foreach (var item in towerMapDic)
            {
                if (item.towerType == type)
                {
                    return item.towerData;
                }
            }

            return null;
        }

        public TowerData GetData(string type)
        {
            foreach (var item in towerMapDic)
            {
                if (item.towerType.ToString() == type)
                {
                    return item.towerData;
                }
            }

            return null;
        }
    }

    [Serializable]
    public class TowerMapItem
    {
        public ETowerType towerType;
        public TowerData towerData;
    }
}