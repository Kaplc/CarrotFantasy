using System;
using System.Collections.Generic;
using App.Data.DataClass.Game.Object;
using App.Game.Generic.BaseObject;
using App.Game.Generic.Map;
using UnityEngine;
using UnityEngine.Events;
using XLua;

namespace App.Game.Object.Monster
{
    [LuaCallCSharp]
    public class LuaMonster : BaseRole, IMonster
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
        public UnityAction<List<Cell>, float, MonsterData> onInitAction;
        public UnityAction<float> onSetSpeedAction;
        public Func<Transform> onGetSignFatherAction;

        public UnityAction onDeadAction;

        public UnityAction<BaseBuffEffect> onAddBuffEffectAction;
        
        public float Hp { get => (float)onGetHpAction?.Invoke(); set => onSetHpAction?.Invoke(value); }
        public float Growth { get => (float)onGetGrowthAction?.Invoke(); set => onSetGrowthAction?.Invoke(value); }
        public new bool IsDead { get => (bool)onGetIsDeadAction?.Invoke(); set => onSetIsDeadAction?.Invoke(value); }
        public MonsterData Data => onGetDataAction?.Invoke();
        public new Transform Transform => onGetTransformAction?.Invoke();

        public override void OnGet() => onGetAction?.Invoke();

        public override void OnPush() => onPushAction?.Invoke();

        public override void Wound(int woundHp) => onWoundAction?.Invoke(woundHp);
        
        public void Init(List<Cell> list, float hard, MonsterData data) => onInitAction?.Invoke(list, hard, data);

        public void SetSpeed(float v) =>  onSetSpeedAction?.Invoke(v);

        public Transform GetSignFather() => onGetSignFatherAction?.Invoke();

        public override void Dead() => onDeadAction?.Invoke();

        public void AddBuffEffect(BaseBuffEffect buffEffect) => onAddBuffEffectAction?.Invoke(buffEffect);
    }
}