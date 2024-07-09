using App.Data.DataClass.Player;
using GameFramework;
using UnityEngine.UI;

namespace App.UI.BeginScene.SettingPanel
{
    public class SelectPage : MonoManager
    {
        public Toggle tgMusic;
        public Toggle tgSound;

        public void UpdateMusicSetting(MusicSettingData data)
        {
            tgMusic.isOn = data.musicOpen;
            tgSound.isOn = data.soundOpen;
        }
    }
}