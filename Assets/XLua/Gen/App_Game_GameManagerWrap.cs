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
    public class AppGameGameManagerWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(App.Game.GameManager);
			Utils.BeginObjectRegister(type, L, translator, 0, 7, 13, 13);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetSceneManager", _m_SetSceneManager);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LoadGameScene", _m_LoadGameScene);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LoadScene", _m_LoadScene);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SaveStatisticalData", _m_SaveStatisticalData);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "StopMusic", _m_StopMusic);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PlayMusic", _m_PlayMusic);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PlaySound", _m_PlaySound);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "poolManager", _g_get_poolManager);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "binaryManager", _g_get_binaryManager);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "factoryManager", _g_get_factoryManager);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "musicManger", _g_get_musicManger);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "buffManager", _g_get_buffManager);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "xLuaManager", _g_get_xLuaManager);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "uiManager", _g_get_uiManager);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "eventCenter", _g_get_eventCenter);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "sdkManager", _g_get_sdkManager);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "loadSceneManager", _g_get_loadSceneManager);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "addressablesesManager", _g_get_addressablesesManager);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "sceneManager", _g_get_sceneManager);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "dataManager", _g_get_dataManager);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "poolManager", _s_set_poolManager);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "binaryManager", _s_set_binaryManager);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "factoryManager", _s_set_factoryManager);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "musicManger", _s_set_musicManger);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "buffManager", _s_set_buffManager);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "xLuaManager", _s_set_xLuaManager);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "uiManager", _s_set_uiManager);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "eventCenter", _s_set_eventCenter);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "sdkManager", _s_set_sdkManager);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "loadSceneManager", _s_set_loadSceneManager);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "addressablesesManager", _s_set_addressablesesManager);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "sceneManager", _s_set_sceneManager);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "dataManager", _s_set_dataManager);
            
			
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
					
					var gen_ret = new App.Game.GameManager();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to App.Game.GameManager constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetSceneManager(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.GameManager gen_to_be_invoked = (App.Game.GameManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    App.Game.SceneManager.ISceneManger _manger = (App.Game.SceneManager.ISceneManger)translator.GetObject(L, 2, typeof(App.Game.SceneManager.ISceneManger));
                    
                    gen_to_be_invoked.SetSceneManager( _manger );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadGameScene(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.GameManager gen_to_be_invoked = (App.Game.GameManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _sceneName = LuaAPI.lua_tostring(L, 2);
                    int _levelID = LuaAPI.xlua_tointeger(L, 3);
                    
                    gen_to_be_invoked.LoadGameScene( _sceneName, _levelID );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadScene(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.GameManager gen_to_be_invoked = (App.Game.GameManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _sceneName = LuaAPI.lua_tostring(L, 2);
                    UnityEngine.Events.UnityAction _callBack = translator.GetDelegate<UnityEngine.Events.UnityAction>(L, 3);
                    
                    gen_to_be_invoked.LoadScene( _sceneName, _callBack );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SaveStatisticalData(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.GameManager gen_to_be_invoked = (App.Game.GameManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    App.Data.DataClass.Player.StatisticalData _data = (App.Data.DataClass.Player.StatisticalData)translator.GetObject(L, 2, typeof(App.Data.DataClass.Player.StatisticalData));
                    
                    gen_to_be_invoked.SaveStatisticalData( _data );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StopMusic(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.GameManager gen_to_be_invoked = (App.Game.GameManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.StopMusic(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PlayMusic(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.GameManager gen_to_be_invoked = (App.Game.GameManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.PlayMusic(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PlaySound(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.GameManager gen_to_be_invoked = (App.Game.GameManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 2);
                    float _volume = (float)LuaAPI.lua_tonumber(L, 3);
                    bool _loop = LuaAPI.lua_toboolean(L, 4);
                    
                    gen_to_be_invoked.PlaySound( _path, _volume, _loop );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_poolManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.GameManager gen_to_be_invoked = (App.Game.GameManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.poolManager);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_binaryManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.GameManager gen_to_be_invoked = (App.Game.GameManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.binaryManager);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_factoryManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.GameManager gen_to_be_invoked = (App.Game.GameManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.factoryManager);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_musicManger(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.GameManager gen_to_be_invoked = (App.Game.GameManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.musicManger);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_buffManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.GameManager gen_to_be_invoked = (App.Game.GameManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.buffManager);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_xLuaManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.GameManager gen_to_be_invoked = (App.Game.GameManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.xLuaManager);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_uiManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.GameManager gen_to_be_invoked = (App.Game.GameManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.uiManager);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_eventCenter(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.GameManager gen_to_be_invoked = (App.Game.GameManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.eventCenter);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_sdkManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.GameManager gen_to_be_invoked = (App.Game.GameManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.sdkManager);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_loadSceneManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.GameManager gen_to_be_invoked = (App.Game.GameManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.loadSceneManager);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_addressablesesManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.GameManager gen_to_be_invoked = (App.Game.GameManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.addressablesesManager);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_sceneManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.GameManager gen_to_be_invoked = (App.Game.GameManager)translator.FastGetCSObj(L, 1);
                translator.PushAny(L, gen_to_be_invoked.sceneManager);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_dataManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.GameManager gen_to_be_invoked = (App.Game.GameManager)translator.FastGetCSObj(L, 1);
                translator.PushAny(L, gen_to_be_invoked.dataManager);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_poolManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.GameManager gen_to_be_invoked = (App.Game.GameManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.poolManager = (Library.PoolManager)translator.GetObject(L, 2, typeof(Library.PoolManager));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_binaryManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.GameManager gen_to_be_invoked = (App.Game.GameManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.binaryManager = (Library.BinaryManager)translator.GetObject(L, 2, typeof(Library.BinaryManager));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_factoryManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.GameManager gen_to_be_invoked = (App.Game.GameManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.factoryManager = (App.Game.Factory.FactoryManager)translator.GetObject(L, 2, typeof(App.Game.Factory.FactoryManager));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_musicManger(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.GameManager gen_to_be_invoked = (App.Game.GameManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.musicManger = (Library.MusicManger)translator.GetObject(L, 2, typeof(Library.MusicManger));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_buffManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.GameManager gen_to_be_invoked = (App.Game.GameManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.buffManager = (App.Game.Buff.BuffManager)translator.GetObject(L, 2, typeof(App.Game.Buff.BuffManager));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_xLuaManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.GameManager gen_to_be_invoked = (App.Game.GameManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.xLuaManager = (Library.XLuaManager)translator.GetObject(L, 2, typeof(Library.XLuaManager));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_uiManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.GameManager gen_to_be_invoked = (App.Game.GameManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.uiManager = (UIManager)translator.GetObject(L, 2, typeof(UIManager));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_eventCenter(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.GameManager gen_to_be_invoked = (App.Game.GameManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.eventCenter = (Library.EventCenter)translator.GetObject(L, 2, typeof(Library.EventCenter));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_sdkManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.GameManager gen_to_be_invoked = (App.Game.GameManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.sdkManager = (App.Game.SDK.SDKManager)translator.GetObject(L, 2, typeof(App.Game.SDK.SDKManager));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_loadSceneManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.GameManager gen_to_be_invoked = (App.Game.GameManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.loadSceneManager = (Library.ZFrameWorkSceneManager)translator.GetObject(L, 2, typeof(Library.ZFrameWorkSceneManager));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_addressablesesManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.GameManager gen_to_be_invoked = (App.Game.GameManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.addressablesesManager = (AddressablesesManager)translator.GetObject(L, 2, typeof(AddressablesesManager));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_sceneManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.GameManager gen_to_be_invoked = (App.Game.GameManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.sceneManager = (App.Game.SceneManager.ISceneManger)translator.GetObject(L, 2, typeof(App.Game.SceneManager.ISceneManger));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_dataManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.GameManager gen_to_be_invoked = (App.Game.GameManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.dataManager = (App.Data.IDataManager)translator.GetObject(L, 2, typeof(App.Data.IDataManager));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
