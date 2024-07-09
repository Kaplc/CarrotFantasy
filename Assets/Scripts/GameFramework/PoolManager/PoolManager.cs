using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameFramework
{
    /// <summary>
    ///     使用对象池的对象要继承该接口
    /// </summary>
    public interface IPoolObject
    {
        void OnGet();
        void OnPush();
    }

    public class PoolData
    {
        // 该容器的父对象
        public GameObject father;

        // 储存的所有游戏对象list
        public List<GameObject> objectList;

        public PoolData(string objectName, GameObject poolObject)
        {
            objectList = new List<GameObject>();
            // 用对象名字创建父对象储存list
            father = new GameObject(objectName);
            // 设为pool对象的子物体
            father.transform.parent = poolObject.transform;
        }

        public GameObject Get()
        {
            // 获取末尾的对象
            var targetObject = objectList[0];
            // 断开父子关系
            targetObject.transform.parent = null;
            // 从list移除
            objectList.RemoveAt(0);

            targetObject.SetActive(true);
            // 调用OnGet方法
            if (targetObject.TryGetComponent<IPoolObject>(out var poolObjectComponent)) poolObjectComponent.OnGet();

            return targetObject;
        }

        public void Push(GameObject gameObject)
        {
            if (objectList.Contains(gameObject)) return;

            objectList.Add(gameObject);
            // 设置为list的子对象
            gameObject.transform.SetParent(father.transform);
            // 调用OnPush方法
            gameObject.GetComponent<IPoolObject>()?.OnPush();
            // 失活
            gameObject.SetActive(false);
        }
    }

    public class PoolManager : BaseSingleton<PoolManager>
    {
        // 容器
        private Dictionary<string, PoolData> poolDic;

        // 缓存池对象
        private GameObject poolObject;

        public PoolManager()
        {
            Init();
        }

        public void Init()
        {
            poolDic = new Dictionary<string, PoolData>();
            // 在场景上创建物体统一管理内容
            poolObject = new GameObject("Pool");
            // 
            Object.DontDestroyOnLoad(poolObject);
        }

        /// <summary>
        /// 获取对象同步
        /// </summary>
        /// <param name="path">资源名</param>
        /// <returns></returns>
        public GameObject GetObject(string path)
        {
            // 检查字典有无对象数据
            if (!poolDic.ContainsKey(path))
                // 无对象则创建新list
                poolDic.Add(path, new PoolData(path, poolObject));

            if (poolDic[path].objectList.Count > 0) return poolDic[path].Get();

            // 同步加载
            GameObject obj = Object.Instantiate(ResourcesManager.Instance.Load<GameObject>(path));
            // 分离出名字
            string name = path.Split('/', '.')[1];
            obj.name = name + "(Pool)";
            // 调用OnGet方法
            obj.GetComponent<IPoolObject>()?.OnGet();

            return obj;
        }

        public void GetObjectAsync(string path, Action<GameObject> callBack)
        {
            // 检查字典有无对象数据
            if (!poolDic.ContainsKey(path))
                // 无对象则创建新list
                poolDic.Add(path, new PoolData(path, poolObject));

            if (poolDic[path].objectList.Count > 0)
            {
                callBack?.Invoke(poolDic[path].Get());
                return;
            }

            // 异步加载
            ResourcesManager.Instance.LoadAsync<GameObject>(path, resObj =>
            {
                var obj = Object.Instantiate(resObj);
                string name = path.Split('/', '.')[1];
                obj.name = name + "(Pool)";
                callBack?.Invoke(obj);
            });
        }

        /// <summary>
        /// 通过Addressable获取对象
        /// </summary>
        /// <param name="callBack"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public void GetObjectAsync(Action<GameObject> callBack, params string[] keys)
        {
            string key = keys[0];
            foreach (string k in keys)
            {
                key += "-" + k;
            }
            // 检查字典有无对象数据
            if (!poolDic.ContainsKey(key))
                // 无对象则创建新list
                poolDic.Add(key, new PoolData(key, poolObject));

            if (poolDic[key].objectList.Count > 0)
            {
                callBack?.Invoke(poolDic[key].Get());
                return;
            }

            AddressablesManager.Instance.LoadAssetAsync(callBack, true, keys);
        }

        // 储存对象
        public void PushObject(GameObject gameObject)
        {
            // 有list
            if (poolDic.TryGetValue(gameObject.name, out var poolData))
            {
                poolData.Push(gameObject);
            }
            else // 无list
            {
                // 创建新list
                poolDic.Add(gameObject.name, new PoolData(gameObject.name, poolObject));
                // 再储存
                poolDic[gameObject.name].Push(gameObject);
            }
        }

        // 清空缓存池
        public void Clear()
        {
            if (poolDic != null)
            {
                poolDic.Clear();
            }
        }

        public void Dispose()
        {
            Clear();
            poolDic = null;
            GameObject.Destroy(poolObject);
            poolObject = null;
        }
    }
}