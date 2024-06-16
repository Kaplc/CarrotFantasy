using App.Game;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityEngine;

namespace App.UI.GameScene.Panel.BuiltPanel.Command
{
    public class UpGradTowerCommand: SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            GameManager.Instance.sceneManger.Spawner.UpGradeTower((Vector3)notification.Body);
        }
    }
}