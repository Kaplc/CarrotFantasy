using System.Collections.Generic;
using App.Data.DataClass;
using App.Data.DataClass.Game.Object;
using App.Data.DataClass.Map;
using App.Game.Generic.BaseObject;
using App.Game.Generic.Map;
using App.Game.Object.Carrot;
using App.Game.Object.Map;
using App.Game.Object.Monster;
using App.Game.Object.Obstacle;
using App.Game.Object.Tower;
using App.Game.SceneManager.NormalGame.interf;
using App.Static;
using App.Static.Enum;
using UnityEngine;

namespace App.Game.Spawner
{
    /// <summary>
    /// 对象生成器
    /// </summary>
    public class Spawner : MonoBehaviour, ISpawner
    {
        public Carrot carrot; // 萝卜
        public Transform startPoint; // 开始路牌位置
        private List<IMonster> monsters = new List<IMonster>(); // 已经出生的怪物
        private List<ITower> towers = new List<ITower>(); // 已创建的塔
        private List<IObstacle> obstaclesList = new List<IObstacle>(); // 已创建的障碍物

        private IMonster collectingFiresTarget; // 集火目标
        public Transform signTrans; // 集火标志

        private List<Cell> pathList;
        private List<Cell> obstacleList;

        #region 出怪

        public bool spawnedComplete; // 完成出怪
        private bool isWaveInProgress;
        private bool isStarted;
        private bool isPaused;
        private int currentWaveIndex;
        private int currentWaveMonsterIndex;
        private float waveTimer;
        private float monsterSpawnTimer;
        public List<WaveData> waveDataList = new List<WaveData>();
        private List<SpawnMonsterData> nowWaveSpawnList = new List<SpawnMonsterData>();

        #endregion

        private INormalSceneManager sceneManger;

        public virtual Carrot Carrot
        {
            get => carrot;
        }

        protected virtual void Update()
        {
            if (!isStarted || isPaused)
            {
                return;
            }

            if (currentWaveIndex < waveDataList.Count)
            {
                if (!isWaveInProgress && waveTimer >= waveDataList[currentWaveIndex].waveDuration)
                {
                    // 开始新一波
                    StartNewWave();
                }

                if (currentWaveMonsterIndex >= nowWaveSpawnList.Count)
                {
                    waveTimer += Time.deltaTime;
                    // 当前波次怪物全部出完
                    if (isWaveInProgress)
                    {
                        EndCurrentWave();
                    }
                }
                else
                {
                    // 生成怪物
                    HandleMonsterSpawning();
                }
            }
            else
            {
                spawnedComplete = true;
            }
        }

        public virtual List<IMonster> GetAllMonsters()
        {
            return monsters;
        }

        public virtual void Init(IMapData data)
        {
            spawnedComplete = false;
            isWaveInProgress = false;
            isStarted = false;
            isPaused = true;
            currentWaveIndex = 0;
            currentWaveMonsterIndex = 0;
            waveTimer = 0;
            monsterSpawnTimer = 0;
            // 转换地图数据
            pathList = data.GetPathList();
            waveDataList = data.GetWaveData();
            obstacleList = data.GetObstacle();

            CreateObstacles();
            CreateCarrot();
            CreateStartBrand();

            // 更新面板波数显示
            sceneManger = GameManager.Instance.sceneManager as INormalSceneManager;
            sceneManger?.UpdateWaveCount(1, waveDataList.Count);

            monsters.Clear();
        }

        #region 集火相关

        public virtual IMonster GetCollectingFiresTarget()
        {
            return collectingFiresTarget;
        }

        public virtual void CancelCollectingFiresTarget()
        {
            collectingFiresTarget = null;
            // 隐藏集火标志
            signTrans.gameObject.SetActive(false);
            signTrans.transform.SetParent(transform);
        }

        #endregion

        #region 出怪相关

        public virtual void StartSpawn()
        {
            isStarted = true;
            isPaused = false;
            currentWaveIndex = 0;
            currentWaveMonsterIndex = 0;
            waveTimer = 0f;
            monsterSpawnTimer = 0f;
            isWaveInProgress = false;
            Debug.Log("Wave spawning started.");
        }

        public virtual void PauseSpawn()
        {
            isPaused = true;
        }

        public virtual void ResumeSpawn()
        {
            isPaused = false;
        }

