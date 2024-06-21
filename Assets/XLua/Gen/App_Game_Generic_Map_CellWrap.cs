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
    public class AppGameGenericMapCellWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(App.Game.Generic.Map.Cell);
			Utils.BeginObjectRegister(type, L, translator, 0, 1, 7, 5);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ToString", _m_ToString);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "X", _g_get_X);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Y", _g_get_Y);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsTowerPos", _g_get_IsTowerPos);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "hasObstacle", _g_get_hasObstacle);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "obstacleName", _g_get_obstacleName);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "obstacle", _g_get_obstacle);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "tower", _g_get_tower);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "IsTowerPos", _s_set_IsTowerPos);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "hasObstacle", _s_set_hasObstacle);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "obstacleName", _s_set_obstacleName);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "obstacle", _s_set_obstacle);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "tower", _s_set_tower);
            
			
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
				if(LuaAPI.lua_gettop(L) == 2 && translator.Assignable<App.Game.Generic.Map.Point>(L, 2))
				{
					App.Game.Generic.Map.Point _point;translator.Get(L, 2, out _point);
					
					var gen_ret = new App.Game.Generic.Map.Cell(_point);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 2 && translator.Assignable<App.Game.Generic.Map.ObjectPointClass>(L, 2))
				{
					App.Game.Generic.Map.ObjectPointClass _objectPointClass = (App.Game.Generic.Map.ObjectPointClass)translator.GetObject(L, 2, typeof(App.Game.Generic.Map.ObjectPointClass));
					
					var gen_ret = new App.Game.Generic.Map.Cell(_objectPointClass);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 2 && translator.Assignable<App.Game.Generic.Map.PointClass>(L, 2))
				{
					App.Game.Generic.Map.PointClass _pointClass = (App.Game.Generic.Map.PointClass)translator.GetObject(L, 2, typeof(App.Game.Generic.Map.PointClass));
					
					var gen_ret = new App.Game.Generic.Map.Cell(_pointClass);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to App.Game.Generic.Map.Cell constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ToString(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                App.Game.Generic.Map.Cell gen_to_be_invoked = (App.Game.Generic.Map.Cell)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.ToString(  );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_X(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Generic.Map.Cell gen_to_be_invoked = (App.Game.Generic.Map.Cell)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.X);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Y(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Generic.Map.Cell gen_to_be_invoked = (App.Game.Generic.Map.Cell)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.Y);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsTowerPos(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Generic.Map.Cell gen_to_be_invoked = (App.Game.Generic.Map.Cell)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.IsTowerPos);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_hasObstacle(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Generic.Map.Cell gen_to_be_invoked = (App.Game.Generic.Map.Cell)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.hasObstacle);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_obstacleName(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Generic.Map.Cell gen_to_be_invoked = (App.Game.Generic.Map.Cell)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.obstacleName);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_obstacle(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Generic.Map.Cell gen_to_be_invoked = (App.Game.Generic.Map.Cell)translator.FastGetCSObj(L, 1);
                translator.PushAny(L, gen_to_be_invoked.obstacle);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_tower(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Generic.Map.Cell gen_to_be_invoked = (App.Game.Generic.Map.Cell)translator.FastGetCSObj(L, 1);
                translator.PushAny(L, gen_to_be_invoked.tower);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_IsTowerPos(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Generic.Map.Cell gen_to_be_invoked = (App.Game.Generic.Map.Cell)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.IsTowerPos = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_hasObstacle(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Generic.Map.Cell gen_to_be_invoked = (App.Game.Generic.Map.Cell)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.hasObstacle = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_obstacleName(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Generic.Map.Cell gen_to_be_invoked = (App.Game.Generic.Map.Cell)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.obstacleName = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_obstacle(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Generic.Map.Cell gen_to_be_invoked = (App.Game.Generic.Map.Cell)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.obstacle = translator.GetObject(L, 2, typeof(object));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_tower(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Game.Generic.Map.Cell gen_to_be_invoked = (App.Game.Generic.Map.Cell)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.tower = translator.GetObject(L, 2, typeof(object));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
