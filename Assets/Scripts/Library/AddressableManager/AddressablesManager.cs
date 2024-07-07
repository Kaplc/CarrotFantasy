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
    public AsyncOperationHandle Handle => handle;

    public CustomAsyncOperationHandle(AsyncOperationHandle h)
    {
        handle = h;
        count = 0;
    }

    public void Release()
    {
        count--;
    }

    public void Use()
    {
        count++;
    }
}

/// <summary>
/// 多资源加载处理器
/// </summary>
public class PreloadAssetInfoHandle
{
    public int count; // 总共要加载的资源数量
    public Action<bool> callBack; // 全部资源加载完成回调

    public PreloadAssetInfoHandle(int count, Action<bool> callBack)
    {
        this.count = count;
        this.callBack = callBack;
    }

    public void Done()
    {
        count--;
        if (count == 0)
        {
            callBack?.Invoke(true);
        }
    }
}

/// <summary>
/// 单资源加载信息
/// </summary> 
public class PreloadAssetInfo
{
    public Type type;
    public Action<bool> callBack;
    public string[] keys;

    public PreloadAssetInfo(Type type, Action<bool> callBack = null, params string[] keys)
    {
        this.type = type;
        this.callBack = callBack;
        this.keys = keys;
    }

    public PreloadAssetInfo(Type type, params string[] keys)
    {
        this.type = type;
        this.callBack = null;
        this.keys = keys;
    }
}

public class PreloadAssetInfo<T> : PreloadAssetInfo where T : class
{
    public PreloadAssetInfo(Action<bool> callBack = null, params string[] keys) : base(typeof(T), callBack, keys)
    {
    }

    public PreloadAssetInfo(params string[] keys) : base(typeof(T), null, keys)
    {
    }
}

public class AddressablesManager : IAddressablesManager
{
    private Dictionary<string, CustomAsyncOperationHandle> handlesDic;

    private List<PreloadAssetInfoHandle> preloadAssetInfoHandles;

    private MethodInfo loadAssetAsyncGeneric;
    private MethodInfo loadAssetsAsyncGeneric;

    public AddressablesManager()
    {
        PreloadAssetInfo<GameObject> preloadAssetInfo = new PreloadAssetInfo<GameObject>("Prefabs/Player");
        handlesDic = new Dictionary<string, CustomAsyncOperationHandle>();
        preloadAssetInfoHandles = new List<PreloadAssetInfoHandle>();

        // 反射获取私有方法
        loadAssetAsyncGeneric = typeof(AddressablesManager).GetMethod(nameof(LoadAssetAsyncGeneric), BindingFlags.NonPublic | BindingFlags.Instance);
        loadAssetsAsyncGeneric = typeof(AddressablesManager).GetMethod(nameof(LoadAssetsAsyncGeneric), BindingFlags.NonPublic | BindingFlags.Instance);
    }

    // /// <summary>
    // /// 单个名称或标签加载
    // /// </summary>
    // /// <param name="name"></param>
    // /// <param name="callBack"></param>
    // /// <typeparam name="T"></typeparam>
    // public void LoadAssetAsync<T>(string name, Action<T> callBack) where T : class
    // {
    //     string key = $"{name}-{typeof(T).Name}";

    //     if (handlesDic.TryGetValue(key, out var value))
    //     {
    //         AsyncOperationHandle<T> h = value.Handle.Convert<T>();

    //         if (h.IsDone)
    //         {
    //             // 加载完成直接执行回调
    //             callBack?.Invoke(h.Result);
    //         }
    //         else
    //         {
    //             // 添加进完成回调
    //             h.Completed += operationHandle =>
    //             {
    //                 if (operationHandle.Status == AsyncOperationStatus.Succeeded)
    //                 {
    //                     // 将加载成功的资源返回
    //                     callBack?.Invoke(operationHandle.Result);
    //                 }
    //                 else
    //                 {
    //                     Debug.LogWarning($"加载失败{key}");
    //                     // 加载失败返回null
    //                     callBack?.Invoke(null);
    //                 }
    //             };
    //         }

    //         return;
    //     }

    //     AsyncOperationHandle handle = Addressables.LoadAssetAsync<T>(name);
    //     handle.Completed += operationHandle =>
    //     {
    //         if (operationHandle.Status == AsyncOperationStatus.Succeeded)
    //         {
    //             // 将加载成功的资源返回
    //             callBack?.Invoke(handlesDic[key].Handle.Convert<T>().Result);
    //         }
    //         else
    //         {
    //             Debug.LogWarning($"加载失败{key}");
    //             // 加载失败返回null
    //             callBack?.Invoke(null);
    //         }
    //     };
    //     // 将操作句柄保存
    //     handlesDic.Add(key, new CustomAsyncOperationHandle(handle));
    // }

