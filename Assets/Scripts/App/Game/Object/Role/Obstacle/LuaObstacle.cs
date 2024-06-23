using System;
using System.Collections.Generic;
using App.Data.DataClass.Game.Object;
using App.Game.Generic.Map;
using UnityEngine;
using UnityEngine.Events;
using XLua;

namespace App.Game.Object.Obstacle
{
    [LuaCallCSharp()]
    public class LuaObstacle: MonoBehaviour, IObstacle
    {
        #region 属性

        public Func<float> onGetHpAction;
        public UnityAction<float> onSetHpAction;
        
        public Func<float> onGetGrowthAction;
        public UnityAction<float> onSetGrowthAction;
        
        public Func<bool> onGetIsDeadAction;
        public UnityAction<bool> onSetIsDeadAction;
        
        public Func<MonsterData> onGetDataAction;
        
        public Func<Transform> onGetTransformAction;

        #endregion

        #region 方法

        public UnityAction<int> onWoundAction;
        public UnityAction onPushAction;
        public UnityAction onGetAction;
        public UnityAction<List<Cell>, float, MonsterData> onInitAction;

        #endregion

        public float Hp
        {
            get=>(float)onGetHpAction?.Invoke(); 
            set=>onSetHpAction?.Invoke(value);
        }

        public float Growth
        {
            get=>(float)onGetGrowthAction?.Invoke(); 
            set=>onSetGrowthAction?.Invoke(value);
        }

        public bool IsDead
        {
            get=>(bool)onGetIsDeadAction?.Invoke();
            set=>onSetIsDeadAction?.Invoke(value);
        }
        public MonsterData Data => onGetDataAction?.Invoke();
        public Transform Transform => onGetTransformAction?.Invoke();

        public void OnGet() => onGetAction?.Invoke();
        public void OnPush() => onPushAction?.Invoke();
        public void Init(List<Cell> list, float hard, MonsterData data) => onInitAction?.Invoke(list, hard, data);
        public void Wound(int woundHp) => onWoundAction?.Invoke(woundHp);
        public void SetSpeed(float v)
        {
            
        }

        public Transform GetSignFather()
        {
            return null;
        }
    }
}