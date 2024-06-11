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
    public class LibraryBasePanelWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Library.BasePanel);
			Utils.BeginObjectRegister(type, L, translator, 0, 4, 4, 3);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Update", _m_Update);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Show", _m_Show);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Hide", _m_Hide);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "BindMediator", _m_BindMediator);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "PanelMediator", _g_get_PanelMediator);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "fadeSpeed", _g_get_fadeSpeed);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "hideCallBack", _g_get_hideCallBack);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "showCallBack", _g_get_showCallBack);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "fadeSpeed", _s_set_fadeSpeed);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "hideCallBack", _s_set_hideCallBack);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "showCallBack", _s_set_showCallBack);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "Library.BasePanel does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Update(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Library.BasePanel gen_to_be_invoked = (Library.BasePanel)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Update(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Show(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Library.BasePanel gen_to_be_invoked = (Library.BasePanel)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)&& translator.Assignable<UnityEngine.Events.UnityAction>(L, 3)) 
                {
                    bool _isFade = LuaAPI.lua_toboolean(L, 2);
                    UnityEngine.Events.UnityAction _callBack = translator.GetDelegate<UnityEngine.Events.UnityAction>(L, 3);
                    
                    gen_to_be_invoked.Show( _isFade, _callBack );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    bool _isFade = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.Show( _isFade );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 1) 
                {
                    
                    gen_to_be_invoked.Show(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Library.BasePanel.Show!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Hide(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Library.BasePanel gen_to_be_invoked = (Library.BasePanel)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Events.UnityAction _callBack = translator.GetDelegate<UnityEngine.Events.UnityAction>(L, 2);
                    
                    gen_to_be_invoked.Hide( _callBack );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_BindMediator(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Library.BasePanel gen_to_be_invoked = (Library.BasePanel)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    PureMVC.Patterns.Mediator.Mediator _m = (PureMVC.Patterns.Mediator.Mediator)translator.GetObject(L, 2, typeof(PureMVC.Patterns.Mediator.Mediator));
                    
                    gen_to_be_invoked.BindMediator( _m );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_PanelMediator(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Library.BasePanel gen_to_be_invoked = (Library.BasePanel)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.PanelMediator);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_fadeSpeed(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Library.BasePanel gen_to_be_invoked = (Library.BasePanel)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.fadeSpeed);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_hideCallBack(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Library.BasePanel gen_to_be_invoked = (Library.BasePanel)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.hideCallBack);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_showCallBack(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Library.BasePanel gen_to_be_invoked = (Library.BasePanel)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.showCallBack);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_fadeSpeed(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Library.BasePanel gen_to_be_invoked = (Library.BasePanel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.fadeSpeed = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_hideCallBack(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Library.BasePanel gen_to_be_invoked = (Library.BasePanel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.hideCallBack = translator.GetDelegate<UnityEngine.Events.UnityAction>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_showCallBack(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Library.BasePanel gen_to_be_invoked = (Library.BasePanel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.showCallBack = translator.GetDelegate<UnityEngine.Events.UnityAction>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
