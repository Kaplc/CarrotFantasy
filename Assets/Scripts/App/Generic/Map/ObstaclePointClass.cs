using System;
using App.DataClass.Map;

namespace App.Generic.Map
{
    [Serializable]
    public class ObstaclePointClass: PointClass
    {
        public EObstacleType obstacleType;
        
        public ObstaclePointClass(int x, int y) : base(x, y)
        {
            
        }
        
        public ObstaclePointClass(int x, int y, EObstacleType obstacleType) : base(x, y)
        {
            this.obstacleType = obstacleType;
        }
    }
}