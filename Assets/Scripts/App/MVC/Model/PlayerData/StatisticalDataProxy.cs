using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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
        if (statisticalData == null)
        {
            LoadStatisticalData();
        }

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

#if UNITY_EDITOR_WIN
        statisticalData = BinaryManager.Instance.Load<StatisticalData>("StatisticalData.zy");
#endif
#if UNITY_ANDROID
        string path = Application.persistentDataPath + "/StatisticalData.zy";
        if (!File.Exists(path))
        {
            File.Create(path);
            statisticalData = new StatisticalData();
        }
        else
        {
            try
            {
                using (FileStream fileStream = File.Open(path, FileMode.Open, FileAccess.Read))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    statisticalData = formatter.Deserialize(fileStream) as StatisticalData;
                    fileStream.Close();
                }
            }
            catch
            {
                // correct the file
                File.Delete(path);
                File.Create(path);
                statisticalData = new StatisticalData();
            }
        }
#endif
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
#if UNITY_EDITOR_WIN
        BinaryManager.Instance.Save("StatisticalData.zy", statisticalData);
#endif
#if UNITY_ANDROID
        using (FileStream fileStream = File.Open(Application.persistentDataPath + "/StatisticalData.zy", FileMode.Open, FileAccess.Write))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fileStream, statisticalData);
            fileStream.Flush();
            fileStream.Close();
        }
#endif

    }
}