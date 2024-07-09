using App.Data.DataClass.Map;
using App.Data.DataClass.Player;
using App.Game.Object.Map;
using App.Game.Object.Monster;
using App.Game.SceneManager.NormalGame.interf;
using App.Game.Spawner;
using App.Static;
using UnityEngine;

namespace App.Game.SceneManager.NormalGame
{
    public class NormalSceneManager : MonoBehaviour, INormalSceneManager
    {
        private GameFacade facade;
        private GameManager gameManager;
        private bool isSpeedUp;

        private Map map;
        private MapData mapData;
        private int money;

        private bool pause;
        private bool stop;


        public INormalSceneDataManager NormalSceneDataManager => SceneDataManager as INormalSceneDataManager;

        protected void Awake()
        {
            facade = GameFacade.Instance;
            gameManager = GameManager.Instance;
            gameManager.SetSceneManager(this);
            SetSceneDataManager(new NormalSceneDataManager());
            DontDestroyOnLoad(this);
        }

        public int NowItemID { get; set; }

        public int NowLevelID { get; set; }


        public bool IsSpeedUp
        {
            get => isSpeedUp;
            set
            {
                if (value)
                    Time.timeScale = 2;
                else
                    Time.timeScale = 1;

                isSpeedUp = value;
            }
        }

        public ISceneDataManager SceneDataManager { get; private set; }

        public ISpawner Spawner { get; set; }

        public IMapData MapData => mapData;

        public void ExitScene()
        {
            Destroy(gameObject);
        }

        #region 游戏事件

        private void JudgeWin()
        {
            if (Spawner.WinJudge())
            {
                GameWin();
            }
            else
            {
                if (Spawner.Carrot.Hp <= 0)
                    // 
                    GameOver();
            }
        }

        #endregion

        #region 游戏缓存变量

        private int getAllMoney;
        private int killMonsterCount;

        #endregion

        #region 游戏进程相关

        public void InitGame(int levelID)
        {
            stop = true;
            pause = true;
            getAllMoney = 0;
            killMonsterCount = 0;
            NowLevelID = levelID;
            // 显示游戏面板
            facade.SendNotification(NotificationName.UI.SHOW_GAME_PANEL);
            // 加载地图数据
            mapData = NormalSceneDataManager.LevelDataManager.GetLevelData(levelID).mapData;
            // 创建地图
            if (!map) map = Instantiate(Resources.Load<GameObject>("Prefabs/Map")).GetComponent<Map>();

            // 地图初始化
            map.Init(mapData);
            // 创建出怪器
            if (Spawner is null) Spawner = Instantiate(Resources.Load<GameObject>("Prefabs/Spawner")).GetComponent<Spawner.Spawner>();

            Spawner.Init(mapData);
            // 刷新钱
            money = mapData.money;
            facade.SendNotification(NotificationName.UI.UPDATE_MONEY, money);
            // 关闭主菜单音乐
            gameManager.StopMusic();
            // 添加事件监听
            gameManager.eventCenter.RemoveEvent("JudgeWin");
            gameManager.eventCenter.AddEventListener("JudgeWin", JudgeWin);
        }

        /// <summary>
        ///     读秒结束真正开始游戏
        /// </summary>
        public void StartGame()
        {
            stop = false;
            pause = false;
            // 开始出怪
            Spawner.StartSpawn();
        }

        public void PauseGame()
        {
            pause = true;
            Spawner.PauseSpawn();
        }

        public void ResumeGame()
        {
            pause = false;
            Spawner.ResumeSpawn();
        }

        public void RestartGame()
        {
            stop = true;
            pause = true;
            // 速度恢复
            SetSpeedUp(false);
            // 移除怪物所有Buff
            gameManager.buffManager.ClearAllBuffs();
            // 回收所有对象
            Spawner.OnPushAllGameObject();
            // 关闭相关面板
            facade.SendNotification(NotificationName.UI.HIDE_BUILT_PANEL);
            facade.SendNotification(NotificationName.UI.HIDE_MENU_PANEL);
            facade.SendNotification(NotificationName.UI.HIDE_GAME_PANEL);
            // 重新初始化
            InitGame(NowLevelID);
        }

