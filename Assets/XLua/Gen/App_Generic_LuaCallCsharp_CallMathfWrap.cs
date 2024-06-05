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
    public class AppGenericLuaCallCsharpCallMathfWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(App.Generic.LuaCallCsharp.CallMathf);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 22, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "Abs", _m_Abs_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Acos", _m_Acos_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Asin", _m_Asin_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Atan", _m_Atan_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Atan2", _m_Atan2_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Ceil", _m_Ceil_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Cos", _m_Cos_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Exp", _m_Exp_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Floor", _m_Floor_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Log", _m_Log_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Log10", _m_Log10_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Max", _m_Max_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Min", _m_Min_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Pow", _m_Pow_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Round", _m_Round_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Sign", _m_Sign_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Sin", _m_Sin_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Sqrt", _m_Sqrt_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Tan", _m_Tan_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Clamp", _m_Clamp_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Lerp", _m_Lerp_xlua_st_);
            
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "App.Generic.LuaCallCsharp.CallMathf does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Abs_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    float _f = (float)LuaAPI.lua_tonumber(L, 1);
                    
                        var gen_ret = App.Generic.LuaCallCsharp.CallMathf.Abs( _f );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Acos_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    float _f = (float)LuaAPI.lua_tonumber(L, 1);
                    
                        var gen_ret = App.Generic.LuaCallCsharp.CallMathf.Acos( _f );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Asin_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    float _f = (float)LuaAPI.lua_tonumber(L, 1);
                    
                        var gen_ret = App.Generic.LuaCallCsharp.CallMathf.Asin( _f );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Atan_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    float _f = (float)LuaAPI.lua_tonumber(L, 1);
                    
                        var gen_ret = App.Generic.LuaCallCsharp.CallMathf.Atan( _f );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Atan2_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    float _y = (float)LuaAPI.lua_tonumber(L, 1);
                    float _x = (float)LuaAPI.lua_tonumber(L, 2);
                    
                        var gen_ret = App.Generic.LuaCallCsharp.CallMathf.Atan2( _y, _x );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Ceil_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    float _f = (float)LuaAPI.lua_tonumber(L, 1);
                    
                        var gen_ret = App.Generic.LuaCallCsharp.CallMathf.Ceil( _f );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Cos_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    float _f = (float)LuaAPI.lua_tonumber(L, 1);
                    
                        var gen_ret = App.Generic.LuaCallCsharp.CallMathf.Cos( _f );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Exp_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    float _power = (float)LuaAPI.lua_tonumber(L, 1);
                    
                        var gen_ret = App.Generic.LuaCallCsharp.CallMathf.Exp( _power );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Floor_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    float _f = (float)LuaAPI.lua_tonumber(L, 1);
                    
                        var gen_ret = App.Generic.LuaCallCsharp.CallMathf.Floor( _f );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Log_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    float _f = (float)LuaAPI.lua_tonumber(L, 1);
                    
                        var gen_ret = App.Generic.LuaCallCsharp.CallMathf.Log( _f );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Log10_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    float _f = (float)LuaAPI.lua_tonumber(L, 1);
                    
                        var gen_ret = App.Generic.LuaCallCsharp.CallMathf.Log10( _f );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Max_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    float _a = (float)LuaAPI.lua_tonumber(L, 1);
                    float _b = (float)LuaAPI.lua_tonumber(L, 2);
                    
                        var gen_ret = App.Generic.LuaCallCsharp.CallMathf.Max( _a, _b );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Min_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    float _a = (float)LuaAPI.lua_tonumber(L, 1);
                    float _b = (float)LuaAPI.lua_tonumber(L, 2);
                    
                        var gen_ret = App.Generic.LuaCallCsharp.CallMathf.Min( _a, _b );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Pow_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    float _f = (float)LuaAPI.lua_tonumber(L, 1);
                    float _p = (float)LuaAPI.lua_tonumber(L, 2);
                    
                        var gen_ret = App.Generic.LuaCallCsharp.CallMathf.Pow( _f, _p );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Round_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    float _f = (float)LuaAPI.lua_tonumber(L, 1);
                    
                        var gen_ret = App.Generic.LuaCallCsharp.CallMathf.Round( _f );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Sign_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    float _f = (float)LuaAPI.lua_tonumber(L, 1);
                    
                        var gen_ret = App.Generic.LuaCallCsharp.CallMathf.Sign( _f );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Sin_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    float _f = (float)LuaAPI.lua_tonumber(L, 1);
                    
                        var gen_ret = App.Generic.LuaCallCsharp.CallMathf.Sin( _f );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Sqrt_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    float _f = (float)LuaAPI.lua_tonumber(L, 1);
                    
                        var gen_ret = App.Generic.LuaCallCsharp.CallMathf.Sqrt( _f );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Tan_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    float _f = (float)LuaAPI.lua_tonumber(L, 1);
                    
                        var gen_ret = App.Generic.LuaCallCsharp.CallMathf.Tan( _f );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Clamp_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    float _value = (float)LuaAPI.lua_tonumber(L, 1);
                    float _min = (float)LuaAPI.lua_tonumber(L, 2);
                    float _max = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        var gen_ret = App.Generic.LuaCallCsharp.CallMathf.Clamp( _value, _min, _max );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Lerp_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    float _a = (float)LuaAPI.lua_tonumber(L, 1);
                    float _b = (float)LuaAPI.lua_tonumber(L, 2);
                    float _t = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        var gen_ret = App.Generic.LuaCallCsharp.CallMathf.Lerp( _a, _b, _t );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
