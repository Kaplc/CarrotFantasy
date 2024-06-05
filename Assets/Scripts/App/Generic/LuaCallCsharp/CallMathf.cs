using UnityEngine;
using XLua;

namespace App.Generic.LuaCallCsharp
{
    [LuaCallCSharp]
    public static class CallMathf
    {
        public static float Abs(float f)
        {
            return Mathf.Abs(f);
        }

        public static float Acos(float f)
        {
            return Mathf.Acos(f);
        }

        public static float Asin(float f)
        {
            return Mathf.Asin(f);
        }

        public static float Atan(float f)
        {
            return Mathf.Atan(f);
        }

        public static float Atan2(float y, float x)
        {
            return Mathf.Atan2(y, x);
        }

        public static float Ceil(float f)
        {
            return Mathf.Ceil(f);
        }

        public static float Cos(float f)
        {
            return Mathf.Cos(f);
        }

        public static float Exp(float power)
        {
            return Mathf.Exp(power);
        }

        public static float Floor(float f)
        {
            return Mathf.Floor(f);
        }

        public static float Log(float f)
        {
            return Mathf.Log(f);
        }

        public static float Log10(float f)
        {
            return Mathf.Log10(f);
        }

        public static float Max(float a, float b)
        {
            return Mathf.Max(a, b);
        }

        public static float Min(float a, float b)
        {
            return Mathf.Min(a, b);
        }

        public static float Pow(float f, float p)
        {
            return Mathf.Pow(f, p);
        }

        public static float Round(float f)
        {
            return Mathf.Round(f);
        }

        public static float Sign(float f)
        {
            return Mathf.Sign(f);
        }

        public static float Sin(float f)
        {
            return Mathf.Sin(f);
        }

        public static float Sqrt(float f)
        {
            return Mathf.Sqrt(f);
        }

        public static float Tan(float f)
        {
            return Mathf.Tan(f);
        }

        public static float Clamp(float value, float min, float max)
        {
            return Mathf.Clamp(value, min, max);
        }

        public static float Lerp(float a, float b, float t)
        {
            return Mathf.Lerp(a, b, t);
        }
    }
}
