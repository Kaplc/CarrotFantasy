using App.Data.DataClass.Player;
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
            SendNotification(NotificationName.UI.MUSIC_SETTING_UPDATED, musicData);
        }

        public void UpdateStatisticalData(StatisticalData data)
        {
            statisticalData = data;
            SendNotification(NotificationName.UI.STATISTICAL_DATA_UPDATED, statisticalData);
        }
    }
}