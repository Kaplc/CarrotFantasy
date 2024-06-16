using App.Data.DataClass.Player;

namespace App.Game.SceneManager.NormalGame.interf
{
    public interface IProcessDataManager
    {
        ProcessData GetProcessData();
        void LoadProcessData();
        void SaveProcessData(int itemID, int levelID, EPassedGrade garde);
    }
}