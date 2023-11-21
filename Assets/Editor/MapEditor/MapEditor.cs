using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Codice.Client.BaseCommands;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Map))]
public class MapEditor : Editor
{
    // 当前正在编辑的地图
    public Map map;
    public int nowMapIndex;
    private string[] levelFileName; // 加载到的关卡信息文件名
    private string fileReName;

    private void Awake()
    {
        map = target as Map; // 关联mono脚本
        LoadAllLevelFileName();
        // 默认选中第一个文件
        nowMapIndex = 0;

        fileReName = "复制新地图文件的文件名.md";
        LoadLevel();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space();

        #region 选择地图文件

        EditorGUILayout.BeginHorizontal();
        // 创建下拉列表
        int newMapIndex = EditorGUILayout.Popup(nowMapIndex, levelFileName);
        if (nowMapIndex != newMapIndex)
        {
            nowMapIndex = newMapIndex;
            LoadLevel();
        }

        EditorGUILayout.EndHorizontal();

        #endregion

        EditorGUILayout.Space();

        #region 修改

        EditorGUILayout.BeginHorizontal();
        fileReName = EditorGUILayout.TextField("", fileReName);
        if (GUILayout.Button("复制新地图文件"))
        {
            if (fileReName == "复制新地图文件的文件名.md")
            {
                return;
            }

            for (int i = 0; i < levelFileName.Length; i++)
            {
                if (levelFileName[i] == fileReName)
                {
                    return;
                }
            }

            File.Copy(BinaryManager.BINARYFILE_PATH + map.Path + levelFileName[nowMapIndex],
                BinaryManager.BINARYFILE_PATH + map.Path + fileReName);

            LoadAllLevelFileName();
            Repaint();
            AssetDatabase.Refresh();
        }

        if (GUILayout.Button("创建新文件"))
        {
            if (fileReName == "复制新地图文件的文件名.md")
            {
                return;
            }

            CreateNewFile(fileReName);
        }

        EditorGUILayout.EndHorizontal();
        //===================================
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("绘制放塔点"))
        {
            map.drawTowerPos = true;
            map.drawPath = false;
        }

        if (GUILayout.Button("清除"))
        {
            ClearAllTowerPos();
        }

        EditorGUILayout.EndHorizontal();
        //===================================
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("绘制路径"))
        {
            map.drawTowerPos = false;
            map.drawPath = true;
        }

        if (GUILayout.Button("清除"))
        {
            ClearAllPath();
        }

        EditorGUILayout.EndHorizontal();
        //===================================
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("保存"))
        {
            SaveMapData();
        }

        EditorGUILayout.EndHorizontal();

        #endregion

        Repaint();
    }

    // 保存当前修改状态
    private void SaveMapData()
    {
        SaveData(levelFileName[nowMapIndex]);
        Repaint();
    }
    
    private void SaveMapData(string fileName)
    {
        SaveData(fileName);
        Repaint();
    }

    /// <summary>
    /// 加载所有level文件
    /// </summary>
    /// <returns></returns>
    private void LoadAllLevelFileName()
    {
        DirectoryInfo directoryInfo = Directory.CreateDirectory(BinaryManager.BINARYFILE_PATH + map.Path);
        FileInfo[] fileInfos = directoryInfo.GetFiles();

        List<string> fileName = new List<string>();
        // 加载level文件
        for (int i = 0; i < fileInfos.Length; i++)
        {
            if (fileInfos[i].Extension == ".md")
            {
                fileName.Add(fileInfos[i].Name);
            }
        }

        levelFileName = fileName.ToArray();
    }
    
    /// <summary>
    /// 加载关卡信息
    /// </summary>
    private void LoadLevel()
    {
        map.mapData = BinaryManager.Instance.Load<MapData>(map.Path + levelFileName[nowMapIndex]);
        LoadData();
    }

    /// <summary>
    /// 创建新地图文件
    /// </summary>
    private void CreateNewFile(string fileName)
    {
        // 创建新文件
        map.mapData = new MapData();
        SaveData(fileName);
        LoadAllLevelFileName();
        AssetDatabase.Refresh();
    }
    
    #region 加载和保存

    public void SaveData(string fileName)
    {
        // 清空旧数据
        map.mapData.pathList.Clear();
        map.mapData.towerList.Clear();

        // 写入新数据
        for (int i = 0; i < map.pathList.Count; i++)
        {
            map.mapData.pathList.Add(map.pathList[i]);
        }

        for (int i = 0; i < map.cellsList.Count; i++)
        {
            if (map.cellsList[i].IsTowerPos)
            {
                map.mapData.towerList.Add(map.cellsList[i]);
            }
        }

        BinaryManager.Instance.Save(ProjectPath.MAPDATA_PATH + fileName, map.mapData);
        AssetDatabase.Refresh();
    }
    

    public void LoadData()
    {
        Clear();
        
        // 生成格子
        for (int y = 0; y < map.rowNum; y++)
        {
            for (int x = 0; x < map.columnNum; x++)
            {
                map.cellsList.Add(new Cell(new Point(x, y)));
            }
        }
        // 覆盖放塔点
        for (int i = 0; i < map.mapData.towerList.Count; i++)
        {
            map.GetCell(map.mapData.towerList[i].X, map.mapData.towerList[i].Y).AllowTowerPos();
        }

        for (int i = 0; i < map.mapData.pathList.Count; i++)
        {
            map.pathList.Add(map.mapData.pathList[i]);
        }
    }

    #endregion
    
    #region 清除

    /// <summary>
    /// 清除所有放塔点
    /// </summary>
    public void ClearAllTowerPos()
    {
        for (int i = 0; i < map.cellsList.Count; i++)
        {
            map.cellsList[i].NotAllowTowerPos();
        }
    }

    /// <summary>
    /// 清除所有怪物路径
    /// </summary>
    public void ClearAllPath()
    {
        map.pathList.Clear();
    }

    public void Clear()
    {
        map.cellsList.Clear();
        map.pathList.Clear();
    }

    #endregion
}