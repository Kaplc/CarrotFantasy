using System;

namespace App.Game.Generic.Map
{
    [Serializable]
    public class PointClass
    {
        public int x;
        public int y;
        
        public PointClass(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        
        public override bool Equals(object obj)
        {
            if (obj is PointClass point)
            {
                return point.x == x && point.y == y;
            }

            return false;
        }
        
        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode();
        }
    }
}