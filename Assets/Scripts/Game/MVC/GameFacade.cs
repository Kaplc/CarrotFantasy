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
        RegisterCommand(NotificationName.PRESS_START, () => new StartGameCommand());
    }

    protected override void InitializeView()
    {
        base.InitializeView();
        
        RegisterMediator(new BeginPanelMediator());
        RegisterMediator(new SelectLevelPanelMediator());
        RegisterMediator(new GamePanelMediator());
        RegisterMediator(new MenuPanelMediator());
    }

    protected override void InitializeModel()
    {
        base.InitializeModel();
    }
}
