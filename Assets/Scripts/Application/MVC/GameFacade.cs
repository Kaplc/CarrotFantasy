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
        RegisterCommand(NotificationName.Init.INIT, () => new InitCommand());
        RegisterCommand(NotificationName.Init.INIT_GAMEMANAGERCONTROLLER, () => new InitGameManagerControllerCommand());
        RegisterCommand(NotificationName.Init.INIT_GAMEDATAPROXY, () => new InitGameDataProxyCommand());
        RegisterCommand(NotificationName.Init.INIT_SPAWNERCONTROLLER, () => new InitSpawnerController());
        RegisterCommand(NotificationName.Init.INIT_LOADSCENECONTROLLER, () => new InitLoadSceneController());
        
        RegisterCommand(NotificationName.Init.INIT_END, () => new InitEndCommand());
        RegisterCommand(NotificationName.Data.LOAD_ATLAS, ()=> new LoadAtlasCommand());
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
