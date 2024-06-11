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
    public class AppMVCViewGameSceneObjectTowerBottleTowerWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(App.MVC.View.GameScene.Object.Tower.BottleTower);
			Utils.BeginObjectRegister(type, L, translator, 0, 1, 2, 2);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Attack", _m_Attack);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "weapon", _g_get_weapon);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "firePos", _g_get_firePos);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "weapon", _s_set_weapon);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "firePos", _s_set_firePos);
            
			
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
					
					var gen_ret = new App.MVC.View.GameScene.Object.Tower.BottleTower();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to App.MVC.View.GameScene.Object.Tower.BottleTower constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Attack(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.MVC.View.GameScene.Object.Tower.BottleTower gen_to_be_invoked = (App.MVC.View.GameScene.Object.Tower.BottleTower)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Attack(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_weapon(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.MVC.View.GameScene.Object.Tower.BottleTower gen_to_be_invoked = (App.MVC.View.GameScene.Object.Tower.BottleTower)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.weapon);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_firePos(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.MVC.View.GameScene.Object.Tower.BottleTower gen_to_be_invoked = (App.MVC.View.GameScene.Object.Tower.BottleTower)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.firePos);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_weapon(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.MVC.View.GameScene.Object.Tower.BottleTower gen_to_be_invoked = (App.MVC.View.GameScene.Object.Tower.BottleTower)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.weapon = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_firePos(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.MVC.View.GameScene.Object.Tower.BottleTower gen_to_be_invoked = (App.MVC.View.GameScene.Object.Tower.BottleTower)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.firePos = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
