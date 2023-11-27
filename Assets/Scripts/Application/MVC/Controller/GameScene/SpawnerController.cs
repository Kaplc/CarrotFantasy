using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityEngine;

/// <summary>
/// 创建塔命令
/// </summary>
public class CreateTowerCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        CreateTowerArgsBogy body = notification.Body as CreateTowerArgsBogy;
        GameManager.Instance.spawner.CreateTowerObject(body.towerID, body.cellWorldPos);
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
public class UpGradeTower : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        GameManager.Instance.spawner.UpGradeTower((Vector3)notification.Body);
    }
}