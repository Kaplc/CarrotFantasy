using System.Collections.Generic;
using XLua;

namespace App.Generic.Map
{
    [LuaCallCSharp]
    public static class PointClassToCell
    {
        public static List<Cell> ToCellList(List<PointClass> l)
        {
            List<Cell> cells = new List<Cell>();
            foreach (var pointClass in l)
            {
                cells.Add(new Cell(pointClass));
            }

            return cells;
        }
        
        public static List<Cell> ToCellList(List<ObjectPointClass> l)
        {
            List<Cell> cells = new List<Cell>();
            foreach (var obstaclePointClass in l)
            {
                cells.Add(new Cell(obstaclePointClass));
            }

            return cells;
        }
        
        public static Cell ToCell(PointClass p)
        {
            return new Cell(p);
        }
    }
}