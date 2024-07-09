using App.Static;
using GameFramework;
using UnityEngine.UI;

namespace App.UI.GameScene.Panel.LosePanel
{
    public class LosePanel : BasePanel
    {
        public Button btnReStart;
        public Button btnSelect;
        public Text txLevel;
        public Text txTotalWavesCount;
        public Text txWavesCount;

        protected override void Init()
        {
            btnReStart.onClick.AddListener(() =>
            {
                PanelMediator.SendNotification(NotificationName.Game.RESTART_GAME);
                UIManager.Instance.Hide<LosePanel>(false);
            });
            btnSelect.onClick.AddListener(() =>
            {
                PanelMediator.SendNotification(NotificationName.UI.SELECT_LEVEL);
                UIManager.Instance.Hide<LosePanel>(false);
            });
        }

        public void UpdatePanelData(int wavesCount, int totalWavesCount, int levelID)
        {
            txWavesCount.text = wavesCount / 10 + "  " + wavesCount % 10;
            txTotalWavesCount.text = totalWavesCount / 10 + "" + totalWavesCount % 10;
            txLevel.text = (levelID + 1) / 10 + "" + (levelID + 1) % 10;
        }
    }
}