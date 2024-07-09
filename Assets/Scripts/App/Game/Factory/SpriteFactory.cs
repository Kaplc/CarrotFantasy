using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

namespace App.Game.Factory
{
    public class SpriteFactory
    {
        private readonly Dictionary<string, SpriteAtlas> atlasDataDic = new Dictionary<string, SpriteAtlas>();
        private readonly Dictionary<string, Sprite> spritesDataDic = new Dictionary<string, Sprite>();


        /// <summary>
        ///     加载图集
        /// </summary>
        /// <param name="path">图集资源路径</param>
        public SpriteAtlas LoadAtlas(string path)
        {
            if (atlasDataDic.TryGetValue(path, out var loadAtlas)) return loadAtlas;

            var atlas = Resources.Load<SpriteAtlas>(path);
            atlasDataDic.Add(path, atlas);
            return atlas;
        }

        public Sprite GetSprite(string atlasName, string spriteName)
        {
            if (!atlasDataDic.ContainsKey(atlasName)) LoadAtlas(atlasName);
            return atlasDataDic[atlasName].GetSprite(spriteName);
        }

        public Sprite GetSprite(string path)
        {
            if (path == null) return null;

            if (!spritesDataDic.ContainsKey(path))
                // 加载Sprite
                return Resources.Load<Sprite>(path);

            return spritesDataDic[path];
        }
    }
}