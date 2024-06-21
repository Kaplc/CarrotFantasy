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
    public class AppUIGameScenePanelGamePanelCountDownPanelWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(App.UI.GameScene.Panel.GamePanel.CountDownPanel);
			Utils.BeginObjectRegister(type, L, translator, 0, 1, 4, 4);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "StartCountDown", _m_StartCountDown);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "speed", _g_get_speed);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "imgCount", _g_get_imgCount);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "imgFire", _g_get_imgFire);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "countDownImage", _g_get_countDownImage);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "speed", _s_set_speed);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "imgCount", _s_set_imgCount);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "imgFire", _s_set_imgFire);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "countDownImage", _s_set_countDownImage);
            
			
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
					
					var gen_ret = new App.UI.GameScene.Panel.GamePanel.CountDownPanel();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to App.UI.GameScene.Panel.GamePanel.CountDownPanel constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StartCountDown(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.UI.GameScene.Panel.GamePanel.CountDownPanel gen_to_be_invoked = (App.UI.GameScene.Panel.GamePanel.CountDownPanel)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.StartCountDown(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_speed(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.UI.GameScene.Panel.GamePanel.CountDownPanel gen_to_be_invoked = (App.UI.GameScene.Panel.GamePanel.CountDownPanel)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.speed);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_imgCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.UI.GameScene.Panel.GamePanel.CountDownPanel gen_to_be_invoked = (App.UI.GameScene.Panel.GamePanel.CountDownPanel)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.imgCount);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_imgFire(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.UI.GameScene.Panel.GamePanel.CountDownPanel gen_to_be_invoked = (App.UI.GameScene.Panel.GamePanel.CountDownPanel)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.imgFire);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_countDownImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.UI.GameScene.Panel.GamePanel.CountDownPanel gen_to_be_invoked = (App.UI.GameScene.Panel.GamePanel.CountDownPanel)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.countDownImage);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_speed(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.UI.GameScene.Panel.GamePanel.CountDownPanel gen_to_be_invoked = (App.UI.GameScene.Panel.GamePanel.CountDownPanel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.speed = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_imgCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.UI.GameScene.Panel.GamePanel.CountDownPanel gen_to_be_invoked = (App.UI.GameScene.Panel.GamePanel.CountDownPanel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.imgCount = (UnityEngine.UI.Image)translator.GetObject(L, 2, typeof(UnityEngine.UI.Image));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_imgFire(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.UI.GameScene.Panel.GamePanel.CountDownPanel gen_to_be_invoked = (App.UI.GameScene.Panel.GamePanel.CountDownPanel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.imgFire = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_countDownImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.UI.GameScene.Panel.GamePanel.CountDownPanel gen_to_be_invoked = (App.UI.GameScene.Panel.GamePanel.CountDownPanel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.countDownImage = (System.Collections.Generic.List<UnityEngine.Sprite>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<UnityEngine.Sprite>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
