using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using App.DataClass.Player;
using App.Static;
using Library;
using PureMVC.Patterns.Proxy;
using UnityEngine;

namespace App.MVC.Model.PlayerData
{
    public class StatisticalDataManager : IStatisticalDataManager
    {
        private StatisticalData statisticalData;

        public int BossMapCount
        {
            set => statisticalData.bossMapCount = value;
        }

        public int AdventureMapCount
        {
            set => statisticalData.adventureMapCount = value;
        }

        public int DestroyObstacleCount
        {
            set => statisticalData.destroyObstacleCount = value;
        }

        public int HideMapCount
        {
            set => statisticalData.hideMapCount = value;
        }

        public int KillBossCount
        {
            set => statisticalData.killBossCount = value;
        }

        public int KillMonsterCount
        {
            set => statisticalData.killMonsterCount = value;
        }

        public int Money
        {
            set => statisticalData.money = value;
        }

        public StatisticalData GetStatisticalData()
        {
            if (statisticalData == null)
            {
                LoadStatisticalData();
            }

            // 复制数据
            StatisticalData newData = new StatisticalData()
            {
                bossMapCount = statisticalData.bossMapCount,
                adventureMapCount = statisticalData.adventureMapCount,
                destroyObstacleCount = statisticalData.destroyObstacleCount,
                hideMapCount = statisticalData.hideMapCount,
                killBossCount = statisticalData.killBossCount,
                killMonsterCount = statisticalData.killMonsterCount,
                money = statisticalData.money
            };

            return newData;
        }

        private void LoadStatisticalData()
        {
            if (statisticalData != null) return;

#if UNITY_EDITOR_WIN
            statisticalData = BinaryManager.Instance.Load<StatisticalData>("StatisticalData.zy");
#endif
#if UNITY_ANDROID
            string path = Application.persistentDataPath + "/StatisticalData.zy";
            if (!File.Exists(path))
            {
                File.Create(path);
                statisticalData = new StatisticalData();
            }
            else
            {
                try
                {
                    using (FileStream fileStream = File.Open(path, FileMode.Open, FileAccess.Read))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        statisticalData = formatter.Deserialize(fileStream) as StatisticalData;
                        fileStream.Close();
                    }
                }
                catch
                {
                    // correct the file
                    File.Delete(path);
                    File.Create(path);
                    statisticalData = new StatisticalData();
                }
            }
#endif
        }

        public void SaveStatisticalData()
        {
#if UNITY_EDITOR_WIN
            BinaryManager.Instance.Save("StatisticalData.zy", statisticalData);
#endif
#if UNITY_ANDROID
            using (FileStream fileStream = File.Open(Application.persistentDataPath + "/StatisticalData.zy", FileMode.Open, FileAccess.Write))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fileStream, statisticalData);
                fileStream.Flush();
                fileStream.Close();
            }
#endif
        }
    }
}