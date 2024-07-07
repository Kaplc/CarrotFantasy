
using System;
using XLua;
using XLua.LuaDLL;

[LuaCallCSharp]
public class LuaPreloadAssetInfo: PreloadAssetInfo
{
    public LuaPreloadAssetInfo(Type type, params string[] keys) : base(type, null, keys)
    {
        
    }
}