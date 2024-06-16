using System;
using System.Collections.Generic;
using App.Data.DataClass.Game.Object;
using App.Game.Generic.Map;
using UnityEngine;
using UnityEngine.Events;
using XLua;

namespace App.Game.Object.Monster
{
    [LuaCallCSharp]
    public class LuaMonster :Monster, IMonster
    {
        public Func<float> onGetHpAction;
        public UnityAction<float> onSetHpAction;
        
        public Func<float> onGetGrowthAction;
        public UnityAction<float> onSetGrowthAction;
        
        public Func<bool> onGetIsDeadAction;
        public UnityAction<bool> onSetIsDeadAction;
        
        public Func<MonsterData> onGetDataAction;
        
        public Func<Transform> onGetTransformAction;
        
        public UnityAction<int> onWoundAction;
        public UnityAction onPushAction;
        public UnityAction onGetAction;
        public UnityAction<List<Cell>> onInitAction;
        public UnityAction<float> onSetSpeedAction;
        public Func<Transform> onGetSignFatherAction;
        
        public new float Hp { get => (float)onGetHpAction?.Invoke(); set => onSetHpAction?.Invoke(value); }
        public new float Growth { get => (float)onGetGrowthAction?.Invoke(); set => onSetGrowthAction?.Invoke(value); }
        public new bool IsDead { get => (bool)onGetIsDeadAction?.Invoke(); set => onSetIsDeadAction?.Invoke(value); }
        public new MonsterData Data => onGetDataAction?.Invoke();
        public new Transform Transform => onGetTransformAction?.Invoke();

        public override void OnGet() => onGetAction?.Invoke();

        public override void OnPush() => onPushAction?.Invoke();

        public override void Wound(int woundHp) => onWoundAction?.Invoke(woundHp);
        
        public override void Init(List<Cell> list) => onInitAction?.Invoke(list);

        public override void SetSpeed(float v) =>  onSetSpeedAction?.Invoke(v);

        public override Transform GetSignFather() => onGetSignFatherAction?.Invoke();

    }
}