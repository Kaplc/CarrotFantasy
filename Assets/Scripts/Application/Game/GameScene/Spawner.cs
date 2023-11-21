using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 出怪脚本
/// </summary>
public class Spawner : MonoBehaviour
{
    private LevelData levelData;
    private Coroutine spawnCoroutine;

    private void Awake()
    {
        // 获取当前关卡数据
        levelData = GameManager.Instance.nowLevelData;
        // 监听开始出怪事件
        GameManager.Instance.EventCenter.AddEventListener(NotificationName.START_SPAWN, StartSpawn);
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
        StopCoroutine(spawnCoroutine);
    }

    private IEnumerator SpawnCoroutine()
    {
        RoundData roundData;
        
        for (int i = 0; i < levelData.roundDataList.Count; i++)
        {
            roundData = levelData.roundDataList[i];
            for (int j = 0; j < roundData.waveCount; j++)
            {
                string prefabsPath = GameManager.Instance.monsterData[roundData.monsterId].prefabsPath;
                // 缓存池取出
                Monster monster = GameManager.Instance.PoolManager.GetObject(prefabsPath).GetComponent<Monster>();
                monster.OnGet(); // 取出时执行还原方法
                // 每只间隔
                yield return new WaitForSeconds(roundData.intervalTimeEach);
            }
            
            // 等待每波间隔
            yield return new WaitForSeconds(levelData.intervalTimePerWave);

        }
    }

    private void OnDestroy()
    {
        // 销毁时移除监听事件
        GameManager.Instance.EventCenter.RemoveEventListener(NotificationName.START_SPAWN, StartSpawn);
        // 停止协程
        StopCoroutine(spawnCoroutine);
        spawnCoroutine = null;
    }
}
