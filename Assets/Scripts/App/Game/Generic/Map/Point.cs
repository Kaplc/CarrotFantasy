using System;
using XLua;

namespace App.Game.Generic.Map
{
    /// <summary>
    ///     地图格子索引
    /// </summary>
    [Serializable]
    [LuaCallCSharp]
    public struct Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }

        public int Y { get; }
    }
}