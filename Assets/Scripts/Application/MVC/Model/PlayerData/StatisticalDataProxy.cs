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
            destroyItemCount = statisticalData.destroyItemCount,
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

    public void SaveStatisticalData(StatisticalData data)
    {
        statisticalData.bossMapCount = data.bossMapCount;
        statisticalData.adventureMapCount = data.adventureMapCount;
        statisticalData.money = data.money;
        statisticalData.killBossCount = data.killBossCount;
        statisticalData.hideMapCount = data.hideMapCount;
        statisticalData.destroyItemCount = data.destroyItemCount;
        statisticalData.killMonsterCount = data.killMonsterCount;
        
        BinaryManager.Instance.Save("StatisticalData.zy", data);
    }
}