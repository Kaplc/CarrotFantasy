using App.Static;
using Library.SceneManager;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;

namespace App.MVC.Controller.Commands
{
    public class InitEndCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            base.Execute(notification);
        
            ZFrameWorkSceneManager.Instance.LoadSceneAsync("2.BeginScene", () =>
            {
                SendNotification(NotificationName.UI.HIDE_INIPANEL);
                SendNotification(NotificationName.UI.SHOW_BEGINPANEL);
            });
        }
    }
}
