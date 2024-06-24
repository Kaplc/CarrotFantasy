using App.Data.DataClass.Map;
using App.Game.Object.Monster;
using App.Game.Spawner;

namespace App.Game.SceneManager
{
    public interface ISceneManger
    {
        ISceneDataManager SceneDataManager { get; }
        ISpawner Spawner { get; }
        IMapData MapData { get; }

        #region 设置

        void SetSpawner(ISpawner s);
        void SetSceneDataManager(ISceneDataManager m);

        #endregion

        #region 进程

        void InitGame(int levelID);
        void StartGame();
        void PauseGame();
        void ResumeGame();
        void RestartGame();
        void EndGame();
        void GameOver();
        void GameWin();
        void NextLevel();
        void SetSpeedUp(bool isSpeedUp);

        #endregion

        #region 状态

        bool IsSpeedUp { get; }
        bool IsPause();
        bool IsStop();

        #endregion

        #region 操作

        void SetFireTarget(IMonster monster);
        void CancelFire();

        #endregion

        #region 数据

        int GetMoney();
        void UpdateKillMonsterCount(int v);
        void UpdateMoney(int v);

        #endregion

        void ExitScene();
    }
}