using System;
using App.Data.DataClass.Game.Object;
using App.Game.Object.Monster;
using UnityEngine;
using UnityEngine.Events;
using XLua;

namespace App.Game.Object.Tower
{
    [LuaCallCSharp]
    public class LuaTower : MonoBehaviour, ITower
    {
        public UnityAction onAttackAction;
        public UnityAction onGetAction;
        public Func<TowerData> onGetDataAction;
        public Func<bool> onGetIsDeadAction;
        public Func<int> onGetLevelAction;
        public UnityAction onPushAction;
        public UnityAction<IMonster> onSetCollectingFiresTargetAction;
        public UnityAction<bool> onSetIsDeadAction;
        public UnityAction onUpGradeAction;

        public bool IsDead
        {
            get => (bool)onGetIsDeadAction?.Invoke();
            set => onSetIsDeadAction?.Invoke(value);
        }

        public Transform Transform => transform;

        public void Wound(int woundHp)
        {
        }

        public void OnGet()
        {
            onGetAction?.Invoke();
        }

        public void OnPush()
        {
            onPushAction?.Invoke();
        }

        public void Attack()
        {
            onAttackAction?.Invoke();
        }

        public void UpGrade()
        {
            onUpGradeAction?.Invoke();
        }

        public void SetCollectingFiresTarget(IMonster monster)
        {
            onSetCollectingFiresTargetAction?.Invoke(monster);
        }

        public TowerData GetData()
        {
            return onGetDataAction?.Invoke();
        }

        public int GetLevel()
        {
            return (int)onGetLevelAction?.Invoke();
        }
    }
}