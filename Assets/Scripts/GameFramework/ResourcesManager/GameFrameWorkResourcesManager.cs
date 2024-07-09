using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace GameFramework
{
    public class ResourcesManager : BaseSingleton<ResourcesManager>
    {
        // 同步资源加载
        public T Load<T>(string fullName) where T : Object
        {
            return Resources.Load<T>(fullName);
        }

        // 异步资源加载
        public void LoadAsync<T>(string fullName, UnityAction<T> callBack) where T : Object
        {
            MonoManager.Instance.StartCoroutineFrameWork(LoadAsyncCoroutine(fullName, callBack));
        }

        // 异步加载协程
        private IEnumerator LoadAsyncCoroutine<T>(string fullName, UnityAction<T> callBack) where T : Object
        {
            var rr = Resources.LoadAsync<T>(fullName);
            yield return rr;
            // 带泛型UnityAction表示执行要带的时的参数类型
            callBack.Invoke(rr.asset as T);
        }
    }
}