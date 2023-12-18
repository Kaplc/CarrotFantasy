using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityEngine;

public class LoadAtlasCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        
        UIDataProxy proxy = GameFacade.Instance.RetrieveProxy("UIDataProxy") as UIDataProxy;
        proxy.LoadAtlas(notification.Body as string);
        
    }
}
