using System;
using GameFramework;
using XLua;

namespace LuaBridge
{
    [LuaCallCSharp]
    public class LuaPreloadAssetInfo: PreloadAssetInfo
    {
        public LuaPreloadAssetInfo(Type type, params string[] keys) : base(type, null, keys)
        {
        
        }

        public LuaPreloadAssetInfo(string type, params string[] keys) : base(Type.GetType(type), null, keys)
        {
        
        }
    }
}