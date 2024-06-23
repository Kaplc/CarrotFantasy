using System.Collections.Generic;
using App.Data.DataClass.Game.Object;
using App.Data.DataClass.Map;
using App.Game.Generic.Map;
using App.Game.Object.Monster;
using App.Game.SceneManager;
using App.Game.Spawner;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using XLua;

namespace App.Game.Generic.LuaCallCsharp
{
    [CSharpCallLua]
    public static class AddType
    {
        [CSharpCallLua]
        public static List<System.Type> CSharpCallLua = new List<System.Type>()
        {
            typeof(System.Action),
            typeof(UnityAction),
            typeof(UnityAction<Scene, LoadSceneMode>),
            typeof(UnityAction<TowerData, Vector3>),
            typeof(UnityAction<int>),
            typeof(UnityAction<bool>),
            typeof(UnityAction<float>),
            typeof(UnityAction<IMonster>),
            typeof(UnityAction<ISpawner>),
            typeof(UnityAction<ISceneDataManager>),
            typeof(UnityAction<Vector3>),
            typeof(UnityAction<List<Cell>, float, MonsterData>),
            typeof(System.Func<bool>),
            typeof(System.Func<float>),
            typeof(System.Func<int>),
            typeof(System.Func<ISceneDataManager>),
            typeof(System.Func<ISpawner>),
            typeof(System.Func<IMapData>),
            typeof(UnityEngine.EventSystems.PointerEventData)
        };

        [LuaCallCSharp]
        public static List<System.Type> LuaCallCSharp = new List<System.Type>()
        {
            typeof(UnityEngine.SceneManagement.SceneManager),
        };
    }
}