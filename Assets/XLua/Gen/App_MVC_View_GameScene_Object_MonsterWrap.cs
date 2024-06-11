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
    public class AppMVCViewGameSceneObjectMonsterWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(App.MVC.View.GameScene.Object.Monster);
			Utils.BeginObjectRegister(type, L, translator, 0, 5, 10, 9);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Init", _m_Init);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Wound", _m_Wound);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetSpeed", _m_SetSpeed);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnPush", _m_OnPush);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnGet", _m_OnGet);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "Hp", _g_get_Hp);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Growth", _g_get_Growth);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Data", _g_get_Data);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "data", _g_get_data);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "hp", _g_get_hp);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "speed", _g_get_speed);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "animator", _g_get_animator);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "hpImageBg", _g_get_hpImageBg);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "hpImageFg", _g_get_hpImageFg);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "signFather", _g_get_signFather);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "Hp", _s_set_Hp);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "Growth", _s_set_Growth);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "data", _s_set_data);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "hp", _s_set_hp);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "speed", _s_set_speed);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "animator", _s_set_animator);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "hpImageBg", _s_set_hpImageBg);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "hpImageFg", _s_set_hpImageFg);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "signFather", _s_set_signFather);
            
			
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
					
					var gen_ret = new App.MVC.View.GameScene.Object.Monster();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to App.MVC.View.GameScene.Object.Monster constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Init(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.MVC.View.GameScene.Object.Monster gen_to_be_invoked = (App.MVC.View.GameScene.Object.Monster)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Collections.Generic.List<App.Generic.Map.Cell> _list = (System.Collections.Generic.List<App.Generic.Map.Cell>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<App.Generic.Map.Cell>));
                    
                    gen_to_be_invoked.Init( _list );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Wound(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.MVC.View.GameScene.Object.Monster gen_to_be_invoked = (App.MVC.View.GameScene.Object.Monster)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _woundHp = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.Wound( _woundHp );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetSpeed(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.MVC.View.GameScene.Object.Monster gen_to_be_invoked = (App.MVC.View.GameScene.Object.Monster)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _v = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    gen_to_be_invoked.SetSpeed( _v );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnPush(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.MVC.View.GameScene.Object.Monster gen_to_be_invoked = (App.MVC.View.GameScene.Object.Monster)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.OnPush(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnGet(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.MVC.View.GameScene.Object.Monster gen_to_be_invoked = (App.MVC.View.GameScene.Object.Monster)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.OnGet(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Hp(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.MVC.View.GameScene.Object.Monster gen_to_be_invoked = (App.MVC.View.GameScene.Object.Monster)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.Hp);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Growth(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.MVC.View.GameScene.Object.Monster gen_to_be_invoked = (App.MVC.View.GameScene.Object.Monster)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.Growth);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Data(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.MVC.View.GameScene.Object.Monster gen_to_be_invoked = (App.MVC.View.GameScene.Object.Monster)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.Data);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_data(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.MVC.View.GameScene.Object.Monster gen_to_be_invoked = (App.MVC.View.GameScene.Object.Monster)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.data);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_hp(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.MVC.View.GameScene.Object.Monster gen_to_be_invoked = (App.MVC.View.GameScene.Object.Monster)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.hp);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_speed(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.MVC.View.GameScene.Object.Monster gen_to_be_invoked = (App.MVC.View.GameScene.Object.Monster)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.speed);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_animator(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.MVC.View.GameScene.Object.Monster gen_to_be_invoked = (App.MVC.View.GameScene.Object.Monster)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.animator);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_hpImageBg(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.MVC.View.GameScene.Object.Monster gen_to_be_invoked = (App.MVC.View.GameScene.Object.Monster)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.hpImageBg);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_hpImageFg(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.MVC.View.GameScene.Object.Monster gen_to_be_invoked = (App.MVC.View.GameScene.Object.Monster)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.hpImageFg);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_signFather(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.MVC.View.GameScene.Object.Monster gen_to_be_invoked = (App.MVC.View.GameScene.Object.Monster)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.signFather);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Hp(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.MVC.View.GameScene.Object.Monster gen_to_be_invoked = (App.MVC.View.GameScene.Object.Monster)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.Hp = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Growth(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.MVC.View.GameScene.Object.Monster gen_to_be_invoked = (App.MVC.View.GameScene.Object.Monster)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.Growth = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_data(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.MVC.View.GameScene.Object.Monster gen_to_be_invoked = (App.MVC.View.GameScene.Object.Monster)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.data = (App.DataClass.Game.Object.MonsterData)translator.GetObject(L, 2, typeof(App.DataClass.Game.Object.MonsterData));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_hp(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.MVC.View.GameScene.Object.Monster gen_to_be_invoked = (App.MVC.View.GameScene.Object.Monster)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.hp = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_speed(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.MVC.View.GameScene.Object.Monster gen_to_be_invoked = (App.MVC.View.GameScene.Object.Monster)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.speed = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_animator(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.MVC.View.GameScene.Object.Monster gen_to_be_invoked = (App.MVC.View.GameScene.Object.Monster)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.animator = (UnityEngine.Animator)translator.GetObject(L, 2, typeof(UnityEngine.Animator));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_hpImageBg(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.MVC.View.GameScene.Object.Monster gen_to_be_invoked = (App.MVC.View.GameScene.Object.Monster)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.hpImageBg = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_hpImageFg(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.MVC.View.GameScene.Object.Monster gen_to_be_invoked = (App.MVC.View.GameScene.Object.Monster)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.hpImageFg = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_signFather(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.MVC.View.GameScene.Object.Monster gen_to_be_invoked = (App.MVC.View.GameScene.Object.Monster)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.signFather = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
