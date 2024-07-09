using App.Game;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;

namespace App.UI.SelectLevelScene
{
    public class StartNormalGameCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            GameManager.Instance.LoadGameScene("5.GameScene", (int)notification.Body);
        }
    }
}