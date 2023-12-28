using System;
using UnityEngine.UI;

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
            GameFacade.Instance.SendNotification(NotificationName.Data.SAVE_MUSCISETTINGDATA, musicSettingData); // 保存数据
            GameFacade.Instance.SendNotification(NotificationName.Data.LOAD_MUSICSETTINGDATA); // 重新加载
            GameFacade.Instance.SendNotification(NotificationName.Game.MUTE_MUSIC, !isOn); // 静音或播放
        });
        tgSound.onValueChanged.AddListener((isOn) =>
        {
            musicSettingData.soundOpen = isOn;
            GameFacade.Instance.SendNotification(NotificationName.Data.SAVE_MUSCISETTINGDATA, musicSettingData);
            GameFacade.Instance.SendNotification(NotificationName.Data.LOAD_MUSICSETTINGDATA);
            GameFacade.Instance.SendNotification(NotificationName.Game.MUTE_SOUND, isOn);
        });
    }

    public void UpdateMusicSetting(MusicSettingData data)
    {
        musicSettingData = data;
        tgMusic.isOn = data.musicOpen;
        tgSound.isOn = data.soundOpen;
    }
    
}