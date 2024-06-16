using Library;

namespace App.Game.Generic.BaseObject
{
    public interface IRole: IPoolObject
    {
        bool IsDead { get; set; }
        
        void Wound(int woundHp);
    }
}