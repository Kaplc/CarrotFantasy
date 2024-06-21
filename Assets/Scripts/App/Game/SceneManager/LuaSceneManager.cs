using System;
using App.Data.DataClass.Map;
using App.Game.Object.Monster;
using App.Game.Spawner;
using UnityEngine.Events;
using XLua;

namespace App.Game.SceneManager
{
    [LuaCallCSharp]
    public class LuaSceneManager : ISceneManger
    {
        public Func<ISceneDataManager> onGetSceneDataManagerAction;
        public Func<ISpawner> onGetSpawnerAction;
        public Func<IMapData> onGetMapDataAction;
        public UnityAction<ISpawner> onSetSpawnerAction;
        public UnityAction<ISceneDataManager> onSetSceneDataManagerAction;
        public UnityAction<int> onInitGameAction;
        public UnityAction onStartGameAction;
        public UnityAction onPauseGameAction;
        public UnityAction onResumeGameAction;
        public UnityAction onRestartGameAction;
        public UnityAction onEndGameAction;
        public UnityAction onGameOverAction;
        public UnityAction onGameWinAction;
        public UnityAction onNextLevelAction;
        public UnityAction<bool> onSetSpeedUpAction;
        public Func<bool> onIsSpeedUpFunc;
        public Func<bool> onIsPauseFunc;
        public Func<bool> onIsStopFunc;
        public UnityAction<IMonster> onSetFireTargetAction;
        public UnityAction onCancelFireAction;
        public Func<int> onGetMoneyFunc;
        public UnityAction<int> onUpdateKillMonsterCountAction;
        public UnityAction<int> onUpdateMoneyAction;

        public ISceneDataManager SceneDataManager
        {
            get => onGetSceneDataManagerAction?.Invoke();
        }

        public ISpawner Spawner
        {
            get => onGetSpawnerAction?.Invoke();
        }

        public IMapData MapData
        {
            get => onGetMapDataAction?.Invoke();
        }

        public bool IsSpeedUp
        {
            get => (bool)onIsSpeedUpFunc?.Invoke();
        }

        public void SetSpawner(ISpawner s) => onSetSpawnerAction?.Invoke(s);
        public void SetSceneDataManager(ISceneDataManager m) => onSetSceneDataManagerAction?.Invoke(m);
        public void InitGame(int levelID) => onInitGameAction?.Invoke(levelID);
        public void StartGame() => onStartGameAction?.Invoke();
        public void PauseGame() => onPauseGameAction?.Invoke();
        public void ResumeGame() => onResumeGameAction?.Invoke();
        public void RestartGame() => onRestartGameAction?.Invoke();
        public void EndGame() => onEndGameAction?.Invoke();
        public void GameOver() => onGameOverAction?.Invoke();
        public void GameWin() => onGameWinAction?.Invoke();
        public void NextLevel() => onNextLevelAction?.Invoke();
        public void SetSpeedUp(bool isSpeedUp) => onSetSpeedUpAction?.Invoke(isSpeedUp);
        public bool IsPause() => (bool)onIsPauseFunc?.Invoke();
        public bool IsStop() => (bool)onIsStopFunc?.Invoke();
        public void SetFireTarget(IMonster monster) => onSetFireTargetAction?.Invoke(monster);
        public void CancelFire() => onCancelFireAction?.Invoke();
        public int GetMoney() => (int)onGetMoneyFunc?.Invoke();
        public void UpdateKillMonsterCount(int v) => onUpdateKillMonsterCountAction?.Invoke(v);
        public void UpdateMoney(int v) => onUpdateMoneyAction?.Invoke(v);
    }
}