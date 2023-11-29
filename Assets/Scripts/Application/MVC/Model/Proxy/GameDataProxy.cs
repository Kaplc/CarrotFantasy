using System.Collections;
using System.Collections.Generic;
using System.IO;
using PureMVC.Patterns.Proxy;
using UnityEngine;

public class GameDataProxy : Proxy
{
    public new const string NAME = "GameDataProxy";

    private Dictionary<int, LevelData> levelsData = new Dictionary<int, LevelData>();
    private Dictionary<int, MonsterData> monstersData = new Dictionary<int, MonsterData>();
    private Dictionary<int, TowerData> towersData = new Dictionary<int, TowerData>();

    public GameDataProxy() : base(NAME)
    {
    }

    /// <summary>
    /// 加载玩家当前进度数据
    /// </summary>
    public void LoadPlayerData()
    {
        PlayerData playerData = BinaryManager.Instance.Load<PlayerData>("PlayerData.zy");
        SendNotification(NotificationName.LOADED_PLAYERDATA, playerData);
    }

    public void SavePlayerData(PlayerData playerData)
    {
        BinaryManager.Instance.Save("PlayerData.zy", playerData);
    }

    /// <summary>
    /// 加载音乐设置数据
    /// </summary>
    public void LoadMusicData()
    {
        MusicSettingData musicSettingData = BinaryManager.Instance.Load<MusicSettingData>("MusicSettingData.zy");
        SendNotification(NotificationName.LOADED_MUSICSETTINGDATA, musicSettingData);
    }

    public void SaveMusicSetting(MusicSettingData musicSettingData)
    {
        BinaryManager.Instance.Save("MusicSettingData.zy", musicSettingData);
    }

    /// <summary>
    /// 加载大关卡数据
    /// </summary>
    /// <param name="bigLevelID">大关卡id</param>
    public void LoadBigLevelData(int bigLevelID)
    {
    }

    /// <summary>
    /// 获取小关卡数据
    /// </summary>
    /// <param name="levelId">关卡id</param>
    public void LoadLevelData(int levelID)
    {
        // 已经加载过直接返回
        if (levelsData.ContainsKey(levelID))
        {
            SendNotification(NotificationName.LOADED_LEVELDATA, new LevelDataBody()
            {
                levelData = levelsData[levelID],
                monstersData = monstersData,
                towersData = towersData
            });
            return;
        }

        // 加载LevelData
        LevelData levelData = Resources.Load<LevelData>(DataPath.LEVELRDATA_PATH + $"Level{levelID}Data");
        // 加载地图数据
        levelData.mapData = GameManager.Instance.BinaryManager.Load<MapData>(DataPath.MAPDATA_PATH + $"Level{levelID}MapData.md");
        levelsData.Add(levelID, levelData);
        // 加载关卡的所有怪物信息
        LoadMonstersData();
        // 加载获该关卡所有塔数据
        LoadTowersData();
        
        // 带出指定levelId的关卡数据和怪物数据
        SendNotification(NotificationName.LOADED_LEVELDATA, new LevelDataBody()
        {
            levelData = levelsData[levelID],
            monstersData = this.monstersData,
            towersData = this.towersData
        });
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

    /// <summary>
    /// 清空数据
    /// </summary>
    public void ClearData()
    {
        levelsData.Clear();
    }
}