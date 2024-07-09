using App.Data.DataClass.Player;
using GameFramework;

namespace App.Data
{
    public class MusicDataManager : IMusicDataManager
    {
        public MusicSettingData musicSettingData;

        public MusicDataManager()
        {
            Load();
        }

        public bool MusicOpen => musicSettingData.musicOpen;
        public bool SoundOpen => musicSettingData.soundOpen;

        public void Save(MusicSettingData data)
        {
            musicSettingData.musicOpen = data.musicOpen;
            musicSettingData.soundOpen = data.soundOpen;
            // 持久化
#if UNITY_EDITOR_WIN
            BinaryManager.Instance.Save("MusicSettingData.zy", musicSettingData);
#endif
#if UNITY_ANDROID
            using (FileStream fileStream = File.Open(Application.persistentDataPath + "/MusicSettingData.zy", FileMode.Open, FileAccess.Write))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fileStream, musicSettingData);
                fileStream.Flush();
                fileStream.Close();
            }
#endif
        }

        /// <summary>
        ///     加载音乐设置数据
        /// </summary>
        private void Load()
        {
#if UNITY_EDITOR_WIN
            musicSettingData = BinaryManager.Instance.Load<MusicSettingData>("MusicSettingData.zy");
#endif
#if UNITY_ANDROID
            string path = Application.persistentDataPath + "/MusicSettingData.zy";
            if (!File.Exists(path))
            {
                File.Create(path);
                musicSettingData = new MusicSettingData();
            }
            else
            {
                try
                {
                    using (FileStream fileStream = File.Open(path, FileMode.Open, FileAccess.Read))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        musicSettingData = formatter.Deserialize(fileStream) as MusicSettingData;
                        fileStream.Close();
                    }
                }
                catch
                {
                    // correct the file
                    File.Delete(path);
                    File.Create(path);
                    musicSettingData = new MusicSettingData();
                }
            }
#endif
        }
    }
}