    /// <summary>
    /// 多名称和标签条件加载
    /// </summary>
    /// <param name="callBack">回调</param>
    /// <param name="keys">筛选条件</param>
    /// <typeparam name="T"></typeparam>
    public void LoadAssetAsync<T>(Action<T> callBack, params string[] keys) where T : class
    {
        List<string> keysList = keys.ToList();
        // 拼接存入 dic的 key
        string key = "";
        for (int i = 0; i < keysList.Count; i++)
        {
            key += keysList[i] + "-";
        }

        key += typeof(T).Name;

        if (handlesDic.TryGetValue(key, out var value))
        {
            AsyncOperationHandle<IList<T>> h = value.Handle.Convert<IList<T>>();
            value.Use();

            if (h.IsDone)
            {
                if (h.Result.Count > 1)
                {
                    Debug.LogWarning(key + "获取到多个资源默认返回第一个");
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
                            Debug.LogWarning(key + "获取到多个资源默认返回第一个");
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
                handlesDic[key].Use();

                // 加载成功回调
                if (h.Result.Count > 1)
                {
                    Debug.LogWarning(key + "获取到多个资源默认返回第一个");
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
            value.Use();

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
                handlesDic[key].Use();
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

    public void LoadAssetAsync(Type type, Action<object> callBack, params string[] keys)
    {
        // 使用反射调用泛型方法
        if (loadAssetAsyncGeneric != null)
        {
            MethodInfo genericMethod = loadAssetAsyncGeneric.MakeGenericMethod(type);
            genericMethod.Invoke(this, new object[] { callBack, keys });
        }
    }

    private void LoadAssetAsyncGeneric<T>(Action<object> callBack, params string[] keys) where T : class
    {
        LoadAssetAsync<T>(callBack, keys);
    }

    public void LoadAssetsAsync(Type type, Addressables.MergeMode mode, Action<IList<object>> callBack, params string[] keys)
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

    #region 预加载

    /// <summary>
    /// 预加载单个资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void PreloadAssets<T>(Action<bool> callBack, params string[] keys) where T : class
    {
        List<string> keysList = keys.ToList();
        // 拼接存入dic的key
        string key = "";
        for (int i = 0; i < keysList.Count; i++)
        {
            key += keysList[i] + "-";
        }

        key += typeof(T).Name;

        if (handlesDic.ContainsKey(key))
        {
            return;
        }

        Action<T> action = obj =>
        {
            callBack?.Invoke(obj != null);
        };

        LoadAssetAsync<T>(action, keys);
    }

    /// <summary>
    /// 预加载单个资源非泛型
    /// </summary>
    /// <param name="type">Type</param>
    /// <param name="callBack">是否加载完成回调</param>
    /// <param name="keys">条件</param>
    public void PreloadAssetAsync(Type type, Action<bool> callBack, params string[] keys)
    {
        string key = "";
        for (int i = 0; i < keys.Length; i++)
        {
            key += keys[i] + "-";
        }

        key += type.Name;

        if (handlesDic.ContainsKey(key))
        {
            if(handlesDic[key].Handle.IsDone)
            {
                callBack?.Invoke(true);
            }
            else
            {
                handlesDic[key].Handle.Completed += operationHandle =>
                {
                    callBack?.Invoke(true);
                };
            }
            return;
        }

        if (loadAssetAsyncGeneric != null)
        {
            Action<object> action = obj =>
            {
                callBack?.Invoke(obj != null);
            };

            MethodInfo genericMethod = loadAssetAsyncGeneric.MakeGenericMethod(type);
            genericMethod.Invoke(this, new object[] { action, keys });
        }
    }

    /// <summary>
    /// 一次性加载预多个资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="callBack">全部加载完成回调</param>
    /// </summary>
    public void PreloadAssetsAsync(Action<bool> callBack, params PreloadAssetInfo[] assetInfos)
    {
        // 全部加载完成移除
        callBack += isDone =>
        {
            for (int i = 0; i < preloadAssetInfoHandles.Count; i++)
            {
                PreloadAssetInfoHandle h = preloadAssetInfoHandles[i];
                if (h.count == 0)
                {
                    preloadAssetInfoHandles.Remove(h);
                }
            }
        };
        PreloadAssetInfoHandle handle = new PreloadAssetInfoHandle(assetInfos.Length, callBack);
        preloadAssetInfoHandles.Add(handle);
        for (int i = 0; i < assetInfos.Length; i++)
        {
            PreloadAssetInfo info = assetInfos[i];
            if (info == null)
            {
                info.callBack = isDone =>
                {
                    handle.Done();
                };
            }
            else
            {
                info.callBack += isDone =>
                {
                    handle.Done();
                };
            }

            PreloadAssetAsync(info.type, info.callBack, info.keys);
        }
    }

    #endregion
}