using System;
using App.DataClass.Game.Object;
using UnityEngine.Events;
using XLua;

namespace App.MVC.View.GameScene.Object.Tower
{
    [LuaCallCSharp]
    public class LuaTower : ITower
    {
        public UnityAction onPushAction;
        public UnityAction onGetAction;
        public UnityAction onAttackAction;
        public UnityAction onUpGradeAction;
        public UnityAction<IMonster> onSetCollectingFiresTargetAction;
        public Func<TowerData> onGetDataAction;
        public Func<int> onGetLevelAction;

        public void OnGet() => onGetAction?.Invoke();

        public void OnPush() => onPushAction?.Invoke();

        public void Attack() => onAttackAction?.Invoke();

        public void UpGrade() => onUpGradeAction?.Invoke();

        public void SetCollectingFiresTarget(IMonster monster) => onSetCollectingFiresTargetAction?.Invoke(monster);

        public TowerData GetData() => onGetDataAction?.Invoke();

        public int GetLevel() => (int)onGetLevelAction?.Invoke();
    }
}