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
        public UnityAction onCancelFireAction;

        public UnityAction onExitSceneAction;
        public UnityAction onGameOverAction;
        public UnityAction onGameWinAction;
        public Func<IMapData> onGetMapDataAction;
        public Func<int> onGetMoneyFunc;
        public Func<ISceneDataManager> onGetSceneDataManagerAction;
        public Func<ISpawner> onGetSpawnerAction;
        public UnityAction<int> onInitGameAction;
        public Func<bool> onIsPauseFunc;
        public Func<bool> onIsSpeedUpFunc;
        public Func<bool> onIsStopFunc;
        public UnityAction onNextLevelAction;
        public UnityAction onPauseGameAction;
        public UnityAction onRestartGameAction;
        public UnityAction onResumeGameAction;
        public UnityAction onSelectLevelAction;
        public UnityAction<IMonster> onSetFireTargetAction;
        public UnityAction<ISceneDataManager> onSetSceneDataManagerAction;
        public UnityAction<ISpawner> onSetSpawnerAction;
        public UnityAction<bool> onSetSpeedUpAction;
        public UnityAction onStartGameAction;
        public UnityAction<int> onUpdateKillMonsterCountAction;
        public UnityAction<int> onUpdateMoneyAction;

        public ISceneDataManager SceneDataManager => onGetSceneDataManagerAction?.Invoke();

        public ISpawner Spawner => onGetSpawnerAction?.Invoke();

        public IMapData MapData => onGetMapDataAction?.Invoke();

        public bool IsSpeedUp => (bool)onIsSpeedUpFunc?.Invoke();

        public void SetSpawner(ISpawner s)
        {
            onSetSpawnerAction?.Invoke(s);
        }

        public void SetSceneDataManager(ISceneDataManager m)
        {
            onSetSceneDataManagerAction?.Invoke(m);
        }

        public void InitGame(int levelID)
        {
            onInitGameAction?.Invoke(levelID);
        }

        public void StartGame()
        {
            onStartGameAction?.Invoke();
        }

        public void PauseGame()
        {
            onPauseGameAction?.Invoke();
        }

        public void ResumeGame()
        {
            onResumeGameAction?.Invoke();
        }

        public void RestartGame()
        {
            onRestartGameAction?.Invoke();
        }

        public void SelectLevel()
        {
            onSelectLevelAction?.Invoke();
        }

        public void GameOver()
        {
            onGameOverAction?.Invoke();
        }

        public void GameWin()
        {
            onGameWinAction?.Invoke();
        }

        public void NextLevel()
        {
            onNextLevelAction?.Invoke();
        }

        public void SetSpeedUp(bool isSpeedUp)
        {
            onSetSpeedUpAction?.Invoke(isSpeedUp);
        }

        public bool IsPause()
        {
            return (bool)onIsPauseFunc?.Invoke();
        }

        public bool IsStop()
        {
            return (bool)onIsStopFunc?.Invoke();
        }

        public void SetFireTarget(IMonster monster)
        {
            onSetFireTargetAction?.Invoke(monster);
        }

        public void CancelFire()
        {
            onCancelFireAction?.Invoke();
        }

        public int GetMoney()
        {
            return (int)onGetMoneyFunc?.Invoke();
        }

        public void UpdateKillMonsterCount(int v)
        {
            onUpdateKillMonsterCountAction?.Invoke(v);
        }

        public void UpdateMoney(int v)
        {
            onUpdateMoneyAction?.Invoke(v);
        }

        public void ExitScene()
        {
            onExitSceneAction?.Invoke();
        }
    }
}