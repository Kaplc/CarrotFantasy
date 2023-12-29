using System.Collections;
using System.Collections.Generic;
using PureMVC.Patterns.Proxy;
using UnityEngine;

public class MusicDataProxy : Proxy
{
    public new const string NAME = "MusicDataProxy";

    public MusicSettingData musicSettingData;

    public MusicDataProxy() : base(NAME)
    {
        LoadMusicSettingData();
    }

    public void GetMusicSettingData()
    {
        // 复制一份防止外部直接通过引用修改
        MusicSettingData newData = new MusicSettingData(){musicOpen = musicSettingData.musicOpen, soundOpen = musicSettingData.soundOpen};
        
        SendNotification(NotificationName.Data.LOADED_MUSICSETTINGDATA, newData);
    }

    /// <summary>
    /// 加载音乐设置数据
    /// </summary>
    private void LoadMusicSettingData()
    {
        if (Data != null) return;

        musicSettingData = BinaryManager.Instance.Load<MusicSettingData>("MusicSettingData.zy");
    }

    public void SaveMusicSettingData(MusicSettingData data)
    {
        // 缓存修改后的
        musicSettingData.musicOpen = data.musicOpen;
        musicSettingData.soundOpen = data.soundOpen;
        // 持久化
        BinaryManager.Instance.Save("MusicSettingData.zy", musicSettingData);
    }
}