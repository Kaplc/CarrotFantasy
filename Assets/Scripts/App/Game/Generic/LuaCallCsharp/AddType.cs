using System;
using System.Collections.Generic;
using App.Data.DataClass.Game.Object;
using App.Data.DataClass.Map;
using App.Game.Generic.BaseObject;
using App.Game.Generic.Map;
using App.Game.Object.Monster;
using App.Game.SceneManager;
using App.Game.Spawner;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using GameFramework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using XLua;

namespace App.Game.Generic.LuaCallCsharp
{
    [CSharpCallLua]
    public static class AddType
    {
        [CSharpCallLua] public static List<Type> CSharpCallLua = new List<Type>
        {
            typeof(Action),
            typeof(Action<bool>),
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
            typeof(UnityAction<BaseBuffEffect>),

            typeof(Func<bool>),
            typeof(Func<float>),
            typeof(Func<int>),
            typeof(Func<ISceneDataManager>),
            typeof(Func<ISpawner>),
            typeof(Func<IMapData>),
            typeof(PointerEventData),
            // Dotween
            typeof(AutoPlay),
            typeof(AxisConstraint),
            typeof(Ease),
            typeof(LogBehaviour),
            typeof(LoopType),
            typeof(PathMode),
            typeof(PathType),
            typeof(RotateMode),
            typeof(ScrambleMode),
            typeof(TweenType),
            typeof(UpdateType),

            typeof(DOTween),
            typeof(DOVirtual),
            typeof(EaseFactory),
            typeof(Tweener),
            typeof(Tween),
            typeof(Sequence),
            typeof(TweenParams),
            typeof(ABSSequentiable),

            typeof(TweenerCore<Vector3, Vector3, VectorOptions>),

            typeof(TweenCallback),
            typeof(TweenExtensions),
            typeof(TweenSettingsExtensions),
            typeof(ShortcutExtensions),

            // Custom
            typeof(PreloadAssetInfo)
        };

        [LuaCallCSharp] public static List<Type> LuaCallCSharp = new List<Type>
        {
            typeof(UnityEngine.SceneManagement.SceneManager)
        };
    }
}