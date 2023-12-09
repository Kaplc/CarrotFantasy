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
        
        RegisterCommand(NotificationName.INIT_END, () => new InitEndCommand());
        
        // SpawnerController
        RegisterCommand(NotificationName.CREATE_TOWER, () => new CreateTowerCommand());
        RegisterCommand(NotificationName.SELL_TOWER, () => new SellTowerCommand());
        RegisterCommand(NotificationName.UPGRADE_TOWER, () => new UpGradeTower());
        
        
        RegisterCommand(NotificationName.LOAD_ATLAS, ()=> new LoadAtlasCommand());
    }

    protected override void InitializeView()
    {
        base.InitializeView();
        // 注册View
        RegisterMediator(new InitPanelMediator());
        RegisterMediator(new BeginPanelMediator());
        RegisterMediator(new SelectBigLevelPanelMediator());
        RegisterMediator(new SelectLevelPanelMediator());
        RegisterMediator(new GamePanelMediator());
        RegisterMediator(new MenuPanelMediator());
        RegisterMediator(new LoadingPanelMediator());
        RegisterMediator(new WinPanelMediator());
        RegisterMediator(new LosePanelMediator());
        RegisterMediator(new BuiltPanelMediator());
    }

    protected override void InitializeModel()
    {
        base.InitializeModel();
        
        RegisterProxy(new GameDataProxy());
        RegisterProxy(new UIDataProxy());
    }
}
