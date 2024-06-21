using System;
using System.Collections.Generic;
using App.Data.DataClass.Game.Object;
using App.Data.DataClass.Map;
using App.Game.Object.Carrot;
using App.Game.Object.Monster;
using UnityEngine;
using UnityEngine.Events;
using XLua;

namespace App.Game.Spawner
{
    [LuaCallCSharp()]
    public class LuaSpawner : Spawner, ISpawner
    {
        public Func<Carrot> onGetCarrotAction;
        public UnityAction onPushAllGameObjectAction;
        public Func<bool> onWinJudgeAction;
        public UnityAction onPauseWavesAction;
        public UnityAction onResumeWavesAction;
        public UnityAction onStartSpawnAction;
        public Func<List<IMonster>> onGetAllMonstersAction;
        public Func<IMonster> onGetCollectingFiresTargetAction;
        public UnityAction<IMonster> onSetCollectingFiresAction;
        public UnityAction onCancelCollectingFiresTargetAction;
        public UnityAction<IMapData> onInitAction;
        public UnityAction<TowerData, Vector3> onCreateTowerObjectAction;
        public UnityAction<Vector3> onUpGradeTowerAction;
        public UnityAction<Vector3> onSellTowerAction;
        public Func<int> onGetNowWaveCountAction;

        public override Carrot Carrot
        {
            get => onGetCarrotAction?.Invoke();
        }

        public override IMonster GetCollectingFiresTarget() => onGetCollectingFiresTargetAction?.Invoke();
        public override List<IMonster> GetAllMonsters() => onGetAllMonstersAction?.Invoke();
        public override int GetNowWaveCount() => (int)onGetNowWaveCountAction?.Invoke();
        public override void Init(IMapData mapData) => onInitAction?.Invoke(mapData);
        public override void SetCollectingFires(IMonster monster) => onSetCollectingFiresAction?.Invoke(monster);
        public override void CancelCollectingFiresTarget() => onCancelCollectingFiresTargetAction?.Invoke();
        public override void StartSpawn() => onStartSpawnAction?.Invoke();
        public override void PauseSpawn() => onPauseWavesAction?.Invoke();
        public override void ResumeSpawn() => onResumeWavesAction?.Invoke();
        public override void CreateTowerObject(TowerData towerData, Vector3 cellWorldPos) => onCreateTowerObjectAction?.Invoke(towerData, cellWorldPos);
        public override void UpGradeTower(Vector3 cellWorldPos) => onUpGradeTowerAction?.Invoke(cellWorldPos);
        public override void SellTower(Vector3 cellWorldPos) => onSellTowerAction?.Invoke(cellWorldPos);
        public override void OnPushAllGameObject() => onPushAllGameObjectAction?.Invoke();
        public override bool WinJudge() => (bool)onWinJudgeAction?.Invoke();
    }
}