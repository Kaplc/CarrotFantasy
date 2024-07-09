using GameFramework;

namespace App.Game.Factory
{
    /// <summary>
    ///     资源工厂管理器
    /// </summary>
    public class FactoryManager : BaseSingleton<FactoryManager>
    {
        public FactoryManager()
        {
            // 注册工厂
            SpriteFactory = new SpriteFactory();
            UIControlFactory = new UIControlFactory();
        }

        public SpriteFactory SpriteFactory { get; }

        public UIControlFactory UIControlFactory { get; }
    }
}