using System.IO;
using Library;
using UnityEngine;
using XLua;

namespace Library
{
    public class XLuaManager : BaseSingleton<XLuaManager>
    {
        public LuaEnv luaEnv;

        public XLuaManager()
        {
            luaEnv = new LuaEnv();
            // 重定向执行路径，执行脚本时先去寻找该路径
            // luaEnv.AddLoader(AddLuaFilePath); 
            // luaEnv.AddLoader(AddABLuaFilePath); 
        }

        // 属性提供访问_G表
        public LuaTable _G => luaEnv.Global;

        public void DoFile(string fillName)
        {
            luaEnv.DoString($"require(\"{fillName}\")");
        }

        public void DoString(string str)
        {
            luaEnv.DoString(str);
        }

        /// <summary>
        ///     lua的GC
        /// </summary>
        public void Tick()
        {
            luaEnv.Tick();
        }

        /// <summary>
        ///     销毁lua解析器
        /// </summary>
        public void Dispose()
        {
            luaEnv.Dispose();
            luaEnv = null;
        }

        #region 重定向执行Lua脚本执行路径

        public void AddLuaFilePath(string path)
        {
            luaEnv.AddLoader((ref string fileName) =>
            {
                if (File.Exists(path + fileName + ".lua")) return File.ReadAllBytes(path + fileName + ".lua");

                return null;
            });
        }

        // 普通路径
        // private byte[] AddLuaFilePath(ref string fileName)
        // {
        //     string path = Application.dataPath + "/Lua/" + fileName + ".lua";
        //
        //     if (File.Exists(path))
        //     {
        //         // 返回读取到的Lua脚本字节数据
        //         return File.ReadAllBytes(path);
        //     }
        //     
        //     return null;
        // }

        // AB包中获取文件
        private byte[] AddABLuaFilePath(ref string fileName)
        {
            var lua = Library.ABManager.Instance.Load<TextAsset>("lua", fileName);

            if (lua != null) return lua.bytes;

            return null;
        }

        #endregion
    }
}