        private void StartNewWave()
        {
            isWaveInProgress = true;
            currentWaveMonsterIndex = 0;
            waveTimer = 0f;
            monsterSpawnTimer = 0f;

            // 生成出怪数据结构list
            nowWaveSpawnList.Clear();
            foreach (EachWaveData eachWaveData in waveDataList[currentWaveIndex].eachWaveDataList)
            {
                for (int i = 0; i < eachWaveData.monsterCount; i++)
                {
                    nowWaveSpawnList.Add(new SpawnMonsterData()
                    {
                        monsterType = eachWaveData.monsterType,
                        nextSpawnTime = eachWaveData.monsterDuration,
                        hard = eachWaveData.hard
                    });
                }
            }

            // 更新面板波数显示
            GameFacade.Instance.SendNotification(NotificationName.UI.WAVES_COUNT_UPDATED, (currentWaveIndex + 1, waveDataList.Count));
        }

        private void EndCurrentWave()
        {
            isWaveInProgress = false;
            currentWaveIndex++;
        }

        private void HandleMonsterSpawning()
        {
            if (currentWaveIndex == 0 && currentWaveMonsterIndex == 0)
            {
                // 第一波的第一个直接出怪不用计时
                SpawnMonster(nowWaveSpawnList[currentWaveMonsterIndex].monsterType, nowWaveSpawnList[currentWaveMonsterIndex].hard);
                currentWaveMonsterIndex++;
                monsterSpawnTimer = 0;
            }
            else
            {
                monsterSpawnTimer += Time.deltaTime;
                if (monsterSpawnTimer >= nowWaveSpawnList[currentWaveMonsterIndex].nextSpawnTime)
                {
                    SpawnMonster(nowWaveSpawnList[currentWaveMonsterIndex].monsterType, nowWaveSpawnList[currentWaveMonsterIndex].hard);
                    currentWaveMonsterIndex++;
                    monsterSpawnTimer = 0;
                }
            }
        }

        private void SpawnMonster(EMonsterType type, float hard)
        {
            Monster monster = GameManager.Instance.poolManager.GetObject("Object/Monster/" + type).GetComponent<Monster>();
            monster.transform.SetParent(transform);
            monster.transform.localScale = Vector3.one;
            MonsterDataMap map = Resources.Load<MonsterDataMap>("Data/Monster/MonsterDataMap");
            monster.Init(pathList, hard, map.GetData(type));
            monsters.Add(monster);
        }

        #endregion

        #region 升级、出售

        /// <summary>
        /// 升级塔
        /// </summary>
        public virtual void UpGradeTower(Vector3 cellWorldPos)
        {
            ITower tower = Map.GetCell(cellWorldPos).tower as ITower;
            if (tower == null) return;
            var level = tower.GetLevel();
            var data = tower.GetData();
            // 最大等级直接返回
            if (level == 2) return;

            // 够钱才升级
            if (GameManager.Instance.sceneManager.GetMoney() < data.prices[level + 1]) return;
            // 扣钱
            sceneManger.UpdateMoney(-data.prices[level + 1]);
            // 调用更新方法
            tower.UpGrade();
            // 关闭建造面板
            GameFacade.Instance.SendNotification(NotificationName.UI.HIDE_BUILT_PANEL);
        }

        /// <summary>
        /// 出售塔
        /// </summary>
        public virtual void SellTower(Vector3 cellWorldPos)
        {
            Cell cell = Map.GetCell(cellWorldPos);
            ITower tower = cell.tower as ITower;
            if (tower == null) return;
            var level = tower.GetLevel();
            var data = tower.GetData();
            // 加钱
            sceneManger.UpdateMoney(+data.sellPrices[level]);
            // 回收对象
            GameManager.Instance.poolManager.PushObject(tower.Transform.gameObject);
            // 清空格子
            cell.tower = null;
            // 关闭建造面板
            GameFacade.Instance.SendNotification(NotificationName.UI.HIDE_BUILT_PANEL);
            // 从列表移除
            towers.Remove(tower);
        }

        #endregion

        #region 创建对象

        public virtual void SetCollectingFires(IMonster monster)
        {
            for (int i = 0; i < towers.Count; i++)
            {
                towers[i].SetCollectingFiresTarget(monster);
            }

            // 设置集火标志
            collectingFiresTarget = monster;
            signTrans.gameObject.SetActive(true);
            signTrans.SetParent(monster.GetSignFather());
            signTrans.localPosition = Vector3.zero;
            signTrans.localScale = Vector3.one;
        }

