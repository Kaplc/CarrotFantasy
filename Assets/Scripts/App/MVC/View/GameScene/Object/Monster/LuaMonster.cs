using System;
using System.Collections.Generic;
using App.DataClass.Game.Object;
using App.Generic.Map;
using UnityEngine;
using UnityEngine.Events;

namespace App.MVC.View.GameScene.Object
{
    public class LuaMonster : IMonster
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


        public float Hp { get => (float)onGetHpAction?.Invoke(); set => onSetHpAction?.Invoke(value); }
        public float Growth { get => (float)onGetGrowthAction?.Invoke(); set => onSetGrowthAction?.Invoke(value); }
        public bool IsDead { get => (bool)onGetIsDeadAction?.Invoke(); set => onSetIsDeadAction?.Invoke(value); }
        public MonsterData Data => onGetDataAction?.Invoke();
        public Transform Transform => onGetTransformAction?.Invoke();

        public void OnGet() => onGetAction?.Invoke();

        public void OnPush() => onPushAction?.Invoke();

        public void Wound(int woundHp) => onWoundAction?.Invoke(woundHp);
        
        public void Init(List<Cell> list) => onInitAction?.Invoke(list);

        public void SetSpeed(float v) =>  onSetSpeedAction?.Invoke(v);

        public Transform GetSignFather() => onGetSignFatherAction?.Invoke();

    }
}