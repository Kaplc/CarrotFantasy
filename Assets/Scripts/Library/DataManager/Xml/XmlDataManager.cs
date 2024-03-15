using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml.Serialization;
using UnityEngine;

public class XmlDataManager: BaseSingleton<XmlDataManager>
{
    
    public void Save(object data, string fileName)
    {
        string path = Application.persistentDataPath + "/" + fileName + ".xml";

        using (StreamWriter file = new StreamWriter(path))
        {
            XmlSerializer ser = new XmlSerializer(data.GetType());
            ser.Serialize(file, data);
        }
    }

    public object Load(Type type, string fileName)
    {
        string path = Application.persistentDataPath + "/" + fileName + ".xml";

        if (!File.Exists(path))
        {
            path = Application.streamingAssetsPath + "/" + fileName + ".xml";
            if (!File.Exists(path))
            {
                // 两个路径都没有则创建空对象
                return Activator.CreateInstance(type);
            }
        }
        
        using (StreamReader file = new StreamReader(path))
        {
            XmlSerializer serializer = new XmlSerializer(type);
            return serializer.Deserialize(file);
        }
    }
}