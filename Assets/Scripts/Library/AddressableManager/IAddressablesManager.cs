using System;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;


public interface IAddressablesManager
{
    void LoadAssetAsync<T>(string name, Action<T> callBack) where T : class;

    void LoadAssetAsync(string name, Type type, Action<object> callBack);

    void LoadAssetAsync<T>(Action<T> callBack, params string[] keys) where T : class;

    void LoadAssetAsync(Action<object> callBack, Type type, params string[] keys);

    void LoadAssetsAsync<T>(Addressables.MergeMode mode, Action<IList<T>> callBack, params string[] keys) where T : class;

    void LoadAssetsAsync(Addressables.MergeMode mode, Action<IList<object>> callBack, Type type, params string[] keys);

    void Release<T>(string name);

    void Release<T>(params string[] keys);

    void Release(string name, Type type);
    void Release(Type type, params string[] keys);
    void Clear();
}