using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CustomAsyncOperationHandle
{
    private AsyncOperationHandle handle;
    private int count; // 被引用次数

    public int Count => count;

    public AsyncOperationHandle Handle
    {
        get
        {
            count++;
            return handle;
        }
    }

    public CustomAsyncOperationHandle(AsyncOperationHandle h)
    {
        handle = h;
        count = 0;
    }

    public void Release()
    {
        count--;
    }
}


public class AddressablesesManager : IAddressablesManager
{
    private Dictionary<string, CustomAsyncOperationHandle> handlesDic;
    
    private MethodInfo loadAssetAsyncSingle;
    private MethodInfo loadAssetAsyncMultiple;
    private MethodInfo loadAssetsAsyncGeneric;

    public AddressablesesManager()
    {
        handlesDic = new Dictionary<string, CustomAsyncOperationHandle>();
        
        // 反射获取私有方法
        loadAssetAsyncSingle = typeof(AddressablesesManager).GetMethod("LoadAssetAsyncSingle", BindingFlags.NonPublic | BindingFlags.Instance);
        loadAssetAsyncMultiple= typeof(AddressablesesManager).GetMethod("LoadAssetAsyncMultiple", BindingFlags.NonPublic | BindingFlags.Instance);
        loadAssetsAsyncGeneric = typeof(AddressablesesManager).GetMethod("LoadAssetsAsyncGeneric", BindingFlags.NonPublic | BindingFlags.Instance);
    }

    /// <summary>
    /// 单个名称或标签加载
    /// </summary>
    /// <param name="name"></param>
    /// <param name="callBack"></param>
    /// <typeparam name="T"></typeparam>
    public void LoadAssetAsync<T>(string name, Action<T> callBack) where T : class
    {
        string key = $"{name}-{typeof(T).Name}";

        if (handlesDic.TryGetValue(key, out var value))
        {
            AsyncOperationHandle<T> h = value.Handle.Convert<T>();

            if (h.IsDone)
            {
                // 加载完成直接执行回调
                callBack?.Invoke(h.Result);
            }
            else
            {
                // 添加进完成回调
                h.Completed += operationHandle =>
                {
                    if (operationHandle.Status == AsyncOperationStatus.Succeeded)
                    {
                        // 将加载成功的资源返回
                        callBack?.Invoke(operationHandle.Result);
                    }
                    else
                    {
                        Debug.LogWarning($"加载失败{key}");
                        // 加载失败返回null
                        callBack?.Invoke(null);
                    }
                };
            }

            return;
        }

        AsyncOperationHandle handle = Addressables.LoadAssetAsync<T>(name);
        handle.Completed += operationHandle =>
        {
            if (operationHandle.Status == AsyncOperationStatus.Succeeded)
            {
                // 将加载成功的资源返回
                callBack?.Invoke(handlesDic[key].Handle.Convert<T>().Result);
            }
            else
            {
                Debug.LogWarning($"加载失败{key}");
                // 加载失败返回null
                callBack?.Invoke(null);
            }
        };
        // 将操作句柄保存
        handlesDic.Add(key, new CustomAsyncOperationHandle(handle));
    }

