using App.DataClass.Player;
using App.MVC.Controller;
using App.Static;
using PureMVC.Patterns.Proxy;

namespace App.UI.BeginScene.BeginPanel
{
    public class BeginPanelProxy : Proxy
    {
        private readonly MusicSettingData musicData;
        private readonly StatisticalData statisticalData;

        public BeginPanelProxy() : base(nameof(BeginPanelProxy))
        {
            musicData = new MusicSettingData();
            statisticalData = new StatisticalData();
        }

        public void UpdateMusicData()
        {
            musicData.musicOpen = GameManager.Instance.dataManager.MusicDataManager.MusicOpen;
            musicData.soundOpen = GameManager.Instance.dataManager.MusicDataManager.SoundOpen;
            SendNotification(NotificationName.UI.UPDATE_MUSIC_SETTING, musicData);
        }

        public void UpdateStatisticalData()
        {
            StatisticalData data = GameManager.Instance.dataManager.StatisticalDataManager.GetStatisticalData();
            statisticalData.money = data.money;
            statisticalData.bossMapCount = data.bossMapCount;
            statisticalData.adventureMapCount = data.adventureMapCount;
            statisticalData.killBossCount = data.killBossCount;
            statisticalData.killMonsterCount = data.killMonsterCount;
            statisticalData.destroyObstacleCount = data.destroyObstacleCount;
            statisticalData.hideMapCount = data.hideMapCount;

            SendNotification(NotificationName.UI.UPDATE_STATISTICAL_DATA, statisticalData);
        }
    }
}