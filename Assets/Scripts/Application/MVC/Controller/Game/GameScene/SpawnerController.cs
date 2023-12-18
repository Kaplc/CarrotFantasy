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
        base.Execute(notification);
        
        GameFacade.Instance.RegisterCommand(NotificationName.CREATE_TOWER, () => new CreateTowerCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.SELL_TOWER, () => new SellTowerCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.UPGRADE_TOWER, () => new UpGradeTowerCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.START_SPAWN, () => new StartSpawnCommand());
        GameFacade.Instance.RegisterCommand(NotificationName.STOP_SPAWN, () => new StopSpawnCommand());
    }
}

/// <summary>
/// 开始出怪
/// </summary>
public class StartSpawnCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
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
        base.Execute(notification);
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
        base.Execute(notification);
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
        base.Execute(notification);
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
        base.Execute(notification);
        GameManager.Instance.spawner.UpGradeTower((Vector3)notification.Body);
    }
}