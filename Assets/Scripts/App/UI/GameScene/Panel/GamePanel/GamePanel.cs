using App.Static;
using GameFramework;
using UnityEngine.UI;

namespace App.UI.GameScene.Panel.GamePanel
{
    public class GamePanel : BasePanel
    {
        public Button btnMenu;
        public Button btnSpeed1;
        public Button btnSpeed2;

        public CountDownPanel countDownPanel;
        public Image imgPause;
        public Toggle tgPause;
        public Text txMoney;
        public Text txNowWave;
        public Text txTotalWaves;

        protected override void Init()
        {
            btnSpeed1.onClick.AddListener(() =>
            {
                btnSpeed2.gameObject.SetActive(true);
                btnSpeed1.gameObject.SetActive(false);
                GameFacade.Instance.SendNotification(NotificationName.Game.SET_SPEED_UP, true);
            });
            btnSpeed2.onClick.AddListener(() =>
            {
                btnSpeed1.gameObject.SetActive(true);
                btnSpeed2.gameObject.SetActive(false);
                GameFacade.Instance.SendNotification(NotificationName.Game.SET_SPEED_UP, false);
            });
            tgPause.onValueChanged.AddListener(isOn =>
            {
                if (isOn)
                    PanelMediator.SendNotification(NotificationName.Game.RESUME_GAME);
                else
                    PanelMediator.SendNotification(NotificationName.Game.PAUSE_GAME);
            });
            btnMenu.onClick.AddListener(() => { PanelMediator.SendNotification(NotificationName.UI.SHOW_MENU_PANEL); });
            btnSpeed2.gameObject.SetActive(false);
            imgPause.gameObject.SetActive(false);

            // 开始倒计时
            countDownPanel.StartCountDown();
        }


        public void UpdateMoney(int num)
        {
            txMoney.text = num.ToString();
        }

        public void UpdateWavesCount((int nowWave, int totalWavesCount) data)
        {
            txNowWave.text = $"{data.nowWave / 10}  {data.nowWave % 10}";
            txTotalWaves.text = data.totalWavesCount.ToString();
        }
    }
}