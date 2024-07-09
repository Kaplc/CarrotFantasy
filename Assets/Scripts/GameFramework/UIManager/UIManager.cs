using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using XLua;
using Object = UnityEngine.Object;

namespace GameFramework
{
    public enum EUILayerType
    {
        Bottom,
        Middle,
        Top,
        System
    }

    [LuaCallCSharp]
    public class UIManager
    {
        public Canvas canvas;

        private readonly Dictionary<string, BasePanel> panelsDic = new Dictionary<string, BasePanel>();
        public Camera uiCamera;

        private UIManager()
        {
            // 获取canvas
            canvas = GameObject.Find("Canvas")?.GetComponent<Canvas>();

            if (!canvas)
            {
                Debug.Log("Canvas未获取成功自动创建");
                canvas = Object.Instantiate(Resources.Load<GameObject>("UI/Canvas")).GetComponent<Canvas>();
            }

            // 获取摄像机
            uiCamera = GameObject.Find("UICamera")?.GetComponent<Camera>();
            if (!uiCamera) Debug.Log("UI摄像机未获取成功");

            // 获取各层
            Bottom = canvas.transform.Find("Bottom");
            if (!Bottom)
            {
                Bottom = new GameObject("Bottom").transform;
                Bottom.SetParent(canvas.transform);
            }

            Middle = canvas.transform.Find("Middle");
            if (!Middle)
            {
                Middle = new GameObject("Middle").transform;
                Middle.SetParent(canvas.transform);
            }

            Top = canvas.transform.Find("Top");
            if (!Top)
            {
                Top = new GameObject("Top").transform;
                Top.SetParent(canvas.transform);
            }

            System = canvas.transform.Find("System");
            if (!System)
            {
                System = new GameObject("System").transform;
                System.SetParent(canvas.transform);
            }

            Object.DontDestroyOnLoad(canvas);
        }

        public static UIManager Instance { get; } = new UIManager();

        // 各层面板
        public Transform Bottom { get; }
        public Transform Middle { get; }
        public Transform Top { get; }
        public Transform System { get; }


        public void Hide<T>(bool isFade = true, UnityAction callBack = null) where T : BasePanel
        {
            var panelName = typeof(T).Name;

            if (panelsDic.TryGetValue(panelName, out var panel))
            {
                if (isFade)
                {
                    callBack += () => { Object.Destroy(panel.gameObject); };
                    // 淡出
                    panel.Hide(callBack);
                    // 删除面板
                    panelsDic.Remove(panelName);
                }
                else
                {
                    // 直接删除面板
                    panelsDic.Remove(panelName);
                    Object.Destroy(panel.gameObject);
                    // 直接执行回调
                    callBack?.Invoke();
                }
            }
        }

        public void Hide(string panelName, bool isFade = true, UnityAction callBack = null)
        {
            if (panelsDic.TryGetValue(panelName, out var panel))
            {
                if (isFade)
                {
                    callBack += () => { Object.Destroy(panel.gameObject); };
                    // 淡出
                    panel.Hide(callBack);
                    // 删除面板
                    panelsDic.Remove(panelName);
                }
                else
                {
                    // 直接删除面板
                    panelsDic.Remove(panelName);
                    Object.Destroy(panel.gameObject);
                    // 直接执行回调
                    callBack?.Invoke();
                }
            }
        }

        public T GetPanel<T>() where T : BasePanel
        {
            var panelName = typeof(T).Name;

            if (panelsDic.TryGetValue(panelName, out var panel)) return panel as T;

            return null;
        }

        public BasePanel GetPanel(string panelName)
        {
            if (panelsDic.TryGetValue(panelName, out var panel)) return panel;

            return null;
        }

        public void CloseAllPanel()
        {
            foreach (var panel in panelsDic) Object.Destroy(panel.Value.gameObject);

            panelsDic.Clear();
        }

        #region show

        public T Show<T>(bool isFade = true, EUILayerType layerType = EUILayerType.Bottom, UnityAction callBack = null) where T : BasePanel
        {
            // 获取类名与预设体同名
            var panelName = typeof(T).Name;

            // 存在面板直接取出
            if (panelsDic.TryGetValue(panelName, out var value))
            {
                value.Show(isFade, callBack);
                return value as T;
            }

            return CreateNewPanel(panelName, isFade, layerType, callBack) as T;
        }

        public BasePanel Show(Type panelType, bool isFade = true, EUILayerType layerType = EUILayerType.Bottom, UnityAction callBack = null)
        {
            // 获取类名与预设体同名
            var panelName = panelType.Name;

            // 存在面板直接取出
            if (panelsDic.TryGetValue(panelName, out var value))
            {
                value.Show(isFade, callBack);
                return value;
            }

            return CreateNewPanel(panelName, isFade, layerType, callBack);
        }

        public BasePanel Show(string path, string panelName, bool isFade = true, EUILayerType layerType = EUILayerType.Bottom,
            UnityAction callBack = null)
        {
            // 不存在直接创建并保存
            var newPanel = Object.Instantiate(Resources.Load<GameObject>(path + panelName), canvas.transform).GetComponent(panelName) as BasePanel;
            panelsDic.Add(panelName, newPanel);
            newPanel.Show(isFade, callBack);

            // 设置层级
            switch (layerType)
            {
                case EUILayerType.Bottom:
                    newPanel.transform.SetParent(Bottom);
                    break;
                case EUILayerType.Middle:
                    newPanel.transform.SetParent(Middle);
                    break;
                case EUILayerType.Top:
                    newPanel.transform.SetParent(Top);
                    break;
                case EUILayerType.System:
                    newPanel.transform.SetParent(System);
                    break;
            }

            return newPanel;
        }

        private BasePanel CreateNewPanel(string panelName, bool isFade, EUILayerType layerType, UnityAction callBack)
        {
            try
            {
                // 不存在直接创建并保存
                var newPanel = Object.Instantiate(Resources.Load<GameObject>("UI/" + panelName), canvas.transform).GetComponent<BasePanel>();
                panelsDic.Add(panelName, newPanel);
                newPanel.Show(isFade, callBack);

                // 设置层级
                switch (layerType)
                {
                    case EUILayerType.Bottom:
                        newPanel.transform.SetParent(Bottom);
                        break;
                    case EUILayerType.Middle:
                        newPanel.transform.SetParent(Middle);
                        break;
                    case EUILayerType.Top:
                        newPanel.transform.SetParent(Top);
                        break;
                    case EUILayerType.System:
                        newPanel.transform.SetParent(System);
                        break;
                }

                return newPanel;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }
        }

        #endregion
    }
}