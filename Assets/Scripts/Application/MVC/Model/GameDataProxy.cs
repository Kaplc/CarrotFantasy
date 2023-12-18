using System.Collections;
using System.Collections.Generic;
using System.IO;
using PureMVC.Patterns.Proxy;
using UnityEngine;

public class GameDataProxy : Proxy
{
    public new const string NAME = "GameDataProxy";

    private PlayerData playerData;

    private Dictionary<int, LevelData> loadedLevelsDataDic = new Dictionary<int, LevelData>(); // 已经加载过的关卡缓存
    private Dictionary<int, ItemData> loadedItemsDataDic = new Dictionary<int, ItemData>(); // 已经加载过的主题

    public GameDataProxy() : base(NAME)
    {
    }

    /// <summary>
    /// 初始化加载必要的游戏数据
    /// </summary>
    public void LoadInitGameData()
    {
        LoadPlayerData();
        // 加载大关卡数据
        LoadBigLevelData();
    }

    #region 游戏数据

    private void LoadPlayerData()
    {
        playerData = new PlayerData();
        LoadStatisticalData();
        LoadMusicSettingData();
        LoadProcessData();
    }

    public void GetStatisticalData()
    {
        if (playerData != null)
        {
            SendNotification(NotificationName.LOADED_STATISTICALDATA, playerData.statisticalData);
        }
    }

    private void LoadStatisticalData()
    {
        if (playerData.statisticalData != null) return;

        playerData.statisticalData = BinaryManager.Instance.Load<StatisticalData>("StatisticalData.zy");
    }

    public void SaveStatisticalData(StatisticalData data)
    {
        BinaryManager.Instance.Save("StatisticalData.zy", data);
    }

    public void GetProcessData()
    {
        if (playerData.processData != null)
        {
            SendNotification(NotificationName.LOADED_PROCESSDATA, playerData.processData);
        }
    }

    private void LoadProcessData()
    {
        if (playerData.processData != null) return;

        playerData.processData = BinaryManager.Instance.Load<ProcessData>("ProcessData.zy");
        CalPassedLevelCount();
    }
    
    /// <summary>
    /// 计算通关关卡数
    /// </summary>
    private void CalPassedLevelCount()
    {
        // 遍历每个主题
        foreach (var passedBigLevelItem in playerData.processData.passedItemsDic)
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

    public void SaveProcessData((int levelID, EPassedGrade garde) data)
    {
        // 获取BigLevelID
        int bigLevelID = loadedLevelsDataDic[data.levelID].itemID;
        // 缓存的通关数据
        PassedLevelData passedLevelData = playerData.processData.passedItemsDic[bigLevelID];
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
        BinaryManager.Instance.Save("ProcessData.zy", playerData.processData);
    }

    #endregion

    #region 音乐数据

    public void GetMusicSettingData()
    {
        if (playerData.musicSettingData != null)
        {
            SendNotification(NotificationName.LOADED_MUSICSETTINGDATA, playerData.musicSettingData);
        }
    }

    /// <summary>
    /// 加载音乐设置数据
    /// </summary>
    private void LoadMusicSettingData()
    {
        if (playerData.musicSettingData != null) return;

        playerData.musicSettingData = BinaryManager.Instance.Load<MusicSettingData>("MusicSettingData.zy");
    }

    public void SaveMusicSettingData(MusicSettingData data)
    {
        BinaryManager.Instance.Save("MusicSettingData.zy", data);
    }

    #endregion

    #region 关卡数据

    public void GetBigLevelData(int id)
    {
        if (loadedItemsDataDic.ContainsKey(id))
        {
            SendNotification(NotificationName.LOADED_ITEMDATA, loadedItemsDataDic[id]);
        }
    }

    /// <summary>
    /// 加载所有大关卡数据
    /// </summary>
    private void LoadBigLevelData()
    {
        ItemData[] datas = Resources.LoadAll<ItemData>(DataPath.LEVELRDATA_PATH);
        for (int i = 0; i < datas.Length; i++)
        {
            loadedItemsDataDic.Add(datas[i].id, datas[i]);
        }
    }

    /// <summary>
    /// 根据关卡id获取关卡数据
    /// </summary>
    public void LoadLevelData(int levelID)
    {
        LevelData levelData;
        // 已经加载过直接返回
        if (loadedLevelsDataDic.TryGetValue(levelID, out levelData))
        {
            SendNotification(NotificationName.LOADED_LEVELDATA, levelData);
            return;
        }

        // 遍历所有大关卡数据
        foreach (KeyValuePair<int, ItemData> item in loadedItemsDataDic)
        {
            for (int i = 0; i < item.Value.levels.Count; i++)
            {
                if (levelID == item.Value.levels[i].levelID)
                {
                    levelData = item.Value.levels[i];
                    // 加载地图数据
                    levelData.mapData = GameManager.Instance.BinaryManager.Load<MapData>(DataPath.MAPDATA_PATH + $"{levelData.mapDataPath}.md");
                    // 缓存已加载过的关卡
                    loadedLevelsDataDic.Add(levelData.levelID, levelData);
                    SendNotification(NotificationName.LOADED_LEVELDATA, item.Value.levels[i]);
                    return;
                }
            }
        }
    }

    #endregion
}