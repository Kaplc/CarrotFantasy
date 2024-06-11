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
    public class AppMVCViewGameSceneObjectLuaObstacleWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(App.MVC.View.GameScene.Object.LuaObstacle);
			Utils.BeginObjectRegister(type, L, translator, 0, 5, 15, 14);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnGet", _m_OnGet);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnPush", _m_OnPush);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Init", _m_Init);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Wound", _m_Wound);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetSpeed", _m_SetSpeed);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "Hp", _g_get_Hp);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Growth", _g_get_Growth);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsDead", _g_get_IsDead);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Data", _g_get_Data);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnGetHpAction", _g_get_OnGetHpAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnSetHpAction", _g_get_OnSetHpAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnGetGrowthAction", _g_get_OnGetGrowthAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnSetGrowthAction", _g_get_OnSetGrowthAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnGetIsDeadAction", _g_get_OnGetIsDeadAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnSetIsDeadAction", _g_get_OnSetIsDeadAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnGetDataAction", _g_get_OnGetDataAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnWoundAction", _g_get_OnWoundAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnPushAction", _g_get_OnPushAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnGetAction", _g_get_OnGetAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnInitAction", _g_get_OnInitAction);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "Hp", _s_set_Hp);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "Growth", _s_set_Growth);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "IsDead", _s_set_IsDead);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnGetHpAction", _s_set_OnGetHpAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnSetHpAction", _s_set_OnSetHpAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnGetGrowthAction", _s_set_OnGetGrowthAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnSetGrowthAction", _s_set_OnSetGrowthAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnGetIsDeadAction", _s_set_OnGetIsDeadAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnSetIsDeadAction", _s_set_OnSetIsDeadAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnGetDataAction", _s_set_OnGetDataAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnWoundAction", _s_set_OnWoundAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnPushAction", _s_set_OnPushAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnGetAction", _s_set_OnGetAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnInitAction", _s_set_OnInitAction);
            
			
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
					
					var gen_ret = new App.MVC.View.GameScene.Object.LuaObstacle();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to App.MVC.View.GameScene.Object.LuaObstacle constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnGet(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.MVC.View.GameScene.Object.LuaObstacle gen_to_be_invoked = (App.MVC.View.GameScene.Object.LuaObstacle)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.OnGet(  );
                    
                    
                    
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
            
            
                App.MVC.View.GameScene.Object.LuaObstacle gen_to_be_invoked = (App.MVC.View.GameScene.Object.LuaObstacle)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.OnPush(  );
                    
                    
                    
                    return 0;
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
            
            
                App.MVC.View.GameScene.Object.LuaObstacle gen_to_be_invoked = (App.MVC.View.GameScene.Object.LuaObstacle)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                App.MVC.View.GameScene.Object.LuaObstacle gen_to_be_invoked = (App.MVC.View.GameScene.Object.LuaObstacle)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                App.MVC.View.GameScene.Object.LuaObstacle gen_to_be_invoked = (App.MVC.View.GameScene.Object.LuaObstacle)translator.FastGetCSObj(L, 1);
            
            
                
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
        static int _g_get_Hp(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.MVC.View.GameScene.Object.LuaObstacle gen_to_be_invoked = (App.MVC.View.GameScene.Object.LuaObstacle)translator.FastGetCSObj(L, 1);
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
			
                App.MVC.View.GameScene.Object.LuaObstacle gen_to_be_invoked = (App.MVC.View.GameScene.Object.LuaObstacle)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.Growth);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsDead(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.MVC.View.GameScene.Object.LuaObstacle gen_to_be_invoked = (App.MVC.View.GameScene.Object.LuaObstacle)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.IsDead);
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
			
                App.MVC.View.GameScene.Object.LuaObstacle gen_to_be_invoked = (App.MVC.View.GameScene.Object.LuaObstacle)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.Data);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnGetHpAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.MVC.View.GameScene.Object.LuaObstacle gen_to_be_invoked = (App.MVC.View.GameScene.Object.LuaObstacle)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnGetHpAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnSetHpAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.MVC.View.GameScene.Object.LuaObstacle gen_to_be_invoked = (App.MVC.View.GameScene.Object.LuaObstacle)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnSetHpAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnGetGrowthAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.MVC.View.GameScene.Object.LuaObstacle gen_to_be_invoked = (App.MVC.View.GameScene.Object.LuaObstacle)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnGetGrowthAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnSetGrowthAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.MVC.View.GameScene.Object.LuaObstacle gen_to_be_invoked = (App.MVC.View.GameScene.Object.LuaObstacle)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnSetGrowthAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnGetIsDeadAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.MVC.View.GameScene.Object.LuaObstacle gen_to_be_invoked = (App.MVC.View.GameScene.Object.LuaObstacle)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnGetIsDeadAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnSetIsDeadAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.MVC.View.GameScene.Object.LuaObstacle gen_to_be_invoked = (App.MVC.View.GameScene.Object.LuaObstacle)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnSetIsDeadAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnGetDataAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.MVC.View.GameScene.Object.LuaObstacle gen_to_be_invoked = (App.MVC.View.GameScene.Object.LuaObstacle)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnGetDataAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnWoundAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.MVC.View.GameScene.Object.LuaObstacle gen_to_be_invoked = (App.MVC.View.GameScene.Object.LuaObstacle)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnWoundAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnPushAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.MVC.View.GameScene.Object.LuaObstacle gen_to_be_invoked = (App.MVC.View.GameScene.Object.LuaObstacle)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnPushAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnGetAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.MVC.View.GameScene.Object.LuaObstacle gen_to_be_invoked = (App.MVC.View.GameScene.Object.LuaObstacle)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnGetAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnInitAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.MVC.View.GameScene.Object.LuaObstacle gen_to_be_invoked = (App.MVC.View.GameScene.Object.LuaObstacle)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnInitAction);
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
			
                App.MVC.View.GameScene.Object.LuaObstacle gen_to_be_invoked = (App.MVC.View.GameScene.Object.LuaObstacle)translator.FastGetCSObj(L, 1);
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
			
                App.MVC.View.GameScene.Object.LuaObstacle gen_to_be_invoked = (App.MVC.View.GameScene.Object.LuaObstacle)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.Growth = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_IsDead(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.MVC.View.GameScene.Object.LuaObstacle gen_to_be_invoked = (App.MVC.View.GameScene.Object.LuaObstacle)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.IsDead = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnGetHpAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.MVC.View.GameScene.Object.LuaObstacle gen_to_be_invoked = (App.MVC.View.GameScene.Object.LuaObstacle)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnGetHpAction = translator.GetDelegate<System.Func<float>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnSetHpAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.MVC.View.GameScene.Object.LuaObstacle gen_to_be_invoked = (App.MVC.View.GameScene.Object.LuaObstacle)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnSetHpAction = translator.GetDelegate<UnityEngine.Events.UnityAction<float>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnGetGrowthAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.MVC.View.GameScene.Object.LuaObstacle gen_to_be_invoked = (App.MVC.View.GameScene.Object.LuaObstacle)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnGetGrowthAction = translator.GetDelegate<System.Func<float>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnSetGrowthAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.MVC.View.GameScene.Object.LuaObstacle gen_to_be_invoked = (App.MVC.View.GameScene.Object.LuaObstacle)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnSetGrowthAction = translator.GetDelegate<UnityEngine.Events.UnityAction<float>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnGetIsDeadAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.MVC.View.GameScene.Object.LuaObstacle gen_to_be_invoked = (App.MVC.View.GameScene.Object.LuaObstacle)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnGetIsDeadAction = translator.GetDelegate<System.Func<bool>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnSetIsDeadAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.MVC.View.GameScene.Object.LuaObstacle gen_to_be_invoked = (App.MVC.View.GameScene.Object.LuaObstacle)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnSetIsDeadAction = translator.GetDelegate<UnityEngine.Events.UnityAction<bool>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnGetDataAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.MVC.View.GameScene.Object.LuaObstacle gen_to_be_invoked = (App.MVC.View.GameScene.Object.LuaObstacle)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnGetDataAction = translator.GetDelegate<System.Func<App.DataClass.Game.Object.MonsterData>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnWoundAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.MVC.View.GameScene.Object.LuaObstacle gen_to_be_invoked = (App.MVC.View.GameScene.Object.LuaObstacle)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnWoundAction = translator.GetDelegate<UnityEngine.Events.UnityAction<int>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnPushAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.MVC.View.GameScene.Object.LuaObstacle gen_to_be_invoked = (App.MVC.View.GameScene.Object.LuaObstacle)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnPushAction = translator.GetDelegate<UnityEngine.Events.UnityAction>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnGetAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.MVC.View.GameScene.Object.LuaObstacle gen_to_be_invoked = (App.MVC.View.GameScene.Object.LuaObstacle)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnGetAction = translator.GetDelegate<UnityEngine.Events.UnityAction>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnInitAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.MVC.View.GameScene.Object.LuaObstacle gen_to_be_invoked = (App.MVC.View.GameScene.Object.LuaObstacle)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnInitAction = translator.GetDelegate<UnityEngine.Events.UnityAction<System.Collections.Generic.List<App.Generic.Map.Cell>>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
