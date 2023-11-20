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

        fileReName = "复制新地图文件的文件名.lvi";
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
            if (fileReName == "复制新地图文件的文件名.lvi")
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
            map.ClearAllTowerPos();
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
            map.ClearAllPath();
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
        map.SaveData(levelFileName[nowMapIndex]);
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
            if (fileInfos[i].Extension == ".lvi")
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
        map.LoadLevel();
    }
}