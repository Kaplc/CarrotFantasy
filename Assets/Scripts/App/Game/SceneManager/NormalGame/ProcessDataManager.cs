using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using App.Data.DataClass.Player;
using App.Game.SceneManager.NormalGame.interf;
using Library;
using UnityEngine;

namespace App.Game.SceneManager.NormalGame
{
    public class ProcessDataManager :IProcessDataManager
    {
        private ProcessData processData;
        
        public ProcessDataManager()
        {
            LoadProcessData();
        }
        
        public ProcessData GetProcessData()
        {
            if (processData != null)
            {
                ProcessData newData = new ProcessData()
                {
                    passedItemsDic = new Dictionary<int, PassedLevelData>(processData.passedItemsDic)
                };
                
                return newData;
            }

            return null;
        }

        public void LoadProcessData()
        {
            if (processData != null) return;

#if UNITY_EDITOR_WIN
            processData = BinaryManager.Instance.Load<ProcessData>("ProcessData.zy");
#endif
#if UNITY_ANDROID
            string path = Application.persistentDataPath + "/ProcessData.zy";
            if (!File.Exists(path))
            {
                File.Create(path);
                processData = new ProcessData();
            }
            else
            {
                try
                {
                    using (FileStream fileStream = File.Open(path, FileMode.Open, FileAccess.Read))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        processData = formatter.Deserialize(fileStream) as ProcessData;
                        fileStream.Close();
                    }
                }
                catch
                {
                    // correct the file
                    File.Delete(path);
                    File.Create(path);
                    processData = new ProcessData();
                }

            }

#endif

            CalPassedLevelCount();
        }

        /// <summary>
        /// 计算通关关卡数
        /// </summary>
        private void CalPassedLevelCount()
        {
            // 遍历每个主题
            foreach (var passedBigLevelItem in processData.passedItemsDic)
            {
                // 计数
                int count = 0;
                // 遍历每个主题下的小关卡
                foreach (var passedLevelItem in passedBigLevelItem.Value.passedLevelDic)
                {
                    if (passedLevelItem.Value != EPassedGrade.None)
                    {
                        count++;
                    }
                }

                passedBigLevelItem.Value.passedLevelCount = count;
            }
        }

        public void SaveProcessData(int itemID, int levelID, EPassedGrade garde)
        {
            // 缓存的通关数据
            PassedLevelData passedLevelData = processData.passedItemsDic[itemID];
            // 存在已经解锁的关卡更新通关等级
            EPassedGrade grade = passedLevelData.passedLevelDic[levelID];
            // 仅刷新最高记录
            if ((int)garde > (int)grade)
            {
                passedLevelData.passedLevelDic[levelID] = garde;
            }

            // 判断下一关是否解锁
            if (!passedLevelData.passedLevelDic.ContainsKey(levelID + 1))
            {
                // 未解锁下一关则解锁
                passedLevelData.passedLevelDic[levelID + 1] = EPassedGrade.None;
            }

            CalPassedLevelCount();

            // 数据持久化
#if UNITY_EDITOR_WIN
            BinaryManager.Instance.Save("ProcessData.zy", processData);
#endif
#if UNITY_ANDROID
            using (FileStream fs = File.Open(Application.persistentDataPath +"/ProcessData.zy", FileMode.Open, FileAccess.Write))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, processData);
                fs.Flush();
                fs.Close();
            }
#endif
        }
    }
}