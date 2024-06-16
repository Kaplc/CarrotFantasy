using Library;

namespace App.Game.Factory
{
    /// <summary>
    /// 资源工厂管理器
    /// </summary>
    public class FactoryManager: BaseSingleton<FactoryManager>
    {
        private SpriteFactory spriteFactory;
        private UIControlFactory uiControlFactory;
        
        public SpriteFactory SpriteFactory => spriteFactory;
        public UIControlFactory UIControlFactory => uiControlFactory;

        public FactoryManager()
        {
            // 注册工厂
            spriteFactory = new SpriteFactory();
            uiControlFactory = new UIControlFactory();
        }
    
    }
}