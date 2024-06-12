using App.Static;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;

namespace App.MVC.Controller.Commands
{
    public class InitCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            base.Execute(notification);
            SendNotification(NotificationName.UI.SHOW_INITPANEL);
        
            // 初始化Controller
            SendNotification(NotificationName.Init.INIT_GAME_COMMAND);
            SendNotification(NotificationName.Init.INIT_SPAWNER_COMMAND);
            SendNotification(NotificationName.Init.INIT_LOADSCENE_COMMAND);
            SendNotification(NotificationName.Init.INIT_BUFFMANAGER_COMMAND);
            // ModelController
            SendNotification(NotificationName.Init.INIT_GAMEDATAPROXY_COMMAND);
            SendNotification(NotificationName.Init.INIT_MUSICDATAPROXY_COMMAND);
            SendNotification(NotificationName.Init.INIT_PROCESSDATAPROXY_COMMAND);
            SendNotification(NotificationName.Init.INIT_STATICALDATAPROXY_COMMAND);
        
            // 初始化游戏数据
            SendNotification(NotificationName.Init.INIT_GAMEDATA);
            // 初始化完成跳转开始场景
            SendNotification(NotificationName.LoadScene.LOADSCENE_INIT_TO_BEGIN);
        }
    }
}
