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
        
        
        // 加载场景
        SceneManager.LoadScene("3.GameScene");
        // 加载当前关卡数据
        GameDataProxy gameDataProxy = GameFacade.Instance.RetrieveProxy("GameDataProxy") as GameDataProxy;
        gameDataProxy?.LoadLevelData(GameManager.Instance.nowBigLevelId, GameManager.Instance.nowLevelId);
        // 加载完成隐藏LoadingPanel
        SendNotification(NotificationName.HIDE_LOADINGPANEL);
    }
    
}