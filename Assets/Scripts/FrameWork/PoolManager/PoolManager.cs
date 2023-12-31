﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 使用对象池的对象要继承该接口
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
        GameObject targetObject = objectList[0];
        // 断开父子关系
        targetObject.transform.parent = null;
        // 从list移除
        objectList.RemoveAt(0);
        
        targetObject.SetActive(true);
        // 调用OnGet方法
        if (targetObject.TryGetComponent<IPoolObject>(out var poolObjectComponent))
        {
            poolObjectComponent.OnGet();
        }
        
        return targetObject;
    }

    public void Push(GameObject gameObject)
    {
        objectList.Add(gameObject);
        // 设置为list的子对象
        gameObject.transform.SetParent(father.transform);
        
        // 调用OnPush方法
        if (gameObject.TryGetComponent<IPoolObject>(out var poolObjectComponent))
        {
            poolObjectComponent.OnPush();
        }
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

    // 初始化缓存池
    private void Init()
    {
        poolDic = new Dictionary<string, PoolData>();
        // 在场景上创建物体统一管理内容
        poolObject = new GameObject("Pool");
        // 
        GameObject.DontDestroyOnLoad(poolObject);
    }


    /// <summary>
    /// 获取对象
    /// </summary>
    /// <param name="fullName">资源名</param>
    /// <param name="asyncHandleFun">异步加载时的执行函数</param>
    /// <returns></returns>
    public GameObject GetObject(string fullName, UnityAction<GameObject> asyncHandleFunc = null)
    {
        // 初始化缓存池
        if (!poolObject) Init();

        // 检查字典有无对象数据
        if (!poolDic.ContainsKey(fullName))
        {
            // 无对象则创建新list
            poolDic.Add(fullName, new PoolData(fullName, poolObject));
        }

        if (poolDic[fullName].objectList.Count > 0)
        {
            return poolDic[fullName].Get();
        }

        // 同步加载
        if (asyncHandleFunc == null)
        {
            GameObject gameObject = GameObject.Instantiate(ResourcesFrameWork.Instance.Load<GameObject>(fullName));
            gameObject.name = fullName;
            
            // 调用OnGet方法
            if (gameObject.TryGetComponent<IPoolObject>(out var poolObjectComponent))
            {
                poolObjectComponent.OnGet();
            }
            
            return gameObject;
        }

        // 异步加载
        ResourcesFrameWork.Instance.LoadAsync<GameObject>(fullName, resObj =>
        {
            GameObject gameObject = GameObject.Instantiate(resObj);
            gameObject.name = fullName;
            asyncHandleFunc.Invoke(gameObject);
        });


        return null;
    }

    // 储存对象
    public void PushObject(GameObject gameObject)
    {
        if (!poolObject) Init();

        // 有list
        if (poolDic.TryGetValue(gameObject.name, out PoolData poolData))
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
            GameObject.Destroy(poolObject);
            poolObject = null;
        }
    }
}