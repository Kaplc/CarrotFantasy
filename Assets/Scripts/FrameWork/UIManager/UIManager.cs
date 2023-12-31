﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EUILayerType
{
    Bottom,
    Middle,
    Top,
    System
}

public class UIManager
{
    private static UIManager instance = new UIManager();
    public static UIManager Instance => instance;

    private Dictionary<string, BasePanel> panelsDic = new Dictionary<string, BasePanel>();

    public Canvas canvas;
    public Camera uiCamera;

    // 各层面板
    private Transform bottom;
    private Transform middle;
    private Transform top;
    private Transform system;

    private UIManager()
    {
        // 获取canvas
        canvas = GameObject.Find("Canvas")?.GetComponent<Canvas>();

        if (!canvas)
        {
            canvas = GameObject.Instantiate(Resources.Load<GameObject>("UI/Canvas")).GetComponent<Canvas>();
        }
        
        // 获取摄像机
        uiCamera = GameObject.Find("UICamera")?.GetComponent<Camera>();
        if (!uiCamera)
        {
            Debug.Log("UI摄像机未获取成功");
        }
        
        // 获取各层
        bottom = canvas.transform.Find("Bottom");
        if (!bottom)
        {
            bottom = new GameObject("Bottom").transform;
            bottom.SetParent(canvas.transform);
        }

        middle = canvas.transform.Find("Middle");
        if (!middle)
        {
            middle = new GameObject("Middle").transform;
            middle.SetParent(canvas.transform);
        }

        top = canvas.transform.Find("Top");
        if (!top)
        {
            top = new GameObject("Top").transform;
            top.SetParent(canvas.transform);
        }

        system = canvas.transform.Find("System");
        if (!system)
        {
            system = new GameObject("System").transform;
            system.SetParent(canvas.transform);
        }

        GameObject.DontDestroyOnLoad(canvas);
    }

    public T Show<T>(bool isFade = true, EUILayerType layerType = EUILayerType.Bottom, UnityAction callBack = null) where T : BasePanel
    {
        // 获取类名与预设体同名
        string panelName = typeof(T).Name;

        // 存在面板直接取出
        if (panelsDic.TryGetValue(panelName, out var value))
        {
            value.Show(isFade, callBack);
            return value as T;
        }

        // 不存在直接创建并保存
        T newPanel = GameObject.Instantiate(Resources.Load<GameObject>("UI/" + panelName), canvas.transform).GetComponent<T>();
        panelsDic.Add(panelName, newPanel);
        newPanel.Show(isFade, callBack);

        // 设置层级
        switch (layerType)
        {
            case EUILayerType.Bottom:
                newPanel.transform.SetParent(bottom);
                break;
            case EUILayerType.Middle:
                newPanel.transform.SetParent(middle);
                break;
            case EUILayerType.Top:
                newPanel.transform.SetParent(top);
                break;
            case EUILayerType.System:
                newPanel.transform.SetParent(system);
                break;
        }

        return newPanel;
    }

    public void Hide<T>(bool isFade = true, UnityAction callBack = null) where T : BasePanel
    {
        string panelName = typeof(T).Name;

        if (panelsDic.TryGetValue(panelName, out BasePanel panel))
        {
            if (isFade)
            {
                callBack += () => { GameObject.Destroy(panel.gameObject); };
                // 淡出
                panel.Hide(callBack);
                // 删除面板
                panelsDic.Remove(panelName);
            }
            else
            {
                // 直接删除面板
                panelsDic.Remove(panelName);
                GameObject.Destroy(panel.gameObject);
                // 直接执行回调
                callBack?.Invoke();
            }
        }
    }

    public T GetPanel<T>() where T : BasePanel
    {
        string panelName = typeof(T).Name;

        if (panelsDic.TryGetValue(panelName, out BasePanel panel))
        {
            return panel as T;
        }

        return null;
    }

    public void CloseAllPanel()
    {
        foreach (KeyValuePair<string, BasePanel> panel in panelsDic)
        {
            GameObject.Destroy(panel.Value.gameObject);
        }
        panelsDic.Clear();
    }
}