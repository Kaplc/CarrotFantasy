using App.Static;
using GameFramework;
using UnityEngine.UI;

namespace App.UI.GameScene.Panel.MenuPanel
{
    public class MenuPanel : BasePanel
    {
        public Button btnContinue;
        public Button btnReStart;
        public Button btnSelect;

        protected override void Init()
        {
            btnContinue.onClick.AddListener(() =>
            {
                UIManager.Instance.Hide<MenuPanel>(false);
                GameFacade.Instance.SendNotification(NotificationName.Game.RESUME_GAME);
            });

            btnReStart.onClick.AddListener(() =>
            {
                UIManager.Instance.Hide<MenuPanel>(false);
                GameFacade.Instance.SendNotification(NotificationName.Game.RESTART_GAME);
            });

            btnSelect.onClick.AddListener(() =>
            {
                UIManager.Instance.Hide<MenuPanel>(false);
                // 退出游戏
                GameFacade.Instance.SendNotification(NotificationName.UI.SELECT_LEVEL);
            });
        }
    }
}