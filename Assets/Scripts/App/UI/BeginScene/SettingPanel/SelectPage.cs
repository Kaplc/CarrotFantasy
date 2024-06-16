using App.Data.DataClass.Player;
using App.Game;
using App.Static;
using Library;
using UnityEngine.UI;

namespace App.UI.BeginScene.SettingPanel
{
    public class SelectPage: MonoManager
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