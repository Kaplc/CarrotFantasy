using App.Game;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;

namespace App.UI.GameScene.Panel.GamePanel
{
    /// <summary>
    ///     重新开始游戏命令
    /// </summary>
    public class RestartGameCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            GameManager.Instance.sceneManager.RestartGame();
        }
    }
}