using App.DataClass.Player;
using Library;

namespace App.MVC.Model.PlayerData
{
    public interface IMusicDataManager
    {
        bool MusicOpen { get; } // bool musicOpen
        bool SoundOpen { get; } // bool soundOpen
        
        void Save(MusicSettingData data);
    }
}