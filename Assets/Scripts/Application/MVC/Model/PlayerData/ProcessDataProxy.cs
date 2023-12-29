using System.Collections;
using System.Collections.Generic;
using PureMVC.Patterns.Proxy;
using UnityEngine;

public class ProcessDataProxy : Proxy
{
    public new const string NAME = "ProcessDataProxy";

    private ProcessData processData;

    public ProcessDataProxy() : base(NAME)
    {
        LoadProcessData();
    }

    public void GetProcessData()
    {
        if(processData is null)return;

        ProcessData newData = new ProcessData()
        {
            passedItemsDic = new Dictionary<int, PassedLevelData>(processData.passedItemsDic)
        };
        
        SendNotification(NotificationName.Data.LOADED_PROCESSDATA, newData);
    }

    private void LoadProcessData()
    {
        if (processData != null) return;
            
        processData = BinaryManager.Instance.Load<ProcessData>("ProcessData.zy");
        CalPassedLevelCount();
    }

    /// <summary>
    /// 计算通关关卡数
    /// </summary>
    private void CalPassedLevelCount()
    {
        // 遍历每个主题
        foreach (var passedBigLevelItem in processData.passedItemsDic)
        {
            // 计数
            int count = 0;
            // 遍历每个主题下的小关卡
            foreach (var passedLevelItem in passedBigLevelItem.Value.passedLevelDic)
            {
                if (passedLevelItem.Value != EPassedGrade.None)
                {
                    count++;
                }
            }

            passedBigLevelItem.Value.passedLevelCount = count;
        }
    }

    public void SaveProcessData((int itemID, int levelID, EPassedGrade garde) data)
    {
        // 缓存的通关数据
        PassedLevelData passedLevelData = processData.passedItemsDic[data.itemID];
        // 存在已经解锁的关卡更新通关等级
        EPassedGrade grade = passedLevelData.passedLevelDic[data.levelID];
        // 仅刷新最高记录
        if ((int)data.garde > (int)grade)
        {
            passedLevelData.passedLevelDic[data.levelID] = data.garde;
        }

        // 判断下一关是否解锁
        if (!passedLevelData.passedLevelDic.ContainsKey(data.levelID + 1))
        {
            // 未解锁下一关则解锁
            passedLevelData.passedLevelDic[data.levelID + 1] = EPassedGrade.None;
        }

        CalPassedLevelCount();

        // 数据持久化
        BinaryManager.Instance.Save("ProcessData.zy", processData);
    }
}