using App.Data.DataClass.Player;

namespace App.Data
{
    public interface IMusicDataManager
    {
        bool MusicOpen { get; } // bool musicOpen
        bool SoundOpen { get; } // bool soundOpen

        void Save(MusicSettingData data);
    }
}