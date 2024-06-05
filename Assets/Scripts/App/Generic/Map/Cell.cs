using System;

namespace App.Generic
{
    [Serializable]
    public class Cell
    {
        // 格子坐标
        private Point point;
        public int X => point.X;
        public int Y => point.Y;

        public bool hasObstacle; // 上方存在障碍物
        public string obstacleName; // 障碍物名
        public object obstacle; // 障碍物対象
    
        // 是否可以放塔
        private bool isTowerPos;
        public object tower = null;

        public bool IsTowerPos
        {
            get => isTowerPos;
            set => isTowerPos = value;
        }

        public Cell(Point point)
        {
            this.point = point;
        }

        public override string ToString()
        {
            return $"x:{X},y:{Y}";
        }
    }
}