        /// <summary>
        /// 根据保存的地图数据生成障碍物
        /// </summary>
        private void CreateObstacles()
        {
            for (int i = 0; i < obstacleList.Count; i++)
            {
                if (obstacleList[i].obstacleName == "None")
                {
                    continue;
                }

                Cell cell = obstacleList[i];
                // 创建实例
                Obstacle obstacle = GameManager.Instance.poolManager.GetObject($"Object/Obstacle/{cell.obstacleName}").GetComponent<Obstacle>();
                obstacle.transform.SetParent(transform);
                obstacle.transform.localScale = Vector3.one;
                obstacle.transform.position = Map.GetCellCenterPos(cell);
                cell.obstacle = obstacle.gameObject;
                obstaclesList.Add(obstacle);
            }
        }

        /// <summary>
        /// 创建塔对象
        /// </summary>
        /// <param name="towerData"></param>
        /// <param name="cellWorldPos">创建的位置世界坐标</param>
        public virtual void CreateTowerObject(TowerData towerData, Vector3 cellWorldPos)
        {
            // 够钱才创建
            if (GameManager.Instance.sceneManager.GetMoney() >= towerData.prices[0])
            {
                ITower tower = GameManager.Instance.poolManager.GetObject(towerData.prefabsPath).GetComponent<ITower>();
                tower.Transform.SetParent(transform);
                tower.Transform.localScale = Vector3.one;
                tower.Transform.position = cellWorldPos;
                // 扣钱
                sceneManger.UpdateMoney(-towerData.prices[0]);
                // 记录该格子已经存在塔
                Map.GetCell(cellWorldPos).tower = tower;
                // 关闭建造面板
                GameFacade.Instance.SendNotification(NotificationName.UI.HIDE_BUILT_PANEL);
                // 添加进列表
                if (!towers.Contains(tower))
                {
                    towers.Add(tower);
                }
            }
        }

        /// <summary>
        /// 创建萝卜
        /// </summary>
        private void CreateCarrot()
        {
            carrot = GameManager.Instance.poolManager.GetObject("Object/Carrot").GetComponent<Carrot>();
            carrot.transform.SetParent(transform);
            carrot.transform.localScale = Vector3.one;
            // 设置萝卜位置
            Cell lastPathCell = pathList[pathList.Count - 1];
            carrot.transform.position = Map.GetCellCenterPos(lastPathCell);
        }

        /// <summary>
        /// 创建开始路牌
        /// </summary>
        private void CreateStartBrand()
        {
            startPoint = Instantiate(Resources.Load<GameObject>("Object/StartPoint")).GetComponent<Transform>();
            startPoint.SetParent(transform);
            startPoint.localScale = Vector3.one;
            // 设置开始路牌位置
            Cell firstPathCell = pathList[0];
            startPoint.position = Map.GetCellCenterPos(firstPathCell);
        }

        #endregion

        #region 缓存池相关

        /// <summary>
        /// 回收所有游戏対象
        /// </summary>
        public virtual void OnPushAllGameObject()
        {
            // 隐藏标志
            signTrans.gameObject.SetActive(false);

            GameManager.Instance.poolManager.PushObject(carrot.gameObject);
            OnPushAllTowers();
            OnPushAllMonsters();
            OnPushAllObstacles();
        }

        /// <summary>
        /// 回收未死亡的怪物
        /// </summary>
        private void OnPushAllMonsters()
        {
            for (int i = 0; i < monsters.Count; i++)
            {
                if (!monsters[i].IsDead)
                {
                    GameManager.Instance.poolManager.PushObject(monsters[i].Transform.gameObject);
                }
            }

            monsters.Clear();
        }

        private void OnPushAllTowers()
        {
            for (int i = 0; i < towers.Count; i++)
            {
                GameManager.Instance.poolManager.PushObject(((MonoBehaviour)towers[i]).gameObject);
            }

            towers.Clear();
        }

        private void OnPushAllObstacles()
        {
            for (int i = 0; i < obstaclesList.Count; i++)
            {
                Obstacle o = (Obstacle)obstaclesList[i];
                if (!o.IsDead)
                {
                    GameManager.Instance.poolManager.PushObject(o.gameObject);
                }
            }

            obstaclesList.Clear();
        }

        #endregion

        #region 外部调用

        public virtual int GetNowWaveCount()
        {
            return currentWaveIndex;
        }

        #endregion

        #region 判断胜利

        public virtual bool WinJudge()
        {
            // 1.出怪完成
            if (!spawnedComplete)
            {
                return false;
            }

            // 2.萝卜没死
            if (carrot.IsDead)
            {
                return false;
            }

            // 3.怪物全部死亡
            for (int i = 0; i < monsters.Count; i++)
            {
                // 有一个没死亡都无效
                if (monsters[i].IsDead == false)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}