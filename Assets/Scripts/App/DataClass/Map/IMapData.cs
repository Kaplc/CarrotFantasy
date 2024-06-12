using System.Collections.Generic;
using App.DataClass.Game.Object;
using App.Generic.Map;

namespace App.DataClass.Map
{
    public interface IMapData
    {
        int GetWaveCount();
        TowerData GetTowerData(int index);
        List<Cell> GetPathList();
    }
}