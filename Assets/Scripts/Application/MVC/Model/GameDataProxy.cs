using System.Collections;
using System.Collections.Generic;
using PureMVC.Patterns.Proxy;
using UnityEngine;

public class GameDataProxy : Proxy
{
    public new const string NAME = "GameDataProxy";

    private Dictionary<int, LevelData> nowBigLevelData = new Dictionary<int, LevelData>();
    private Dictionary<int, MonsterData> monsterData = new Dictionary<int, MonsterData>();

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
    /// 获取关卡数据
    /// </summary>
    /// <param name="levelId">关卡id</param>
    public void LoadLevelData(int bigLevelId, int levelId)
    {
        // 已经加载过直接返回
        if (nowBigLevelData.ContainsKey(levelId))
        {
            SendNotification(NotificationName.LOADED_LEVELDATA, new LevelDataBody() { levelData = nowBigLevelData[levelId], monsterData = this.monsterData });
            return;
        }
        
        BigLevelData bigLevelData = Resources.Load<BigLevelData>(ProjectPath.LEVELRDATA_PATH + $"BigLevel{bigLevelId}Data");
        // 加载所有小关卡数据
        for (int i = 0; i < bigLevelData.levelIds.Count; i++)
        {
            // 加载LevelData
            LevelData levelData = Resources.Load<LevelData>(ProjectPath.LEVELRDATA_PATH + $"Level{bigLevelData.levelIds[i]}Data");
            levelData.mapData =
                GameManager.Instance.BinaryManager.Load<MapData>(ProjectPath.MAPDATA_PATH + $"Level{bigLevelData.levelIds[i]}MapData.md");
            nowBigLevelData.Add(bigLevelData.levelIds[i], levelData);
            // 加载该大关卡的所有怪物信息
            for (int j = 0; j < levelData.monsterIds.Count; j++)
            {
                if (!monsterData.ContainsKey(j))
                {
                    monsterData.Add(j, Resources.Load<MonsterData>(ProjectPath.MONSTERDATA_PATH+$"Monster{j}Data"));
                }
            }
        }

        // 带出指定levelId的关卡数据
        SendNotification(NotificationName.LOADED_LEVELDATA,
            new LevelDataBody() { levelData = nowBigLevelData[levelId], monsterData = this.monsterData });
    }

    /// <summary>
    /// 清空数据
    /// </summary>
    public void ClearData()
    {
        nowBigLevelData.Clear();
    }
}