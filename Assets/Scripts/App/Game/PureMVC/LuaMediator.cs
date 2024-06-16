using System;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using UnityEngine.Events;
using XLua;

namespace App.Game
{
    [LuaCallCSharp()]
    public class LuaMediator: Mediator
    {
        public UnityAction<INotification> handleAction;
        public Func<string[]> listenInterestsFunc;

        public LuaMediator(string mediatorName, object viewComponent = null) : base(mediatorName, viewComponent)
        {
        }

        public override void HandleNotification(INotification notification)
        {
            handleAction?.Invoke(notification);
        }

        public override string[] ListNotificationInterests()
        {
            return listenInterestsFunc?.Invoke();
        }
    }
}