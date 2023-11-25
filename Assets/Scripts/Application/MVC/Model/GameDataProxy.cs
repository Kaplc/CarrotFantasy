using System.Collections;
using System.Collections.Generic;
using PureMVC.Patterns.Proxy;
using UnityEngine;

public class GameDataProxy : Proxy
{
    public new const string NAME = "GameDataProxy";

    private Dictionary<int, LevelData> levelsData = new Dictionary<int, LevelData>();
    private Dictionary<int, MonsterData> monstersData = new Dictionary<int, MonsterData>();

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
                monstersData = monstersData
            });
            return;
        }

        // 加载LevelData
        LevelData levelData = Resources.Load<LevelData>(ProjectPath.LEVELRDATA_PATH + $"Level{levelID}Data");
        // 加载地图数据
        levelData.mapData = GameManager.Instance.BinaryManager.Load<MapData>(ProjectPath.MAPDATA_PATH + $"Level{levelID}MapData.md");
        levelsData.Add(levelID, levelData);
        // 加载关卡的所有怪物信息
        LoadMonstersData(levelData.monsterIds);
        
        // 带出指定levelId的关卡数据和怪物数据
        SendNotification(NotificationName.LOADED_LEVELDATA, new LevelDataBody()
        {
            levelData = levelsData[levelID],
            monstersData = monstersData
        });
    }
    
    /// <summary>
    /// 加载多个怪物数据
    /// </summary>
    /// <param name="monstersID"></param>
    public void LoadMonstersData(int[] monstersID)
    {
        for (int i = 0; i < monstersID.Length; i++)
        {
            if (!monstersData.ContainsKey(monstersID[i]))
            {
                monstersData.Add(monstersID[i], Resources.Load<MonsterData>(ProjectPath.MONSTERDATA_PATH + $"Monster{monstersID[i]}Data"));
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