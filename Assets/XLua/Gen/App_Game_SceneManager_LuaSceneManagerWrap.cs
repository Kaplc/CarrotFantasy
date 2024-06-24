#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class AppGameSceneManagerLuaSceneManagerWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(App.Game.SceneManager.LuaSceneManager);
			Utils.BeginObjectRegister(type, L, translator, 0, 20, 28, 24);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetSpawner", _m_SetSpawner);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetSceneDataManager", _m_SetSceneDataManager);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "InitGame", _m_InitGame);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "StartGame", _m_StartGame);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PauseGame", _m_PauseGame);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ResumeGame", _m_ResumeGame);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RestartGame", _m_RestartGame);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "EndGame", _m_EndGame);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GameOver", _m_GameOver);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GameWin", _m_GameWin);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "NextLevel", _m_NextLevel);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetSpeedUp", _m_SetSpeedUp);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsPause", _m_IsPause);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsStop", _m_IsStop);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetFireTarget", _m_SetFireTarget);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CancelFire", _m_CancelFire);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetMoney", _m_GetMoney);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "UpdateKillMonsterCount", _m_UpdateKillMonsterCount);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "UpdateMoney", _m_UpdateMoney);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ExitScene", _m_ExitScene);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "SceneDataManager", _g_get_SceneDataManager);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Spawner", _g_get_Spawner);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "MapData", _g_get_MapData);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsSpeedUp", _g_get_IsSpeedUp);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onGetSceneDataManagerAction", _g_get_onGetSceneDataManagerAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onGetSpawnerAction", _g_get_onGetSpawnerAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onGetMapDataAction", _g_get_onGetMapDataAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onSetSpawnerAction", _g_get_onSetSpawnerAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onSetSceneDataManagerAction", _g_get_onSetSceneDataManagerAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onInitGameAction", _g_get_onInitGameAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onStartGameAction", _g_get_onStartGameAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onPauseGameAction", _g_get_onPauseGameAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onResumeGameAction", _g_get_onResumeGameAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onRestartGameAction", _g_get_onRestartGameAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onEndGameAction", _g_get_onEndGameAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onGameOverAction", _g_get_onGameOverAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onGameWinAction", _g_get_onGameWinAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onNextLevelAction", _g_get_onNextLevelAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onSetSpeedUpAction", _g_get_onSetSpeedUpAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onIsSpeedUpFunc", _g_get_onIsSpeedUpFunc);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onIsPauseFunc", _g_get_onIsPauseFunc);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onIsStopFunc", _g_get_onIsStopFunc);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onSetFireTargetAction", _g_get_onSetFireTargetAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onCancelFireAction", _g_get_onCancelFireAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onGetMoneyFunc", _g_get_onGetMoneyFunc);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onUpdateKillMonsterCountAction", _g_get_onUpdateKillMonsterCountAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onUpdateMoneyAction", _g_get_onUpdateMoneyAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onExitSceneAction", _g_get_onExitSceneAction);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "onGetSceneDataManagerAction", _s_set_onGetSceneDataManagerAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onGetSpawnerAction", _s_set_onGetSpawnerAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onGetMapDataAction", _s_set_onGetMapDataAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onSetSpawnerAction", _s_set_onSetSpawnerAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onSetSceneDataManagerAction", _s_set_onSetSceneDataManagerAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onInitGameAction", _s_set_onInitGameAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onStartGameAction", _s_set_onStartGameAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onPauseGameAction", _s_set_onPauseGameAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onResumeGameAction", _s_set_onResumeGameAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onRestartGameAction", _s_set_onRestartGameAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onEndGameAction", _s_set_onEndGameAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onGameOverAction", _s_set_onGameOverAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onGameWinAction", _s_set_onGameWinAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onNextLevelAction", _s_set_onNextLevelAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onSetSpeedUpAction", _s_set_onSetSpeedUpAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onIsSpeedUpFunc", _s_set_onIsSpeedUpFunc);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onIsPauseFunc", _s_set_onIsPauseFunc);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onIsStopFunc", _s_set_onIsStopFunc);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onSetFireTargetAction", _s_set_onSetFireTargetAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onCancelFireAction", _s_set_onCancelFireAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onGetMoneyFunc", _s_set_onGetMoneyFunc);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onUpdateKillMonsterCountAction", _s_set_onUpdateKillMonsterCountAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onUpdateMoneyAction", _s_set_onUpdateMoneyAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onExitSceneAction", _s_set_onExitSceneAction);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					var gen_ret = new App.Game.SceneManager.LuaSceneManager();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to App.Game.SceneManager.LuaSceneManager constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetSpawner(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    App.Game.Spawner.ISpawner _s = (App.Game.Spawner.ISpawner)translator.GetObject(L, 2, typeof(App.Game.Spawner.ISpawner));
                    
                    gen_to_be_invoked.SetSpawner( _s );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetSceneDataManager(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    App.Game.SceneManager.ISceneDataManager _m = (App.Game.SceneManager.ISceneDataManager)translator.GetObject(L, 2, typeof(App.Game.SceneManager.ISceneDataManager));
                    
                    gen_to_be_invoked.SetSceneDataManager( _m );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_InitGame(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _levelID = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.InitGame( _levelID );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StartGame(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.StartGame(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PauseGame(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.PauseGame(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ResumeGame(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.ResumeGame(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RestartGame(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.RestartGame(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EndGame(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.EndGame(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GameOver(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.GameOver(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GameWin(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.GameWin(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_NextLevel(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.NextLevel(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetSpeedUp(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _isSpeedUp = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.SetSpeedUp( _isSpeedUp );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsPause(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.IsPause(  );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsStop(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.IsStop(  );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetFireTarget(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    App.Game.Object.Monster.IMonster _monster = (App.Game.Object.Monster.IMonster)translator.GetObject(L, 2, typeof(App.Game.Object.Monster.IMonster));
                    
                    gen_to_be_invoked.SetFireTarget( _monster );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CancelFire(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.CancelFire(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetMoney(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.GetMoney(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UpdateKillMonsterCount(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _v = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.UpdateKillMonsterCount( _v );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UpdateMoney(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _v = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.UpdateMoney( _v );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ExitScene(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.ExitScene(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_SceneDataManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                translator.PushAny(L, gen_to_be_invoked.SceneDataManager);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Spawner(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                translator.PushAny(L, gen_to_be_invoked.Spawner);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_MapData(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                translator.PushAny(L, gen_to_be_invoked.MapData);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsSpeedUp(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.IsSpeedUp);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onGetSceneDataManagerAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onGetSceneDataManagerAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onGetSpawnerAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onGetSpawnerAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onGetMapDataAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onGetMapDataAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onSetSpawnerAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onSetSpawnerAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onSetSceneDataManagerAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onSetSceneDataManagerAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onInitGameAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onInitGameAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onStartGameAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onStartGameAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onPauseGameAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onPauseGameAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onResumeGameAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onResumeGameAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onRestartGameAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onRestartGameAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onEndGameAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onEndGameAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onGameOverAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onGameOverAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onGameWinAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onGameWinAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onNextLevelAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onNextLevelAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onSetSpeedUpAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onSetSpeedUpAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onIsSpeedUpFunc(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onIsSpeedUpFunc);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onIsPauseFunc(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onIsPauseFunc);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onIsStopFunc(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onIsStopFunc);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onSetFireTargetAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onSetFireTargetAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onCancelFireAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onCancelFireAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onGetMoneyFunc(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onGetMoneyFunc);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onUpdateKillMonsterCountAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onUpdateKillMonsterCountAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onUpdateMoneyAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onUpdateMoneyAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onExitSceneAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onExitSceneAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onGetSceneDataManagerAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onGetSceneDataManagerAction = translator.GetDelegate<System.Func<App.Game.SceneManager.ISceneDataManager>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onGetSpawnerAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onGetSpawnerAction = translator.GetDelegate<System.Func<App.Game.Spawner.ISpawner>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onGetMapDataAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onGetMapDataAction = translator.GetDelegate<System.Func<App.Data.DataClass.Map.IMapData>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onSetSpawnerAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onSetSpawnerAction = translator.GetDelegate<UnityEngine.Events.UnityAction<App.Game.Spawner.ISpawner>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onSetSceneDataManagerAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onSetSceneDataManagerAction = translator.GetDelegate<UnityEngine.Events.UnityAction<App.Game.SceneManager.ISceneDataManager>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onInitGameAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onInitGameAction = translator.GetDelegate<UnityEngine.Events.UnityAction<int>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onStartGameAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onStartGameAction = translator.GetDelegate<UnityEngine.Events.UnityAction>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onPauseGameAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onPauseGameAction = translator.GetDelegate<UnityEngine.Events.UnityAction>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onResumeGameAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onResumeGameAction = translator.GetDelegate<UnityEngine.Events.UnityAction>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onRestartGameAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onRestartGameAction = translator.GetDelegate<UnityEngine.Events.UnityAction>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onEndGameAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onEndGameAction = translator.GetDelegate<UnityEngine.Events.UnityAction>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onGameOverAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onGameOverAction = translator.GetDelegate<UnityEngine.Events.UnityAction>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onGameWinAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onGameWinAction = translator.GetDelegate<UnityEngine.Events.UnityAction>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onNextLevelAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onNextLevelAction = translator.GetDelegate<UnityEngine.Events.UnityAction>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onSetSpeedUpAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onSetSpeedUpAction = translator.GetDelegate<UnityEngine.Events.UnityAction<bool>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onIsSpeedUpFunc(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onIsSpeedUpFunc = translator.GetDelegate<System.Func<bool>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onIsPauseFunc(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onIsPauseFunc = translator.GetDelegate<System.Func<bool>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onIsStopFunc(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onIsStopFunc = translator.GetDelegate<System.Func<bool>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onSetFireTargetAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onSetFireTargetAction = translator.GetDelegate<UnityEngine.Events.UnityAction<App.Game.Object.Monster.IMonster>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onCancelFireAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onCancelFireAction = translator.GetDelegate<UnityEngine.Events.UnityAction>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onGetMoneyFunc(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onGetMoneyFunc = translator.GetDelegate<System.Func<int>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onUpdateKillMonsterCountAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onUpdateKillMonsterCountAction = translator.GetDelegate<UnityEngine.Events.UnityAction<int>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onUpdateMoneyAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onUpdateMoneyAction = translator.GetDelegate<UnityEngine.Events.UnityAction<int>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onExitSceneAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.SceneManager.LuaSceneManager gen_to_be_invoked = (App.Game.SceneManager.LuaSceneManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onExitSceneAction = translator.GetDelegate<UnityEngine.Events.UnityAction>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
