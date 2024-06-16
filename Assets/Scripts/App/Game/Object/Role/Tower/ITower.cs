using App.Data.DataClass.Game.Object;
using App.Game.Object.Monster;
using Library;

namespace App.Game.Object.Tower
{
    public interface ITower: IPoolObject
    {
        void Attack();
        void UpGrade();
        void SetCollectingFiresTarget(IMonster monster);
        TowerData GetData();
        int GetLevel();
    }
}