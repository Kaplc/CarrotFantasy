using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

public class ABManager : BaseSingleton<ABManager>
{
    // 主包
    private AssetBundle mainAssetBundle;

    // 配置文件
    private AssetBundleManifest manifest;

    // 储存已经加载的ab包
    private Dictionary<string, AssetBundle> assetBundlesDic = new Dictionary<string, AssetBundle>();

    private string abPath = Application.streamingAssetsPath + "/";

    // 根据平台选择主包名
    private string MainAbName
    {
        get
        {
#if UNITY_IOS
                return "IOS";
#elif UNITY_ANDROID
                return "Android";
#else
            return "PC";
#endif
        }
    }

    /// <summary>
    /// 加载主包和配置文件
    /// </summary>
    private void LoadMainAssetBundle()
    {
        if (!mainAssetBundle)
        {
            mainAssetBundle = AssetBundle.LoadFromFile(abPath + MainAbName);
            manifest = mainAssetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }
    }

    /// <summary>
    /// 加载所有依赖包
    /// </summary>
    /// <param name="abName"></param>
    private void LoadDependencyPackage(string abName)
    {
        LoadMainAssetBundle();

        // 获取该包的所有依赖包名
        string[] dependencyPackages = manifest.GetAllDependencies(abName);
        for (int i = 0; i < dependencyPackages.Length; i++)
        {
            if (!assetBundlesDic.ContainsKey(dependencyPackages[i]))
            {
                AssetBundle ab = AssetBundle.LoadFromFile(abPath + dependencyPackages[i]);
                assetBundlesDic.Add(dependencyPackages[i], ab);
            }
        }
    }

    #region AB包同步加载

    /// <summary>
    /// 泛型同步加载
    /// </summary>
    /// <param name="abName">ab包名</param>
    /// <param name="resName">包中资源文件名</param>
    /// <typeparam name="T">返回泛型类</typeparam>
    /// <returns></returns>
    public T Load<T>(string abName, string resName) where T : Object
    {
        LoadDependencyPackage(abName);

        if (!assetBundlesDic.ContainsKey(abName))
        {
            AssetBundle ab = AssetBundle.LoadFromFile(abPath + abName);
            assetBundlesDic.Add(abName, ab);
        }

        // 如果为GameObject实例化再返回
        if (typeof(T) == typeof(GameObject))
        {
            GameObject obj = assetBundlesDic[abName].LoadAsset<GameObject>(resName);
            return GameObject.Instantiate(obj) as T;
        }

        return assetBundlesDic[abName].LoadAsset<T>(resName);
    }

    /// <summary>
    /// Type同步加载
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public Object Load(string abName, string resName, Type type)
    {
        LoadDependencyPackage(abName);

        if (!assetBundlesDic.ContainsKey(abName))
        {
            AssetBundle ab = AssetBundle.LoadFromFile(abPath + abName);
            assetBundlesDic.Add(abName, ab);
        }

        Object obj = assetBundlesDic[abName].LoadAsset(resName, type);

        if (type == typeof(GameObject))
        {
            return GameObject.Instantiate(obj);
        }

        return obj;
    }

    /// <summary>
    /// 名字同步加载
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <returns></returns>
    public Object Load(string abName, string resName)
    {
        LoadDependencyPackage(abName);

        if (!assetBundlesDic.ContainsKey(abName))
        {
            AssetBundle ab = AssetBundle.LoadFromFile(abPath + abName);
            assetBundlesDic.Add(abName, ab);
        }

        return assetBundlesDic[abName].LoadAsset(resName);
    }

    #endregion

    #region AB包异步加载

    /// <summary>
    /// 泛型异步加载
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <param name="callback"></param>
    /// <typeparam name="T"></typeparam>
    public void LoadAsync<T>(string abName, string resName, UnityAction<T> callback) where T : class
    {
        LoadDependencyPackage(abName);

        // 是否加载过ab包
        if (!assetBundlesDic.ContainsKey(abName))
        {
            // 开启协程加载ab包
            MonoManager.Instance.StartCoroutineFrameWork(LoadAbAsyncCoroutine(abName, resName, callback));
        }
    }

