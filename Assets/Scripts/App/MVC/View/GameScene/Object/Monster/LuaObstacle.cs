using System;
using System.Collections.Generic;
using App.DataClass.Game.Object;
using App.Generic.Map;
using UnityEngine.Events;
using XLua;

namespace App.MVC.View.GameScene.Object
{
    [LuaCallCSharp()]
    public class LuaObstacle: IObstacle
    {
        #region 属性

        public Func<float> OnGetHpAction;
        public UnityAction<float> OnSetHpAction;
        
        public Func<float> OnGetGrowthAction;
        public UnityAction<float> OnSetGrowthAction;
        
        public Func<bool> OnGetIsDeadAction;
        public UnityAction<bool> OnSetIsDeadAction;
        
        public Func<MonsterData> OnGetDataAction;

        #endregion

        #region 方法

        public UnityAction<int> OnWoundAction;
        public UnityAction OnPushAction;
        public UnityAction OnGetAction;
        public UnityAction<List<Cell>> OnInitAction;

        #endregion

        public float Hp
        {
            get=>(float)OnGetHpAction?.Invoke(); 
            set=>OnSetHpAction?.Invoke(value);
        }

        public float Growth
        {
            get=>(float)OnGetGrowthAction?.Invoke(); 
            set=>OnSetGrowthAction?.Invoke(value);
        }

        public bool IsDead
        {
            get=>(bool)OnGetIsDeadAction?.Invoke();
            set=>OnSetIsDeadAction?.Invoke(value);
        }
        public MonsterData Data => OnGetDataAction?.Invoke();
        
        public void OnGet() => OnGetAction?.Invoke();
        public void OnPush() => OnPushAction?.Invoke();
        public void Init(List<Cell> list) => OnInitAction?.Invoke(list);
        public void Wound(int woundHp) => OnWoundAction?.Invoke(woundHp);
        public void SetSpeed(float v)
        {
            
        }
    }
}