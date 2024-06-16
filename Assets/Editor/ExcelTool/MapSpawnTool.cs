using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using App.Data.DataClass.Map;
using App.Static.Enum;
using Excel;
using Library;
using UnityEditor;
using UnityEngine;

public class MapSpawnTool : EditorWindow
{
    private string excelPath = "未选择Excel文件";
    private string cSharpSavePath;
    private string binarySavePath;

    private List<DataTable> tablesList = new List<DataTable>();
    private int index;
    private List<string> tableNames = new List<string>();
    private MapData mapData;

    [MenuItem("Editor/ExcelTool/MapSpawnTool")]
    public static void OpenWindow()
    {
        EditorWindow w = GetWindow<MapSpawnTool>();
        w.Show();
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Excel配表规则:\n" +
                                   "1.第一行写字段名\n" +
                                   "2.第二行写字段类型\n" +
                                   "3.第三行用‘Key’标识主键位置\n" +
                                   "4.第四行为字段名中文中文提示\n" +
                                   "5.第五行开始真正的数据\n" +
                                   "6.相同波数视为合并一起同一波出怪", GUILayout.Height(110));

        EditorGUILayout.BeginHorizontal();
        
        if (GUILayout.Button("选择Excel文件"))
        {
            excelPath = EditorUtility.OpenFilePanel("选择Excel文件", Application.dataPath, "xlsx,xls");
            LoadExcelFile(excelPath);
        }

        if (GUILayout.Button("重新加载Excel"))
        {
            LoadExcelFile(excelPath);
        }
        
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.LabelField("当前Excel文件路径:" + excelPath);

        index = EditorGUILayout.Popup("选择表", index, tableNames.ToArray());
        mapData = EditorGUILayout.ObjectField("写入的地图数据", mapData, typeof(MapData), false) as MapData;

        if (GUILayout.Button("写入ScriptableObject"))
        {
            if (excelPath == "未选择Excel文件")
            {
                Debug.LogError("未选择Excel文件");
                return;
            }

            GetExcelData();
        }
    }

    #region 获取表的规则

    private void LoadExcelFile(string path)
    {
        // 加载Excel文件
        using (FileStream fileStream = File.Open(path, FileMode.Open, FileAccess.Read))
        {
            IExcelDataReader excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fileStream);
            // 读取所有表
            DataTableCollection tables = excelDataReader.AsDataSet().Tables;
            // 获取所有表的名字
            tableNames = new List<string>();
            tablesList = new List<DataTable>();
            foreach (DataTable table in tables)
            {
                tableNames.Add(table.TableName);
                tablesList.Add(table);
            }
        }
    }

    private void GetExcelData()
    {
        if (mapData is null)
        {
            Debug.LogError("未选择地图数据");
            return;
        }
        // 加载Excel数据
        DataTable table = tablesList[index];
        List<WaveData> waveDataList = new List<WaveData>();
        WaveData waveData = new WaveData();
        int lastRowWaveCount = 0;
        for (int i = 4; i < table.Rows.Count; i++)
        {
            DataRow row = table.Rows[i];

            if (lastRowWaveCount != Convert.ToInt32(row[0]))
            {
                // 读取数据
                int waveCount = Convert.ToInt32(row[0]);
                float waveDuration = Convert.ToSingle(row[1]);
                float waveHard = Convert.ToSingle(row[2]);
                EMonsterType monsterType = (EMonsterType)Enum.Parse(typeof(EMonsterType), row[3].ToString());
                int monsterCount = Convert.ToInt32(row[4]);
                float monsterDuration = Convert.ToSingle(row[5]);

                // 写入数据
                waveData = new WaveData(); // 重新创建一个波数据
                waveData.waveCount = waveCount;
                waveData.waveDuration = waveDuration;
                EachWaveData eachWaveData = new EachWaveData(); // 创建一个波中具体细节数据
                eachWaveData.hard = waveHard;
                eachWaveData.monsterType = monsterType;
                eachWaveData.monsterCount = monsterCount;
                eachWaveData.monsterDuration = monsterDuration;
                waveData.eachWaveDataList.Add(eachWaveData);
                waveDataList.Add(waveData);
                
                lastRowWaveCount = waveCount;
            }
            else
            {
                // 相同波数视为合并一起同一波出怪
                float waveHard = Convert.ToSingle(row[2]);
                EMonsterType monsterType = (EMonsterType)Enum.Parse(typeof(EMonsterType), row[3].ToString());
                int monsterCount = Convert.ToInt32(row[4]);
                float monsterDuration = Convert.ToSingle(row[5]);
                
                EachWaveData eachWaveData = new EachWaveData(); // 创建一个波中具体细节数据
                eachWaveData.hard = waveHard;
                eachWaveData.monsterType = monsterType;
                eachWaveData.monsterCount = monsterCount;
                eachWaveData.monsterDuration = monsterDuration;
                waveData.eachWaveDataList.Add(eachWaveData);
            }
        }
        
        SaveToScriptableObject(waveDataList);
    }
    
    private void SaveToScriptableObject(List<WaveData> waveDataList)
    {
        mapData.waveDataList = waveDataList;
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 获取主键在第几列的索引
    /// </summary>
    private int GetPrimaryKeyIndex(DataTable table)
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
    private DataRow GetFieldName(DataTable table)
    {
        return table.Rows[0];
    }

    /// <summary>
    /// 获取字段类型的行
    /// </summary>
    private DataRow GetFieldType(DataTable table)
    {
        return table.Rows[1];
    }

    #endregion
}