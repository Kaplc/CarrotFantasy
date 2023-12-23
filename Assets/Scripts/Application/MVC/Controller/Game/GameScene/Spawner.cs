using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 出怪脚本
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


    private void Awake()
    {
        spawnedComplete = false;

        // 获取当前关卡数据
        levelData = GameManager.Instance.nowLevelData;
        // 更新面板波数显示
        GameFacade.Instance.SendNotification(NotificationName.UPDATE_WAVESCOUNT, (1, levelData.roundDataList.Count));
    }

    public void CollectingFires(Monster monster)
    {
        for (int i = 0; i < towers.Count; i++)
        {
            towers[i].target = monster;
        }
    }

    /// <summary>
    /// 升级塔
    /// </summary>
    public void UpGradeTower(Vector3 cellWorldPos)
    {
        BaseTower tower = Map.GetCell(cellWorldPos).tower as BaseTower;
        if(!tower)return;
        
        // 最大等级直接返回
        if (tower.level == 2)return;

        // 够钱才升级
        if (GameManager.Instance.money < tower.data.prices[tower.level + 1])return;

        // 扣钱
        GameManager.Instance.money -= tower.data.prices[tower.level + 1];
        // 更新面板
        GameFacade.Instance.SendNotification(NotificationName.UPDATE_MONEY, GameManager.Instance.money);
        // 调用更新方法
        tower.UpGrade();
        // 关闭建造面板
        GameFacade.Instance.SendNotification(NotificationName.HIDE_BUILTPANEL);
    }

    /// <summary>
    /// 出售塔
    /// </summary>
    public void SellTower(Vector3 cellWorldPos)
    {
        Cell cell = Map.GetCell(cellWorldPos);
        BaseTower tower = cell.tower as BaseTower;
        if(!tower)return;
        
        // 加钱
        GameManager.Instance.money += tower.data.sellPrices[tower.level];
        // 更新面板
        GameFacade.Instance.SendNotification(NotificationName.UPDATE_MONEY, GameManager.Instance.money);
        // 回收对象
        GameManager.Instance.PoolManager.PushObject(tower.gameObject);
        // 清空格子
        cell.tower = null;

        // 关闭建造面板
        GameFacade.Instance.SendNotification(NotificationName.HIDE_BUILTPANEL);
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
            GameManager.Instance.money -= towerData.prices[0];
            // 更新面板
            GameFacade.Instance.SendNotification(NotificationName.UPDATE_MONEY, GameManager.Instance.money);
            // 记录该格子已经存在塔
            Map.GetCell(cellWorldPos).tower = tower;

            // 关闭建造面板
            GameFacade.Instance.SendNotification(NotificationName.HIDE_BUILTPANEL);
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
        // carrot.OnGet();

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
            GameFacade.Instance.SendNotification(NotificationName.UPDATE_WAVESCOUNT, (i + 1, levelData.roundDataList.Count));

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
                        yield return new WaitForSeconds(roundData.intervalTimeEach + lastSpawnTime - GameManager.Instance.PauseTime);
                    }

                    // 缓存池取出
                    Monster monster = GameManager.Instance.PoolManager.GetObject(groupData.monsterData.prefabsPath).GetComponent<Monster>();
                    // 保存出生的怪物
                    monsters.Add(monster);
                    // 记录时间
                    lastSpawnTime = Time.realtimeSinceStartup;
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
    /// 回收未死亡的怪物
    /// </summary>
    public void OnPushAllMonsters()
    {
        for (int i = 0; i < monsters.Count; i++)
        {
            if (monsters[i].isDead == false)
            {
                GameManager.Instance.PoolManager.PushObject(monsters[i].gameObject);
            }
        }

        monsters.Clear();
    }

    public void OnPushAllTowers()
    {
        for (int i = 0; i < towers.Count; i++)
        {
            GameManager.Instance.PoolManager.PushObject(towers[i].gameObject);
        }
    }
}