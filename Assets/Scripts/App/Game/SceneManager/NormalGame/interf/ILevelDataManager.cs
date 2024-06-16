using App.Data.DataClass.Game.Level;

namespace App.Game.SceneManager.NormalGame.interf
{
    public interface ILevelDataManager
    {
        ItemData GetItemLevelData(int itemID);
        LevelData GetLevelData(int levelID);
    }
}