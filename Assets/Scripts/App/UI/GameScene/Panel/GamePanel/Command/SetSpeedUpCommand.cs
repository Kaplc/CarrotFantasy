using App.Game;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;

namespace App.UI.GameScene.Panel.GamePanel
{
    /// <summary>
    /// 两倍速
    /// </summary>
    public class SetSpeedUpCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            GameManager.Instance.sceneManger.SetSpeedUp((bool)notification.Body);
        }
    }
}