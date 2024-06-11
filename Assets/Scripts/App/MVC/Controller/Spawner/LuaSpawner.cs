using System;
using System.Collections.Generic;
using App.DataClass.Game.Object;
using App.DataClass.Map;
using App.MVC.View.GameScene.Object;
using App.MVC.View.GameScene.Object.Carrot;
using UnityEngine;
using UnityEngine.Events;
using XLua;

    [LuaCallCSharp()]
    public class LuaSpawner: ISpawner
    {
        public Func<Carrot> onGetCarrotAction;
        public UnityAction onPushAllGameObjectAction;
        public Func<bool> onWinJudgeAction;
        public UnityAction onPauseWavesAction;
        public UnityAction onResumeWavesAction;
        public UnityAction onStartSpawnAction;
        public Func<List<Monster>> onGetAllMonstersAction;
        public Func<Monster> onGetCollectingFiresTargetAction;
        public UnityAction<Monster> onSetCollectingFiresAction;
        public UnityAction<Monster> onSetCollectingFiresTargetAction;
        public UnityAction onCancelCollectingFiresTargetAction;
        public UnityAction<MapData> onInitAction;
        public UnityAction<TowerData, Vector3> onCreateTowerObjectAction;
        public UnityAction<Vector3> onUpGradeTowerAction;
        public UnityAction<Vector3> onSellTowerAction;
        public Func<int> onGetNowWaveCountAction;


        public Carrot GetCarrot() => onGetCarrotAction?.Invoke();
        public Monster GetCollectingFiresTarget() => onGetCollectingFiresTargetAction?.Invoke();
        public List<Monster> GetAllMonsters() => onGetAllMonstersAction?.Invoke();
        public int GetNowWaveCount() => (int)onGetNowWaveCountAction?.Invoke();
        public void Init(MapData mapData) => onInitAction?.Invoke(mapData);
        public void SetCollectingFires(Monster monster) => onSetCollectingFiresAction?.Invoke(monster);
        public void SetCollectingFiresTarget(Monster monster) => onSetCollectingFiresTargetAction?.Invoke(monster);
        public void CancelCollectingFiresTarget() => onCancelCollectingFiresTargetAction?.Invoke();
        public void StartSpawn() => onStartSpawnAction?.Invoke();
        public void PauseWaves() => onPauseWavesAction?.Invoke();
        public void ResumeWaves() => onResumeWavesAction?.Invoke();
        public void CreateTowerObject(TowerData towerData, Vector3 cellWorldPos) => onCreateTowerObjectAction?.Invoke(towerData, cellWorldPos);
        public void UpGradeTower(Vector3 cellWorldPos) => onUpGradeTowerAction?.Invoke(cellWorldPos);
        public void SellTower(Vector3 cellWorldPos) => onSellTowerAction?.Invoke(cellWorldPos);
        public void OnPushAllGameObject() => onPushAllGameObjectAction?.Invoke();
        public bool WinJudge() => (bool)onWinJudgeAction?.Invoke();
    }