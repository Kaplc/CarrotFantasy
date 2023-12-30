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
        RegisterCommand(NotificationName.Init.INIT_GAMEMANAGER_CONTROLLER, () => new InitGameManagerControllerCommand());
        RegisterCommand(NotificationName.Init.INIT_SPAWNER_CONTROLLER, () => new InitSpawnerController());
        RegisterCommand(NotificationName.Init.INIT_LOADSCENE_CONTROLLER, () => new InitLoadSceneController());
        RegisterCommand(NotificationName.Init.INIT_BUFFMANAGER_CONTROLLER, () => new InitBuffManagerControllerCommand());
        // ModelController
        RegisterCommand(NotificationName.Init.INIT_GAMEDATAPROXY_CONTROLLER, () => new InitGameDataProxyCommand());
        RegisterCommand(NotificationName.Init.INIT_MUSICDATAPROXY_CONTROLLER, () => new InitMusicDataProxyControllerCommand());
        RegisterCommand(NotificationName.Init.INIT_STATICALDATAPROXY_CONTROLLER, () => new InitStaticalDataProxyControllerCommand());
        RegisterCommand(NotificationName.Init.INIT_PROCESSDATAPROXY_CONTROLLER, () => new InitProcessDataProxyControllerCommand());
        
        
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
        RegisterProxy(new MusicDataProxy());
        RegisterProxy(new StatisticalDataProxy());
        RegisterProxy(new ProcessDataProxy());
        
    }
}
