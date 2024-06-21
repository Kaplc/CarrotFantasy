using App.Game;
using App.Static;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;

namespace App.UI.GameScene.Panel.MenuPanel.Command
{
    /// <summary>
    /// 菜单点击选择关卡
    /// </summary>
    public class SelectLevelCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            // 回到选择界面完全退出游戏
            GameManager.Instance.sceneManager.EndGame();
        }
    }
}