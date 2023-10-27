using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using Excel;
using UnityEditor;
using UnityEngine;

public static class ExcelTool
{
    
    // 使用菜单必须为静态方法
    [MenuItem("Editor/ExcelTool/ViewRules")]
    public static void ViewRules()
    {
        Debug.Log("-----------------------------------");
        Debug.Log("Excel配表规则:");
        Debug.Log("1.第一行写字段名");
        Debug.Log("2.第二行写字段类型");
        Debug.Log("3.第三行用‘Key’标识主键位置");
        Debug.Log("4.第四行为字段名中文中文提示");
        Debug.Log("5.第五行开始真正的数据");
    }


    [MenuItem("Editor/ExcelTool/ExcelToBinary")]
    public static void ExcelToBinary()
    {
        // 获取文件夹
        DirectoryInfo directoriesInfo = Directory.CreateDirectory(BinaryManager.EXCELFILE_PATH);
        // 获取文件
        FileInfo[] files = directoriesInfo.GetFiles();
        // 储存Excel表容器
        DataTableCollection tables;

        // 遍历所有Excel文件
        for (int i = 0; i < files.Length; i++)
        {
            // 去除非excel文件
            if (files[i].Extension == ".xlsx" || files[i].Extension == ".xls")
            {
                // 打开文件
                using (FileStream fileStream = files[i].Open(FileMode.Open, FileAccess.Read))
                {
                    // 读取Excel
                    IExcelDataReader excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fileStream);
                    // 读取所有表
                    tables = excelDataReader.AsDataSet().Tables;
                    fileStream.Close();
                }

                // 遍历Excel内所有表
                foreach (DataTable table in tables)
                {
                    // 根据字段名生成信息类和对应容器
                    GenerateClass(table);
                    GenerateContainer(table);
                    GenerateBinary(table);
                }
            }
        }
        
        Debug.Log($"Finish! => Files in {BinaryManager.BINARYFILE_PATH}");
    }

    /// <summary>
    /// 生成信息类
    /// </summary>
    public static void GenerateClass(DataTable table)
    {
        // 判断路径
        if (!Directory.Exists(BinaryManager.INFOCLASS_PATH))
        {
            Directory.CreateDirectory(BinaryManager.INFOCLASS_PATH);
        }

        // 获取字段名
        DataRow fieldNameRow = GetFieldName(table);
        // 获取字段类型
        DataRow fieldTypeRow = GetFieldType(table);

        // 拼接文本
        string text = $"public class {table.TableName}\n";
        text += "{\n";
        for (int i = 0; i < table.Columns.Count; i++)
        {
            // 遍历所有列，拼接字段
            text += $"    public {fieldTypeRow[i]} {fieldNameRow[i]};\n";
        }

        text += "}\n";

        File.WriteAllText(BinaryManager.INFOCLASS_PATH + $"{table.TableName}.cs", text);

        // 刷新Project
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 生成存储信息类的容器
    /// </summary>
    public static void GenerateContainer(DataTable table)
    {
        if (!Directory.Exists(BinaryManager.INFOCLASS_PATH))
        {
            Directory.CreateDirectory(BinaryManager.INFOCLASS_PATH);
        }

        // 获取字段类型
        DataRow fieldTypeRow = GetFieldType(table);
        // 主键列索引
        int primaryKeyIndex = GetPrimaryKeyIndex(table);

        string text = "using System.Collections.Generic;\n";
        text += $"public class {table.TableName}Container\n";
        text += "{\n";
        text +=
            $"    public Dictionary<{fieldTypeRow[primaryKeyIndex]},{table.TableName}> {table.TableName}Dic = new Dictionary<{fieldTypeRow[primaryKeyIndex]},{table.TableName}>();\n";
        text += "}\n";

        File.WriteAllText(BinaryManager.INFOCLASS_PATH + $"{table.TableName}Container.cs", text);
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 生成二进制文件
    /// </summary>
    public static void GenerateBinary(DataTable table)
    {
        if (!Directory.Exists(BinaryManager.BINARYFILE_PATH))
        {
            Directory.CreateDirectory(BinaryManager.BINARYFILE_PATH);
        }

        // 创建新的二进制文件
        using (FileStream fileStream = new FileStream(BinaryManager.BINARYFILE_PATH + table.TableName + ".zy", FileMode.Create, FileAccess.Write))
        {
            // 定义储存规则
            // 1.写入真实数据的行数, (table.Rows.Count - 4)第5行才是数据
            fileStream.Write(BitConverter.GetBytes(table.Rows.Count - 4), 0, sizeof(int));
            // 2.写入主键名
            string primaryKeyName = GetFieldName(table)[GetPrimaryKeyIndex(table)].ToString();
            fileStream.Write(BitConverter.GetBytes(primaryKeyName.Length), 0, sizeof(int)); // 先储存主键字符串名长度
            fileStream.Write(Encoding.UTF8.GetBytes(primaryKeyName), 0, primaryKeyName.Length); // 储存主键名
            // 3.遍历表中所有数据行写入
            for (int i = 4; i < table.Rows.Count; i++)
            {
                // 遍历每一列
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    // 根据每一列的类型
                    if (GetFieldType(table)[j].ToString() == "int")
                    {
                        fileStream.Write(BitConverter.GetBytes(int.Parse(table.Rows[i][j].ToString())), 0, sizeof(int));
                    }

                    if (GetFieldType(table)[j].ToString() == "float")
                    {
                        fileStream.Write(BitConverter.GetBytes(float.Parse(table.Rows[i][j].ToString())), 0, sizeof(float));
                    }

                    if (GetFieldType(table)[j].ToString() == "bool")
                    {
                        fileStream.Write(BitConverter.GetBytes(bool.Parse(table.Rows[i][j].ToString())), 0, sizeof(bool));
                    }

                    if (GetFieldType(table)[j].ToString() == "string")
                    {
                        string text = table.Rows[i][j].ToString();
                        // 先储存字符串长度
                        fileStream.Write(BitConverter.GetBytes(text.Length), 0, sizeof(int));
                        // 再存字符串
                        fileStream.Write(Encoding.UTF8.GetBytes(text), 0, text.Length);
                    }
                }
            }

            fileStream.Close();
        }

        AssetDatabase.Refresh();
    }

    #region 获取表的规则

    /// <summary>
    /// 获取主键在第几列的索引
    /// </summary>
    public static int GetPrimaryKeyIndex(DataTable table)
    {
        // 获取主键的行 
        DataRow row = table.Rows[2];
        for (int i = 0; i < table.Rows.Count; i++)
        {
            if (row[i].ToString() == "Key" || row[i].ToString() == "key")
            {
                return i;
            }
        }

        return 0;
    }

    /// <summary>
    /// 获取字段名的行
    /// </summary>
    public static DataRow GetFieldName(DataTable table)
    {
        return table.Rows[0];
    }

    /// <summary>
    /// 获取字段类型的行
    /// </summary>
    public static DataRow GetFieldType(DataTable table)
    {
        return table.Rows[1];
    }

    #endregion
}