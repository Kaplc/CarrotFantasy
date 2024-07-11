using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace GameFramework
{
    public class CustomAsyncOperationHandle
    {
        public CustomAsyncOperationHandle(AsyncOperationHandle h)
        {
            Handle = h;
            Count = 0;
        }

        public int Count { get; private set; }

        public AsyncOperationHandle Handle { get; }

        public void Release()
        {
            Count--;
        }

        public void Use()
        {
            Count++;
        }
    }

    /// <summary>
    ///     多资源加载处理器
    /// </summary>
    public class PreloadAssetInfoHandle
    {
        private readonly Action<bool> callBack; // 全部资源加载完成回调
        public int count; // 总共要加载的资源数量

        public PreloadAssetInfoHandle(int count, Action<bool> callBack)
        {
            this.count = count;
            this.callBack = callBack;
        }

        public void Done()
        {
            count--;
            if (count == 0) callBack?.Invoke(true);
        }
    }

    /// <summary>
    ///     单资源加载信息
    /// </summary>
    public class PreloadAssetInfo
    {
        public Action<bool> callBack;
        public string[] keys;
        public Type type;

        public PreloadAssetInfo(Type type, Action<bool> callBack = null, params string[] keys)
        {
            this.type = type;
            this.callBack = callBack;
            this.keys = keys;
        }

        public PreloadAssetInfo(Type type, params string[] keys)
        {
            this.type = type;
            callBack = null;
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

    public class AddressablesManager : BaseSingleton<AddressablesManager>, IAddressablesManager
    {
        private readonly Dictionary<string, CustomAsyncOperationHandle> handlesDic;

        private readonly MethodInfo loadAssetAsyncGeneric;
        private readonly MethodInfo loadAssetsAsyncGeneric;

        private readonly List<PreloadAssetInfoHandle> preloadAssetInfoHandles;
        public AddressablesManager()
        {
            handlesDic = new Dictionary<string, CustomAsyncOperationHandle>();
            preloadAssetInfoHandles = new List<PreloadAssetInfoHandle>();

            // 反射获取私有方法
            loadAssetAsyncGeneric =
                typeof(AddressablesManager).GetMethod(nameof(LoadAssetAsyncGeneric), BindingFlags.NonPublic | BindingFlags.Instance);
            loadAssetsAsyncGeneric =
                typeof(AddressablesManager).GetMethod(nameof(LoadAssetsAsyncGeneric), BindingFlags.NonPublic | BindingFlags.Instance);
        }

        #region 拼接Key
        private string GetKey<T>(params string[] keys)
        {
            var key = keys[0];
            for (var i = 1; i < keys.ToList().Count; i++) key += "-" + keys[i];

            key += "-" + typeof(T).Name;
            return key;
        }

        private string GetKey(Type type, params string[] keys)
        {
            var key = keys[0];
            for (var i = 1; i < keys.ToList().Count; i++) key += "-" + keys[i];

            key += "-" + type.Name;
            return key;
        }
        #endregion

        #region 异步

        /// <summary>
        ///     多名称和标签条件加载
        /// </summary>
        /// <param name="callBack">回调</param>
        /// <param name="use"></param>
        /// <param name="keys">筛选条件</param>
        /// <typeparam name="T"></typeparam>
        public void LoadAssetAsync<T>(Action<T> callBack, bool use, params string[] keys) where T : class
        {
            var key = GetKey<T>(keys);

            if (handlesDic.TryGetValue(key, out var value))
            {
                var loadedHandle = value.Handle.Convert<T>();
                value.Use();

                if (loadedHandle.IsDone)
                    // 加载完成直接执行回调
                    callBack?.Invoke(loadedHandle.Result);
                else
                    // 添加进完成回调
                    loadedHandle.Completed += operationHandle =>
                    {
                        if (operationHandle.Status == AsyncOperationStatus.Succeeded)
                        {
                            // 将加载成功的资源返回
                            callBack?.Invoke(loadedHandle.Result);
                        }
                        else
                        {
                            Debug.LogWarning($"加载失败{key}");
                            // 加载失败返回null
                            callBack?.Invoke(null);
                        }
                    };

                return;
            }

            // 完全没有加载过, 通过资源地址加载资源
            var handle =
                Addressables.LoadResourceLocationsAsync(keys.ToList(), Addressables.MergeMode.Intersection, typeof(T));
            handle.Completed += operationHandle =>
            {
                if (operationHandle.Status == AsyncOperationStatus.Succeeded)
                {
                    if (handle.Result.Count == 0)
                    {
                        Debug.LogError(key + "未获取到资源");
                        return;
                    }

                    // 加载成功回调
                    if (handle.Result.Count > 1)
                    {
                        Debug.LogError(key + " 获取到多个资源请检查筛选条件或使用LoadAssetsAsync方法");
                        return;
                    }

                    LocationToLoadAssetAsync(handle.Result[0], callBack, use, keys);
                }
                else
                {
                    Debug.LogWarning($"{key} 加载失败");
                    callBack?.Invoke(null);
                }
            };
        }

        private void LocationToLoadAssetAsync<T>(IResourceLocation location, Action<T> callBack, bool use, params string[] keys) where T : class
        {
            var key = GetKey<T>(keys);
            if (handlesDic.TryGetValue(key, out var value))
            {
                var loadedHandle = value.Handle.Convert<T>();
                if (use) value.Use();

                if (loadedHandle.IsDone)
                    // 加载完成直接执行回调
                    callBack?.Invoke(loadedHandle.Result);
                else
                    // 添加进完成回调
                    loadedHandle.Completed += operationHandle =>
                    {
                        if (operationHandle.Status == AsyncOperationStatus.Succeeded)
                            // 将加载成功的资源返回
                            callBack?.Invoke(loadedHandle.Result);
                        else
                            Debug.LogWarning($"{key} 加载失败");
                    };

                return;
            }

            AsyncOperationHandle handle = Addressables.LoadAssetAsync<T>(location);

            // 完全没有加载过
            handle.Completed += operationHandle =>
            {
                if (operationHandle.Status == AsyncOperationStatus.Succeeded)
                {
                    var loadedHandle = handle.Convert<T>();
                    if (use) handlesDic[key].Use();

                    // 加载成功回调
                    callBack?.Invoke(loadedHandle.Result);
                }
                else
                {
                    Debug.LogWarning($"{key} 加载失败");
                }
            };
            handlesDic.Add(key, new CustomAsyncOperationHandle(handle));
        }

        /// <summary>
        ///     加载多个资源
        /// </summary>
        /// <param name="mode">
        ///     None：不发生合并，将使用第一组结果
        ///     UseFirst：应用第一组结果
        ///     Union：合并所有结果
        ///     Intersection：使用相交结果
        /// </param>
        /// <param name="callBack"></param>
        /// <param name="use"></param>
        /// <param name="keys"></param>
        /// <typeparam name="T"></typeparam>
        public void LoadAssetsAsync<T>(Action<IList<T>> callBack, Addressables.MergeMode mode, bool use, params string[] keys) where T : class
        {
            var handle = Addressables.LoadResourceLocationsAsync(keys.ToList(), mode, typeof(T));
            handle.Completed += operationHandle =>
            {
                if (operationHandle.Status == AsyncOperationStatus.Succeeded)
                {
                    var list = new List<T>();

                    foreach (var location in handle.Result)
                    {
                        // 添加资源名作为筛选条件
                        var keysList = keys.ToList();
                        keysList.Insert(0, location.PrimaryKey);

                        // 通过资源地址加载资源
                        LocationToLoadAssetAsync<T>(location, res =>
                        {
                            list.Add(res);
                            if (list.Count == handle.Result.Count) callBack?.Invoke(list);
                        }, use, keysList.ToArray());
                    }
                }
                else
                {
                    Debug.LogWarning(GetKey<T>(keys) + " 加载失败");
                    callBack?.Invoke(null);
                }
            };
        }

        public void LoadSceneAsync(string sceneName, Action<bool> callBack)
        {
            Addressables.LoadSceneAsync(sceneName).Completed += operationHandle =>
            {
                if (operationHandle.Status == AsyncOperationStatus.Succeeded)
                {
                    callBack?.Invoke(true);
                }
                else
                {
                    Debug.LogWarning($"{sceneName} 加载失败");
                    callBack?.Invoke(false);
                }
            };
        }

        public T GetAsset<T>(params string[] keys) where T : class
        {
            var key = GetKey<T>(keys);
            if (handlesDic.TryGetValue(key, out var value))
            {
                var loadedHandle = value.Handle.Convert<T>();
                value.Use();
                return loadedHandle.Result;
            }

            Debug.LogWarning($"{key} 未加载过");
            return null;
        }

        #region 非泛型方法

        public void LoadAssetAsync(Type type, Action<object> callBack, bool use, params string[] keys)
        {
            // 使用反射调用泛型方法
            if (loadAssetAsyncGeneric != null)
            {
                var genericMethod = loadAssetAsyncGeneric.MakeGenericMethod(type);
                genericMethod.Invoke(this, new object[] { callBack, use, keys });
            }
        }

        public void LoadAssetAsync(string type, Action<object> callBack, bool use, params string[] keys)
        {
            LoadAssetAsync(Type.GetType(type), callBack, use, keys);
        }

        private void LoadAssetAsyncGeneric<T>(Action<object> callBack, bool use, params string[] keys) where T : class
        {
            LoadAssetAsync<T>(callBack, use, keys);
        }

        public void LoadAssetsAsync(Type type, Action<IList<object>> callBack, Addressables.MergeMode mode, bool use, params string[] keys)
        {
            // 使用反射调用泛型方法
            if (loadAssetsAsyncGeneric != null)
            {
                var genericMethod = loadAssetsAsyncGeneric.MakeGenericMethod(type);
                genericMethod.Invoke(this, new object[] { mode, callBack, use, keys });
            }
        }

        private void LoadAssetsAsyncGeneric<T>(Addressables.MergeMode mode, Action<IList<T>> callBack, bool use, params string[] keys) where T : class
        {
            LoadAssetsAsync(callBack, mode, use, keys);
        }

        public object GetAsset(Type type, params string[] keys)
        {
            var key = GetKey(type, keys);
            if (handlesDic.TryGetValue(key, out var value))
            {
                value.Use();
                return value.Handle.Result;
            }

            Debug.LogWarning($"{key} 未加载过");
            return null;
        }

        public object GetAsset(string type, params string[] keys)
        {
            return GetAsset(Type.GetType(type), keys);
        }

        #endregion

        #endregion

        #region 释放

        public void Release<T>(params string[] keys)
        {
            var key = GetKey<T>(keys);
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
                Debug.LogWarning($"{key} 释放失败");
            }
        }

        public void Release(Type type, params string[] keys)
        {
            var key = GetKey(type, keys);
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
                Debug.LogWarning($"{key} 释放失败");
            }
        }

        public void ReleaseAll()
        {
            foreach (var item in handlesDic.Values) Addressables.Release(item.Handle);

            handlesDic.Clear();
            AssetBundle.UnloadAllAssetBundles(true);
            Resources.UnloadUnusedAssets();
            GC.Collect();
        }

        #endregion

        #region 预加载

        /// <summary>
        ///     预加载单个资源泛型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void PreloadAssets<T>(Action<bool> callBack, params string[] keys) where T : class
        {
            var key = GetKey<T>(keys);
            if (handlesDic.ContainsKey(key)) return;

            Action<T> action = obj => { callBack?.Invoke(obj != null); };

            LoadAssetAsync(action, false, keys);
        }

        /// <summary>
        /// 预加载单个资源Type
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="callBack">是否加载完成回调</param>
        /// <param name="keys">条件</param>
        public void PreloadAssetAsync(Type type, Action<bool> callBack, params string[] keys)
        {
            var key = GetKey(type, keys);
            if (handlesDic.ContainsKey(key))
            {
                if (handlesDic[key].Handle.IsDone)
                    callBack?.Invoke(true);
                else
                    handlesDic[key].Handle.Completed += operationHandle => { callBack?.Invoke(true); };

                return;
            }

            if (loadAssetAsyncGeneric == null) return;

            Action<object> action = obj => { callBack?.Invoke(obj != null); };

            var genericMethod = loadAssetAsyncGeneric.MakeGenericMethod(type);
            genericMethod.Invoke(this, new object[] { action, false, keys });
        }

        /// <summary>
        /// 预加载单个资源字符串转Type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="callBack"></param>
        /// <param name="keys"></param>
        public void PreloadAssetAsync(string type, Action<bool> callBack, params string[] keys)
        {
            PreloadAssetAsync(Type.GetType(type), callBack, keys);
        }


        /// <summary>
        /// 预加载多个单一资源
        /// </summary>
        /// <param name="callBack"></param>
        /// <param name="assetInfos"></param>
        public void PreloadAssetsAsync(Action<bool> callBack, params PreloadAssetInfo[] assetInfos)
        {
            // 全部加载完成移除
            callBack += isDone =>
            {
                for (var i = 0; i < preloadAssetInfoHandles.Count; i++)
                {
                    var h = preloadAssetInfoHandles[i];
                    if (h.count == 0) preloadAssetInfoHandles.Remove(h);
                }
            };
            var handle = new PreloadAssetInfoHandle(assetInfos.Length, callBack);
            preloadAssetInfoHandles.Add(handle);
            foreach (var info in assetInfos)
            {
                if (info.callBack == null)
                    info.callBack = isDone => { handle.Done(); };
                else
                    info.callBack += isDone => { handle.Done(); };

                PreloadAssetAsync(info.type, info.callBack, info.keys);
            }
        }

        /// <summary>
        /// 预加载多个相同条件资源
        /// </summary>
        /// <param name="callBack"></param>
        /// <param name="keys"></param>
        /// <typeparam name="T"></typeparam>
        public void PreloadAssetsAsync<T>(Action<bool> callBack, params string[] keys) where T : class
        {
            LoadAssetsAsync<T>(list => { callBack?.Invoke(list != null); }, Addressables.MergeMode.Intersection, false, keys);
        }

        #endregion

        #region 更新

        /// <summary>
        /// 检查是否有更新并更新目录
        /// </summary>
        /// <param name="callBack"></param>
        public void CheckCatalogs(Action<bool> callBack)
        {
            // 检查是否有目录更新
            AsyncOperationHandle<List<string>> checkHandle = Addressables.CheckForCatalogUpdates();
            checkHandle.Completed += handle =>
            {
                if (handle.Result != null && handle.Result.Count > 0)
                {
                    callBack?.Invoke(true);
                }
                else
                {
                    callBack?.Invoke(false);
                }
            };

        }

        public void UpdateCatalogs(Action<float> callBack = null)
        {
            MonoManager.Instance.StartCoroutine(UpdateCatalogsAsync(callBack));
        }

        private IEnumerator UpdateCatalogsAsync(Action<float> callBack)
        {
            // 检查是否有目录更新
            AsyncOperationHandle<List<string>> checkHandle = Addressables.CheckForCatalogUpdates(false);

            yield return checkHandle;

            if (checkHandle.Status == AsyncOperationStatus.Succeeded)
            {
                List<string> catalogsToUpdate = checkHandle.Result;

                if (catalogsToUpdate != null && catalogsToUpdate.Count > 0)
                {
                    // 应用更新
                    AsyncOperationHandle<List<IResourceLocator>> updateHandle = Addressables.UpdateCatalogs(catalogsToUpdate, false);

                    yield return updateHandle;

                    if (updateHandle.Status == AsyncOperationStatus.Succeeded)
                    {
                        callBack?.Invoke(-2);
                        Debug.Log("Catalogs updated successfully.");
                    }
                    else
                    {
                        callBack?.Invoke(-3);
                        Debug.LogError("Failed to update catalogs.");
                    }
                    Addressables.Release(updateHandle);
                }
                else
                {
                    Debug.Log("No catalogs need updating.");
                    callBack?.Invoke(-1);
                }
            }
            else
            {
                Debug.LogError("Failed to check for catalog updates.");
            }
            Addressables.Release(checkHandle);
        }
        #endregion
    }
}