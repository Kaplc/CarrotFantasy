using System.Collections;
using System.Collections.Generic;
using PureMVC.Patterns.Facade;
using UnityEngine;

public class GameFacade : Facade
{
    public static GameFacade Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameFacade();
            }
            return instance as GameFacade;
        }
    }
    
    // controller注册绑定通知
    protected override void InitializeController()
    {
        base.InitializeController();
        
        // 初始化通知
        RegisterCommand(NotificationName.INIT, () => new InitCommand());
        RegisterCommand(NotificationName.INIT_GAMEMANAGERCONTROLLER, () => new InitGameManagerControllerCommand());
        RegisterCommand(NotificationName.INIT_GAMEDATAPROXY, () => new InitGameDataProxyCommand());
        RegisterCommand(NotificationName.INIT_SPAWNERCONTROLLER, () => new InitSpawnerController());
        RegisterCommand(NotificationName.INIT_LOADSCENECONTROLLER, () => new InitLoadSceneController());
        
        RegisterCommand(NotificationName.INIT_END, () => new InitEndCommand());
        RegisterCommand(NotificationName.LOAD_ATLAS, ()=> new LoadAtlasCommand());
    }

    protected override void InitializeView()
    {
        base.InitializeView();
        // 注册View
        RegisterMediator(new LoadingPanelMediator());
    }

    protected override void InitializeModel()
    {
        base.InitializeModel();
        
        RegisterProxy(new GameDataProxy());
        // RegisterProxy(new SpriteFactory());
    }
}
