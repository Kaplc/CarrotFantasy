using PureMVC.Interfaces;
using PureMVC.Patterns.Command;

namespace App.UI.GameScene.Panel.GamePanel
{
    public class UpdateWaveCountCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            var proxy = GameFacade.Instance.RetrieveProxy<GamePanelProxy>();
            var data = ((int, int))notification.Body;
            proxy.UpdateWaveCount(data.Item1, data.Item2);
        }
    }
}