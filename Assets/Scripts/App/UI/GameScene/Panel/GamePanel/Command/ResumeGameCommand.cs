using App.Game;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;

namespace App.UI.GameScene.Panel.GamePanel
{
    /// <summary>
    /// 继续游戏
    /// </summary>
    public class ResumeGameCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            GameManager.Instance.sceneManger.ResumeGame();
        }
    }
}