using App.Data.DataClass.Player;
using GameFramework;

namespace App.Data
{
    public class StatisticalDataManager : IStatisticalDataManager
    {
        private StatisticalData statisticalData;

        public StatisticalDataManager()
        {
            LoadStatisticalData();
        }

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
            if (statisticalData == null) LoadStatisticalData();

            // 复制数据
            var newData = new StatisticalData
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

        public void SaveStatisticalData(StatisticalData data)
        {
            statisticalData.killMonsterCount += data.killMonsterCount;
            statisticalData.money += data.money;

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
    }
}