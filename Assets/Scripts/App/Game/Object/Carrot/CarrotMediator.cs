using App.Static;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;

namespace App.Game.Object.Carrot
{
    public class CarrotMediator : Mediator
    {
        public new static string NAME = nameof(CarrotMediator);

        private readonly Carrot carrot;

        public CarrotMediator(Carrot carrot) : base(NAME)
        {
            this.carrot = carrot;
        }

        public override string[] ListNotificationInterests()
        {
            return new[]
            {
                NotificationName.Game.REACH_ENDPOINT
            };
        }

        public override void HandleNotification(INotification notification)
        {
            base.HandleNotification(notification);
            carrot.Wound((int)(float)notification.Body);
        }
    }
}