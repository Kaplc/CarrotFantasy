using System.Collections.Generic;
using App.DataClass.Player;
using App.Static;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;

namespace App.UI.SelectItemScene
{
    public class SelectItemPanelMediator : Mediator
    {
        public new static string NAME = "SelectItemPanelMediator";

        public SelectItemPanel Panel
        {
            get => ViewComponent as SelectItemPanel;
            set
            {
                ViewComponent = value;
                (ViewComponent as SelectItemPanel)?.BindMediator(this);
            }
        }

        public SelectItemPanelMediator() : base(NAME)
        {
        }

        public override string[] ListNotificationInterests()
        {
            return new string[]
            {
                NotificationName.UI.SHOW_SELECT_ITEM_PANEL,
                NotificationName.UI.ITEM_DATA_UPDATED
            };
        }

        public override void HandleNotification(INotification notification)
        {
            base.HandleNotification(notification);

            switch (notification.Name)
            {
                case NotificationName.UI.SHOW_SELECT_ITEM_PANEL:
                    Panel = UIManager.Instance.Show<SelectItemPanel>(false);
                    SendNotification(NotificationName.Data.REQUEST_UPDATE_ITEM_PROCESS_DATA);
                    break;
                case NotificationName.UI.ITEM_DATA_UPDATED:
                    if (Panel)
                    {
                        Panel.UpdateItem(notification.Body as ProcessData);
                    }
                    break;
            }
        }
    }
}