using App.DataClass.Game.Level;
using App.DataClass.Player;
using App.Manager.Game.SceneManager.Interf;
using App.MVC.Controller.Commands.Args;
using App.MVC.Model.PlayerData;
using App.MVC.View.GameScene.Object.Map;
using App.Static;
using UnityEngine;

namespace App.MVC.Controller
{
    public class NormalSceneManager : BaseSceneManager, INormalSceneManager
    {
        private int nowBigLevelID;
        private int nowLevelID;
        private LevelData nowLevelData;
        private StatisticalData statisticalData;

        private void Awake()
        {
            SendNotification(NotificationName.Game.INIT_SCENE_MANAGER, new InitSceneManagerCommandArgs()
            {
                sceneManger = this
            });
            DontDestroyOnLoad(this);
        }

        public override void InitGame()
        {
            base.InitGame();
            // 显示游戏面板
            SendNotification(NotificationName.UI.SHOW_GAMEPANEL);
            // 创建地图
            map = Instantiate(Resources.Load<GameObject>("Prefabs/Map")).GetComponent<Map>();
            // 地图初始化
            map.Init(mapData);
            // 创建出怪器
            spawner = Instantiate(Resources.Load<GameObject>("Prefabs/Spawner")).GetComponent<Spawner>();
            spawner.Init(mapData);
            // 刷新钱
            money = mapData.money;
            // 更新面板
            SendNotification(NotificationName.UIEvent.GAMEPANEL_UPDATE_MONEY, money);
        }

        public override void StartGame()
        {
            base.StartGame();
            
            // 开始出怪
            spawner.StartSpawn();
        }

        public override void GameWin()
        {
            base.GameWin();
            
            float hp = spawner.GetCarrot().Hp;
            // 结算通关等级
            EPassedGrade grade;
            if (1 <= hp && hp <= 3)
            {
                // 铜
                grade = EPassedGrade.Copper;
            }
            else if (4 <= hp && hp <= 6)
            {
                // 银
                grade = EPassedGrade.Sliver;
            }
            else
            {
                // 金
                grade = EPassedGrade.Gold;
            }
            // 显示胜利面板
            GameFacade.Instance.SendNotification(NotificationName.UI.SHOW_WIN_PANEL,
                (
                    spawner.GetNowWaveCount(),
                    nowLevelData.mapData.GetWaveCount(),
                    nowLevelData.levelID,
                    grade
                )
            );
            
            // 保存游戏进度
            ProcessDataManager manager = GameFacade.Instance.RetrieveProxy(nameof(ProcessDataManager)) as ProcessDataManager;
            manager?.SaveProcessData(nowBigLevelID, nowLevelData.levelID, grade);

            // 通知保存统计数据
            GameManager.Instance.SaveGameData();
        }

        public override void GameOver()
        {
            base.GameOver();
            // 停止出怪
            GameFacade.Instance.SendNotification(NotificationName.Game.STOP_SPAWN);
            // 显示失败面板
            GameFacade.Instance.SendNotification(NotificationName.UI.SHOW_LOSE_PANEL,
                (
                    spawner.GetNowWaveCount(),
                    nowLevelData.mapData.GetWaveCount(),
                    nowLevelData.levelID
                )
            );
            // 持久化统计信息
            GameFacade.Instance.SendNotification(NotificationName.Data.SAVE_STATISTICAL_DATA);
        }

        public override void EndGame()
        {
            base.EndGame();
            // 移除怪物所有Buff
            GameManager.Instance.BuffManager.ClearAllBuffs();
            // 回收所有对象
            GameManager.Instance.PoolManager.PushObject(spawner.GetCarrot().gameObject);
            spawner.OnPushAllGameObject();
            // 关闭相关面板
            SendNotification(NotificationName.UI.HIDE_BUILTPANEL);
            SendNotification(NotificationName.UI.HIDE_MENUPANEL);
            SendNotification(NotificationName.UI.HIDE_GAMEPANEL);
            // 持久化统计信息
            GameFacade.Instance.SendNotification(NotificationName.Data.SAVE_STATISTICAL_DATA);
        }

        public override void UpdateMoney(int v)
        {
            base.UpdateMoney(v);
            
            // 更新面板
            GameFacade.Instance.SendNotification(NotificationName.UIEvent.GAMEPANEL_UPDATE_MONEY, money);
            if (v > 0)
            {
                // 记录到统计信息
                SendNotification(NotificationName.Data.CHANGE_MONEY_COUNT, +v);
            }
        }

        public override void NextLevel()
        {
            base.NextLevel();

            nowLevelID++;
            // 加载地图数据
            
        }

        public override void PauseGame()
        {
            base.PauseGame();
            spawner.PauseSpawn();
        }

        public override void RestartGame()
        {
            base.RestartGame();
            
            // 重新加载游戏
            InitGame();
        }

        public override void ResumeGame()
        {
            base.ResumeGame();
        }

        public int GetNowBigLevelID()
        {
            return nowBigLevelID;
        }

        public int GetLevelID()
        {
            return nowLevelID;
        }

        public int NowBigLevelID
        {
            get => nowBigLevelID;
            set => nowBigLevelID = value;
        }
        public int NowLevelID
        {
            get => nowLevelID;
            set => nowLevelID = value;
        }
    }
}