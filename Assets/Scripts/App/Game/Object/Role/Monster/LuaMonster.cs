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
        public UnityAction<BaseBuffEffect> onAddBuffEffectAction;

        public UnityAction onDeadAction;
        public UnityAction onGetAction;

        public Func<MonsterData> onGetDataAction;

        public Func<float> onGetGrowthAction;
        public Func<float> onGetHpAction;

        public Func<bool> onGetIsDeadAction;
        public Func<Transform> onGetSignFatherAction;

        public Func<Transform> onGetTransformAction;
        public UnityAction<List<Cell>, float, MonsterData> onInitAction;

        public UnityAction onMouseDownAction;
        public UnityAction onPushAction;
        public UnityAction<float> onSetGrowthAction;
        public UnityAction<float> onSetHpAction;
        public UnityAction<bool> onSetIsDeadAction;
        public UnityAction<float> onSetSpeedAction;

        public UnityAction<int> onWoundAction;

        public void OnMouseDown()
        {
            onMouseDownAction?.Invoke();
        }

        public float Hp
        {
            get => (float)onGetHpAction?.Invoke();
            set => onSetHpAction?.Invoke(value);
        }

        public float Growth
        {
            get => (float)onGetGrowthAction?.Invoke();
            set => onSetGrowthAction?.Invoke(value);
        }

        public new bool IsDead
        {
            get => (bool)onGetIsDeadAction?.Invoke();
            set => onSetIsDeadAction?.Invoke(value);
        }

        public MonsterData Data => onGetDataAction?.Invoke();
        public new Transform Transform => onGetTransformAction?.Invoke();

        public override void OnGet()
        {
            onGetAction?.Invoke();
        }

        public override void OnPush()
        {
            onPushAction?.Invoke();
        }

        public override void Wound(int woundHp)
        {
            onWoundAction?.Invoke(woundHp);
        }

        public void Init(List<Cell> list, float hard, MonsterData data)
        {
            onInitAction?.Invoke(list, hard, data);
        }

        public void SetSpeed(float v)
        {
            onSetSpeedAction?.Invoke(v);
        }

        public Transform GetSignFather()
        {
            return onGetSignFatherAction?.Invoke();
        }

        public override void Dead()
        {
            onDeadAction?.Invoke();
        }

        public void AddBuffEffect(BaseBuffEffect buffEffect)
        {
            onAddBuffEffectAction?.Invoke(buffEffect);
        }
    }
}