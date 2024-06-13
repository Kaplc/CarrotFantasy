using App.DataClass.Game.Level;

namespace App.Manager.Data.NormalGame
{
    public interface ILevelDataManager
    {
        ItemData GetItemLevelData(int itemID);
        LevelData GetLevelData(int levelID);
    }
}