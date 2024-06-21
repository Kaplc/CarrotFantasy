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
    public class AppGameGenericMapPointClassToCellWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(App.Game.Generic.Map.PointClassToCell);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 3, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "ToCellList", _m_ToCellList_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ToCell", _m_ToCell_xlua_st_);
            
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "App.Game.Generic.Map.PointClassToCell does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ToCellList_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& translator.Assignable<System.Collections.Generic.List<App.Game.Generic.Map.PointClass>>(L, 1)) 
                {
                    System.Collections.Generic.List<App.Game.Generic.Map.PointClass> _l = (System.Collections.Generic.List<App.Game.Generic.Map.PointClass>)translator.GetObject(L, 1, typeof(System.Collections.Generic.List<App.Game.Generic.Map.PointClass>));
                    
                        var gen_ret = App.Game.Generic.Map.PointClassToCell.ToCellList( _l );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1&& translator.Assignable<System.Collections.Generic.List<App.Game.Generic.Map.ObjectPointClass>>(L, 1)) 
                {
                    System.Collections.Generic.List<App.Game.Generic.Map.ObjectPointClass> _l = (System.Collections.Generic.List<App.Game.Generic.Map.ObjectPointClass>)translator.GetObject(L, 1, typeof(System.Collections.Generic.List<App.Game.Generic.Map.ObjectPointClass>));
                    
                        var gen_ret = App.Game.Generic.Map.PointClassToCell.ToCellList( _l );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to App.Game.Generic.Map.PointClassToCell.ToCellList!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ToCell_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    App.Game.Generic.Map.PointClass _p = (App.Game.Generic.Map.PointClass)translator.GetObject(L, 1, typeof(App.Game.Generic.Map.PointClass));
                    
                        var gen_ret = App.Game.Generic.Map.PointClassToCell.ToCell( _p );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
