using System.Collections;
using System.Collections.Generic;
using PureMVC.Patterns.Proxy;
using UnityEngine;

public class GameDataProxy : Proxy
{
    public new const string NAME = "GameDataProxy";

    public Dictionary<int, MapData> nowBigLevelData = new Dictionary<int, MapData>();

    public GameDataProxy() : base(NAME)
    {
    }

    /// <summary>
    /// 加载玩家当前进度数据
    /// </summary>
    public void LoadPlayerData()
    {
    }

    /// <summary>
    /// 加载音乐设置数据
    /// </summary>
    public void LoadMusicData()
    {
    }

    /// <summary>
    /// 获取关卡数据
    /// </summary>
    /// <param name="levelId">关卡id</param>
    public void LoadLevelData(int bigLevelId, int levelId)
    {
        BigLevelData bigLevelData = Resources.Load<BigLevelData>($"Data/BigLevel{bigLevelId}");
        // 加载所有小关卡数据
        for (int i = 0; i < bigLevelData.levelIds.Count; i++)
        {
            MapData mapData = BinaryManager.Instance.Load<MapData>("Level/" + $"Level{bigLevelData.levelIds[i]}");
            nowBigLevelData.Add(bigLevelData.levelIds[i], mapData);
        }
        
        SendNotification(NotificationName.LOADED_LEVELDATA, nowBigLevelData);
    }

    /// <summary>
    /// 清空数据
    /// </summary>
    public void ClearData()
    {
    }
}