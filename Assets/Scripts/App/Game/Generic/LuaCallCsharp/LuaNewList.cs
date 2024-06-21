using System;
using System.Collections.Generic;
using XLua;


[LuaCallCSharp]
public static class LuaNewList
{
    public static List<T> New<T>()
    {
        return new List<T>();
    }
}