using App.Static;
using Library;
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
                PanelMediator.SendNotification(NotificationName.Game.RESUME_GAME);
            });
        
            btnReStart.onClick.AddListener(() =>
            {
                UIManager.Instance.Hide<MenuPanel>(false);
                PanelMediator.SendNotification(NotificationName.Game.RESTART_GAME);
            });
        
            btnSelect.onClick.AddListener(() =>
            {
                UIManager.Instance.Hide<MenuPanel>(false);
                // 退出游戏
                PanelMediator.SendNotification(NotificationName.UI.SELECT_LEVEL);
            });
        }
    
    }
}

