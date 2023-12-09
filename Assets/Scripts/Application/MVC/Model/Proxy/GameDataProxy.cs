using System.Collections;
using System.Collections.Generic;
using System.IO;
using PureMVC.Patterns.Proxy;
using UnityEngine;

public class GameDataProxy : Proxy
{
    public new const string NAME = "GameDataProxy";

    private StatisticalData statisticalData;
    private ProcessData processData;
    private MusicSettingData musicSettingData;

    private Dictionary<int, LevelData> levelsData = new Dictionary<int, LevelData>();
    private Dictionary<int, MonsterData> monstersData = new Dictionary<int, MonsterData>();
    private Dictionary<int, TowerData> towersData = new Dictionary<int, TowerData>();
    private Dictionary<int, BigLevelData> bigLevelsData = new Dictionary<int, BigLevelData>();

    public GameDataProxy() : base(NAME)
    {
    }

    /// <summary>
    /// 初始化加载必要的游戏数据
    /// </summary>
    public void LoadInitGameData()
    {
        LoadStatisticalData();
        LoadMusicSettingData();
        LoadProcessData();

        // 加载关卡的所有怪物信息
        LoadMonstersData();
        // 加载获该关卡所有塔数据
        LoadTowersData();
        // 加载大关卡数据
        LoadBigLevelData();
    }

    #region 游戏进程数据

    public void GetStatisticalData()
    {
        if (statisticalData != null)
        {
            SendNotification(NotificationName.LOADED_STATISTICALDATA, statisticalData);
        }
    }

    private void LoadStatisticalData()
    {
        if (statisticalData != null) return;

        statisticalData = BinaryManager.Instance.Load<StatisticalData>("StatisticalData.zy");
    }

    public void SaveStatisticalData(StatisticalData data)
    {
        BinaryManager.Instance.Save("StatisticalData.zy", data);
    }

    public void GetProcessData()
    {
        if (processData != null)
        {
            SendNotification(NotificationName.LOADED_PROCESSDATA, processData);
        }
    }

    private void LoadProcessData()
    {
        if (processData != null) return;

        processData = BinaryManager.Instance.Load<ProcessData>("ProcessData.zy");
    }

    public void SaveProcessData(ProcessData data)
    {
        BinaryManager.Instance.Save("ProcessData.zy", data);
    }

    #endregion

    #region 音乐数据

    public void GetMusicSettingData()
    {
        if (musicSettingData != null)
        {
            SendNotification(NotificationName.LOADED_MUSICSETTINGDATA, musicSettingData);
        }
    }

    /// <summary>
    /// 加载音乐设置数据
    /// </summary>
    private void LoadMusicSettingData()
    {
        if (musicSettingData != null) return;

        musicSettingData = BinaryManager.Instance.Load<MusicSettingData>("MusicSettingData.zy");
    }

    public void SaveMusicSettingData(MusicSettingData data)
    {
        BinaryManager.Instance.Save("MusicSettingData.zy", data);
    }

    #endregion

    #region 关卡数据

    public void GetBigLevelData(int id)
    {
        if (bigLevelsData.ContainsKey(id))
        {
            SendNotification(NotificationName.LOADED_BIGLEVELDATA, bigLevelsData[id]);
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
            bigLevelsData.Add(datas[i].id, datas[i]);
        }
    }

    /// <summary>
    /// 加载关卡的地图数据
    /// </summary>
    /// <param name="levelData">未加载地图数据的关卡数据</param>
    private void LoadMapData(LevelData levelData)
    {
        // 已经加载过直接返回
        if (levelsData.ContainsKey(levelData.levelId))
        {
            SendNotification(NotificationName.LOADED_LEVELDATA, levelsData[levelData.levelId]);
            return;
        }
        
        // 加载地图数据
        levelData.mapData = GameManager.Instance.BinaryManager.Load<MapData>(DataPath.MAPDATA_PATH + $"{levelData.mapDataPath}.md");
        levelsData.Add(levelData.levelId, levelData);

        // 带出指定的关卡数据
        SendNotification(NotificationName.LOADED_LEVELDATA, levelData);
    }
    
    /// <summary>
    /// 根据关卡id获取关卡数据
    /// </summary>
    /// <param name="id">关卡id</param>
    public void LoadLevelData(int id)
    {
        foreach (KeyValuePair<int,BigLevelData> item in bigLevelsData)
        {
            for (int i = 0; i < item.Value.levels.Count; i++)
            {
                if (id == item.Value.levels[i].levelId)
                {
                    // 加载地图数据
                    LoadMapData(item.Value.levels[i]);
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
            if (!monstersData.ContainsKey(data[i].id))
            {
                monstersData.Add(data[i].id, data[i]);
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
            if (!towersData.ContainsKey(data[i].id))
            {
                towersData.Add(data[i].id, data[i]);
            }
        }
    }

    #endregion
    
    /// <summary>
    /// 清空数据
    /// </summary>
    public void ClearData()
    {
        levelsData.Clear();
    }
}