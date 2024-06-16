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
    public class LuaSpawner : ISpawner
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
        
        public Carrot Carrot
        {
            get => onGetCarrotAction?.Invoke();
        }
        public IMonster GetCollectingFiresTarget() => onGetCollectingFiresTargetAction?.Invoke();
        public List<IMonster> GetAllMonsters() => onGetAllMonstersAction?.Invoke();
        public int GetNowWaveCount() => (int)onGetNowWaveCountAction?.Invoke();
        public void Init(IMapData mapData) => onInitAction?.Invoke(mapData);
        public void SetCollectingFires(IMonster monster) => onSetCollectingFiresAction?.Invoke(monster);
        public void CancelCollectingFiresTarget() => onCancelCollectingFiresTargetAction?.Invoke();
        public void StartSpawn() => onStartSpawnAction?.Invoke();
        public void PauseSpawn() => onPauseWavesAction?.Invoke();
        public void ResumeSpawn() => onResumeWavesAction?.Invoke();
        public void CreateTowerObject(TowerData towerData, Vector3 cellWorldPos) => onCreateTowerObjectAction?.Invoke(towerData, cellWorldPos);
        public void UpGradeTower(Vector3 cellWorldPos) => onUpGradeTowerAction?.Invoke(cellWorldPos);
        public void SellTower(Vector3 cellWorldPos) => onSellTowerAction?.Invoke(cellWorldPos);
        public void OnPushAllGameObject() => onPushAllGameObjectAction?.Invoke();
        public bool WinJudge() => (bool)onWinJudgeAction?.Invoke();
    }
}