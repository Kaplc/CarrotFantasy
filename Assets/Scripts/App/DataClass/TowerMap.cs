using System;
using System.Collections.Generic;
using App.DataClass.Game.Object;
using App.Static.Enum;
using UnityEngine;

namespace App.DataClass
{
    [CreateAssetMenu(fileName = "TowerMap", menuName = "TowerMap", order = 0)]
    public class TowerMap: ScriptableObject
    {
        public List<TowerMapItem> towerMapItems = new List<TowerMapItem>();
    }

    [Serializable]
    public class TowerMapItem
    {
        public ETowerType towerType;
        public TowerData towerData;
    }
}