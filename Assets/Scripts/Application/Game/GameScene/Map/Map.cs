﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// 地图格子索引
/// </summary>
[Serializable]
public struct Point
{
    private int x;
    private int y;

    public int X => x;
    public int Y => y;

    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}


public class Map : MonoBehaviour
{
    public const int rowNum = 8; // 地图行数
    public const int columnNum = 12; // 列数

    private static float mapWidth;
    private static float mapHeight;
    private static float cellWidth;
    private static float cellHeight;

    public SpriteRenderer mapBgSpriteRenderer;
    public SpriteRenderer roadSpriteRenderer;

    #region 编辑器相关字段

    public static List<Cell> cellsList = new List<Cell>(); // 所有格子
    public static List<Cell> pathList = new List<Cell>(); // 所有路径拐点

    public MapData nowEditorMapData; // 当前编辑地图数据

    public bool drawGizmos; // 开启绘制
    [HideInInspector] public bool drawTowerPos;
    [HideInInspector] public bool drawPath;

    #endregion

    #region 游戏相关字段

    public bool gaming; // 游戏中标识
    
    public Carrot carrot; // 萝卜
    public Transform startPoint; // 开始路牌位置
    [HideInInspector] public MapData nowMapData; // 当前选择的关卡信息

    #endregion

    private void Awake()
    {
        // 计算格子数据
        CalCellSize();
        
        if (!gaming) return;
    }

    private void Update()
    {
        // 编辑器模式才开启检测
        if (drawGizmos)
        {
            CheckMouse();
        }
    }

    #region 编辑器相关

    /// <summary>
    /// 检测绘画鼠标事件
    /// </summary>
    private void CheckMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (drawTowerPos)
            {
                Cell cell = GetMousePositionCell();
                cell.AllowTowerPos();
            }

            if (drawPath)
            {
                Cell cell = GetMousePositionCell();
                pathList.Add(cell);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (drawTowerPos)
            {
                Cell cell = GetMousePositionCell();
                cell.NotAllowTowerPos();
            }

            if (drawPath)
            {
                Cell cell = GetMousePositionCell();
                pathList.Remove(cell);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (!drawGizmos) return;

        CalCellSize();
        Gizmos.color = Color.green;

        // 绘制行
        for (int i = 0; i <= rowNum; i++)
        {
            Vector2 from = new Vector2(0 - mapWidth / 2f, i * cellHeight - mapHeight / 2f);
            Vector2 to = new Vector2(mapWidth - mapWidth / 2f, i * cellWidth - mapHeight / 2f);
            Gizmos.DrawLine(from, to);
        }

        // 绘制列
        for (int i = 0; i <= columnNum; i++)
        {
            Vector2 from = new Vector2(i * cellWidth - mapWidth / 2f, 0 - mapHeight / 2f);
            Vector2 to = new Vector2(i * cellWidth - mapWidth / 2f, mapHeight - mapHeight / 2f);
            Gizmos.DrawLine(from, to);
        }

        // 绘制放塔点
        for (int i = 0; i < cellsList.Count; i++)
        {
            if (cellsList[i].IsTowerPos)
            {
                Gizmos.DrawIcon(GetCellCenterPos(cellsList[i]), "holder.png", true);
            }
        }

        Gizmos.color = Color.red;
        // 绘制路径
        if (pathList.Count > 0)
        {
            Gizmos.DrawIcon(GetCellCenterPos(pathList[0]), "start.png", true);
            Gizmos.DrawIcon(GetCellCenterPos(pathList[pathList.Count - 1]), "end.png", true);
        }

        for (int i = 0; i < pathList.Count - 1; i++)
        {
            Vector2 from = GetCellCenterPos(pathList[i]);
            Vector2 to = GetCellCenterPos(pathList[i + 1]);
            Gizmos.DrawLine(from, to);
        }
    }

    #endregion

    #region 计算

    /// <summary>
    /// 计算格子大小
    /// </summary>
    private void CalCellSize()
    {
        // 摄像机视口左下和右上转世界坐标
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0));
        Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1));
        // 地图大小
        mapWidth = Mathf.Abs(topRight.x - bottomLeft.x);
        mapHeight = Mathf.Abs(topRight.y - bottomLeft.y);
        // 格子大小
        cellWidth = mapWidth / columnNum;
        cellHeight = mapHeight / rowNum;
    }

    #endregion

    #region 格子数据相关

    /// <summary>
    /// 索引获取格子
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static Cell GetCell(int x, int y)
    {
        return cellsList[x + y * columnNum];
    }

    /// <summary>
    /// 世界坐标获取格子
    /// </summary>
    /// <param name="worldPos"></param>
    /// <returns></returns>
    public static Cell GetCell(Vector3 worldPos)
    {
        float x = (worldPos.x + mapWidth / 2f) / (mapWidth / columnNum);
        float y = (worldPos.y + mapHeight / 2f) / (mapHeight / rowNum);

        return GetCell((int)x, (int)y);
    }

    /// <summary>
    /// 返回格子中心点坐标坐标
    /// </summary>
    /// <param name="cell"></param>
    /// <returns></returns>
    public static Vector3 GetCellCenterPos(Cell cell)
    {
        return new Vector3(-mapWidth / 2f + cellWidth / 2f + cell.X * cellWidth, -mapHeight / 2f + cellHeight / 2f + cell.Y * cellHeight);
    }

    /// <summary>
    /// 获取当前鼠标位置的格子
    /// </summary>
    /// <returns></returns>
    public static Cell GetMousePositionCell()
    {
        Vector3 mouseViewPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector3 mouseWorldPos = Camera.main.ViewportToWorldPoint(mouseViewPos);

        return GetCell(mouseWorldPos);
    }

    #endregion

    #region 游戏相关
    
    /// <summary>
    /// 初始化地图
    /// </summary>
    public void InitMap()
    {
        // 获取当前地图数据
        nowMapData = GameManager.Instance.nowLevelData.mapData;
        
        // 设置地图背景
        mapBgSpriteRenderer.sprite = Resources.Load<Sprite>(nowMapData.mapBgSpritePath);
        // 设置路径背景
        roadSpriteRenderer.sprite = Resources.Load<Sprite>(nowMapData.roadSpritePath);
        
        // 创建萝卜和开始路牌
        carrot = GameManager.Instance.PoolManager.GetObject("Object/Carrot").GetComponent<Carrot>();
        carrot.OnGet();
        startPoint = Instantiate(Resources.Load<GameObject>("Object/StartPoint")).GetComponent<Transform>();
        // 设置萝卜位置
        Cell lastPathCell = nowMapData.pathList[nowMapData.pathList.Count - 1];
        carrot.transform.position = GetCellCenterPos(lastPathCell);
        // 设置开始路牌位置
        Cell firstPathCell = nowMapData.pathList[0];
        startPoint.position = GetCellCenterPos(firstPathCell);
    }
    
    #endregion
}