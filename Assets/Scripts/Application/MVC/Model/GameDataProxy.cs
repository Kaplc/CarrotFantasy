using System.Collections;
using System.Collections.Generic;
using System.IO;
using PureMVC.Patterns.Proxy;
using UnityEngine;

public class GameDataProxy : Proxy
{
    public new const string NAME = "GameDataProxy";

    private PlayerData playerData;

    private Dictionary<int, LevelData> loadedLevelsData = new Dictionary<int, LevelData>(); // 已经加载过的关卡缓存
    private Dictionary<int, MonsterData> loadedMonstersData = new Dictionary<int, MonsterData>();
    private Dictionary<int, TowerData> loadedTowersData = new Dictionary<int, TowerData>();
    private Dictionary<int, BigLevelData> loadedBigLevelsData = new Dictionary<int, BigLevelData>(); // 已经加载过的主题

    public GameDataProxy() : base(NAME)
    {
    }

    /// <summary>
    /// 初始化加载必要的游戏数据
    /// </summary>
    public void LoadInitGameData()
    {
        LoadPlayerData();

        // 加载关卡的所有怪物信息
        LoadMonstersData();
        // 加载获该关卡所有塔数据
        LoadTowersData();
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
    }

    public void SaveProcessData((int levelID, EPassedGrade garde) data)
    {
        foreach (var item in playerData.processData.passedBigLevelsDic)
        {
            // 存在已经解锁的关卡更新通关等级
            EPassedGrade grade =item.Value.passedLevelDic[data.levelID];
            // 仅刷新最高记录
            if ((int)data.garde > (int)grade)
            {
                item.Value.passedLevelDic[data.levelID] = data.garde;
            }

            // 判断下一关是否解锁
            if (!item.Value.passedLevelDic.ContainsKey(data.levelID + 1))
            {
                // 未解锁下一关则解锁
                item.Value.passedLevelDic[data.levelID + 1] = EPassedGrade.None;
            }
        }

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
        if (loadedBigLevelsData.ContainsKey(id))
        {
            SendNotification(NotificationName.LOADED_BIGLEVELDATA, loadedBigLevelsData[id]);
        }
    }

    /// <summary>
    /// 加载大关卡数据
    /// </summary>
    /// <param name="bigLevelID">大关卡id</param>
    private void LoadBigLevelData()
    {
        BigLevelData[] datas = Resources.LoadAll<BigLevelData>(DataPath.LEVELRDATA_PATH);
        for (int i = 0; i < datas.Length; i++)
        {
            loadedBigLevelsData.Add(datas[i].id, datas[i]);
        }
    }

    /// <summary>
    /// 根据关卡id获取关卡数据
    /// </summary>
    /// <param name="id">关卡id</param>
    public void LoadLevelData(int levelID)
    {
        // 已经加载过直接返回
        if (loadedLevelsData.ContainsKey(levelID))
        {
            SendNotification(NotificationName.LOADED_LEVELDATA, loadedLevelsData[levelID]);
            return;
        }

        // 遍历所有大关卡数据
        foreach (KeyValuePair<int, BigLevelData> item in loadedBigLevelsData)
        {
            for (int i = 0; i < item.Value.levels.Count; i++)
            {
                if (levelID == item.Value.levels[i].levelId)
                {
                    LevelData levelData = item.Value.levels[i];
                    // 加载地图数据
                    levelData.mapData = GameManager.Instance.BinaryManager.Load<MapData>(DataPath.MAPDATA_PATH + $"{levelData.mapDataPath}.md");
                    // 缓存已加载过的关卡
                    loadedLevelsData.Add(levelData.levelId, levelData);
                    SendNotification(NotificationName.LOADED_LEVELDATA, item.Value.levels[i]);
                    return;
                }
            }
        }
    }

    /// <summary>
    /// 加载该关卡所有怪物数据
    /// </summary>
    private void LoadMonstersData()
    {
        MonsterData[] data = Resources.LoadAll<MonsterData>(DataPath.MONSTERDATA_PATH);

        for (int i = 0; i < data.Length; i++)
        {
            if (!loadedMonstersData.ContainsKey(data[i].id))
            {
                loadedMonstersData.Add(data[i].id, data[i]);
            }
        }
    }

    /// <summary>
    /// 加载获该关卡所有塔数据
    /// </summary>
    private void LoadTowersData()
    {
        TowerData[] data = Resources.LoadAll<TowerData>(DataPath.TOWERDATA_PATH);

        for (int i = 0; i < data.Length; i++)
        {
            if (!loadedTowersData.ContainsKey(data[i].id))
            {
                loadedTowersData.Add(data[i].id, data[i]);
            }
        }
    }

    #endregion

    /// <summary>
    /// 清空数据
    /// </summary>
    public void ClearData()
    {
        loadedLevelsData.Clear();
    }
}