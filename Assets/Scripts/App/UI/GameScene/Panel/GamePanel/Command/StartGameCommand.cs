using App.Game;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;

namespace App.UI.GameScene.Panel.GamePanel
{
    public class StartGameCommand: SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            GameManager.Instance.sceneManger.StartGame();
        }
    }
}