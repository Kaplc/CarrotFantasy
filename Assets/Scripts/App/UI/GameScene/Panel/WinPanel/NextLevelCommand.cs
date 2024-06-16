using App.Game;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;

namespace App.UI.GameScene.Panel.WinPanel
{
    public class NextLevelCommand: SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            GameManager.Instance.sceneManger.NextLevel();
        }
    }
}