using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitEndCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        
        ZFrameWorkSceneManager.Instance.LoadSceneAsync("2.BeginScene", () =>
        {
            SendNotification(NotificationName.HIDE_INIPANEL);
            SendNotification(NotificationName.SHOW_BEGINPANEL);
        });
    }
}
