using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对象生成器
/// </summary>
public class Spawner : MonoBehaviour
{
    public bool spawnedComplete; // 完成出怪
    public float lastSpawnTime; // 上一只出怪时间
    public int nowWavesCount; // 当前第几波

    public Carrot carrot; // 萝卜
    public Transform startPoint; // 开始路牌位置
    private LevelData levelData;
    private Coroutine spawnCoroutine;
    public List<Monster> monsters = new List<Monster>(); // 已经出生的怪物
    public List<BaseTower> towers = new List<BaseTower>(); // 已创建的塔
    public List<Obstacle> obstaclesList = new List<Obstacle>(); // 已创建的障碍物

    public Monster collectingFiresTarget; // 集火目标
    public Transform signTrans; // 集火标志

    private void Awake()
    {
        spawnedComplete = false;

        // 获取当前关卡数据
        levelData = GameManager.Instance.nowLevelData;
        // 更新面板波数显示
        GameFacade.Instance.SendNotification(NotificationName.UIEvent.GAMEPANEL_UPDATE_WAVESCOUNT, (1, levelData.roundDataList.Count));
    }
    
    /// <summary>
    /// 根据保存的地图数据生成障碍物
    /// </summary>
    public void CreateObstacles()
    {
        MapData nowMapData = GameManager.Instance.nowLevelData.mapData;
        
        for (int i = 0; i < nowMapData.obstacleList.Count; i++)
        {
            Cell cell = nowMapData.obstacleList[i];

            // 创建实例
            Obstacle obstacle = GameManager.Instance.PoolManager.GetObject($"Object/Obstacle/{cell.obstacleName}").GetComponent<Obstacle>();
            obstacle.transform.position = Map.GetCellCenterPos(cell);
            cell.obstacle = obstacle.gameObject;
            obstaclesList.Add(obstacle);
        }
    }

    public void SetCollectingFires(Monster monster)
    {
        for (int i = 0; i < towers.Count; i++)
        {
            towers[i].target = monster;
        }

        // 设置集火标志
        collectingFiresTarget = monster;
        signTrans.gameObject.SetActive(true);
        signTrans.SetParent(monster.signFather.transform);
        signTrans.localPosition = Vector3.zero;
        signTrans.localScale = Vector3.one;
    }

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
    public void CreateCarrot()
    {
        carrot = GameManager.Instance.PoolManager.GetObject("Object/Carrot").GetComponent<Carrot>();

        // 设置萝卜位置
        Cell lastPathCell = levelData.mapData.pathList[levelData.mapData.pathList.Count - 1];
        carrot.transform.position = Map.GetCellCenterPos(lastPathCell);
    }

    /// <summary>
    /// 创建开始路牌
    /// </summary>
    public void CreateStartBrand()
    {
        startPoint = Instantiate(Resources.Load<GameObject>("Object/StartPoint")).GetComponent<Transform>();

        // 设置开始路牌位置
        Cell firstPathCell = levelData.mapData.pathList[0];
        startPoint.position = Map.GetCellCenterPos(firstPathCell);
    }

    /// <summary>
    /// 开启出怪协程
    /// </summary>
    public void StartSpawn()
    {
        spawnCoroutine = StartCoroutine(SpawnCoroutine());
    }

    public void StopSpawn()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
    }

    private IEnumerator SpawnCoroutine()
    {
        nowWavesCount = 0;
        for (int i = 0; i < levelData.roundDataList.Count; i++)
        {
            nowWavesCount++;
            // 更新面板波数显示
            GameFacade.Instance.SendNotification(NotificationName.UIEvent.GAMEPANEL_UPDATE_WAVESCOUNT, (i + 1, levelData.roundDataList.Count));

            RoundData roundData = levelData.roundDataList[i];

            // 创建每组怪
            for (int j = 0; j < roundData.group.Count; j++)
            {
                GroupData groupData = roundData.group[j];
                for (int k = 0; k < groupData.count; k++)
                {
                    // 实现暂停
                    if (GameManager.Instance.Pause)
                    {
                        yield return new WaitWhile(() => GameManager.Instance.Pause);
                        yield return new WaitForSeconds(roundData.intervalTimeEach + lastSpawnTime - GameManager.Instance.pauseTime);
                    }

                    // 缓存池取出
                    Monster monster = GameManager.Instance.PoolManager.GetObject(groupData.monsterData.prefabsPath).GetComponent<Monster>();
                    // 赋值成长系数
                    monster.Growth = roundData.growth;
                    // 保存出生的怪物
                    monsters.Add(monster);
                    // 记录时间
                    lastSpawnTime = Time.time;
                    // 最后一组怪最后一只跳过等待
                    if (!(j == roundData.group.Count - 1 && k == groupData.count - 1))
                    {
                        // 每只间隔
                        yield return new WaitForSeconds(roundData.intervalTimeEach);
                    }
                }
            }

            // 下一波前直接判断还有无下一波怪物
            if (levelData.roundDataList.Count - 1 == i)
            {
                spawnedComplete = true;
            }

            // 等待每波间隔
            yield return new WaitForSeconds(levelData.intervalTimePerWave);
        }
    }
    
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
            if (!monsters[i].isDead)
            {
                GameManager.Instance.PoolManager.PushObject(monsters[i].gameObject);
            }
        }

        monsters.Clear();
    }

    private void OnPushAllTowers()
    {
        for (int i = 0; i < towers.Count; i++)
        {
            GameManager.Instance.PoolManager.PushObject(towers[i].gameObject);
        }
        
        towers.Clear();
    }
    
    private void OnPushAllObstacles()
    {
        for (int i = 0; i < obstaclesList.Count; i++)
        {
            if (!obstaclesList[i].isDead)
            {
                GameManager.Instance.PoolManager.PushObject(obstaclesList[i].gameObject);
            }
        }
        
        obstaclesList.Clear();
    }
}