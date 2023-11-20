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
        
        RegisterCommand(NotificationName.INIT_END, () => new InitEndCommand());
        RegisterCommand(NotificationName.START_GAME, () => new StartGameCommand());
        RegisterCommand(NotificationName.SELECT_LEVEL, () => new SelectLevelCommand());
        RegisterCommand(NotificationName.LOADED_LEVELDATA, ()=> new LoadDataCommand());
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
        RegisterMediator(new HelpPanelMediator());
        RegisterMediator(new LoadingPanelMediator());
        RegisterMediator(new WinPanelMediator());
        RegisterMediator(new LosePanelMediator());
    }

    protected override void InitializeModel()
    {
        base.InitializeModel();
        
        RegisterProxy(new GameDataProxy());
    }
}
