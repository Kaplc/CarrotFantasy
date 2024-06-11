using App.DataClass.Game.Object;
using Library;

namespace App.Generic.BaseObject
{
    public interface IRole: IPoolObject
    {
        bool IsDead { get; set; }
        
        void Wound(int woundHp);
    }
}