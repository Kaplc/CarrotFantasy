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
    public class AppGenericLuaCallCsharpUIInterfaceClassWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(App.Generic.LuaCallCsharp.UIInterfaceClass);
			Utils.BeginObjectRegister(type, L, translator, 0, 3, 3, 3);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnBeginDrag", _m_OnBeginDrag);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnDrag", _m_OnDrag);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnEndDrag", _m_OnEndDrag);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "onBeginDragAction", _g_get_onBeginDragAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onEndDragAction", _g_get_onEndDragAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onDragAction", _g_get_onDragAction);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "onBeginDragAction", _s_set_onBeginDragAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onEndDragAction", _s_set_onEndDragAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onDragAction", _s_set_onDragAction);
            
			
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
					
					var gen_ret = new App.Generic.LuaCallCsharp.UIInterfaceClass();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to App.Generic.LuaCallCsharp.UIInterfaceClass constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnBeginDrag(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Generic.LuaCallCsharp.UIInterfaceClass gen_to_be_invoked = (App.Generic.LuaCallCsharp.UIInterfaceClass)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.EventSystems.PointerEventData _eventData = (UnityEngine.EventSystems.PointerEventData)translator.GetObject(L, 2, typeof(UnityEngine.EventSystems.PointerEventData));
                    
                    gen_to_be_invoked.OnBeginDrag( _eventData );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnDrag(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Generic.LuaCallCsharp.UIInterfaceClass gen_to_be_invoked = (App.Generic.LuaCallCsharp.UIInterfaceClass)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.EventSystems.PointerEventData _eventData = (UnityEngine.EventSystems.PointerEventData)translator.GetObject(L, 2, typeof(UnityEngine.EventSystems.PointerEventData));
                    
                    gen_to_be_invoked.OnDrag( _eventData );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnEndDrag(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Generic.LuaCallCsharp.UIInterfaceClass gen_to_be_invoked = (App.Generic.LuaCallCsharp.UIInterfaceClass)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.EventSystems.PointerEventData _eventData = (UnityEngine.EventSystems.PointerEventData)translator.GetObject(L, 2, typeof(UnityEngine.EventSystems.PointerEventData));
                    
                    gen_to_be_invoked.OnEndDrag( _eventData );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onBeginDragAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Generic.LuaCallCsharp.UIInterfaceClass gen_to_be_invoked = (App.Generic.LuaCallCsharp.UIInterfaceClass)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onBeginDragAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onEndDragAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Generic.LuaCallCsharp.UIInterfaceClass gen_to_be_invoked = (App.Generic.LuaCallCsharp.UIInterfaceClass)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onEndDragAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onDragAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Generic.LuaCallCsharp.UIInterfaceClass gen_to_be_invoked = (App.Generic.LuaCallCsharp.UIInterfaceClass)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onDragAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onBeginDragAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Generic.LuaCallCsharp.UIInterfaceClass gen_to_be_invoked = (App.Generic.LuaCallCsharp.UIInterfaceClass)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onBeginDragAction = translator.GetDelegate<UnityEngine.Events.UnityAction<UnityEngine.EventSystems.PointerEventData>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onEndDragAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Generic.LuaCallCsharp.UIInterfaceClass gen_to_be_invoked = (App.Generic.LuaCallCsharp.UIInterfaceClass)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onEndDragAction = translator.GetDelegate<UnityEngine.Events.UnityAction<UnityEngine.EventSystems.PointerEventData>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onDragAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Generic.LuaCallCsharp.UIInterfaceClass gen_to_be_invoked = (App.Generic.LuaCallCsharp.UIInterfaceClass)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onDragAction = translator.GetDelegate<UnityEngine.Events.UnityAction<UnityEngine.EventSystems.PointerEventData>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
