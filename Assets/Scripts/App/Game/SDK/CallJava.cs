using UnityEngine;

namespace App.Game.SDK
{
    public class CallJava : MonoBehaviour
    {
        private void Awake()
        {
            // 获取java类
            using (var javaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                // 获取UnityPlayer中currentActivity字段的值, 返回的是Activity对象
                using (var javaObject = javaClass.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    // 静态变量
                    var javaStatic = javaObject.GetStatic<int>("jvStatic");
                    Debug.Log(javaStatic);
                    var jvInt = javaObject.Get<int>("jvInt");
                    Debug.Log(jvInt);
                    var jvFloat = javaObject.Get<float>("jvFloat");
                    Debug.Log(jvFloat);
                    var jvBool = javaObject.Get<bool>("jvBool");
                    Debug.Log(jvBool);
                    var jvString = javaObject.Get<string>("jvString");
                    Debug.Log(jvString);

                    var jvStatic = javaObject.CallStatic<string>("staticFun");
                    Debug.Log(jvStatic);
                    var jvFun = javaObject.Call<string>("Fun", "Kaplc");
                    Debug.Log(jvFun);
                }
            }
        }
    }
}