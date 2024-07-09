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
    [LuaCallCSharp]
    public class LuaSpawner : ISpawner
    {
        public UnityAction onCancelCollectingFiresTargetAction;
        public UnityAction<TowerData, Vector3> onCreateTowerObjectAction;
        public Func<List<IMonster>> onGetAllMonstersAction;
        public Func<Carrot> onGetCarrotAction;
        public Func<IMonster> onGetCollectingFiresTargetAction;
        public Func<int> onGetNowWaveCountAction;
        public UnityAction<IMapData> onInitAction;
        public UnityAction onPauseWavesAction;
        public UnityAction onPushAllGameObjectAction;
        public UnityAction onResumeWavesAction;
        public UnityAction<Vector3> onSellTowerAction;
        public UnityAction<IMonster> onSetCollectingFiresAction;
        public UnityAction onStartSpawnAction;
        public UnityAction<Vector3> onUpGradeTowerAction;
        public Func<bool> onWinJudgeAction;

        public Carrot Carrot => onGetCarrotAction?.Invoke();

        public IMonster GetCollectingFiresTarget()
        {
            return onGetCollectingFiresTargetAction?.Invoke();
        }

        public List<IMonster> GetAllMonsters()
        {
            return onGetAllMonstersAction?.Invoke();
        }

        public int GetNowWaveCount()
        {
            return (int)onGetNowWaveCountAction?.Invoke();
        }

        public void Init(IMapData mapData)
        {
            onInitAction?.Invoke(mapData);
        }

        public void SetCollectingFires(IMonster monster)
        {
            onSetCollectingFiresAction?.Invoke(monster);
        }

        public void CancelCollectingFiresTarget()
        {
            onCancelCollectingFiresTargetAction?.Invoke();
        }

        public void StartSpawn()
        {
            onStartSpawnAction?.Invoke();
        }

        public void PauseSpawn()
        {
            onPauseWavesAction?.Invoke();
        }

        public void ResumeSpawn()
        {
            onResumeWavesAction?.Invoke();
        }

        public void CreateTowerObject(TowerData towerData, Vector3 cellWorldPos)
        {
            onCreateTowerObjectAction?.Invoke(towerData, cellWorldPos);
        }

        public void UpGradeTower(Vector3 cellWorldPos)
        {
            onUpGradeTowerAction?.Invoke(cellWorldPos);
        }

        public void SellTower(Vector3 cellWorldPos)
        {
            onSellTowerAction?.Invoke(cellWorldPos);
        }

        public void OnPushAllGameObject()
        {
            onPushAllGameObjectAction?.Invoke();
        }

        public bool WinJudge()
        {
            return (bool)onWinJudgeAction?.Invoke();
        }
    }
}