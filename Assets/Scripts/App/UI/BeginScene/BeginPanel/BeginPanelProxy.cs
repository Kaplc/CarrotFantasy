using App.DataClass.Player;
using App.MVC.Controller;
using App.Static;
using PureMVC.Patterns.Proxy;

namespace App.UI.BeginScene.BeginPanel
{
    public class BeginPanelProxy : Proxy
    {
        private MusicSettingData musicData;
        private StatisticalData statisticalData;

        public BeginPanelProxy() : base(nameof(BeginPanelProxy))
        {
            musicData = new MusicSettingData();
            statisticalData = new StatisticalData();
        }

        public void UpdateMusicData(MusicSettingData data)
        {
            musicData = data;
            SendNotification(NotificationName.UI.UPDATE_MUSIC_SETTING, musicData);
        }

        public void UpdateStatisticalData(StatisticalData data)
        {
            statisticalData = data;
            SendNotification(NotificationName.UI.UPDATE_STATISTICAL_DATA, statisticalData);
        }
    }
}