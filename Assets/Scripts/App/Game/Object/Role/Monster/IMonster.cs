using System.Collections.Generic;
using App.Data.DataClass.Game.Object;
using App.Game.Generic.BaseObject;
using App.Game.Generic.Map;
using Library;
using UnityEngine;
using XLua;

namespace App.Game.Object.Monster
{
    [LuaCallCSharp][CSharpCallLua]
    public interface IMonster : IRole
    {
        float Hp { get; set; }
        float Growth { get; set; }
        MonsterData Data { get; }

        void Init(List<Cell> list, float hard, MonsterData dat);

        void SetSpeed(float v);
        Transform GetSignFather();
        void Dead();

        void AddBuffEffect(BaseBuffEffect buffEffect);
    }
}