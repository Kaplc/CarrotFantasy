using System.Collections;
using System.Collections.Generic;
using PureMVC.Patterns.Proxy;
using UnityEngine;

public class StatisticalDataProxy : Proxy
{
    public new const string NAME = "StatisticalDataProxy";

    private StatisticalData statisticalData;

    public StatisticalDataProxy() : base(NAME)
    {
        LoadStatisticalData();
    }

    public void GetStatisticalData()
    {
        // 复制数据
        StatisticalData newData = new StatisticalData()
        {
            bossMapCount = statisticalData.bossMapCount,
            adventureMapCount = statisticalData.adventureMapCount,
            destroyObstacleCount = statisticalData.destroyObstacleCount,
            hideMapCount = statisticalData.hideMapCount,
            killBossCount = statisticalData.killBossCount,
            killMonsterCount = statisticalData.killMonsterCount,
            money = statisticalData.money
        };

        SendNotification(NotificationName.Data.LOADED_STATISTICALDATA, newData);
    }

    private void LoadStatisticalData()
    {
        if (statisticalData != null) return;

        statisticalData = BinaryManager.Instance.Load<StatisticalData>("StatisticalData.zy");
    }

    public void ChangeBossMapCount(int num)
    {
        statisticalData.bossMapCount = num;
    }

    public void ChangeAdventureMapCount(int num)
    {
        statisticalData.adventureMapCount = num;
    }

    public void ChangeHideMapCount(int num)
    {
        statisticalData.hideMapCount = num;
    }

    public void ChangeDestroyObstacleCount(int num)
    {
        statisticalData.destroyObstacleCount += num;
    }

    public void ChangeKillBossCount(int num)
    {
        statisticalData.killBossCount += num;
    }

    public void ChangeKillMonsterCount(int num)
    {
        statisticalData.killMonsterCount += num;
    }

    public void ChangeMoneyCount(int num)
    {
        statisticalData.money += num;
    }

    public void SaveStatisticalData()
    {
        BinaryManager.Instance.Save("StatisticalData.zy", statisticalData);
    }
}