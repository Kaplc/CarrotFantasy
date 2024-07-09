using System.Collections.Generic;
using UnityEngine;

namespace App.Data.DataClass.Game.Level
{
    [CreateAssetMenu]
    public class ItemData : ScriptableObject
    {
        public int id;
        public List<LevelData> levels; // 大关卡下的所有小关卡id
    }
}