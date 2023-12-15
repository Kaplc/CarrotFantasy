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
            GameFacade.Instance.SendNotification(NotificationName.SAVE_MUSCISETTINGDATA, musicSettingData);
        });
        tgSound.onValueChanged.AddListener((isOn) =>
        {
            musicSettingData.soundOpen = isOn;
            GameFacade.Instance.SendNotification(NotificationName.SAVE_MUSCISETTINGDATA, musicSettingData);
        });
    }

    public void UpdateMusicSetting(MusicSettingData data)
    {
        musicSettingData = data;
        tgMusic.isOn = data.musicOpen;
        tgSound.isOn = data.soundOpen;
    }
    
}