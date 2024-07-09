using PureMVC.Interfaces;
using PureMVC.Patterns.Command;

namespace App.UI.GameScene.Panel.GamePanel
{
    public class UpdateMoneyCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            var panelProxy = GameFacade.Instance.RetrieveProxy<GamePanelProxy>();
            panelProxy.UpdateMoney((int)notification.Body);
        }
    }
}