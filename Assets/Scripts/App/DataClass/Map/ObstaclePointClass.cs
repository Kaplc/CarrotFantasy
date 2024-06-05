using System;

namespace App.DataClass.Map
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