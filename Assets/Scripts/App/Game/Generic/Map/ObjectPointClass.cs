using System;
using App.Static.Enum;

namespace App.Game.Generic.Map
{
    [Serializable]
    public class ObjectPointClass: PointClass
    {
        public EObstacleType obstacleType;
        public ETowerType towerType;
        
        public ObjectPointClass(int x, int y) : base(x, y)
        {
            obstacleType = EObstacleType.None;
            towerType = ETowerType.None;
        }
        
        public ObjectPointClass(int x, int y, EObstacleType obstacleType) : base(x, y)
        {
            this.obstacleType = obstacleType;
        }
        
        public ObjectPointClass(int x, int y, ETowerType towerType) : base(x, y)
        {
            this.towerType = towerType;
        }
    }
}