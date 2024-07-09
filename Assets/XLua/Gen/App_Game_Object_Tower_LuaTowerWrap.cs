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
    public class AppGameObjectTowerLuaTowerWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(App.Game.Object.Tower.LuaTower);
			Utils.BeginObjectRegister(type, L, translator, 0, 8, 11, 10);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Wound", _m_Wound);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnGet", _m_OnGet);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnPush", _m_OnPush);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Attack", _m_Attack);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "UpGrade", _m_UpGrade);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetCollectingFiresTarget", _m_SetCollectingFiresTarget);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetData", _m_GetData);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetLevel", _m_GetLevel);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsDead", _g_get_IsDead);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Transform", _g_get_Transform);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onAttackAction", _g_get_onAttackAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onGetAction", _g_get_onGetAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onGetDataAction", _g_get_onGetDataAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onGetIsDeadAction", _g_get_onGetIsDeadAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onGetLevelAction", _g_get_onGetLevelAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onPushAction", _g_get_onPushAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onSetCollectingFiresTargetAction", _g_get_onSetCollectingFiresTargetAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onSetIsDeadAction", _g_get_onSetIsDeadAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onUpGradeAction", _g_get_onUpGradeAction);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "IsDead", _s_set_IsDead);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onAttackAction", _s_set_onAttackAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onGetAction", _s_set_onGetAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onGetDataAction", _s_set_onGetDataAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onGetIsDeadAction", _s_set_onGetIsDeadAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onGetLevelAction", _s_set_onGetLevelAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onPushAction", _s_set_onPushAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onSetCollectingFiresTargetAction", _s_set_onSetCollectingFiresTargetAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onSetIsDeadAction", _s_set_onSetIsDeadAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onUpGradeAction", _s_set_onUpGradeAction);
            
			
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
					
					var gen_ret = new App.Game.Object.Tower.LuaTower();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to App.Game.Object.Tower.LuaTower constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Wound(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.Object.Tower.LuaTower gen_to_be_invoked = (App.Game.Object.Tower.LuaTower)translator.FastGetCSObj(L, 1);
            
            
                
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
        static int _m_OnGet(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.Object.Tower.LuaTower gen_to_be_invoked = (App.Game.Object.Tower.LuaTower)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                App.Game.Object.Tower.LuaTower gen_to_be_invoked = (App.Game.Object.Tower.LuaTower)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.OnPush(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Attack(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.Object.Tower.LuaTower gen_to_be_invoked = (App.Game.Object.Tower.LuaTower)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Attack(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UpGrade(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.Object.Tower.LuaTower gen_to_be_invoked = (App.Game.Object.Tower.LuaTower)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.UpGrade(  );
                    
                    
                    
                    return 0;
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
            
            
                App.Game.Object.Tower.LuaTower gen_to_be_invoked = (App.Game.Object.Tower.LuaTower)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    App.Game.Object.Monster.IMonster _monster = (App.Game.Object.Monster.IMonster)translator.GetObject(L, 2, typeof(App.Game.Object.Monster.IMonster));
                    
                    gen_to_be_invoked.SetCollectingFiresTarget( _monster );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetData(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.Object.Tower.LuaTower gen_to_be_invoked = (App.Game.Object.Tower.LuaTower)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.GetData(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetLevel(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.Object.Tower.LuaTower gen_to_be_invoked = (App.Game.Object.Tower.LuaTower)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.GetLevel(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsDead(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Object.Tower.LuaTower gen_to_be_invoked = (App.Game.Object.Tower.LuaTower)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.IsDead);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Transform(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Object.Tower.LuaTower gen_to_be_invoked = (App.Game.Object.Tower.LuaTower)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.Transform);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onAttackAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Object.Tower.LuaTower gen_to_be_invoked = (App.Game.Object.Tower.LuaTower)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onAttackAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onGetAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Object.Tower.LuaTower gen_to_be_invoked = (App.Game.Object.Tower.LuaTower)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onGetAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onGetDataAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Object.Tower.LuaTower gen_to_be_invoked = (App.Game.Object.Tower.LuaTower)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onGetDataAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onGetIsDeadAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Object.Tower.LuaTower gen_to_be_invoked = (App.Game.Object.Tower.LuaTower)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onGetIsDeadAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onGetLevelAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Object.Tower.LuaTower gen_to_be_invoked = (App.Game.Object.Tower.LuaTower)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onGetLevelAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onPushAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Object.Tower.LuaTower gen_to_be_invoked = (App.Game.Object.Tower.LuaTower)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onPushAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onSetCollectingFiresTargetAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Object.Tower.LuaTower gen_to_be_invoked = (App.Game.Object.Tower.LuaTower)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onSetCollectingFiresTargetAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onSetIsDeadAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Object.Tower.LuaTower gen_to_be_invoked = (App.Game.Object.Tower.LuaTower)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onSetIsDeadAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onUpGradeAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Object.Tower.LuaTower gen_to_be_invoked = (App.Game.Object.Tower.LuaTower)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onUpGradeAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_IsDead(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Object.Tower.LuaTower gen_to_be_invoked = (App.Game.Object.Tower.LuaTower)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.IsDead = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onAttackAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Object.Tower.LuaTower gen_to_be_invoked = (App.Game.Object.Tower.LuaTower)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onAttackAction = translator.GetDelegate<UnityEngine.Events.UnityAction>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onGetAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Object.Tower.LuaTower gen_to_be_invoked = (App.Game.Object.Tower.LuaTower)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onGetAction = translator.GetDelegate<UnityEngine.Events.UnityAction>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onGetDataAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Object.Tower.LuaTower gen_to_be_invoked = (App.Game.Object.Tower.LuaTower)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onGetDataAction = translator.GetDelegate<System.Func<App.Data.DataClass.Game.Object.TowerData>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onGetIsDeadAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Object.Tower.LuaTower gen_to_be_invoked = (App.Game.Object.Tower.LuaTower)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onGetIsDeadAction = translator.GetDelegate<System.Func<bool>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onGetLevelAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Object.Tower.LuaTower gen_to_be_invoked = (App.Game.Object.Tower.LuaTower)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onGetLevelAction = translator.GetDelegate<System.Func<int>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onPushAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Object.Tower.LuaTower gen_to_be_invoked = (App.Game.Object.Tower.LuaTower)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onPushAction = translator.GetDelegate<UnityEngine.Events.UnityAction>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onSetCollectingFiresTargetAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Object.Tower.LuaTower gen_to_be_invoked = (App.Game.Object.Tower.LuaTower)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onSetCollectingFiresTargetAction = translator.GetDelegate<UnityEngine.Events.UnityAction<App.Game.Object.Monster.IMonster>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onSetIsDeadAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Object.Tower.LuaTower gen_to_be_invoked = (App.Game.Object.Tower.LuaTower)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onSetIsDeadAction = translator.GetDelegate<UnityEngine.Events.UnityAction<bool>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onUpGradeAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Object.Tower.LuaTower gen_to_be_invoked = (App.Game.Object.Tower.LuaTower)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onUpGradeAction = translator.GetDelegate<UnityEngine.Events.UnityAction>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
