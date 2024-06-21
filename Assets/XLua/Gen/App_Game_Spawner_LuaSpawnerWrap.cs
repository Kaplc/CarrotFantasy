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
    public class AppGameSpawnerLuaSpawnerWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(App.Game.Spawner.LuaSpawner);
			Utils.BeginObjectRegister(type, L, translator, 0, 14, 16, 15);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetCollectingFiresTarget", _m_GetCollectingFiresTarget);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetAllMonsters", _m_GetAllMonsters);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetNowWaveCount", _m_GetNowWaveCount);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Init", _m_Init);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetCollectingFires", _m_SetCollectingFires);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CancelCollectingFiresTarget", _m_CancelCollectingFiresTarget);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "StartSpawn", _m_StartSpawn);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PauseSpawn", _m_PauseSpawn);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ResumeSpawn", _m_ResumeSpawn);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CreateTowerObject", _m_CreateTowerObject);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "UpGradeTower", _m_UpGradeTower);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SellTower", _m_SellTower);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnPushAllGameObject", _m_OnPushAllGameObject);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "WinJudge", _m_WinJudge);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "Carrot", _g_get_Carrot);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onGetCarrotAction", _g_get_onGetCarrotAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onPushAllGameObjectAction", _g_get_onPushAllGameObjectAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onWinJudgeAction", _g_get_onWinJudgeAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onPauseWavesAction", _g_get_onPauseWavesAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onResumeWavesAction", _g_get_onResumeWavesAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onStartSpawnAction", _g_get_onStartSpawnAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onGetAllMonstersAction", _g_get_onGetAllMonstersAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onGetCollectingFiresTargetAction", _g_get_onGetCollectingFiresTargetAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onSetCollectingFiresAction", _g_get_onSetCollectingFiresAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onCancelCollectingFiresTargetAction", _g_get_onCancelCollectingFiresTargetAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onInitAction", _g_get_onInitAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onCreateTowerObjectAction", _g_get_onCreateTowerObjectAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onUpGradeTowerAction", _g_get_onUpGradeTowerAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onSellTowerAction", _g_get_onSellTowerAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onGetNowWaveCountAction", _g_get_onGetNowWaveCountAction);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "onGetCarrotAction", _s_set_onGetCarrotAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onPushAllGameObjectAction", _s_set_onPushAllGameObjectAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onWinJudgeAction", _s_set_onWinJudgeAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onPauseWavesAction", _s_set_onPauseWavesAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onResumeWavesAction", _s_set_onResumeWavesAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onStartSpawnAction", _s_set_onStartSpawnAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onGetAllMonstersAction", _s_set_onGetAllMonstersAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onGetCollectingFiresTargetAction", _s_set_onGetCollectingFiresTargetAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onSetCollectingFiresAction", _s_set_onSetCollectingFiresAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onCancelCollectingFiresTargetAction", _s_set_onCancelCollectingFiresTargetAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onInitAction", _s_set_onInitAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onCreateTowerObjectAction", _s_set_onCreateTowerObjectAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onUpGradeTowerAction", _s_set_onUpGradeTowerAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onSellTowerAction", _s_set_onSellTowerAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onGetNowWaveCountAction", _s_set_onGetNowWaveCountAction);
            
			
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
					
					var gen_ret = new App.Game.Spawner.LuaSpawner();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to App.Game.Spawner.LuaSpawner constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetCollectingFiresTarget(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.Spawner.LuaSpawner gen_to_be_invoked = (App.Game.Spawner.LuaSpawner)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.GetCollectingFiresTarget(  );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetAllMonsters(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.Spawner.LuaSpawner gen_to_be_invoked = (App.Game.Spawner.LuaSpawner)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.GetAllMonsters(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetNowWaveCount(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.Spawner.LuaSpawner gen_to_be_invoked = (App.Game.Spawner.LuaSpawner)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.GetNowWaveCount(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Init(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.Spawner.LuaSpawner gen_to_be_invoked = (App.Game.Spawner.LuaSpawner)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    App.Data.DataClass.Map.IMapData _mapData = (App.Data.DataClass.Map.IMapData)translator.GetObject(L, 2, typeof(App.Data.DataClass.Map.IMapData));
                    
                    gen_to_be_invoked.Init( _mapData );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetCollectingFires(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.Spawner.LuaSpawner gen_to_be_invoked = (App.Game.Spawner.LuaSpawner)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    App.Game.Object.Monster.IMonster _monster = (App.Game.Object.Monster.IMonster)translator.GetObject(L, 2, typeof(App.Game.Object.Monster.IMonster));
                    
                    gen_to_be_invoked.SetCollectingFires( _monster );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CancelCollectingFiresTarget(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.Spawner.LuaSpawner gen_to_be_invoked = (App.Game.Spawner.LuaSpawner)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.CancelCollectingFiresTarget(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StartSpawn(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.Spawner.LuaSpawner gen_to_be_invoked = (App.Game.Spawner.LuaSpawner)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.StartSpawn(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PauseSpawn(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.Spawner.LuaSpawner gen_to_be_invoked = (App.Game.Spawner.LuaSpawner)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.PauseSpawn(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ResumeSpawn(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.Spawner.LuaSpawner gen_to_be_invoked = (App.Game.Spawner.LuaSpawner)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.ResumeSpawn(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateTowerObject(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.Spawner.LuaSpawner gen_to_be_invoked = (App.Game.Spawner.LuaSpawner)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    App.Data.DataClass.Game.Object.TowerData _towerData = (App.Data.DataClass.Game.Object.TowerData)translator.GetObject(L, 2, typeof(App.Data.DataClass.Game.Object.TowerData));
                    UnityEngine.Vector3 _cellWorldPos;translator.Get(L, 3, out _cellWorldPos);
                    
                    gen_to_be_invoked.CreateTowerObject( _towerData, _cellWorldPos );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UpGradeTower(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.Spawner.LuaSpawner gen_to_be_invoked = (App.Game.Spawner.LuaSpawner)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Vector3 _cellWorldPos;translator.Get(L, 2, out _cellWorldPos);
                    
                    gen_to_be_invoked.UpGradeTower( _cellWorldPos );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SellTower(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.Spawner.LuaSpawner gen_to_be_invoked = (App.Game.Spawner.LuaSpawner)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Vector3 _cellWorldPos;translator.Get(L, 2, out _cellWorldPos);
                    
                    gen_to_be_invoked.SellTower( _cellWorldPos );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnPushAllGameObject(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.Spawner.LuaSpawner gen_to_be_invoked = (App.Game.Spawner.LuaSpawner)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.OnPushAllGameObject(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WinJudge(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.Spawner.LuaSpawner gen_to_be_invoked = (App.Game.Spawner.LuaSpawner)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.WinJudge(  );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Carrot(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Spawner.LuaSpawner gen_to_be_invoked = (App.Game.Spawner.LuaSpawner)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.Carrot);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onGetCarrotAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Spawner.LuaSpawner gen_to_be_invoked = (App.Game.Spawner.LuaSpawner)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onGetCarrotAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onPushAllGameObjectAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Spawner.LuaSpawner gen_to_be_invoked = (App.Game.Spawner.LuaSpawner)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onPushAllGameObjectAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onWinJudgeAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Spawner.LuaSpawner gen_to_be_invoked = (App.Game.Spawner.LuaSpawner)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onWinJudgeAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onPauseWavesAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Spawner.LuaSpawner gen_to_be_invoked = (App.Game.Spawner.LuaSpawner)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onPauseWavesAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onResumeWavesAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Spawner.LuaSpawner gen_to_be_invoked = (App.Game.Spawner.LuaSpawner)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onResumeWavesAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onStartSpawnAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Spawner.LuaSpawner gen_to_be_invoked = (App.Game.Spawner.LuaSpawner)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onStartSpawnAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onGetAllMonstersAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Spawner.LuaSpawner gen_to_be_invoked = (App.Game.Spawner.LuaSpawner)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onGetAllMonstersAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onGetCollectingFiresTargetAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Spawner.LuaSpawner gen_to_be_invoked = (App.Game.Spawner.LuaSpawner)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onGetCollectingFiresTargetAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onSetCollectingFiresAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Spawner.LuaSpawner gen_to_be_invoked = (App.Game.Spawner.LuaSpawner)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onSetCollectingFiresAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onCancelCollectingFiresTargetAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Spawner.LuaSpawner gen_to_be_invoked = (App.Game.Spawner.LuaSpawner)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onCancelCollectingFiresTargetAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onInitAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Spawner.LuaSpawner gen_to_be_invoked = (App.Game.Spawner.LuaSpawner)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onInitAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onCreateTowerObjectAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Spawner.LuaSpawner gen_to_be_invoked = (App.Game.Spawner.LuaSpawner)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onCreateTowerObjectAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onUpGradeTowerAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Spawner.LuaSpawner gen_to_be_invoked = (App.Game.Spawner.LuaSpawner)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onUpGradeTowerAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onSellTowerAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Spawner.LuaSpawner gen_to_be_invoked = (App.Game.Spawner.LuaSpawner)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onSellTowerAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onGetNowWaveCountAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Spawner.LuaSpawner gen_to_be_invoked = (App.Game.Spawner.LuaSpawner)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onGetNowWaveCountAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onGetCarrotAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Spawner.LuaSpawner gen_to_be_invoked = (App.Game.Spawner.LuaSpawner)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onGetCarrotAction = translator.GetDelegate<System.Func<App.Game.Object.Carrot.Carrot>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onPushAllGameObjectAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Spawner.LuaSpawner gen_to_be_invoked = (App.Game.Spawner.LuaSpawner)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onPushAllGameObjectAction = translator.GetDelegate<UnityEngine.Events.UnityAction>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onWinJudgeAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Spawner.LuaSpawner gen_to_be_invoked = (App.Game.Spawner.LuaSpawner)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onWinJudgeAction = translator.GetDelegate<System.Func<bool>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onPauseWavesAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Spawner.LuaSpawner gen_to_be_invoked = (App.Game.Spawner.LuaSpawner)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onPauseWavesAction = translator.GetDelegate<UnityEngine.Events.UnityAction>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onResumeWavesAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Spawner.LuaSpawner gen_to_be_invoked = (App.Game.Spawner.LuaSpawner)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onResumeWavesAction = translator.GetDelegate<UnityEngine.Events.UnityAction>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onStartSpawnAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Spawner.LuaSpawner gen_to_be_invoked = (App.Game.Spawner.LuaSpawner)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onStartSpawnAction = translator.GetDelegate<UnityEngine.Events.UnityAction>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onGetAllMonstersAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Spawner.LuaSpawner gen_to_be_invoked = (App.Game.Spawner.LuaSpawner)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onGetAllMonstersAction = translator.GetDelegate<System.Func<System.Collections.Generic.List<App.Game.Object.Monster.IMonster>>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onGetCollectingFiresTargetAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Spawner.LuaSpawner gen_to_be_invoked = (App.Game.Spawner.LuaSpawner)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onGetCollectingFiresTargetAction = translator.GetDelegate<System.Func<App.Game.Object.Monster.IMonster>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onSetCollectingFiresAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Spawner.LuaSpawner gen_to_be_invoked = (App.Game.Spawner.LuaSpawner)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onSetCollectingFiresAction = translator.GetDelegate<UnityEngine.Events.UnityAction<App.Game.Object.Monster.IMonster>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onCancelCollectingFiresTargetAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Spawner.LuaSpawner gen_to_be_invoked = (App.Game.Spawner.LuaSpawner)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onCancelCollectingFiresTargetAction = translator.GetDelegate<UnityEngine.Events.UnityAction>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onInitAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Spawner.LuaSpawner gen_to_be_invoked = (App.Game.Spawner.LuaSpawner)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onInitAction = translator.GetDelegate<UnityEngine.Events.UnityAction<App.Data.DataClass.Map.IMapData>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onCreateTowerObjectAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Spawner.LuaSpawner gen_to_be_invoked = (App.Game.Spawner.LuaSpawner)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onCreateTowerObjectAction = translator.GetDelegate<UnityEngine.Events.UnityAction<App.Data.DataClass.Game.Object.TowerData, UnityEngine.Vector3>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onUpGradeTowerAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Spawner.LuaSpawner gen_to_be_invoked = (App.Game.Spawner.LuaSpawner)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onUpGradeTowerAction = translator.GetDelegate<UnityEngine.Events.UnityAction<UnityEngine.Vector3>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onSellTowerAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Spawner.LuaSpawner gen_to_be_invoked = (App.Game.Spawner.LuaSpawner)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onSellTowerAction = translator.GetDelegate<UnityEngine.Events.UnityAction<UnityEngine.Vector3>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onGetNowWaveCountAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Spawner.LuaSpawner gen_to_be_invoked = (App.Game.Spawner.LuaSpawner)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onGetNowWaveCountAction = translator.GetDelegate<System.Func<int>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
