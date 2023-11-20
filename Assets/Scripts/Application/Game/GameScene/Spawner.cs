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
        EventCenter.Instance.AddEventListener(NotificationName.START_SPAWN, StartSpawn);
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
                Debug.Log("出怪");
                // 每只间隔
                yield return new WaitForSeconds(roundData.intervalTimeEach);
            }
            
            // 等待每波间隔
            yield return new WaitForSeconds(levelData.intervalTimePerWave);
            Debug.Log("下一波");
        }
    }

    private void OnDestroy()
    {
        // 销毁时移除监听事件
        EventCenter.Instance.RemoveEventListener(NotificationName.START_SPAWN, StartSpawn);
    }
}
