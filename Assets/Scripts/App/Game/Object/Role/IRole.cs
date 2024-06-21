using Library;
using UnityEngine;

namespace App.Game.Generic.BaseObject
{
    public interface IRole: IPoolObject
    {
        bool IsDead { get; set; }
        Transform Transform { get; }
        
        void Wound(int woundHp);
    }
}