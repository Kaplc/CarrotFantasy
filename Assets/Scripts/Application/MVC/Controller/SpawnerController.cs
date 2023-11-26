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
        GameManager.Instance.spawner.CreateTowerObject(body.towerID, body.createWorldPos);
    }
}
