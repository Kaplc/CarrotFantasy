﻿using System;
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
        RoundData roundData;

        for (int i = 0; i < levelData.roundDataList.Count; i++)
        {
            Debug.Log("下");
            roundData = levelData.roundDataList[i];
            for (int j = 0; j < roundData.waveCount; j++)
            {
                while (GameManager.Instance.isPause)
                {
                    yield return null;
                    // 取消暂停继续时间
                    if (!GameManager.Instance.isPause)
                    {
                        yield return new WaitForSeconds(roundData.intervalTimeEach + lastSpawnTime - GameManager.Instance.pauseTime); // 还应继续读多少秒才下一个
                        break;
                    }
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
                if (roundData.waveCount - 1 == j)
                {
                    // 
                    break;
                }

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