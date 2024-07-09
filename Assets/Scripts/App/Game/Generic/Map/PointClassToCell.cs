using System.Collections.Generic;
using XLua;

namespace App.Game.Generic.Map
{
    [LuaCallCSharp]
    public static class PointClassToCell
    {
        public static List<Cell> ToCellList(List<PointClass> l)
        {
            var cells = new List<Cell>();
            foreach (var pointClass in l) cells.Add(new Cell(pointClass));

            return cells;
        }

        public static List<Cell> ToCellList(List<ObjectPointClass> l)
        {
            var cells = new List<Cell>();
            foreach (var obstaclePointClass in l) cells.Add(new Cell(obstaclePointClass));

            return cells;
        }

        public static Cell ToCell(PointClass p)
        {
            return new Cell(p);
        }
    }
}