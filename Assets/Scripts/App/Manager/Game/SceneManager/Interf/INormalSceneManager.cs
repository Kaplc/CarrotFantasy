namespace App.Manager.Game.SceneManager.Interf
{
    public interface INormalSceneManager: ISceneManger
    {
        int NowBigLevelID { get; set; }
        int NowLevelID { get; set; }
    }
}