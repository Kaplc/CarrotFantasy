using App.Game;
using App.Game.Generic.NotificationBody;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;

namespace App.UI.GameScene.Panel.BuiltPanel.Command
{
    public class CreateTowerCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            var args = notification.Body as CreateTowerArgs;
            var s = GameManager.Instance.sceneManager.Spawner;
            GameManager.Instance.sceneManager.Spawner.CreateTowerObject(args.towerData, args.cellWorldPos);
        }
    }
}