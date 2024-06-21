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
        public UnityAction onPushAction;
        public UnityAction onGetAction;
        public UnityAction onAttackAction;
        public UnityAction onUpGradeAction;
        public UnityAction<IMonster> onSetCollectingFiresTargetAction;
        public Func<TowerData> onGetDataAction;
        public Func<int> onGetLevelAction;
        public Func<bool> onGetIsDeadAction;
        public UnityAction<bool> onSetIsDeadAction;

        public bool IsDead
        {
            get => (bool)onGetIsDeadAction?.Invoke();
            set => onSetIsDeadAction?.Invoke(value);
        }

        public Transform Transform => transform;

        public void Wound(int woundHp)
        {
        }

        public void OnGet() => onGetAction?.Invoke();

        public void OnPush() => onPushAction?.Invoke();

        public void Attack() => onAttackAction?.Invoke();

        public void UpGrade() => onUpGradeAction?.Invoke();

        public void SetCollectingFiresTarget(IMonster monster) => onSetCollectingFiresTargetAction?.Invoke(monster);

        public TowerData GetData() => onGetDataAction?.Invoke();

        public int GetLevel() => (int)onGetLevelAction?.Invoke();
    }
}