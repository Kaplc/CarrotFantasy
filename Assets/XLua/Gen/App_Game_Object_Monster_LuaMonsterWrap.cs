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
    public class AppGameObjectMonsterLuaMonsterWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(App.Game.Object.Monster.LuaMonster);
			Utils.BeginObjectRegister(type, L, translator, 0, 9, 22, 20);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnGet", _m_OnGet);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnPush", _m_OnPush);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Wound", _m_Wound);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Init", _m_Init);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetSpeed", _m_SetSpeed);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetSignFather", _m_GetSignFather);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Dead", _m_Dead);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddBuffEffect", _m_AddBuffEffect);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnMouseDown", _m_OnMouseDown);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "Hp", _g_get_Hp);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Growth", _g_get_Growth);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsDead", _g_get_IsDead);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Data", _g_get_Data);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Transform", _g_get_Transform);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onGetHpAction", _g_get_onGetHpAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onSetHpAction", _g_get_onSetHpAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onGetGrowthAction", _g_get_onGetGrowthAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onSetGrowthAction", _g_get_onSetGrowthAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onGetIsDeadAction", _g_get_onGetIsDeadAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onSetIsDeadAction", _g_get_onSetIsDeadAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onGetDataAction", _g_get_onGetDataAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onGetTransformAction", _g_get_onGetTransformAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onWoundAction", _g_get_onWoundAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onPushAction", _g_get_onPushAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onGetAction", _g_get_onGetAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onInitAction", _g_get_onInitAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onSetSpeedAction", _g_get_onSetSpeedAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onGetSignFatherAction", _g_get_onGetSignFatherAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onDeadAction", _g_get_onDeadAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onAddBuffEffectAction", _g_get_onAddBuffEffectAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onMouseDownAction", _g_get_onMouseDownAction);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "Hp", _s_set_Hp);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "Growth", _s_set_Growth);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "IsDead", _s_set_IsDead);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onGetHpAction", _s_set_onGetHpAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onSetHpAction", _s_set_onSetHpAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onGetGrowthAction", _s_set_onGetGrowthAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onSetGrowthAction", _s_set_onSetGrowthAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onGetIsDeadAction", _s_set_onGetIsDeadAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onSetIsDeadAction", _s_set_onSetIsDeadAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onGetDataAction", _s_set_onGetDataAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onGetTransformAction", _s_set_onGetTransformAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onWoundAction", _s_set_onWoundAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onPushAction", _s_set_onPushAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onGetAction", _s_set_onGetAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onInitAction", _s_set_onInitAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onSetSpeedAction", _s_set_onSetSpeedAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onGetSignFatherAction", _s_set_onGetSignFatherAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onDeadAction", _s_set_onDeadAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onAddBuffEffectAction", _s_set_onAddBuffEffectAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onMouseDownAction", _s_set_onMouseDownAction);
            
			
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
					
					var gen_ret = new App.Game.Object.Monster.LuaMonster();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to App.Game.Object.Monster.LuaMonster constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnGet(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.OnPush(  );
                    
                    
                    
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
            
            
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
            
            
                
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
        static int _m_Init(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Collections.Generic.List<App.Game.Generic.Map.Cell> _list = (System.Collections.Generic.List<App.Game.Generic.Map.Cell>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<App.Game.Generic.Map.Cell>));
                    float _hard = (float)LuaAPI.lua_tonumber(L, 3);
                    App.Data.DataClass.Game.Object.MonsterData _data = (App.Data.DataClass.Game.Object.MonsterData)translator.GetObject(L, 4, typeof(App.Data.DataClass.Game.Object.MonsterData));
                    
                    gen_to_be_invoked.Init( _list, _hard, _data );
                    
                    
                    
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
            
            
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
            
            
                
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
        static int _m_GetSignFather(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.GetSignFather(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Dead(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Dead(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddBuffEffect(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    App.Game.Generic.BaseObject.BaseBuffEffect _buffEffect = (App.Game.Generic.BaseObject.BaseBuffEffect)translator.GetObject(L, 2, typeof(App.Game.Generic.BaseObject.BaseBuffEffect));
                    
                    gen_to_be_invoked.AddBuffEffect( _buffEffect );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnMouseDown(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.OnMouseDown(  );
                    
                    
                    
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
			
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
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
			
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
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
			
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
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
			
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.Data);
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
			
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.Transform);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onGetHpAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onGetHpAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onSetHpAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onSetHpAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onGetGrowthAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onGetGrowthAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onSetGrowthAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onSetGrowthAction);
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
			
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onGetIsDeadAction);
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
			
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onSetIsDeadAction);
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
			
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onGetDataAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onGetTransformAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onGetTransformAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onWoundAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onWoundAction);
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
			
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onPushAction);
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
			
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onGetAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onInitAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onInitAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onSetSpeedAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onSetSpeedAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onGetSignFatherAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onGetSignFatherAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onDeadAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onDeadAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onAddBuffEffectAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onAddBuffEffectAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onMouseDownAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onMouseDownAction);
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
			
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
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
			
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
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
			
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.IsDead = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onGetHpAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onGetHpAction = translator.GetDelegate<System.Func<float>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onSetHpAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onSetHpAction = translator.GetDelegate<UnityEngine.Events.UnityAction<float>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onGetGrowthAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onGetGrowthAction = translator.GetDelegate<System.Func<float>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onSetGrowthAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onSetGrowthAction = translator.GetDelegate<UnityEngine.Events.UnityAction<float>>(L, 2);
            
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
			
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onGetIsDeadAction = translator.GetDelegate<System.Func<bool>>(L, 2);
            
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
			
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onSetIsDeadAction = translator.GetDelegate<UnityEngine.Events.UnityAction<bool>>(L, 2);
            
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
			
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onGetDataAction = translator.GetDelegate<System.Func<App.Data.DataClass.Game.Object.MonsterData>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onGetTransformAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onGetTransformAction = translator.GetDelegate<System.Func<UnityEngine.Transform>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onWoundAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onWoundAction = translator.GetDelegate<UnityEngine.Events.UnityAction<int>>(L, 2);
            
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
			
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onPushAction = translator.GetDelegate<UnityEngine.Events.UnityAction>(L, 2);
            
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
			
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onGetAction = translator.GetDelegate<UnityEngine.Events.UnityAction>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onInitAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onInitAction = translator.GetDelegate<UnityEngine.Events.UnityAction<System.Collections.Generic.List<App.Game.Generic.Map.Cell>, float, App.Data.DataClass.Game.Object.MonsterData>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onSetSpeedAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onSetSpeedAction = translator.GetDelegate<UnityEngine.Events.UnityAction<float>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onGetSignFatherAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onGetSignFatherAction = translator.GetDelegate<System.Func<UnityEngine.Transform>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onDeadAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onDeadAction = translator.GetDelegate<UnityEngine.Events.UnityAction>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onAddBuffEffectAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onAddBuffEffectAction = translator.GetDelegate<UnityEngine.Events.UnityAction<App.Game.Generic.BaseObject.BaseBuffEffect>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onMouseDownAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Object.Monster.LuaMonster gen_to_be_invoked = (App.Game.Object.Monster.LuaMonster)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onMouseDownAction = translator.GetDelegate<UnityEngine.Events.UnityAction>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
