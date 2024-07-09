namespace App.Data
{
    public interface IDataManager
    {
        IMusicDataManager MusicDataManager { get; }
        IStatisticalDataManager StatisticalDataManager { get; }

        void SetMusicDataManager(IMusicDataManager musicDataManager);
        void SetStatisticalDataManager(IStatisticalDataManager statisticalDataManager);
    }
}