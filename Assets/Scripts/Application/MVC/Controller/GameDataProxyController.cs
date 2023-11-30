using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;


/// <summary>
/// 加载大关卡数据
/// </summary>
public class LoadBigLevelDataCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        
        

    }
    
}

/// <summary>
/// 加载小关卡数据
/// </summary>
public class LoadLevelDataCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        
        
    }
    
}

/// <summary>
/// 加载所有怪物数据
/// </summary>
public class LoadMonsterDataCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        
        
    }
    
}

/// <summary>
/// 加载所有塔数据
/// </summary>
public class LoadTowerDataCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        
        
    }
    
}