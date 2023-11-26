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
    public int nowMapIndex = 0; // 默认选中第一个文件
    private string[] fileNames; // 加载到的地图信息文件名集合
    private string fileReName = "必须填写新地图文件的文件名.md";
    private string default_MapBgSpritePath = "Map/BigLevel0/BG0";
    private string default_RoadSpritePath = "Map/BigLevel0/Road1";

    private void Awake()
    {
        map = target as Map; // 关联mono脚本
        // 加载文件名
        LoadAllLevelFileName();
        // 默认加载第一个文件数据
        LoadMapData();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space();

        #region 选择地图文件

        EditorGUILayout.BeginHorizontal();
        // 创建下拉列表
        int newMapIndex = EditorGUILayout.Popup(nowMapIndex, fileNames);
        if (nowMapIndex != newMapIndex)
        {
            nowMapIndex = newMapIndex;
            LoadMapData();
        }

        EditorGUILayout.EndHorizontal();

        #endregion

        EditorGUILayout.Space();

        #region 修改

        EditorGUILayout.BeginHorizontal();
        fileReName = EditorGUILayout.TextField("", fileReName);
        if (GUILayout.Button("复制地图文件"))
        {
            if (fileReName == "必须填写新地图文件的文件名.md")
            {
                return;
            }

            for (int i = 0; i < fileNames.Length; i++)
            {
                if (fileNames[i] == fileReName)
                {
                    return;
                }
            }

            File.Copy(BinaryManager.BINARYFILE_PATH + DataPath.MAPDATA_PATH + fileNames[nowMapIndex],
                BinaryManager.BINARYFILE_PATH + DataPath.MAPDATA_PATH + fileReName);

            LoadAllLevelFileName();
            Repaint();
            AssetDatabase.Refresh();
        }

        if (GUILayout.Button("创建新文件"))
        {
            if (fileReName == "必须填写新地图文件的文件名.md")
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
            SaveData(fileNames[nowMapIndex]);
        }

        EditorGUILayout.EndHorizontal();

        #endregion

        Repaint();
    }

    /// <summary>
    /// 加载所有MapData文件
    /// </summary>
    /// <returns></returns>
    private void LoadAllLevelFileName()
    {
        DirectoryInfo directoryInfo = Directory.CreateDirectory(BinaryManager.BINARYFILE_PATH + DataPath.MAPDATA_PATH);
        FileInfo[] fileInfos = directoryInfo.GetFiles();

        List<string> fileName = new List<string>();
        // 加载文件名
        for (int i = 0; i < fileInfos.Length; i++)
        {
            if (fileInfos[i].Extension == ".md")
            {
                fileName.Add(fileInfos[i].Name);
            }
        }

        fileNames = fileName.ToArray();
    }

    /// <summary>
    /// 读取数据文件加载绘制好的路径和放塔点
    /// </summary>
    private void LoadMapData()
    {
        // 读取二进制文件
        map.nowEditorMapData = BinaryManager.Instance.Load<MapData>(DataPath.MAPDATA_PATH + fileNames[nowMapIndex]);

        // 清除所有修改缓存
        Clear();
        // 生成新格子
        Map.GenerateCell();

        // 加载放塔点覆盖空数据的格子
        for (int i = 0; i < map.nowEditorMapData.towerList.Count; i++)
        {
            Map.GetCell(map.nowEditorMapData.towerList[i].X, map.nowEditorMapData.towerList[i].Y).IsTowerPos = true;
        }

        // 加载路径
        for (int i = 0; i < map.nowEditorMapData.pathList.Count; i++)
        {
            Map.pathList.Add(map.nowEditorMapData.pathList[i]);
        }
        
        // 加载地图背景图片
        Sprite mapBgSprite = Resources.Load<Sprite>(map.nowEditorMapData.mapBgSpritePath);
        if (!mapBgSprite)
        {
            // 无地图背景图片使用默认
            mapBgSprite = Resources.Load<Sprite>(default_MapBgSpritePath);
        }
        
        map.mapBgSpriteRenderer.sprite = mapBgSprite;

        // 加载路径图片
        Sprite roadSprite = Resources.Load<Sprite>(map.nowEditorMapData.roadSpritePath);
        if (!roadSprite)
        {
            // 无地图背景图片使用默认
            roadSprite = Resources.Load<Sprite>(default_RoadSpritePath);
        }
        
        map.roadSpriteRenderer.sprite = roadSprite;
    }

    /// <summary>
    /// 创建新地图文件
    /// </summary>
    private void CreateNewFile(string fileName)
    {
        // 创建新文件
        map.nowEditorMapData = new MapData();
        SaveData(fileName);
        LoadAllLevelFileName();
        AssetDatabase.Refresh();
    }

    #region 加载和保存

    private void SaveData(string fileName)
    {
        // 清空旧数据
        map.nowEditorMapData.pathList.Clear();
        map.nowEditorMapData.towerList.Clear();

        // 写入新数据
        for (int i = 0; i < Map.pathList.Count; i++)
        {
            map.nowEditorMapData.pathList.Add(Map.pathList[i]);
        }

        for (int i = 0; i < Map.cellsList.Count; i++)
        {
            if (Map.cellsList[i].IsTowerPos)
            {
                map.nowEditorMapData.towerList.Add(Map.cellsList[i]);
            }
        }
        
        // 没有设置地图图片自动保存默认路径
        if (map.nowEditorMapData.mapBgSpritePath == "")
        {
            map.nowEditorMapData.mapBgSpritePath = default_MapBgSpritePath;
        }

        if (map.nowEditorMapData.roadSpritePath == "")
        {
            map.nowEditorMapData.roadSpritePath = default_RoadSpritePath;
        }

        BinaryManager.Instance.Save(DataPath.MAPDATA_PATH + fileName, map.nowEditorMapData);
        
        // 保存后重新读取
        LoadMapData();
        
        Repaint();
        AssetDatabase.Refresh();
    }

    #endregion

    #region 清除

    /// <summary>
    /// 清除放塔点修改缓存
    /// </summary>
    public void ClearAllTowerPos()
    {
        for (int i = 0; i < Map.cellsList.Count; i++)
        {
            Map.cellsList[i].IsTowerPos = false;
        }
    }

    /// <summary>
    /// 清除怪物路径修改缓存 
    /// </summary>
    public void ClearAllPath()
    {
        Map.pathList.Clear();
    }

    /// <summary>
    /// 清空修改缓存
    /// </summary>
    public void Clear()
    {   
        Map.cellsList.Clear();
        Map.pathList.Clear();
    }

    #endregion
}