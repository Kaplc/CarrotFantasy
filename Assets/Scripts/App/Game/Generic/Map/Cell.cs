using System;
using XLua;

namespace App.Game.Generic.Map
{
    [Serializable]
    [LuaCallCSharp]
    public class Cell
    {
        public bool hasObstacle; // 上方存在障碍物
        public string obstacleName; // 障碍物名

        // 是否可以放塔
        private bool isTowerPos;

        public object obstacle; // 障碍物対象

        // 格子坐标
        private Point point;
        public object tower = null;

        public Cell(Point point)
        {
            this.point = point;
        }

        public Cell(ObjectPointClass objectPointClass)
        {
            point = new Point(objectPointClass.x, objectPointClass.y);
            obstacleName = objectPointClass.obstacleType.ToString();
        }

        public Cell(PointClass pointClass)
        {
            point = new Point(pointClass.x, pointClass.y);
        }

        public int X => point.X;
        public int Y => point.Y;

        public bool IsTowerPos
        {
            get => isTowerPos;
            set => isTowerPos = value;
        }

        public override string ToString()
        {
            return $"x:{X},y:{Y}";
        }
    }
}