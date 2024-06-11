using System.Collections.Generic;
using App.DataClass.Game.Object;
using App.Generic.BaseObject;
using App.Generic.Map;

namespace App.MVC.View.GameScene.Object
{
    public interface IMonster: IRole
    {
        float Hp{get; set;}
        float Growth{get; set;}
        MonsterData Data { get;}

        void Init(List<Cell> list);
        void Wound(int woundHp);

        void SetSpeed(float v);
    }
}