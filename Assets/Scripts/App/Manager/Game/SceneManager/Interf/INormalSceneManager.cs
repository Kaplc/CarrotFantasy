namespace App.Manager.Game.SceneManager.Interf
{
    public interface INormalSceneManager: ISceneManger
    {
        int NowItemID { get; set; }
        int NowLevelID { get; set; }
    }
}