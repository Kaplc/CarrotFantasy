using System;
using System.Collections.Generic;
using App.DataClass.Game.Object;
using App.DataClass.Map;
using App.Generic.BaseObject;
using App.Generic.Map;
using App.MVC;
using App.MVC.Controller;
using App.MVC.View.GameScene.Object;
using App.MVC.View.GameScene.Object.Carrot;
using App.MVC.View.GameScene.Object.Map;
using App.MVC.View.GameScene.Object.Tower;
using App.Static;
using App.Static.Enum;
using PureMVC.Interfaces;
using UnityEngine;
using UnityEngine.Events;
using XLua;

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

    // private MapData mapData;
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

    #region lua重写action

    public UnityAction<MapData> initAction;

    #endregion

    private void Awake()
    {
        spawnedComplete = false;
    }

    private void Update()
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

    public Carrot GetCarrot()
    {
        return carrot;
    }

    public List<IMonster> GetAllMonsters()
    {
        return monsters;
    }

    public void Init(MapData data)
    {
        if (initAction != null)
        {
            initAction(data);
            return;
        }

        // 转换地图数据
        pathList = PointClassToCell.ToCellList(data.pathList);
        waveDataList = data.waveDataList;
        obstacleList = PointClassToCell.ToCellList(data.obstacleList);

        CreateObstacles();
        CreateCarrot();
        CreateStartBrand();

        // 更新面板波数显示
        GameFacade.Instance.SendNotification(NotificationName.UIEvent.GAMEPANEL_UPDATE_WAVESCOUNT, (1, waveDataList.Count));
    }

    #region 集火相关

    public IMonster GetCollectingFiresTarget()
    {
        return collectingFiresTarget;
    }

    public void SetCollectingFiresTarget(IMonster monster)
    {
        collectingFiresTarget = monster;
    }

    public void CancelCollectingFiresTarget()
    {
        collectingFiresTarget = null;
        // 隐藏集火标志
        signTrans.gameObject.SetActive(false);
        signTrans.transform.SetParent(transform);
    }

    #endregion

    #region 出怪相关

    public void StartSpawn()
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

    public void PauseWaves()
    {
        isPaused = true;
        Debug.Log("Wave spawning paused.");
    }

    public void ResumeWaves()
    {
        isPaused = false;
        Debug.Log("Wave spawning resumed.");
    }

    private void StartNewWave()
    {
        isWaveInProgress = true;
        currentWaveMonsterIndex = 0;
        waveTimer = 0f;
        monsterSpawnTimer = 0f;
        Debug.Log("Starting wave " + (currentWaveIndex + 1));

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
        GameFacade.Instance.SendNotification(NotificationName.UIEvent.GAMEPANEL_UPDATE_WAVESCOUNT, (currentWaveIndex + 1, waveDataList.Count));
    }

    private void EndCurrentWave()
    {
        isWaveInProgress = false;
        currentWaveIndex++;
        Debug.Log("Wave " + currentWaveIndex + " ended.");
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
        Monster monster = GameManager.Instance.PoolManager.GetObject("Object/Monster/" + type).GetComponent<Monster>();
        monster.transform.SetParent(transform);
        monster.transform.localScale = Vector3.one;
        monster.transform.position = Map.GetCellCenterPos(pathList[0]);
        monster.data.maxHp *= hard;
        monster.Init(pathList);
        monsters.Add(monster);
    }

    #endregion

    #region 升级、出售

    /// <summary>
    /// 升级塔
    /// </summary>
    public void UpGradeTower(Vector3 cellWorldPos)
    {
        BaseTower tower = Map.GetCell(cellWorldPos).tower as BaseTower;
        if (!tower) return;

        // 最大等级直接返回
        if (tower.level == 2) return;

        // 够钱才升级
        if (GameManager.Instance.money < tower.data.prices[tower.level + 1]) return;
        // 扣钱
        GameFacade.Instance.SendNotification(NotificationName.Game.UPDATE_MONEY, -tower.data.prices[tower.level + 1]);
        // 调用更新方法
        tower.UpGrade();
        // 关闭建造面板
        GameFacade.Instance.SendNotification(NotificationName.UI.HIDE_BUILTPANEL);
    }

    /// <summary>
    /// 出售塔
    /// </summary>
    public void SellTower(Vector3 cellWorldPos)
    {
        Cell cell = Map.GetCell(cellWorldPos);
        BaseTower tower = cell.tower as BaseTower;
        if (!tower) return;

        // 加钱
        GameFacade.Instance.SendNotification(NotificationName.Game.UPDATE_MONEY, +tower.data.sellPrices[tower.level]);
        // 回收对象
        GameManager.Instance.PoolManager.PushObject(tower.gameObject);
        // 清空格子
        cell.tower = null;

        // 关闭建造面板
        GameFacade.Instance.SendNotification(NotificationName.UI.HIDE_BUILTPANEL);
        // 从列表移除
        towers.Remove(tower);
    }

    #endregion

    #region 创建对象

    public void SetCollectingFires(IMonster monster)
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
            Obstacle obstacle = GameManager.Instance.PoolManager.GetObject($"Object/Obstacle/{cell.obstacleName}").GetComponent<Obstacle>();
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
    public void CreateTowerObject(TowerData towerData, Vector3 cellWorldPos)
    {
        // 够钱才创建
        if (GameManager.Instance.money >= towerData.prices[0])
        {
            BaseTower tower = GameManager.Instance.PoolManager.GetObject(towerData.prefabsPath).GetComponent<BaseTower>();
            tower.transform.SetParent(transform);
            tower.transform.localScale = Vector3.one;
            tower.transform.position = cellWorldPos;
            // 扣钱
            GameFacade.Instance.SendNotification(NotificationName.Game.UPDATE_MONEY, -towerData.prices[0]);
            // 记录该格子已经存在塔
            Map.GetCell(cellWorldPos).tower = tower;

            // 关闭建造面板
            GameFacade.Instance.SendNotification(NotificationName.UI.HIDE_BUILTPANEL);
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
        carrot = GameManager.Instance.PoolManager.GetObject("Object/Carrot").GetComponent<Carrot>();
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
    public void OnPushAllGameObject()
    {
        // 销毁标志
        Destroy(signTrans.gameObject);

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
                GameManager.Instance.PoolManager.PushObject(monsters[i].Transform.gameObject);
            }
        }

        monsters.Clear();
    }

    private void OnPushAllTowers()
    {
        for (int i = 0; i < towers.Count; i++)
        {
            GameManager.Instance.PoolManager.PushObject(((MonoBehaviour)towers[i]).gameObject);
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
                GameManager.Instance.PoolManager.PushObject(o.gameObject);
            }
        }

        obstaclesList.Clear();
    }

    #endregion

    #region 外部调用

    public int GetNowWaveCount()
    {
        return currentWaveIndex;
    }

    #endregion

    #region 判断对象状态

    public bool WinJudge()
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