        public void NextLevel()
        {
            // 速度恢复
            SetSpeedUp(false);
            // 移除怪物所有Buff
            gameManager.buffManager.ClearAllBuffs();
            // 回收所有对象
            Spawner.OnPushAllGameObject();
            // 清空对象池
            gameManager.poolManager.Clear();
            // 持久化统计
            var data = new StatisticalData
            {
                killMonsterCount = killMonsterCount,
                money = getAllMoney
            };
            gameManager.SaveStatisticalData(data);
            // 移除事件监听
            gameManager.eventCenter.RemoveAllListener("JudgeWin");
            // 下一关ID增加后进行初始化
            NowLevelID++;
            // 关闭相关面板
            facade.SendNotification(NotificationName.UI.HIDE_BUILT_PANEL);
            facade.SendNotification(NotificationName.UI.HIDE_MENU_PANEL);
            facade.SendNotification(NotificationName.UI.HIDE_GAME_PANEL);
            InitGame(NowLevelID);
        }

        public void GameOver()
        {
            // 停止出怪
            PauseGame();
            // 显示失败面板
            facade.SendNotification(NotificationName.UI.SHOW_LOSE_PANEL, (Spawner.GetNowWaveCount(), mapData.GetWaveCount(), NowLevelID));
            // 持久化统计
            var data = new StatisticalData
            {
                killMonsterCount = killMonsterCount,
                money = getAllMoney
            };
            gameManager.SaveStatisticalData(data);
        }

        public void SelectLevel()
        {
            // 速度恢复
            SetSpeedUp(false);
            // 移除怪物所有Buff
            gameManager.buffManager.ClearAllBuffs();
            // 回收所有对象
            Spawner.OnPushAllGameObject();
            // 清空对象池
            gameManager.poolManager.Clear();
            // 关闭相关面板
            facade.SendNotification(NotificationName.UI.HIDE_BUILT_PANEL);
            facade.SendNotification(NotificationName.UI.HIDE_MENU_PANEL);
            facade.SendNotification(NotificationName.UI.HIDE_GAME_PANEL);
            // 持久化统计
            var data = new StatisticalData
            {
                killMonsterCount = killMonsterCount,
                money = getAllMoney
            };
            gameManager.SaveStatisticalData(data);
            // 移除事件监听
            gameManager.eventCenter.RemoveAllListener("JudgeWin");

            Spawner = null;
            map = null;
            facade.SendNotification(NotificationName.LoadScene.LOADSCENE_GAME_TO_SELECTLEVEL);
        }

        public void GameWin()
        {
            var hp = Spawner.Carrot.Hp;
            // 结算通关等级
            EPassedGrade grade;
            if (1 <= hp && hp <= 3)
                // 铜
                grade = EPassedGrade.Copper;
            else if (4 <= hp && hp <= 6)
                // 银
                grade = EPassedGrade.Sliver;
            else
                // 金
                grade = EPassedGrade.Gold;

            // 显示胜利面板
            facade.SendNotification(NotificationName.UI.SHOW_WIN_PANEL,
                (
                    Spawner.GetNowWaveCount(),
                    mapData.GetWaveCount(),
                    NowLevelID,
                    grade
                )
            );

            // 保存游戏进度
            NormalSceneDataManager.ProcessDataManager.SaveProcessData(NowItemID, NowLevelID, grade);
            // 通知保存统计数据
            var data = new StatisticalData
            {
                killMonsterCount = killMonsterCount,
                money = getAllMoney
            };
            gameManager.SaveStatisticalData(data);
        }

        #endregion


        #region 游戏数据相关

        public void SetSpeedUp(bool isSpeedUp)
        {
            IsSpeedUp = isSpeedUp;
        }

        public bool IsPause()
        {
            return pause;
        }

        public bool IsStop()
        {
            return stop;
        }

        public void SetFireTarget(IMonster monster)
        {
            Spawner.SetCollectingFires(monster);
        }


        public void SetSpawner(ISpawner s)
        {
            Spawner = s;
        }

        public int GetMoney()
        {
            return money;
        }

        public void UpdateMoney(int v)
        {
            money += v;
            // 更新面板
            facade.SendNotification(NotificationName.UI.MONEY_UPDATED, money);
            // 记录到统计信息
            if (v > 0) getAllMoney += v;
        }

        public void SetSceneDataManager(ISceneDataManager m)
        {
            SceneDataManager = m;
        }

        public void UpdateWaveCount(int now, int total)
        {
            GameFacade.Instance.SendNotification(NotificationName.UI.WAVES_COUNT_UPDATED, (now, total));
        }

        public void UpdateKillMonsterCount(int v)
        {
            killMonsterCount += v;
        }

        public void CancelFire()
        {
            Spawner.CancelCollectingFiresTarget();
        }

        #endregion
    }
}