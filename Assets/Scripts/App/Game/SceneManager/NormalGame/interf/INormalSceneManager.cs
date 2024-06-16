namespace App.Game.SceneManager.NormalGame.interf
{
    public interface INormalSceneManager: ISceneManger
    {
        int NowItemID { get; set; }
        int NowLevelID { get; set; }
        
        void UpdateWaveCount(int nowNum, int totalNum);
    }
}