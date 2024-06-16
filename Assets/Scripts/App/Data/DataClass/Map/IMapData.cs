using System.Collections.Generic;
using App.Data.DataClass.Game.Object;
using App.Game.Generic.Map;

namespace App.Data.DataClass.Map
{
    public interface IMapData
    {
        int GetWaveCount();
        TowerData GetTowerData(int index);
        List<Cell> GetPathList();
        List<WaveData> GetWaveData();
        List<Cell> GetObstacle();
    }
}