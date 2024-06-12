using App.Generic.BaseObject;
using App.MVC.View.GameScene.Object;
using App.Static;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;

namespace App.MVC.Controller.Commands
{
    public class InitBuffManagerControllerCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            GameFacade.Instance.RegisterCommand(NotificationName.Game.ADD_BUFF, () => new AddBuffCommand());
            GameFacade.Instance.RegisterCommand(NotificationName.Game.REMOVE_BUFF, () => new RemoveBuffCommand());
            GameFacade.Instance.RegisterCommand(NotificationName.Game.REMOVE_BUFFS, () => new RemoveBuffsCommand());
        }
    }

    public class AddBuffCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            (IMonster monster, BaseBuff buff) data = ((IMonster, BaseBuff))notification.Body;
            GameManager.Instance.BuffManager.ApplyBuff(data.monster, data.buff);
        }
    }

    public class RemoveBuffCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            (IMonster monster, BaseBuff buff) data = ((IMonster, BaseBuff))notification.Body;
            GameManager.Instance.BuffManager.RemoveBuff(data.monster, data.buff);
        }
    }

    public class RemoveBuffsCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            GameManager.Instance.BuffManager.RemoveAllBuffs(notification.Body as IMonster);
        }
    }
}