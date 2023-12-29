using System.Collections;
using System.Collections.Generic;
using System.IO;
using PureMVC.Patterns.Proxy;
using UnityEngine;

public class GameDataProxy : Proxy
{
    public new const string NAME = "GameDataProxy";
    
    private Dictionary<int, LevelData> loadedLevelsDataDic = new Dictionary<int, LevelData>(); // 已经加载过的关卡缓存
    private Dictionary<int, ItemData> loadedItemsDataDic = new Dictionary<int, ItemData>(); // 已经加载过的主题

    public GameDataProxy() : base(NAME)
    {
        // 加载大关卡数据
        LoadBigLevelData();
    }

    public void GetBigLevelData(int itemID)
    {
        if (loadedItemsDataDic.ContainsKey(itemID))
        {
            SendNotification(NotificationName.Data.LOADED_ITEMDATA, loadedItemsDataDic[itemID]);
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
            SendNotification(NotificationName.Data.LOADED_LEVELDATA, levelData);
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
                    levelData.mapData = GameManager.Instance.BinaryManager.Load<MapData>(DataPath.MAPDATA_PATH + $"{levelData.mapDataFileName}.md");
                    // 缓存已加载过的关卡
                    loadedLevelsDataDic.Add(levelData.levelID, levelData);
                    SendNotification(NotificationName.Data.LOADED_LEVELDATA, item.Value.levels[i]);
                    return;
                }
            }
        }
    }
}