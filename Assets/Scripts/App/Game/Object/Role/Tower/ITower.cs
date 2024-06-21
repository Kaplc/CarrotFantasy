using App.Data.DataClass.Game.Object;
using App.Game.Generic.BaseObject;
using App.Game.Object.Monster;
using Library;
using UnityEngine;

namespace App.Game.Object.Tower
{
    public interface ITower : IRole
    {
        void Attack();
        void UpGrade();
        void SetCollectingFiresTarget(IMonster monster);
        TowerData GetData();
        int GetLevel();
    }
}