using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace GameFramework
{
    public class XmlDataManager : BaseSingleton<XmlDataManager>
    {
        public void Save(object data, string fileName)
        {
            var path = Application.persistentDataPath + "/" + fileName + ".xml";

            using (var file = new StreamWriter(path))
            {
                var ser = new XmlSerializer(data.GetType());
                ser.Serialize(file, data);
            }
        }

        public object Load(Type type, string fileName)
        {
            var path = Application.persistentDataPath + "/" + fileName + ".xml";

            if (!File.Exists(path))
            {
                path = Application.streamingAssetsPath + "/" + fileName + ".xml";
                if (!File.Exists(path))
                    // 两个路径都没有则创建空对象
                    return Activator.CreateInstance(type);
            }

            using (var file = new StreamReader(path))
            {
                var serializer = new XmlSerializer(type);
                return serializer.Deserialize(file);
            }
        }
    }
}