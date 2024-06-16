using App.Static;
using PureMVC.Patterns.Proxy;

namespace App.UI.GameScene.Panel.GamePanel
{
    public class GamePanelProxy: Proxy
    {
        public GamePanelProxy() : base(nameof(GamePanelProxy))
        {
        }

        public void UpdateWaveCount(int nowNum, int totalNum)
        {
            SendNotification(NotificationName.UI.WAVES_COUNT_UPDATED, (nowNum, totalNum));
        }
        
        public void UpdateMoney(int money)
        {
            SendNotification(NotificationName.UI.MONEY_UPDATED, money);
        }
    }
}