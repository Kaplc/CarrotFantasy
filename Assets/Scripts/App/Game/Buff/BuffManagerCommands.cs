using App.Game.Generic.BaseObject;
using App.Game.Object.Monster;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;

namespace App.Game.Commands
{
    public class AddBuffCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            (IMonster monster, BaseBuff buff) data = ((IMonster, BaseBuff))notification.Body;
            GameManager.Instance.buffManager.ApplyBuff(data.monster, data.buff);
        }
    }

    public class RemoveBuffCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            (IMonster monster, BaseBuff buff) data = ((IMonster, BaseBuff))notification.Body;
            GameManager.Instance.buffManager.RemoveBuff(data.monster, data.buff);
        }
    }

    public class RemoveBuffsCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            GameManager.Instance.buffManager.RemoveAllBuffs(notification.Body as IMonster);
        }
    }
}