using PureMVC.Patterns.Proxy;
using XLua;

namespace App.Game
{
    [LuaCallCSharp]
    public class LuaProxy : Proxy
    {
        public LuaProxy(string proxyName, object data = null) : base(proxyName, data)
        {
        }
    }
}