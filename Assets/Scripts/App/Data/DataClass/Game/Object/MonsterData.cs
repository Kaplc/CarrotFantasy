using UnityEngine;
using XLua;

namespace App.Data.DataClass.Game.Object
{
    [CreateAssetMenu]
    public class MonsterData: ScriptableObject
    {
        public int id;
        public float speed;
        public float maxHp;
        public float atk;
        public int baseMoney;
        public string prefabsPath;
    }
}
