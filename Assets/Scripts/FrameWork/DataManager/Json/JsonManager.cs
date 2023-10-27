using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;

/// <summary>
/// json解析工具类型
/// </summary>
public enum E_JsonTool
{
    JsonUtility,
    LitJson
}

public class JsonManager: BaseSingleton<JsonManager>
{
    
    /// <summary>
    /// 数据保存为Json文件
    /// </summary>
    /// <param name="fileName">文件名</param>
    /// <param name="data">数据对象</param>
    /// <param name="toolType">序列化工具, 默认使用LitJson</param>
    public void Save(string fileName, object data, E_JsonTool toolType)
    {
        string path = Application.persistentDataPath + "/" + fileName + ".json";
        string json = "";

        switch (toolType)
        {
            case E_JsonTool.JsonUtility:
                json = JsonUtility.ToJson(data);
                break;
            case E_JsonTool.LitJson:
                json = JsonMapper.ToJson(data);
                break;
        }

        File.WriteAllText(path, json);
    }
    
    /// <summary>
    /// 加载Json
    /// </summary>
    /// <param name="fileName">文件名</param>
    /// <param name="toolType">序列化工具</param>
    /// <typeparam name="T">泛型</typeparam>
    /// <returns></returns>
    public T Load<T>(string fileName, E_JsonTool toolType) where T : new()
    {
        string path = Application.streamingAssetsPath + "/" + fileName + ".json";

        if (!File.Exists(path))
        {
            path = Application.persistentDataPath + "/" + fileName + ".json";
        }

        T newObj = new T();
        string json = "";
        json = File.ReadAllText(path);
        switch (toolType)
        {
            case E_JsonTool.JsonUtility:
                newObj = JsonUtility.FromJson<T>(json);
                break;
            case E_JsonTool.LitJson:
                newObj = JsonMapper.ToObject<T>(json);
                break;
        }

        return newObj;
    }
}