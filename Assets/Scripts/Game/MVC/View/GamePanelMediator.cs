using System.Collections;
using System.Collections.Generic;
using PureMVC.Patterns.Mediator;
using UnityEngine;

public class GamePanelMediator : Mediator
{
    
    
    public GamePanelMediator(string mediatorName, object viewComponent = null) : base(mediatorName, viewComponent)
    {
    }
}
