using App.DataClass.Player;
using App.MVC.Controller;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;

namespace App.UI.BeginScene.BeginPanel
{
    public class LoadStatisticalDataCommand: SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            StatisticalData data = GameManager.Instance.dataManager.StatisticalDataManager.GetStatisticalData();

            var proxy = Facade.RetrieveProxy(nameof(BeginPanelProxy)) as BeginPanelProxy;
            proxy.UpdateStatisticalData(data);
        }
    }
}