    /// <summary>
    /// 多名称和标签条件加载
    /// </summary>
    /// <param name="callBack">回调</param>
    /// <param name="keys">筛选条件</param>
    /// <typeparam name="T"></typeparam>
    public void LoadAssetAsync<T>(Action<T> callBack, params string[] keys)
        where T : class
    {
        List<string> keysList = keys.ToList();
        // 拼接存入dic的key
        string key = "";
        for (int i = 0; i < keysList.Count; i++)
        {
            key += keysList[i] + "-";
        }

        key += typeof(T).Name;

        if (handlesDic.TryGetValue(key, out var value))
        {
            AsyncOperationHandle<IList<T>> h = value.Handle.Convert<IList<T>>();

            if (h.IsDone)
            {
                if (h.Result.Count > 1)
                {
                    Debug.LogWarning("获取到多个资源默认返回第一个");
                }

                // 加载完成直接执行回调
                callBack?.Invoke(h.Result[0]);
            }
            else
            {
                // 添加进完成回调
                h.Completed += operationHandle =>
                {
                    if (operationHandle.Status == AsyncOperationStatus.Succeeded)
                    {
                        if (h.Result.Count > 1)
                        {
                            Debug.LogWarning("获取到多个资源默认返回第一个");
                        }

                        // 将加载成功的资源返回
                        callBack?.Invoke(h.Result[0]);
                    }
                    else
                    {
                        Debug.LogWarning($"加载失败{key}");
                        // 加载失败返回null
                        callBack?.Invoke(null);
                    }
                };
            }

            return;
        }

        AsyncOperationHandle handle = Addressables.LoadAssetsAsync<T>(keysList, null, Addressables.MergeMode.Intersection);

        handle.Completed += operationHandle =>
        {
            if (operationHandle.Status == AsyncOperationStatus.Succeeded)
            {
                AsyncOperationHandle<IList<T>> h = handlesDic[key].Handle.Convert<IList<T>>();
                // 加载成功回调
                if (h.Result.Count > 1)
                {
                    Debug.LogWarning("获取到多个资源默认返回第一个");
                }

                // 将加载成功的资源返回
                callBack?.Invoke(h.Result[0]);
            }
            else
            {
                Debug.LogWarning($"加载失败{key}");
                callBack?.Invoke(null);
            }
        };
        handlesDic.Add(key, new CustomAsyncOperationHandle(handle));
    }

    /// <summary>
    /// 加载多个资源
    /// </summary>
    /// <param name="mode">
    /// None：不发生合并，将使用第一组结果
    /// UseFirst：应用第一组结果
    /// Union：合并所有结果
    /// Intersection：使用相交结果</param>
    /// <param name="callBack"></param>
    /// <param name="keys"></param>
    /// <typeparam name="T"></typeparam>
    public void LoadAssetsAsync<T>(Addressables.MergeMode mode, Action<IList<T>> callBack, params string[] keys) where T : class
    {
        List<string> keysList = keys.ToList();
        // 拼接存入dic的key
        string key = "";
        for (int i = 0; i < keysList.Count; i++)
        {
            key += keysList[i] + "-";
        }

        key += typeof(T).Name;

        if (handlesDic.TryGetValue(key, out var value))
        {
            AsyncOperationHandle<IList<T>> h = value.Handle.Convert<IList<T>>();

            if (h.IsDone)
            {
                // 加载完成直接执行回调
                callBack?.Invoke(h.Result);
            }
            else
            {
                // 添加进完成回调
                h.Completed += operationHandle =>
                {
                    if (operationHandle.Status == AsyncOperationStatus.Succeeded)
                    {
                        // 将加载成功的资源返回
                        callBack?.Invoke(operationHandle.Result);
                    }
                    else
                    {
                        Debug.LogWarning($"加载失败{key}");
                        // 加载失败返回null
                        callBack?.Invoke(null);
                    }
                };
            }

            return;
        }

        AsyncOperationHandle handle = Addressables.LoadAssetsAsync<T>(keysList, null, mode);

        handle.Completed += operationHandle =>
        {
            if (operationHandle.Status == AsyncOperationStatus.Succeeded)
            {
                // 加载成功回调
                handlesDic.Add(key, new CustomAsyncOperationHandle(operationHandle));
                callBack?.Invoke(handlesDic[key].Handle.Convert<IList<T>>().Result);
            }
            else
            {
                Debug.LogWarning($"加载失败{key}");
                callBack?.Invoke(null);
            }
        };
    }

    public void Release<T>(string name)
    {
        string key = name + "-" + typeof(T).Name;
        if (handlesDic.TryGetValue(key, out var value))
        {
            value.Release();
            Debug.Log(key + ":引用" + value.Count);
            // 引用计数为0才真正释放
            if (value.Count == 0)
            {
                Addressables.Release(value.Handle);
                handlesDic.Remove(key);
            }
        }
        else
        {
            Debug.LogWarning("释放失败-" + name);
        }
    }

