using App.DataClass.Game.Object;
using Library;

namespace App.MVC.View.GameScene.Object.Tower
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