using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace GameFramework
{
    public class SceneManager : BaseSingleton<SceneManager>
    {
        private Action<float> processAcion;

        public List<string> sceneNames;

        public SceneManager()
        {
            sceneNames = new List<string>();
            for (var i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings; i++)
            {
                var scenePath = SceneUtility.GetScenePathByBuildIndex(i);
                var sceneFileName = Path.GetFileNameWithoutExtension(scenePath);
                sceneNames.Add(sceneFileName);
            }
        }

        public void LoadScene(string sceneName, UnityAction callBack = null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
            callBack?.Invoke();
        }

        public void LoadSceneAsync(string sceneName, Action<bool> callBack = null)
        {
            MonoManager.Instance.StartCoroutineFrameWork(LoadSceneAsyncCoroutine(sceneName, callBack));
        }

        private IEnumerator LoadSceneAsyncCoroutine(string sceneName, Action<bool> callBack)
        {
            // 判断本地有无场景
            if (sceneNames.Contains(sceneName))
            {
                var ao = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);

                while (!ao.isDone)
                {
                    processAcion?.Invoke(ao.progress);
                    yield return ao;
                }

                callBack?.Invoke(true);
                yield break;
            }

            // 从addressables加载场景
            AddressablesManager.Instance.LoadSceneAsync(sceneName, callBack);
        }


        public void GetProcess(Action<float> action)
        {
            processAcion = action;
        }
    }
}