using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using UnityEngine;

public class SerializerDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
{
    public XmlSchema GetSchema()
    {
        return null;
    }
    
    // 读(反序列化)
    public void ReadXml(XmlReader reader)
    {
        reader.Read(); // 跳过根

        while (reader.NodeType != XmlNodeType.EndElement) // 读到根末尾标签就停止
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey)); 
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

            TKey key = (TKey)keySerializer.Deserialize(reader); // 一行一行读取
            TValue value = (TValue)valueSerializer.Deserialize(reader); ;
            
            this.Add(key, value);
        }
    }

    // 写(序列化)
    public void WriteXml(XmlWriter writer)
    {
        // writer为文件流
        
        // 遍历自身
        foreach (KeyValuePair<TKey,TValue> keyValuePair in this)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey)); // key序列化器
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));
            
            keySerializer.Serialize(writer, keyValuePair.Key); // 使用序列化器写入
            valueSerializer.Serialize(writer, keyValuePair.Value);
        }
        
    }
}