using UnityEngine;

namespace GameFramework
{
    /// <summary>
    ///     手动mono单例要拖动到对象上
    /// </summary>
    public class BaseMonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            Instance = this as T;
        }
    }
}