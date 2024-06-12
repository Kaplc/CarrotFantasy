using App.DataClass.Game.Level;
using App.DataClass.Map;

namespace App.Manager.Game.SceneManager.Interf
{
    public interface ISceneManger
    {
        ISpawner Spawner { get; set; }
        bool IsSpeedUp { get; set; }
        IMapData MapData { get;}
        
        void InitGame();
        void StartGame();
        void PauseGame();
        void ResumeGame();
        void RestartGame();
        void EndGame();
        void GameOver();
        void GameWin();
        void NextLevel();
        void SetLevelData(LevelData levelData);
        
        int GetMoney();
        void UpdateMoney(int v);
        bool IsPause();
        bool IsStop();
    }
}