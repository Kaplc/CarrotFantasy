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
    public class AppDataDataClassGameObjectTowerDataWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(App.Data.DataClass.Game.Object.TowerData);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 11, 11);
			
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "id", _g_get_id);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "rotaSpeed", _g_get_rotaSpeed);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "prefabsPath", _g_get_prefabsPath);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "attackRangesList", _g_get_attackRangesList);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "atkList", _g_get_atkList);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "bulletsPrefabsPath", _g_get_bulletsPrefabsPath);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "prices", _g_get_prices);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "sellPrices", _g_get_sellPrices);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "icon", _g_get_icon);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "greyIcon", _g_get_greyIcon);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "selectLevelIcon", _g_get_selectLevelIcon);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "id", _s_set_id);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "rotaSpeed", _s_set_rotaSpeed);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "prefabsPath", _s_set_prefabsPath);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "attackRangesList", _s_set_attackRangesList);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "atkList", _s_set_atkList);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "bulletsPrefabsPath", _s_set_bulletsPrefabsPath);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "prices", _s_set_prices);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "sellPrices", _s_set_sellPrices);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "icon", _s_set_icon);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "greyIcon", _s_set_greyIcon);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "selectLevelIcon", _s_set_selectLevelIcon);
            
			
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
					
					var gen_ret = new App.Data.DataClass.Game.Object.TowerData();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to App.Data.DataClass.Game.Object.TowerData constructor!");
            
        }
        
		
        
		
        
        
        
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_id(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Data.DataClass.Game.Object.TowerData gen_to_be_invoked = (App.Data.DataClass.Game.Object.TowerData)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.id);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_rotaSpeed(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Data.DataClass.Game.Object.TowerData gen_to_be_invoked = (App.Data.DataClass.Game.Object.TowerData)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.rotaSpeed);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_prefabsPath(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Data.DataClass.Game.Object.TowerData gen_to_be_invoked = (App.Data.DataClass.Game.Object.TowerData)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.prefabsPath);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_attackRangesList(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Data.DataClass.Game.Object.TowerData gen_to_be_invoked = (App.Data.DataClass.Game.Object.TowerData)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.attackRangesList);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_atkList(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Data.DataClass.Game.Object.TowerData gen_to_be_invoked = (App.Data.DataClass.Game.Object.TowerData)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.atkList);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_bulletsPrefabsPath(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Data.DataClass.Game.Object.TowerData gen_to_be_invoked = (App.Data.DataClass.Game.Object.TowerData)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.bulletsPrefabsPath);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_prices(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Data.DataClass.Game.Object.TowerData gen_to_be_invoked = (App.Data.DataClass.Game.Object.TowerData)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.prices);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_sellPrices(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Data.DataClass.Game.Object.TowerData gen_to_be_invoked = (App.Data.DataClass.Game.Object.TowerData)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.sellPrices);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_icon(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Data.DataClass.Game.Object.TowerData gen_to_be_invoked = (App.Data.DataClass.Game.Object.TowerData)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.icon);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_greyIcon(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Data.DataClass.Game.Object.TowerData gen_to_be_invoked = (App.Data.DataClass.Game.Object.TowerData)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.greyIcon);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_selectLevelIcon(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Data.DataClass.Game.Object.TowerData gen_to_be_invoked = (App.Data.DataClass.Game.Object.TowerData)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.selectLevelIcon);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_id(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Data.DataClass.Game.Object.TowerData gen_to_be_invoked = (App.Data.DataClass.Game.Object.TowerData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.id = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_rotaSpeed(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Data.DataClass.Game.Object.TowerData gen_to_be_invoked = (App.Data.DataClass.Game.Object.TowerData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.rotaSpeed = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_prefabsPath(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Data.DataClass.Game.Object.TowerData gen_to_be_invoked = (App.Data.DataClass.Game.Object.TowerData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.prefabsPath = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_attackRangesList(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Data.DataClass.Game.Object.TowerData gen_to_be_invoked = (App.Data.DataClass.Game.Object.TowerData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.attackRangesList = (System.Collections.Generic.List<float>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<float>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_atkList(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Data.DataClass.Game.Object.TowerData gen_to_be_invoked = (App.Data.DataClass.Game.Object.TowerData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.atkList = (System.Collections.Generic.List<int>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<int>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_bulletsPrefabsPath(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Data.DataClass.Game.Object.TowerData gen_to_be_invoked = (App.Data.DataClass.Game.Object.TowerData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.bulletsPrefabsPath = (System.Collections.Generic.List<string>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<string>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_prices(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Data.DataClass.Game.Object.TowerData gen_to_be_invoked = (App.Data.DataClass.Game.Object.TowerData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.prices = (System.Collections.Generic.List<int>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<int>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_sellPrices(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Data.DataClass.Game.Object.TowerData gen_to_be_invoked = (App.Data.DataClass.Game.Object.TowerData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.sellPrices = (System.Collections.Generic.List<int>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<int>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_icon(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Data.DataClass.Game.Object.TowerData gen_to_be_invoked = (App.Data.DataClass.Game.Object.TowerData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.icon = (UnityEngine.Sprite)translator.GetObject(L, 2, typeof(UnityEngine.Sprite));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_greyIcon(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Data.DataClass.Game.Object.TowerData gen_to_be_invoked = (App.Data.DataClass.Game.Object.TowerData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.greyIcon = (UnityEngine.Sprite)translator.GetObject(L, 2, typeof(UnityEngine.Sprite));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_selectLevelIcon(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                App.Data.DataClass.Game.Object.TowerData gen_to_be_invoked = (App.Data.DataClass.Game.Object.TowerData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.selectLevelIcon = (UnityEngine.Sprite)translator.GetObject(L, 2, typeof(UnityEngine.Sprite));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
