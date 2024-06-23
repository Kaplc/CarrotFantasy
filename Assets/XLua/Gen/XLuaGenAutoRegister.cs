#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using System;
using System.Collections.Generic;
using System.Reflection;


namespace XLua.CSObjectWrap
{
    public class XLua_Gen_Initer_Register__
	{
        
        
        static void wrapInit0(LuaEnv luaenv, ObjectTranslator translator)
        {
        
            translator.DelayWrapLoader(typeof(CallMathf), CallMathfWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(LuaNewList), LuaNewListWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(MonoScript), MonoScriptWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UIInterfaceClass), UIInterfaceClassWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(GameFacade), GameFacadeWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UIManager), UIManagerWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Tutorial.BaseClass), TutorialBaseClassWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Tutorial.TestEnum), TutorialTestEnumWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Tutorial.DerivedClass), TutorialDerivedClassWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Tutorial.ICalc), TutorialICalcWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Tutorial.DerivedClassExtensions), TutorialDerivedClassExtensionsWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Library.BasePanel), LibraryBasePanelWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(App.UI.GameScene.Panel.GamePanel.CountDownPanel), AppUIGameScenePanelGamePanelCountDownPanelWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(App.UI.BaseControl.BasePageFlipping), AppUIBaseControlBasePageFlippingWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(App.Game.GameManager), AppGameGameManagerWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(App.Game.LuaCommand), AppGameLuaCommandWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(App.Game.LuaMediator), AppGameLuaMediatorWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(App.Game.LuaProxy), AppGameLuaProxyWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(App.Game.Spawner.LuaSpawner), AppGameSpawnerLuaSpawnerWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(App.Game.SceneManager.LuaSceneDataManager), AppGameSceneManagerLuaSceneDataManagerWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(App.Game.SceneManager.LuaSceneManager), AppGameSceneManagerLuaSceneManagerWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(App.Game.Object.Tower.LuaTower), AppGameObjectTowerLuaTowerWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(App.Game.Object.Obstacle.LuaObstacle), AppGameObjectObstacleLuaObstacleWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(App.Game.Object.Obstacle.Obstacle), AppGameObjectObstacleObstacleWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(App.Game.Object.Monster.LuaMonster), AppGameObjectMonsterLuaMonsterWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(App.Game.Object.Map.Map), AppGameObjectMapMapWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(App.Game.Generic.Map.Cell), AppGameGenericMapCellWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(App.Game.Generic.Map.Point), AppGameGenericMapPointWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(App.Game.Generic.Map.PointClassToCell), AppGameGenericMapPointClassToCellWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.SceneManagement.SceneManager), UnityEngineSceneManagementSceneManagerWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(App.Data.DataClass.Map.SpawnMonsterData), AppDataDataClassMapSpawnMonsterDataWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(App.Data.DataClass.Game.Object.MonsterData), AppDataDataClassGameObjectMonsterDataWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(App.Data.DataClass.Game.Object.TowerData), AppDataDataClassGameObjectTowerDataWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Tutorial.DerivedClass.TestEnumInner), TutorialDerivedClassTestEnumInnerWrap.__Register);
        
        
        
        }
        
        public static void Init(LuaEnv luaenv, ObjectTranslator translator)
        {
            
            wrapInit0(luaenv, translator);
            
            
            translator.AddInterfaceBridgeCreator(typeof(Tutorial.CSCallLua.ItfD), TutorialCSCallLuaItfDBridge.__Create);
            
        }
	}
}

namespace XLua
{
	internal partial class InternalGlobals_Gen
    {
	    
        private delegate bool TryArrayGet(Type type, RealStatePtr L, ObjectTranslator translator, object obj, int index);
        private delegate bool TryArraySet(Type type, RealStatePtr L, ObjectTranslator translator, object obj, int array_idx, int obj_idx);
	    private static void Init(
            out Dictionary<Type, IEnumerable<MethodInfo>> extensionMethodMap,
            out TryArrayGet genTryArrayGetPtr,
            out TryArraySet genTryArraySetPtr)
		{
            XLua.LuaEnv.AddIniter(XLua.CSObjectWrap.XLua_Gen_Initer_Register__.Init);
            XLua.LuaEnv.AddIniter(XLua.ObjectTranslator_Gen.Init);
		    extensionMethodMap = new Dictionary<Type, IEnumerable<MethodInfo>>()
			{
			    
			};
			
            genTryArrayGetPtr = StaticLuaCallbacks_Wrap.__tryArrayGet;
            genTryArraySetPtr = StaticLuaCallbacks_Wrap.__tryArraySet;
		}
	}
}
