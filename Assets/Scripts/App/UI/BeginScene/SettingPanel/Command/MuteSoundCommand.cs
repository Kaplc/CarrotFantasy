using App.Game;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;

namespace App.UI.BeginScene.SettingPanel
{
    /// <summary>
    /// 静音音效
    /// </summary>
    public class MuteSoundCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            GameManager.Instance.musicManger.MuteSound((bool)notification.Body);
        }
    }
}