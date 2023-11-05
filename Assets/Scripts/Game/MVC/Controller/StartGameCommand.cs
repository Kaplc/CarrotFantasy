using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);

        MonoManager.Instance.StartCoroutineFrameWork(LoadSceneAsync());
        
    }

    private IEnumerator LoadSceneAsync()
    {
        AsyncOperation ao;
        do
        {
            ao = SceneManager.LoadSceneAsync("3.GameScene");

            yield return ao;
        } while (!ao.isDone);
        
        // 异步加载场景完成隐藏LoadingPanel
        SendNotification(NotificationName.HIDE_LOADINGPANEL);
    }
}