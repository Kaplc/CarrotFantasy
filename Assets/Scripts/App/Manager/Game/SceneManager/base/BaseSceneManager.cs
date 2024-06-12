using App.DataClass.Game.Level;
using App.DataClass.Map;
using App.Manager.Game.SceneManager.Interf;
using App.MVC.View.GameScene.Object.Map;
using App.Static;
using UnityEngine;

namespace App.MVC.Controller
{
    public abstract class BaseSceneManager : MonoBehaviour, ISceneManger
    {
        protected int money;
        protected bool isSpeedUp;
        private bool pause;
        private bool stop;

        protected Map map;
        protected MapData mapData;
        protected LevelData levelData;
        protected ISpawner spawner;

        private GameFacade GameFacade => GameFacade.Instance;

        public ISpawner Spawner
        {
            get => spawner;
            set => spawner = value;
        }

        public bool IsSpeedUp
        {
            get => isSpeedUp;
            set
            {
                if (value)
                {
                    Time.timeScale = 2;
                }
                else
                {
                    Time.timeScale = 1;
                }
            }
        }

        public IMapData MapData { get => mapData; }

        public virtual void InitGame()
        {
            stop = true;
            pause = true;

            mapData = levelData.mapData;
        }

        /// <summary>
        /// 读秒结束真正开始游戏
        /// </summary>
        public virtual void StartGame()
        {
            stop = false;
            pause = false;
        }

        public virtual void PauseGame()
        {
            pause = true;
        }

        public virtual void ResumeGame()
        {
            pause = false;
        }

        public virtual void RestartGame()
        {
            EndGame();
        }

        public virtual void EndGame()
        {
            // 速度恢复
            isSpeedUp = false;
        }

        public virtual void NextLevel()
        {
        }

        public virtual void SetLevelData(LevelData levelData)
        {
            this.levelData = levelData;
        }

        public virtual void SetGameSpeed(bool isSpeedUp)
        {
            this.isSpeedUp = isSpeedUp;
        }

        public virtual void GameOver()
        {
        }

        public virtual void GameWin()
        {
        }

        public virtual void SetSpawner(ISpawner s)
        {
            spawner = s;
        }

        public int GetMoney()
        {
            return money;
        }

        public virtual void UpdateMoney(int v)
        {
            money += v;
        }

        public bool IsPause()
        {
            return pause;
        }

        public bool IsStop()
        {
            return stop;
        }

        protected void SendNotification(string name, object body = null)
        {
            GameFacade.SendNotification(name, body);
        }
    }
}