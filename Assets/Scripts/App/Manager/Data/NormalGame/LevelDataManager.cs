using System.Collections.Generic;
using App.DataClass.Game.Level;
using App.Manager.Data.NormalGame;
using App.Static;
using UnityEngine;

namespace App.MVC.Model.GameData
{
    public class LevelDataManager : ILevelDataManager
    {
        private readonly Dictionary<int, LevelData> loadedLevelsDataDic = new Dictionary<int, LevelData>(); // 已经加载过的关卡缓存
        private readonly Dictionary<int, ItemData> loadedItemsDataDic = new Dictionary<int, ItemData>(); // 已经加载过的主题
        
        public void GetBigLevelData(int itemID)
        {
            if (loadedItemsDataDic.TryGetValue(itemID, out var value))
            {
                GameFacade.Instance.SendNotification(NotificationName.Data.LOADED_ITEMDATA, value);
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
                GameFacade.Instance.SendNotification(NotificationName.Data.LOADED_LEVELDATA, levelData);
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
#if UNITY_EDITOR_WIN
                        // levelData.mapData = GameManager.Instance.BinaryManager.Load<MapData>(DataPath.MAPDATA_PATH + $"{levelData.mapDataFileName}.md");
#endif
#if UNITY_ANDROID
                        // android load from streamingAssets
                        // UnityWebRequest request =
                        //     UnityWebRequest.Get(Application.streamingAssetsPath + "/" + DataPath.MAPDATA_PATH + $"{levelData.mapDataFileName}.md");
                        // request.SendWebRequest();
                        // // wait for request
                        // while (!request.isDone)
                        // {
                        //
                        // }
                        // // get bytes
                        // byte[] bytes = request.downloadHandler.data;
                        // // deserialize
                        // BinaryFormatter formatter = new BinaryFormatter();
                        // levelData.mapData = formatter.Deserialize(new MemoryStream(bytes)) as MapData;
#endif
                        // 缓存已加载过的关卡
                        loadedLevelsDataDic.Add(levelData.levelID, levelData);
                        GameFacade.Instance.SendNotification(NotificationName.Data.LOADED_LEVELDATA, item.Value.levels[i]);
                        return;
                    }
                }
            }
        }
    }
}