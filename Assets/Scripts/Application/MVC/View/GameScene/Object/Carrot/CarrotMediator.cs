using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;

public class CarrotMediator : Mediator
{
    public new static string NAME = nameof(CarrotMediator);

    private Carrot carrot;
    
    public CarrotMediator(Carrot carrot) : base(NAME)
    {
        this.carrot = carrot;
    }

    public override string[] ListNotificationInterests()
    {
        return new string[]
        {
            NotificationName.REACH_ENDPOINT
        };
    }

    public override void HandleNotification(INotification notification)
    {
        base.HandleNotification(notification);
        
        carrot.Wound((int)notification.Body);
    }
}