using App.Game;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;

namespace App.UI.GameScene.Panel.GamePanel
{
    /// <summary>
    ///     暂停游戏
    /// </summary>
    public class PauseGameCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            GameManager.Instance.sceneManager.PauseGame();
        }
    }
}