    /// <summary>
    /// 泛型异步加载AB包协程
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <param name="callback"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    private IEnumerator LoadAbAsyncCoroutine<T>(string abName, string resName, UnityAction<T> callback) where T : class
    {
        AssetBundleCreateRequest abCreateRequest = AssetBundle.LoadFromFileAsync(abPath + abName);
        yield return abCreateRequest;

        // 加载完成的AB包存入容器
        assetBundlesDic.Add(abName, abCreateRequest.assetBundle);

        // 开启协程加载包中资源
        MonoManager.Instance.StartCoroutineFrameWork(LoadResAsyncCoroutine(abCreateRequest.assetBundle, resName, callback));
    }

    /// <summary>
    /// 泛型异步加载ab包资源协程
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    private IEnumerator LoadResAsyncCoroutine<T>(AssetBundle assetBundle, string resName, UnityAction<T> callBack) where T : class
    {
        AssetBundleRequest abRequest = assetBundle.LoadAssetAsync<T>(resName);
        yield return abRequest;

        if (typeof(T) == typeof(GameObject))
        {
            callBack.Invoke(GameObject.Instantiate(abRequest.asset) as T);
        }
        else
        {
            callBack.Invoke(abRequest.asset as T);
        }
    }

    public void LoadAsync(string abName, string resName, Type type, UnityAction<Object> callback)
    {
        LoadDependencyPackage(abName);

        // 是否加载过ab包
        if (!assetBundlesDic.ContainsKey(abName))
        {
            // 开启协程加载ab包
            MonoManager.Instance.StartCoroutineFrameWork(TypeLoadAbAsyncCoroutine(abName, resName, type, callback));
        }
    }

    private IEnumerator TypeLoadAbAsyncCoroutine(string abName, string resName, Type type, UnityAction<Object> callback)
    {
        AssetBundleCreateRequest abCreateRequest = AssetBundle.LoadFromFileAsync(abPath + abName);
        yield return abCreateRequest;

        // 加载完成的AB包存入容器
        assetBundlesDic.Add(abName, abCreateRequest.assetBundle);

        // 开启协程加载包中资源
        MonoManager.Instance.StartCoroutineFrameWork(TypeLoadResAsyncCoroutine(abCreateRequest.assetBundle, resName, type, callback));
    }

    private IEnumerator TypeLoadResAsyncCoroutine(AssetBundle assetBundle, string resName, Type type, UnityAction<Object> callBack)
    {
        AssetBundleRequest abRequest = assetBundle.LoadAssetAsync(abPath + resName);
        yield return abRequest;

        if (type == typeof(GameObject))
        {
            callBack.Invoke(GameObject.Instantiate(abRequest.asset));
        }
        else
        {
            callBack.Invoke(abRequest.asset);
        }
    }

    public void LoadAsync(string abName, string resName, UnityAction<Object> callback)
    {
        LoadDependencyPackage(abName);

        // 是否加载过ab包
        if (!assetBundlesDic.ContainsKey(abName))
        {
            // 开启协程加载ab包
            MonoManager.Instance.StartCoroutineFrameWork(NameLoadAbAsyncCoroutine(abName, resName, callback));
        }
    }

    private IEnumerator NameLoadAbAsyncCoroutine(string abName, string resName, UnityAction<Object> callback)
    {
        AssetBundleCreateRequest abCreateRequest = AssetBundle.LoadFromFileAsync(abPath + abName);
        yield return abCreateRequest;

        // 加载完成的AB包存入容器
        assetBundlesDic.Add(abName, abCreateRequest.assetBundle);

        // 开启协程加载包中资源
        MonoManager.Instance.StartCoroutineFrameWork(NameLoadResAsyncCoroutine(abCreateRequest.assetBundle, resName, callback));
    }

    private IEnumerator NameLoadResAsyncCoroutine(AssetBundle assetBundle, string resName, UnityAction<Object> callBack)
    {
        AssetBundleRequest abRequest = assetBundle.LoadAssetAsync(resName);
        yield return abRequest;
        
        callBack.Invoke(abRequest.asset);
    }

    #endregion

    #region AB包卸载

    /// <summary>
    /// 卸载指定ab包
    /// </summary>
    /// <param name="abName"></param>
    public void UnLoadAssetBundle(string abName)
    {
        if (assetBundlesDic.ContainsKey(abName))
        {
            assetBundlesDic[abName].Unload(false);
            assetBundlesDic.Remove(abPath);
        }
    }

    /// <summary>
    /// 卸载所有ab包
    /// </summary>
    public void UnLoadALlAssetBundle()
    {
        AssetBundle.UnloadAllAssetBundles(false);
        assetBundlesDic.Clear();

        mainAssetBundle = null;
        manifest = null;
    }

    #endregion
}