    public void Release<T>(params string[] keys)
    {
        List<string> keysList = keys.ToList();
        // 拼接存入dic的key
        string key = "";
        for (int i = 0; i < keysList.Count; i++)
        {
            key += keysList[i] + "-";
        }

        key += typeof(T).Name;

        if (handlesDic.TryGetValue(key, out var value))
        {
            value.Release();
            Debug.Log(key + ":引用" + value.Count);
            // 引用计数为0才真正释放
            if (value.Count == 0)
            {
                Addressables.Release(value.Handle);
                handlesDic.Remove(key);
            }
        }
        else
        {
            Debug.LogWarning("释放失败-" + key);
        }
    }

    public void Clear()
    {
        foreach (var item in handlesDic.Values)
        {
            Addressables.Release(item.Handle);
        }

        handlesDic.Clear();
        AssetBundle.UnloadAllAssetBundles(true);
        Resources.UnloadUnusedAssets();
        GC.Collect();
    }

    #region 非泛型方法

    public void LoadAssetAsync(string name, Type type, Action<object> callBack)
    {
        // 使用反射调用泛型方法
        if (loadAssetAsyncSingle != null)
        {
            MethodInfo genericMethod = loadAssetAsyncSingle.MakeGenericMethod(type);
            genericMethod.Invoke(this, new object[] { name, callBack });
        }

    }

    private void LoadAssetAsyncSingle<T>(string name, Action<T> callBack) where T : class
    {
        LoadAssetAsync(name, callBack);
    }

    public void LoadAssetAsync(Action<object> callBack, Type type, params string[] keys)
    {
        // 使用反射调用泛型方法
        if (loadAssetAsyncMultiple != null)
        {
            MethodInfo genericMethod = loadAssetAsyncMultiple.MakeGenericMethod(type);
            genericMethod.Invoke(this, new object[] { callBack, keys });
        }
    }

    private void LoadAssetAsyncMultiple<T>(Action<IList<T>> callBack, params string[] keys) where T : class
    {
        LoadAssetsAsync(Addressables.MergeMode.Intersection, callBack, keys);
    }

    public void LoadAssetsAsync(Addressables.MergeMode mode, Action<IList<object>> callBack, Type type, params string[] keys)
    {
        // 使用反射调用泛型方法
        if (loadAssetsAsyncGeneric != null)
        {
            MethodInfo genericMethod = loadAssetsAsyncGeneric.MakeGenericMethod(type);
            genericMethod.Invoke(this, new object[] { mode, callBack, keys });
        }
    }
    
    private void LoadAssetsAsyncGeneric<T>(Addressables.MergeMode mode, Action<IList<T>> callBack, params string[] keys) where T : class
    {
        LoadAssetsAsync(mode, callBack, keys);
    }

    public void Release(string name, Type type)
    {
        string key = name + "-" + type.Name;
        if (handlesDic.TryGetValue(key, out var value))
        {
            value.Release();
            Debug.Log(key + ":引用" + value.Count);
            // 引用计数为0才真正释放
            if (value.Count == 0)
            {
                Addressables.Release(value.Handle);
                handlesDic.Remove(key);
            }
        }
        else
        {
            Debug.LogWarning("释放失败-" + name);
        }
    }

    public void Release(Type type, params string[] keys)
    {
        List<string> keysList = keys.ToList();
        // 拼接存入dic的key
        string key = "";
        for (int i = 0; i < keysList.Count; i++)
        {
            key += keysList[i] + "-";
        }

        key += type.Name;

        if (handlesDic.TryGetValue(key, out var value))
        {
            value.Release();
            Debug.Log(key + ":引用" + value.Count);
            // 引用计数为0才真正释放
            if (value.Count == 0)
            {
                Addressables.Release(value.Handle);
                handlesDic.Remove(key);
            }
        }
        else
        {
            Debug.LogWarning("释放失败-" + key);
        }
    }

    #endregion

}