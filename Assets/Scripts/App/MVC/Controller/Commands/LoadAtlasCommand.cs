using App.MVC.Model.Factory;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;

namespace App.MVC.Controller.Commands
{
    public class LoadAtlasCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            base.Execute(notification);
        
            SpriteFactory proxy = GameFacade.Instance.RetrieveProxy("UIDataProxy") as SpriteFactory;
            proxy.LoadAtlas(notification.Body as string);
        
        }
    }
}
