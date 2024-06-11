using App.Generic.NotificationBody;
using App.MVC.View.GameScene.Object;
using App.Static;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityEngine;

namespace App.MVC.Controller.Commands
{
    /// <summary>
    /// 初始化controller
    /// </summary>
    public class InitSpawnerController : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            GameFacade.Instance.RegisterCommand(NotificationName.UIEvent.CREATE_TOWER, () => new CreateTowerCommand());
            GameFacade.Instance.RegisterCommand(NotificationName.UIEvent.SELL_TOWER, () => new SellTowerCommand());
            GameFacade.Instance.RegisterCommand(NotificationName.UIEvent.UPGRADE_TOWER, () => new UpGradeTowerCommand());
            GameFacade.Instance.RegisterCommand(NotificationName.Game.START_SPAWN, () => new StartSpawnCommand());
            GameFacade.Instance.RegisterCommand(NotificationName.Game.STOP_SPAWN, () => new StopSpawnCommand());
            GameFacade.Instance.RegisterCommand(NotificationName.Game.SET_COLLECTINGFIRES, () => new SetCollectingFiresCommand());
            GameFacade.Instance.RegisterCommand(NotificationName.Game.CANEL_COLLECTINGFIRES, () => new CancelCollectingFiresCommand());
            GameFacade.Instance.RegisterCommand(NotificationName.Game.MONSTER_DEAD, () => new MonsterDeadCommand());
        }
    }

    /// <summary>
    /// 开始出怪
    /// </summary>
    public class StartSpawnCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            GameManager.Instance.spawner.ResumeWaves();
        }
    }

    /// <summary>
    /// 停止出怪
    /// </summary>
    public class StopSpawnCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            GameManager.Instance.spawner.PauseWaves();
        }
    }

    /// <summary>
    /// 创建塔命令
    /// </summary>
    public class CreateTowerCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            CreateTowerArgsBogy body = notification.Body as CreateTowerArgsBogy;
            GameManager.Instance.spawner.CreateTowerObject(body.towerData, body.cellWorldPos);
        }
    }

    /// <summary>
    /// 出售塔
    /// </summary>
    public class SellTowerCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            GameManager.Instance.spawner.SellTower((Vector3)notification.Body);
        }
    }

    /// <summary>
    /// 升级塔
    /// </summary>
    public class UpGradeTowerCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            GameManager.Instance.spawner.UpGradeTower((Vector3)notification.Body);
        }
    }

    /// <summary>
    /// 集火目标
    /// </summary>
    public class SetCollectingFiresCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            GameManager.Instance.spawner.SetCollectingFires(notification.Body as Monster);
        }
    }

    /// <summary>
    /// 取消集火
    /// </summary>
    public class CancelCollectingFiresCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            ISpawner spawner = GameManager.Instance.spawner;
            Monster tar = GameManager.Instance.spawner.GetCollectingFiresTarget();
            // 判断是否是集火目标
            if (tar == notification.Body as Monster)
            {
                spawner.CancelCollectingFiresTarget();
            }
        }
    }

    /// <summary>
    /// 怪物死亡消息
    /// </summary>
    public class MonsterDeadCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            ISpawner spawner = GameManager.Instance.spawner;

            if ((bool)spawner.WinJudge())
            {
                SendNotification(NotificationName.Game.GAME_WIN);
            }
        }
    }
}