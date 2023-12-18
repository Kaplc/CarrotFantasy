using System.Collections;
using System.Collections.Generic;
using PureMVC.Patterns.Proxy;
using UnityEngine;
using UnityEngine.U2D;

public class SpriteFactory : Proxy
{
    public new const string NAME = "SpriteFactory";
    private Dictionary<string, SpriteAtlas> atlasDataDic= new Dictionary<string, SpriteAtlas>();
    private Dictionary<string, Sprite> spritesDataDic = new Dictionary<string, Sprite>();

    public SpriteFactory() : base(NAME)
    {
    }

    /// <summary>
    /// 加载图集
    /// </summary>
    /// <param name="path">图集资源路径</param>
    public void LoadAtlas(string path)
    {
        if (atlasDataDic.ContainsKey(path))
        {
            SendNotification(NotificationName.LOADED_ATLAS, atlasDataDic[path]);
            return;
        }

        SpriteAtlas atlas = Resources.Load<SpriteAtlas>(path);
        atlasDataDic.Add(path, atlas);
        SendNotification(NotificationName.LOADED_ATLAS, atlas);
    }

    public Sprite GetSprite(string atlasName, string spriteName)
    {
        if (!atlasDataDic.ContainsKey(atlasName))
        {
            LoadAtlas(atlasName);
        }
        return atlasDataDic[atlasName].GetSprite(spriteName);
    }
    
    public Sprite GetSprite(string path)
    {
        if (!spritesDataDic.ContainsKey(path))
        {
            // 加载Sprite
            return Resources.Load<Sprite>(path);
        }

        return spritesDataDic[path];
    }
    
}