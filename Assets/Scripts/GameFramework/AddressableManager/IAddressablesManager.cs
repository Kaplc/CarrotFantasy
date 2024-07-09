using System;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;

namespace GameFramework
{
    public interface IAddressablesManager
    {
        // void LoadAssetAsync<T>(string name, Action<T> callBack) where T : class;

        // void LoadAssetAsync(string name, Type type, Action<object> callBack);

        void LoadAssetAsync<T>(Action<T> callBack, bool use = true, params string[] keys) where T : class;

        void LoadAssetAsync(Type type, Action<object> callBack, bool use = true, params string[] keys);

        void LoadAssetsAsync<T>(Action<IList<T>> callBack, Addressables.MergeMode mode = Addressables.MergeMode.Intersection, bool use = true,
            params string[] keys) where T : class;

        void LoadAssetsAsync(Type type, Action<IList<object>> callBack, Addressables.MergeMode mode = Addressables.MergeMode.Intersection,
            bool use = true, params string[] keys);

        void Release<T>(params string[] keys);
        void Release(Type type, params string[] keys);
        void ReleaseAll();
    }
}