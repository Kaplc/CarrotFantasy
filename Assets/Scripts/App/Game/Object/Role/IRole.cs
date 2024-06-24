using Library;
using UnityEngine;
using XLua;

namespace App.Game.Generic.BaseObject
{
    [LuaCallCSharp][CSharpCallLua]
    public interface IRole: IPoolObject
    {
        bool IsDead { get; set; }
        Transform Transform { get; }
        
        void Wound(int woundHp);
    }
}