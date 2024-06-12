using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.SDK
{
    public class CallKotlin : MonoBehaviour
    {
        private void Start()
        {
            using (AndroidJavaClass javaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                using (AndroidJavaObject javaObject = javaClass.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    // 静态变量
                    // int ktStaticInt = javaObject.GetStatic<int>("ktStaticInt");
                    // Debug.Log(ktStaticInt);
                    // // int
                    // int ktInt = javaObject.Get<int>("ktInt");
                    // Debug.Log(ktInt);
                    // // float： : 
                    // float ktFloat = javaObject.Get<float>("ktFloat");
                    // Debug.Log(ktFloat);
                    // // bool
                    // bool ktBool = javaObject.Get<bool>("ktBool");
                    // Debug.Log(ktBool);
                    // // string
                    // string ktString = javaObject.Get<string>("ktString");
                    // Debug.Log(ktString);
                    // // list
                    // int funGetCount = javaObject.Call<int>("GetListCount");
                    // Debug.Log("ListCount" + funGetCount);
                    // AndroidJavaObject ktList = javaObject.Get<AndroidJavaObject>("ktList");
                    // int count = ktList.Get<int>("size");
                    // Debug.Log(count);
                    // for (int i = 0; i < count; i++)
                    // {
                    //     Debug.Log(javaObject.Call<int>("GetListValue", i));
                    // }

                    // // dic
                    // AndroidJavaObject ktMap = javaObject.Get<AndroidJavaObject>("ktMap");
                    // count = ktMap.Call<int>("size");
                    // Debug.Log(count);

                    // // test kotlin call unity
                    // javaObject.Call("CallUnity", "success");
                    // Debug.Log(javaObject.Get<int>("a"));
                    // Debug.Log(javaObject.Call<string>("Fun"));
                    // Debug.Log(javaObject.GetStatic<int>("staticInt"));
                    AndroidJavaObject companion = javaObject.GetStatic<AndroidJavaObject>("Companion");
                    companion.Call<string>("StaticFun");
                }
            }
        }
        
        

        public void KotlinCallUnity(string args)
        {
            Debug.Log(args);
        }
    }
}