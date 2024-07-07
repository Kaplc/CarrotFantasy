using System.Collections.Generic;
using App.Data.DataClass.Game.Object;
using App.Data.DataClass.Map;
using App.Game.Generic.BaseObject;
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
            typeof(UnityAction<BaseBuffEffect>),

            typeof(System.Func<bool>),
            typeof(System.Func<float>),
            typeof(System.Func<int>),
            typeof(System.Func<ISceneDataManager>),
            typeof(System.Func<ISpawner>),
            typeof(System.Func<IMapData>),
            typeof(UnityEngine.EventSystems.PointerEventData),
            // Dotween
            typeof(DG.Tweening.AutoPlay),
            typeof(DG.Tweening.AxisConstraint),
            typeof(DG.Tweening.Ease),
            typeof(DG.Tweening.LogBehaviour),
            typeof(DG.Tweening.LoopType),
            typeof(DG.Tweening.PathMode),
            typeof(DG.Tweening.PathType),
            typeof(DG.Tweening.RotateMode),
            typeof(DG.Tweening.ScrambleMode),
            typeof(DG.Tweening.TweenType),
            typeof(DG.Tweening.UpdateType),
    
            typeof(DG.Tweening.DOTween),
            typeof(DG.Tweening.DOVirtual),
            typeof(DG.Tweening.EaseFactory),
            typeof(DG.Tweening.Tweener),
            typeof(DG.Tweening.Tween),
            typeof(DG.Tweening.Sequence),
            typeof(DG.Tweening.TweenParams),
            typeof(DG.Tweening.Core.ABSSequentiable),
    
            typeof(DG.Tweening.Core.TweenerCore<Vector3, Vector3, DG.Tweening.Plugins.Options.VectorOptions>),
    
            typeof(DG.Tweening.TweenCallback),
            typeof(DG.Tweening.TweenExtensions),
            typeof(DG.Tweening.TweenSettingsExtensions),
            typeof(DG.Tweening.ShortcutExtensions),

            // Custom
            typeof(PreloadAssetInfo),
        };

        [LuaCallCSharp]
        public static List<System.Type> LuaCallCSharp = new List<System.Type>()
        {
            typeof(UnityEngine.SceneManagement.SceneManager),
        };
    }
}