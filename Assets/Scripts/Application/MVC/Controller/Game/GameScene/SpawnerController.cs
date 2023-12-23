using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityEngine;

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
        GameManager.Instance.spawner.StartSpawn();
    }
}

/// <summary>
/// 停止出怪
/// </summary>
public class StopSpawnCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        GameManager.Instance.spawner.StopSpawn();
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
        Spawner spawner = GameManager.Instance.spawner;
        // 判断是否是集火目标
        if (spawner.collectingFiresTarget == notification.Body as Monster)
        {
            spawner.collectingFiresTarget = null;
            // 隐藏集火标志
            spawner.signTrans.gameObject.SetActive(false);
            spawner.signTrans.transform.SetParent(spawner.transform);
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
        Spawner spawner = GameManager.Instance.spawner;
        
        // 1.出怪完成
        if (!spawner.spawnedComplete) return;

        // 2.萝卜没死
        if (spawner.carrot.isDead) return;

        // 3.怪物全部死亡
        for (int i = 0; i < spawner.monsters.Count; i++)
        {
            // 有一个没死亡都无效
            if (spawner.monsters[i].isDead == false)
            {
                return;
            }
        }
        
        SendNotification(NotificationName.Game.GAME_WIN);
    }
}