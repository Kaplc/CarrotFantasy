using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 出怪脚本
/// </summary>
public class Spawner : MonoBehaviour
{
    private bool spawnedComplete; // 完成出怪
    public float lastSpawnTime; // 上一只出怪时间

    public Carrot carrot; // 萝卜
    public Transform startPoint; // 开始路牌位置
    private LevelData levelData;
    private Coroutine spawnCoroutine;
    public List<Monster> monsters = new List<Monster>(); // 已经出生的怪物


    private void Awake()
    {
        spawnedComplete = false;

        // 获取当前关卡数据
        levelData = GameManager.Instance.nowLevelData;
        // 监听开始出怪事件
        GameManager.Instance.EventCenter.AddEventListener(NotificationName.START_SPAWN, StartSpawn); // 开始出怪
        GameManager.Instance.EventCenter.AddEventListener(NotificationName.GAME_OVER, StopSpawn); // 监听萝卜死亡停止出怪
        GameManager.Instance.EventCenter.AddEventListener(NotificationName.MONSTER_DEAD, CheckMonstersSurvival); // 检查怪物存活情况
    }
    
    /// <summary>
    /// 升级塔
    /// </summary>
    public void UpGradeTower(Vector3 cellWorldPos)
    {
        BaseTower tower = Map.GetCell(cellWorldPos).tower as BaseTower;
        // 最大等级直接返回
        if (tower.level == 2)
        {
            return;
        }
        
        tower.UpGrade();
        // 扣钱
        GameManager.Instance.money -= tower.data.prices[tower.level];
        
        // 关闭建造面板
        GameFacade.Instance.SendNotification(NotificationName.HIDE_BUILTPANEL);
        GameFacade.Instance.SendNotification(NotificationName.ALLOW_CLICKCELL, true);
    }
    
    /// <summary>
    /// 出售塔
    /// </summary>
    public void SellTower(Vector3 cellWorldPos)
    {
        Cell cell = Map.GetCell(cellWorldPos);
        BaseTower tower = cell.tower as BaseTower;
        
        // 加钱
        GameManager.Instance.money += tower.data.sellPrices[tower.level];
        // 回收对象
        tower.OnPush();
        // 清空格子
        cell.tower = null;
        
        // 关闭建造面板
        GameFacade.Instance.SendNotification(NotificationName.HIDE_BUILTPANEL);
        GameFacade.Instance.SendNotification(NotificationName.ALLOW_CLICKCELL, true);
    }
    
    /// <summary>
    /// 创建塔对象
    /// </summary>
    /// <param name="towerID">塔id</param>
    /// <param name="cellWorldPos">创建的位置世界坐标</param>
    public void CreateTowerObject(int towerID, Vector3 cellWorldPos)
    {
        TowerData towerData = GameManager.Instance.towersData[towerID];
        
        // 够钱才创建
        if (GameManager.Instance.money >= towerData.prices[0])
        {
            BaseTower tower = GameManager.Instance.PoolManager.GetObject(towerData.prefabsPath).GetComponent<BaseTower>();
            tower.OnGet();
            tower.transform.position = cellWorldPos;
            
            // 扣钱
            GameManager.Instance.money -= towerData.prices[0];
            // 记录该格子已经存在塔
            Map.GetCell(cellWorldPos).tower = tower;
            
            // 关闭建造面板
            GameFacade.Instance.SendNotification(NotificationName.HIDE_BUILTPANEL);
            // 建造成功允许检测格子
            GameFacade.Instance.SendNotification(NotificationName.ALLOW_CLICKCELL, true);
        }
    }
    
    /// <summary>
    /// 创建萝卜
    /// </summary>
    public void CreateCarrot()
    {
        carrot = GameManager.Instance.PoolManager.GetObject("Object/Carrot").GetComponent<Carrot>();
        carrot.OnGet();
        
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
    /// 检查怪物存活
    /// </summary>
    private void CheckMonstersSurvival()
    {
        // 未完成出怪
        if (!spawnedComplete) return;

        for (int i = 0; i < monsters.Count; i++)
        {
            // 有一个没死亡都无效
            if (monsters[i].isDead == false)
            {
                return;
            }
        }

        // 全部死亡判断是否胜利
        GameManager.Instance.EventCenter.TriggerEvent(NotificationName.JUDGING_WIN);
    }

    /// <summary>
    /// 开启出怪协程
    /// </summary>
    private void StartSpawn()
    {
        spawnCoroutine = StartCoroutine(SpawnCoroutine());
        
    }

    private void StopSpawn()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
    }

    private IEnumerator SpawnCoroutine()
    {
        RoundData roundData;

        for (int i = 0; i < levelData.roundDataList.Count; i++)
        {
            roundData = levelData.roundDataList[i];
            for (int j = 0; j < roundData.waveCount; j++)
            {
                if (GameManager.Instance.Pause)
                {
                    while (GameManager.Instance.Pause)
                    {
                        yield return null;
                    }
                    
                    // 取消暂停继续时间
                    yield return new WaitForSeconds(roundData.intervalTimeEach + lastSpawnTime - GameManager.Instance.PauseTime); // 还应继续读多少秒才下一个
                }
                
                string prefabsPath = GameManager.Instance.monstersData[roundData.monsterId].prefabsPath;
                // 缓存池取出
                Monster monster = GameManager.Instance.PoolManager.GetObject(prefabsPath).GetComponent<Monster>();
                monster.OnGet(); // 取出时执行还原方法
                // 保存出生的怪物
                monsters.Add(monster);
                // 记录时间
                lastSpawnTime = Time.time;
                // 当前波最后一个怪跳过每只间隔读秒
                if (roundData.waveCount - 1 == j) break;

                // 每只间隔
                yield return new WaitForSeconds(roundData.intervalTimeEach);
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
    public void OnPushAllMonster()
    {
        for (int i = 0; i < monsters.Count; i++)
        {
            if (monsters[i].isDead == false)
            {
                monsters[i].OnPush();
            }
        }
    }
}