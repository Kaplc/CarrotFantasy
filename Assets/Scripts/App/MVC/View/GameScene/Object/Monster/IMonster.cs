using System.Collections.Generic;
using App.DataClass.Game.Object;
using App.Generic.BaseObject;
using App.Generic.Map;
using UnityEngine;

namespace App.MVC.View.GameScene.Object
{
    public interface IMonster: IRole
    {
        float Hp{get; set;}
        float Growth{get; set;}
        MonsterData Data { get;}
        Transform Transform { get; }

        void Init(List<Cell> list);

        void SetSpeed(float v);
        Transform GetSignFather();
    }
}