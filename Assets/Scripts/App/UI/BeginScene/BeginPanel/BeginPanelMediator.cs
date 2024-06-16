using App.Data.DataClass.Player;
using App.Static;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;

namespace App.UI.BeginScene.BeginPanel
{
    public class BeginPanelMediator : Mediator
    {
        public static new string NAME = "BeginPanelMediator";

        // 相互绑定的Panel
        public BeginPanel Panel
        {
            get => ViewComponent as BeginPanel;
            set
            {
                ViewComponent = value;
                (ViewComponent as BeginPanel)?.BindMediator(this);
            }
        }

        private MusicSettingData musicSettingData;

        // 命名
        public BeginPanelMediator() : base(NAME)
        {
        }

        // view要监听的事件列表
        public override string[] ListNotificationInterests()
        {
            // 返回事件名数组表示要监听的事件
            return new string[]
            {
                NotificationName.UI.SHOW_BEGIN_PANEL,
                NotificationName.UI.SHOW_HELP_PANEL,
                NotificationName.UI.SHOW_SETTING_PANEL,
                NotificationName.UI.MUSIC_SETTING_UPDATED,
                NotificationName.UI.STATISTICAL_DATA_UPDATED
            };
        }

        // 执行监听的事件
        public override void HandleNotification(INotification notification)
        {
            // 根据不同的事件执行不同的逻辑
            switch (notification.Name)
            {
                case NotificationName.UI.SHOW_BEGIN_PANEL:
                    ShowBeginPanel();
                    break;
                case NotificationName.UI.SHOW_HELP_PANEL:
                    ShowHelpPanel(notification);
                    break;
                case NotificationName.UI.SHOW_SETTING_PANEL:
                    ShowSettingPanel();
                    break;
                case NotificationName.UI.MUSIC_SETTING_UPDATED:
                    MusicSettingDataUpdated(notification);
                    break;
                case NotificationName.UI.STATISTICAL_DATA_UPDATED:
                    StatisticalDataUpdated(notification);
                    break;
            }
        }

        private void StatisticalDataUpdated(INotification notification)
        {
            if (!Panel) return;
            // 刷新统计数据
            Panel.settingPanel.UpdateStatisticalPage(notification.Body as StatisticalData);
        }

        private void ShowSettingPanel()
        {
            Panel.settingPanel.selectPage.tgMusic.onValueChanged.AddListener(OnValueChangeMusicToggle);
            Panel.settingPanel.selectPage.tgSound.onValueChanged.AddListener(OnValueChangeSoundToggle);
            // 给设置面板刷获取数据
            SendNotification(NotificationName.Data.LOAD_MUSIC_SETTING_DATA);
            SendNotification(NotificationName.Data.LOAD_STATISTICAL_DATA);
            // 播放显示HelpPanel的动画
            Panel.animator.SetBool("ShowSettingPanel", true);
        }

        private void ShowHelpPanel(INotification notification)
        {
            // true为有动画过渡
            if ((bool)notification.Body)
            {
                // 播放显示HelpPanel的动画
                Panel.animator.SetBool("ShowHelpPanel", true);
            }
            else
            {
                Panel.animator.SetBool("RawShowHelpPanel", true);
            }
        }

        private void ShowBeginPanel()
        {
            Panel = UIManager.Instance.Show<BeginPanel>(false);
            // 每次显示时复原动画参数
            Panel.animator.SetBool("ShowHelpPanel", false);
            Panel.animator.SetBool("RawShowHelpPanel", false);
            Panel.animator.SetBool("ShowSettingPanel", false);
        }

        private void MusicSettingDataUpdated(INotification notification)
        {
            if (!Panel)return;
            // 刷新音乐设置数据
            musicSettingData = notification.Body as MusicSettingData;
            Panel.settingPanel.UpdateSelectPage(musicSettingData);
        }

        #region 设置面板

        private void OnValueChangeMusicToggle(bool isOn)
        {
            musicSettingData.musicOpen = isOn;
            SendNotification(NotificationName.Data.SAVE_MUSIC_SETTING_DATA, musicSettingData); // 保存数据
            SendNotification(NotificationName.Game.MUTE_MUSIC, !isOn); // 静音或播放
        }

        private void OnValueChangeSoundToggle(bool isOn)
        {
            musicSettingData.soundOpen = isOn;
            SendNotification(NotificationName.Data.SAVE_MUSIC_SETTING_DATA, musicSettingData);
            SendNotification(NotificationName.Game.MUTE_SOUND, !isOn);
        }

        #endregion
    }
}