using System;
using System.Collections.Generic;
using App.Data.DataClass.Game.Object;
using App.Static.Enum;
using UnityEngine;

namespace App.Data.DataClass
{
    [CreateAssetMenu(fileName = "MonsterDataMap", menuName = "MonsterDataMap", order = 0)]
    public class MonsterDataMap : ScriptableObject
    {
        public List<MonsterMapItem> MonsterDataList = new List<MonsterMapItem>();
        private readonly Dictionary<EMonsterType, MonsterData> dataDic = new Dictionary<EMonsterType, MonsterData>();

        private void OnEnable()
        {
            var seenTowerTypes = new HashSet<EMonsterType>();
            foreach (var item in MonsterDataList)
                if (!seenTowerTypes.Add(item.monstetType))
                {
                    Debug.LogWarning($"Duplicate towerType found: {item.monstetType}");
                    return;
                }

            // 
            foreach (var item in MonsterDataList) dataDic.Add(item.monstetType, item.monsterData);
        }

        public MonsterData GetData(EMonsterType type)
        {
            if (dataDic.ContainsKey(type)) return dataDic[type];

            return null;
        }
    }

    [Serializable]
    public class MonsterMapItem
    {
        public EMonsterType monstetType;
        public MonsterData monsterData;
    }
}