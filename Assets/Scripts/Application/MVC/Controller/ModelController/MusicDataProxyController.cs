using PureMVC.Interfaces;
using PureMVC.Patterns.Command;

public class InitMusicDataProxyControllerCommand : SimpleCommand
{
    MusicDataProxy proxy = GameFacade.Instance.RetrieveProxy(nameof(MusicDataProxy)) as MusicDataProxy;
    
    public override void Execute(INotification notification)
    {
        GameFacade.Instance.RegisterCommand(NotificationName.Data.LOAD_MUSICSETTINGDATA, () => new GetMusicSettingDataCommand()
        {
            proxy = proxy
        });
        GameFacade.Instance.RegisterCommand(NotificationName.Data.SAVE_MUSCISETTINGDATA, () => new SaveMusicSettingDataCommand()
        {
            proxy = proxy
        });
    }
}

public class GetMusicSettingDataCommand : SimpleCommand
{
    public MusicDataProxy proxy;

    public override void Execute(INotification notification)
    {
        proxy.GetMusicSettingData();
    }
}

public class SaveMusicSettingDataCommand : SimpleCommand
{
    public MusicDataProxy proxy;

    public override void Execute(INotification notification)
    {
        proxy.SaveMusicSettingData(notification.Body as MusicSettingData);
    }
}