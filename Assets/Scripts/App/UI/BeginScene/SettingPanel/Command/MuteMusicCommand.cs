using App.Game;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;

namespace App.UI.BeginScene.SettingPanel
{
    /// <summary>
    ///     静音背景音乐
    /// </summary>
    public class MuteMusicCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            GameManager.Instance.musicManger.MuteMusic((bool)notification.Body);
        }
    }
}