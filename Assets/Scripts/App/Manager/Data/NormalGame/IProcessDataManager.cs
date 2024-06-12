using App.DataClass.Player;

namespace App.Manager.Data.NormalGame.@interface
{
    public interface IProcessDataManager
    {
        ProcessData GetProcessData();
        void LoadProcessData();
        void SaveProcessData(int itemID, int levelID, EPassedGrade garde);
    }
}