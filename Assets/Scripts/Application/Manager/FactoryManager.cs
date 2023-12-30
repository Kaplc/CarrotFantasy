
/// <summary>
/// 资源工厂管理器
/// </summary>
public class FactoryManager: BaseSingleton<FactoryManager>
{
    public SpriteFactory SpriteFactory => GameFacade.Instance.RetrieveProxy(nameof(SpriteFactory)) as SpriteFactory;
    public UIControlFactory UIControlFactory => GameFacade.Instance.RetrieveProxy(nameof(UIControlFactory)) as UIControlFactory;

    public FactoryManager()
    {
        // 注册工厂
        GameFacade.Instance.RegisterProxy(new SpriteFactory());
        GameFacade.Instance.RegisterProxy(new UIControlFactory());
    }
    
}