using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using Object = UnityEngine.Object;

public class BinaryManager: BaseSingleton<BinaryManager>
{
    public static string EXCELFILE_PATH = Application.dataPath + "/GameInfo/Excel/";
    public static string INFOCLASS_PATH = Application.dataPath + "/Scripts/Data/";
    public static string BINARYFILE_PATH = Application.streamingAssetsPath + "/Data/";

    // 存放读取出来的Excel二进制文件数据
    public Dictionary<string, object> tableDic = new Dictionary<string, object>();
    
    /// <summary>
    /// 读取Excel的二进制文件
    /// </summary>
    /// <typeparam name="T">数据信息类</typeparam>
    /// <typeparam name="K">信息容器类</typeparam>
    public K LoadExcelBinary<T, K>() where K : class
    {
        // 判断是否已经读取过
        if (tableDic.ContainsKey(typeof(T).Name))
        {
            return tableDic[typeof(T).Name] as K;
        }
        
        // 判断文件是否存在
        if (!File.Exists(BinaryManager.BINARYFILE_PATH + typeof(T).Name + ".zy"))
        {
            return default(K);
        }

        using (FileStream fileStream = File.Open(BINARYFILE_PATH + typeof(T).Name + ".zy", FileMode.Open, FileAccess.Read))
        {
            // 定义文件字节大小的字节数组
            byte[] fileBuffer = new byte[fileStream.Length];
            
            // 读取真实数据的行数
            int index = 0;
            fileStream.Read(fileBuffer, index, sizeof(int));
            int count = BitConverter.ToInt32(fileBuffer, 0);
            index += sizeof(int);
            
            // 读取主键字符串
            // 长度
            fileStream.Read(fileBuffer, index, sizeof(int)); 
            int primaryKeyNameLength = BitConverter.ToInt32(fileBuffer, index);
            index += sizeof(int);
            // 字符串
            fileStream.Read(fileBuffer, index, primaryKeyNameLength);
            string primaryKeyName = Encoding.UTF8.GetString(fileBuffer, index, primaryKeyNameLength);
            index += primaryKeyNameLength; // 位移字符串长度
            
            Type type = typeof(T);
            FieldInfo[] fieldInfos = type.GetFields(); // 获取所有字段
            
            // 创建容器
            K newContainer = Activator.CreateInstance<K>();
            
            // 读取每一行数据
            for (int i = 0; i < count; i++)
            {
                // 实例化新的信息类
                T newInfoClass = Activator.CreateInstance<T>();

                // 读取每一列
                for (int j = 0; j < fieldInfos.Length; j++)
                {
                    // 读取int字段
                    if (fieldInfos[j].FieldType == typeof(int))
                    {
                        fileStream.Read(fileBuffer, index, sizeof(int));
                        fieldInfos[j].SetValue(newInfoClass, BitConverter.ToInt32(fileBuffer, index));
                        index += sizeof(int);
                    }
                    
                    if (fieldInfos[j].FieldType == typeof(float))
                    {
                        fileStream.Read(fileBuffer, index, sizeof(float));
                        fieldInfos[j].SetValue(newInfoClass, BitConverter.ToSingle(fileBuffer, index));
                        index += sizeof(float);
                    }
                    
                    if (fieldInfos[j].FieldType == typeof(bool))
                    {
                        fileStream.Read(fileBuffer, index, sizeof(bool));
                        fieldInfos[j].SetValue(newInfoClass, BitConverter.ToBoolean(fileBuffer, index));
                        index += sizeof(bool);
                    }
                
                    // 读取string字段
                    if (fieldInfos[j].FieldType == typeof(string))
                    {
                        // 先获取字符串长度
                        fileStream.Read(fileBuffer, index, sizeof(int));
                        int strLength = BitConverter.ToInt32(fileBuffer, index);
                        index += sizeof(int);
                        
                        fileStream.Read(fileBuffer, index, strLength);
                        string str = Encoding.UTF8.GetString(fileBuffer, index, strLength);
                        fieldInfos[j].SetValue(newInfoClass, str); // 设置字段值
                        index += strLength;
                    }
                }
                
                // 添加进容器
                // 获取
                FieldInfo dic = newContainer.GetType().GetFields()[0];
                MethodInfo methodInfo = dic.FieldType.GetMethod("Add");
                methodInfo?.Invoke(dic.GetValue(newContainer), new object[] {newInfoClass.GetType().GetField(primaryKeyName).GetValue(newInfoClass), newInfoClass });
            }
            
            // 存入tableDic
            tableDic.Add(typeof(T).Name, newContainer);
            
            fileStream.Close();
        }

        return tableDic[typeof(T).Name] as K;
    }
    
    /// <summary>
    /// 读取二进制文件
    /// </summary>
    /// <param name="fileName">文件名要加后缀</param>
    /// <typeparam name="T">返回的类类型</typeparam>
    /// <returns></returns>
    public T Load<T>(string fileName) where T : class
    {
        // 不存在该类的二进制文件返回默认
        if (!File.Exists(BINARYFILE_PATH + fileName))
        {
            return default;
        }

        T obj;
        using (FileStream fileStream = File.Open(BINARYFILE_PATH + fileName, FileMode.Open, FileAccess.Read))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter(); // 序列化工具
            obj  = binaryFormatter.Deserialize(fileStream) as T;
            fileStream.Close();
        }

        return obj;
    }

    public void Save(string fileName, object data)
    {
        // 不存在该类的二进制文件则创建
        if (!File.Exists(BINARYFILE_PATH + fileName))
        {
            File.Create(BINARYFILE_PATH + fileName).Close();
        }

        using (FileStream fileStream = File.Open(BINARYFILE_PATH + fileName, FileMode.Open, FileAccess.Write))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(fileStream, data);
            fileStream.Flush(); // 强制刷新缓存区立刻写入文件
            fileStream.Close();
        }
    }
}
