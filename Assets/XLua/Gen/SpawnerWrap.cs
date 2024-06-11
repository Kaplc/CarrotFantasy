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
    public class SpawnerWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Spawner);
			Utils.BeginObjectRegister(type, L, translator, 0, 16, 10, 10);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetCarrot", _m_GetCarrot);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetAllMonsters", _m_GetAllMonsters);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Init", _m_Init);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetCollectingFiresTarget", _m_GetCollectingFiresTarget);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetCollectingFiresTarget", _m_SetCollectingFiresTarget);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CancelCollectingFiresTarget", _m_CancelCollectingFiresTarget);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "StartSpawn", _m_StartSpawn);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PauseWaves", _m_PauseWaves);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ResumeWaves", _m_ResumeWaves);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "UpGradeTower", _m_UpGradeTower);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SellTower", _m_SellTower);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetCollectingFires", _m_SetCollectingFires);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CreateTowerObject", _m_CreateTowerObject);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnPushAllGameObject", _m_OnPushAllGameObject);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetNowWaveCount", _m_GetNowWaveCount);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "WinJudge", _m_WinJudge);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "carrot", _g_get_carrot);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "startPoint", _g_get_startPoint);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "monsters", _g_get_monsters);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "towers", _g_get_towers);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "obstaclesList", _g_get_obstaclesList);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "collectingFiresTarget", _g_get_collectingFiresTarget);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "signTrans", _g_get_signTrans);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "spawnedComplete", _g_get_spawnedComplete);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "waveDataList", _g_get_waveDataList);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "initAction", _g_get_initAction);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "carrot", _s_set_carrot);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "startPoint", _s_set_startPoint);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "monsters", _s_set_monsters);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "towers", _s_set_towers);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "obstaclesList", _s_set_obstaclesList);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "collectingFiresTarget", _s_set_collectingFiresTarget);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "signTrans", _s_set_signTrans);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "spawnedComplete", _s_set_spawnedComplete);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "waveDataList", _s_set_waveDataList);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "initAction", _s_set_initAction);
            
			
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
					
					var gen_ret = new Spawner();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to Spawner constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetCarrot(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Spawner gen_to_be_invoked = (Spawner)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.GetCarrot(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
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
            
            
                Spawner gen_to_be_invoked = (Spawner)translator.FastGetCSObj(L, 1);
            
            
                
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
        static int _m_Init(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Spawner gen_to_be_invoked = (Spawner)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    App.DataClass.Map.MapData _data = (App.DataClass.Map.MapData)translator.GetObject(L, 2, typeof(App.DataClass.Map.MapData));
                    
                    gen_to_be_invoked.Init( _data );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetCollectingFiresTarget(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Spawner gen_to_be_invoked = (Spawner)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.GetCollectingFiresTarget(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetCollectingFiresTarget(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Spawner gen_to_be_invoked = (Spawner)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    App.MVC.View.GameScene.Object.Monster _monster = (App.MVC.View.GameScene.Object.Monster)translator.GetObject(L, 2, typeof(App.MVC.View.GameScene.Object.Monster));
                    
                    gen_to_be_invoked.SetCollectingFiresTarget( _monster );
                    
                    
                    
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
            
            
                Spawner gen_to_be_invoked = (Spawner)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                Spawner gen_to_be_invoked = (Spawner)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.StartSpawn(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PauseWaves(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Spawner gen_to_be_invoked = (Spawner)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.PauseWaves(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ResumeWaves(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Spawner gen_to_be_invoked = (Spawner)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.ResumeWaves(  );
                    
                    
                    
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
            
            
                Spawner gen_to_be_invoked = (Spawner)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                Spawner gen_to_be_invoked = (Spawner)translator.FastGetCSObj(L, 1);
            
            
                
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
        static int _m_SetCollectingFires(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Spawner gen_to_be_invoked = (Spawner)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    App.MVC.View.GameScene.Object.Monster _monster = (App.MVC.View.GameScene.Object.Monster)translator.GetObject(L, 2, typeof(App.MVC.View.GameScene.Object.Monster));
                    
                    gen_to_be_invoked.SetCollectingFires( _monster );
                    
                    
                    
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
            
            
                Spawner gen_to_be_invoked = (Spawner)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    App.DataClass.Game.Object.TowerData _towerData = (App.DataClass.Game.Object.TowerData)translator.GetObject(L, 2, typeof(App.DataClass.Game.Object.TowerData));
                    UnityEngine.Vector3 _cellWorldPos;translator.Get(L, 3, out _cellWorldPos);
                    
                    gen_to_be_invoked.CreateTowerObject( _towerData, _cellWorldPos );
                    
                    
                    
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
            
            
                Spawner gen_to_be_invoked = (Spawner)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.OnPushAllGameObject(  );
                    
                    
                    
                    return 0;
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
            
            
                Spawner gen_to_be_invoked = (Spawner)translator.FastGetCSObj(L, 1);
            
            
                
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
        static int _m_WinJudge(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Spawner gen_to_be_invoked = (Spawner)translator.FastGetCSObj(L, 1);
            
            
                
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
        static int _g_get_carrot(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spawner gen_to_be_invoked = (Spawner)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.carrot);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_startPoint(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spawner gen_to_be_invoked = (Spawner)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.startPoint);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_monsters(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spawner gen_to_be_invoked = (Spawner)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.monsters);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_towers(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spawner gen_to_be_invoked = (Spawner)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.towers);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_obstaclesList(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spawner gen_to_be_invoked = (Spawner)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.obstaclesList);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_collectingFiresTarget(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spawner gen_to_be_invoked = (Spawner)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.collectingFiresTarget);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_signTrans(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spawner gen_to_be_invoked = (Spawner)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.signTrans);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_spawnedComplete(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spawner gen_to_be_invoked = (Spawner)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.spawnedComplete);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_waveDataList(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spawner gen_to_be_invoked = (Spawner)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.waveDataList);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_initAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spawner gen_to_be_invoked = (Spawner)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.initAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_carrot(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spawner gen_to_be_invoked = (Spawner)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.carrot = (App.MVC.View.GameScene.Object.Carrot.Carrot)translator.GetObject(L, 2, typeof(App.MVC.View.GameScene.Object.Carrot.Carrot));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_startPoint(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spawner gen_to_be_invoked = (Spawner)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.startPoint = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_monsters(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spawner gen_to_be_invoked = (Spawner)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.monsters = (System.Collections.Generic.List<App.MVC.View.GameScene.Object.Monster>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<App.MVC.View.GameScene.Object.Monster>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_towers(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spawner gen_to_be_invoked = (Spawner)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.towers = (System.Collections.Generic.List<App.Generic.BaseObject.BaseTower>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<App.Generic.BaseObject.BaseTower>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_obstaclesList(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spawner gen_to_be_invoked = (Spawner)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.obstaclesList = (System.Collections.Generic.List<App.MVC.View.GameScene.Object.IObstacle>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<App.MVC.View.GameScene.Object.IObstacle>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_collectingFiresTarget(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spawner gen_to_be_invoked = (Spawner)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.collectingFiresTarget = (App.MVC.View.GameScene.Object.Monster)translator.GetObject(L, 2, typeof(App.MVC.View.GameScene.Object.Monster));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_signTrans(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spawner gen_to_be_invoked = (Spawner)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.signTrans = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_spawnedComplete(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spawner gen_to_be_invoked = (Spawner)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.spawnedComplete = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_waveDataList(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spawner gen_to_be_invoked = (Spawner)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.waveDataList = (System.Collections.Generic.List<App.DataClass.Map.WaveData>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<App.DataClass.Map.WaveData>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_initAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spawner gen_to_be_invoked = (Spawner)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.initAction = translator.GetDelegate<UnityEngine.Events.UnityAction<App.DataClass.Map.MapData>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
