using System;
using UnityEngine;

namespace App.SDK
{
    public class CallJava : MonoBehaviour
    {
        private void Awake()
        {
            // 获取java类
            using (AndroidJavaClass javaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                // 获取UnityPlayer中currentActivity字段的值, 返回的是Activity对象
                using (AndroidJavaObject javaObject = javaClass.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    // 静态变量
                    int javaStatic = javaObject.GetStatic<int>("jvStatic");
                    Debug.Log(javaStatic);
                    int jvInt = javaObject.Get<int>("jvInt");
                    Debug.Log(jvInt);
                    float jvFloat = javaObject.Get<float>("jvFloat");
                    Debug.Log(jvFloat);
                    bool jvBool = javaObject.Get<bool>("jvBool");
                    Debug.Log(jvBool);
                    string jvString = javaObject.Get<string>("jvString");
                    Debug.Log(jvString);
                    
                    string jvStatic = javaObject.CallStatic<string>("staticFun");
                    Debug.Log(jvStatic);
                    string jvFun = javaObject.Call<string>("Fun", "Kaplc");
                    Debug.Log(jvFun);
                }
            }
        }
    }
}