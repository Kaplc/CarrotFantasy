using App.Game.SceneManager.NormalGame.interf;
using App.Static;
using App.UI.BeginScene.BeginPanel;
using App.UI.InitScene;
using App.UI.SelectItemScene;
using App.UI.SelectLevelScene;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityEngine.Events;

namespace App.Game.Commands
{
    #region 场景加载

    public class LoadBeginSceneCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            GameFacade.Instance.RegisterMediator(new BeginPanelMediator());

            GameManager.Instance.loadSceneManager.LoadSceneAsync("2.BeginScene", success =>
            {
                SendNotification(NotificationName.UI.SHOW_BEGIN_PANEL);
                // 执行回调
                (notification.Body as UnityAction)?.Invoke();
            });
        }
    }

    public class LoadSelectItemSceneCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            GameFacade.Instance.RegisterMediator(new SelectItemPanelMediator());
            GameManager.Instance.loadSceneManager.LoadSceneAsync("3.SelectItemScene",
                success => { SendNotification(NotificationName.UI.SHOW_SELECT_ITEM_PANEL); });
        }
    }

    public class LoadSelectLevelSceneCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            GameFacade.Instance.RegisterMediator(new SelectLevelPanelMediator());
            GameManager.Instance.loadSceneManager.LoadSceneAsync("4.SelectLevelScene", success =>
            {
                // 根据记录的ID打开对应主题
                SendNotification(NotificationName.UI.SHOW_SELECT_LEVEL_PANEL, ((INormalSceneManager)GameManager.Instance.sceneManager).NowItemID);
            });
        }
    }

    public class LoadGameSceneCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            SendNotification(NotificationName.UI.SHOW_LOADING_PANEL);
            // 停止背景音乐
            GameFacade.Instance.SendNotification(NotificationName.Game.STOP_MUSIC);

            GameManager.Instance.loadSceneManager.LoadSceneAsync("5.GameScene", success =>
            {
                // 传递LevelID
                GameFacade.Instance.SendNotification(NotificationName.Game.LOAD_GAME, (int)notification.Body);
                SendNotification(NotificationName.UI.HIDE_LOADING_PANEL);
            });
        }
    }

    #endregion

    #region 场景跳转

    /// <summary>
    ///     初始化 - 开始
    /// </summary>
    public class LoadSceneInitToBeginCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            SendNotification(NotificationName.UI.HIDE_INIT_PANEL);
            GameFacade.Instance.RemoveMediator(nameof(InitPanelMediator));
            SendNotification(NotificationName.LoadScene.LOADSCENE_BEGIN);
        }
    }

    /// <summary>
    ///     开始 - 选择主题
    /// </summary>
    public class LoadSceneBeginToSelectItemCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            GameFacade.Instance.RemoveMediator(nameof(BeginPanelMediator));
            SendNotification(NotificationName.LoadScene.LOADSCENE_SELECTITEM);
        }
    }

    #region 选择主题场景

    /// <summary>
    ///     选择主题 - 开始
    /// </summary>
    public class LoadSceneSelectItemToBeginCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            GameFacade.Instance.RemoveMediator(nameof(SelectItemPanelMediator));
            SendNotification(NotificationName.LoadScene.LOADSCENE_BEGIN);
        }
    }

    /// <summary>
    ///     选择主题 - 选择关卡
    /// </summary>
    public class LoadSceneSelectItemToSelectLevelCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            GameFacade.Instance.RemoveMediator(nameof(SelectItemPanelMediator));

            SendNotification(NotificationName.LoadScene.LOADSCENE_SELECTLEVEL);
        }
    }

    /// <summary>
    ///     选择主题 - 帮助
    /// </summary>
    public class LoadSceneSelectItemToHelpPanelCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            GameFacade.Instance.RemoveMediator(nameof(SelectItemPanelMediator));
            UnityAction ac = () =>
            {
                // false当消息体传递标识显示helpPanel无动画过渡
                SendNotification(NotificationName.UI.SHOW_HELP_PANEL, false);
            };
            SendNotification(NotificationName.LoadScene.LOADSCENE_BEGIN, ac);
        }
    }

    #endregion

    #region 选择关卡场景

    /// <summary>
    ///     选择关卡 - 游戏场景
    /// </summary>
    public class LoadSceneSelectLevelToGameCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            GameFacade.Instance.RemoveMediator(nameof(SelectLevelPanelMediator));
            SendNotification(NotificationName.LoadScene.LOADSCENE_GAME, (int)notification.Body);
        }
    }

    /// <summary>
    ///     选择关卡 - 帮助
    /// </summary>
    public class LoadSceneSelectLevelToHelpPanelCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            GameFacade.Instance.RemoveMediator(nameof(SelectLevelPanelMediator));
            UnityAction action = () => { SendNotification(NotificationName.UI.SHOW_HELP_PANEL, false); };

            SendNotification(NotificationName.LoadScene.LOADSCENE_BEGIN, action);
        }
    }

    /// <summary>
    ///     选择关卡 - 选择主题
    /// </summary>
    public class LoadSceneSelectLevelToSelectItemCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            GameFacade.Instance.RemoveMediator(nameof(SelectLevelPanelMediator));
            SendNotification(NotificationName.LoadScene.LOADSCENE_SELECTITEM);
        }
    }

    #endregion

    #region 游戏场景

    /// <summary>
    ///     游戏 - 选择关卡
    /// </summary>
    public class LoadSceneGameToSelectLevelCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            // 开启背景音乐
            SendNotification(NotificationName.Game.PLAY_MUSIC);

            SendNotification(NotificationName.LoadScene.LOADSCENE_SELECTLEVEL);
        }
    }

    /// <summary>
    ///     游戏 - 游戏 重新开始或下一关
    /// </summary>
    public class LoadSceneGameToGameCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            SendNotification(NotificationName.LoadScene.LOADSCENE_GAME, (int)notification.Body);
        }
    }

    /// <summary>
    ///     游戏 - 结束
    /// </summary>
    public class LoadSceneGameToEndCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            SendNotification(NotificationName.LoadScene.LOADSCENE_END);
        }
    }

    #endregion

    #endregion
}