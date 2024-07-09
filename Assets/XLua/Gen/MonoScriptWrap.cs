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
    public class MonoScriptWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(MonoScript);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 6, 6);
			
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "onAwakeAction", _g_get_onAwakeAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onDestroyAction", _g_get_onDestroyAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onDisableAction", _g_get_onDisableAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onEnableAction", _g_get_onEnableAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onStartAction", _g_get_onStartAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onUpdateAction", _g_get_onUpdateAction);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "onAwakeAction", _s_set_onAwakeAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onDestroyAction", _s_set_onDestroyAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onDisableAction", _s_set_onDisableAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onEnableAction", _s_set_onEnableAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onStartAction", _s_set_onStartAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onUpdateAction", _s_set_onUpdateAction);
            
			
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
					
					var gen_ret = new MonoScript();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to MonoScript constructor!");
            
        }
        
		
        
		
        
        
        
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onAwakeAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MonoScript gen_to_be_invoked = (MonoScript)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onAwakeAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onDestroyAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MonoScript gen_to_be_invoked = (MonoScript)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onDestroyAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onDisableAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MonoScript gen_to_be_invoked = (MonoScript)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onDisableAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onEnableAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MonoScript gen_to_be_invoked = (MonoScript)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onEnableAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onStartAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MonoScript gen_to_be_invoked = (MonoScript)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onStartAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onUpdateAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MonoScript gen_to_be_invoked = (MonoScript)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onUpdateAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onAwakeAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MonoScript gen_to_be_invoked = (MonoScript)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onAwakeAction = translator.GetDelegate<UnityEngine.Events.UnityAction>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onDestroyAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MonoScript gen_to_be_invoked = (MonoScript)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onDestroyAction = translator.GetDelegate<UnityEngine.Events.UnityAction>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onDisableAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MonoScript gen_to_be_invoked = (MonoScript)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onDisableAction = translator.GetDelegate<UnityEngine.Events.UnityAction>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onEnableAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MonoScript gen_to_be_invoked = (MonoScript)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onEnableAction = translator.GetDelegate<UnityEngine.Events.UnityAction>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onStartAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MonoScript gen_to_be_invoked = (MonoScript)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onStartAction = translator.GetDelegate<UnityEngine.Events.UnityAction>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onUpdateAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MonoScript gen_to_be_invoked = (MonoScript)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onUpdateAction = translator.GetDelegate<UnityEngine.Events.UnityAction>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
