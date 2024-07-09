using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityEngine.Events;
using XLua;

namespace App.Game
{
    [LuaCallCSharp]
    public class LuaCommand : SimpleCommand
    {
        public UnityAction<INotification> action;

        public override void Execute(INotification notification)
        {
            action?.Invoke(notification);
        }
    }
}