using App.DataClass.Player;
using App.MVC;
using App.Static;
using Library;
using UnityEngine.UI;

namespace App.UI.BeginScene.SettingPanel
{
    public class SelectPage: MonoManager
    {
        public Toggle tgMusic;
        public Toggle tgSound;
        private MusicSettingData musicSettingData;

        private void Awake()
        {
            tgMusic.onValueChanged.AddListener((isOn) =>
            {
                musicSettingData.musicOpen = isOn;
                GameFacade.Instance.SendNotification(NotificationName.Data.SAVE_MUSCISETTING_DATA, musicSettingData); // 保存数据
                GameFacade.Instance.SendNotification(NotificationName.Game.MUTE_MUSIC, !isOn); // 静音或播放
            });
            tgSound.onValueChanged.AddListener((isOn) =>
            {
                musicSettingData.soundOpen = isOn;
                GameFacade.Instance.SendNotification(NotificationName.Data.SAVE_MUSCISETTING_DATA, musicSettingData);
                GameFacade.Instance.SendNotification(NotificationName.Game.MUTE_SOUND, !isOn);
            });
        }

        public void UpdateMusicSetting(MusicSettingData data)
        {
            musicSettingData = data;
            tgMusic.isOn = data.musicOpen;
            tgSound.isOn = data.soundOpen;
        }
    
    }
}