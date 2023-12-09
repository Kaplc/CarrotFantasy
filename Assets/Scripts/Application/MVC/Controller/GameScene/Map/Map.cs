using System;
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
    public const int RowNum = 8; // 地图行数
    public const int ColumnNum = 12; // 列数

    private static float mapWidth;
    private static float mapHeight;
    private static float cellWidth;
    private static float cellHeight;

    public SpriteRenderer mapBgSpriteRenderer;
    public SpriteRenderer roadSpriteRenderer;

    public static List<Cell> cellsList = new List<Cell>(); // 所有格子
    public static List<Cell> pathList = new List<Cell>(); // 所有路径拐点

    #region 编辑器相关字段

    public MapData nowEditorMapData; // 当前编辑地图数据

    public bool drawGizmos; // 开启绘制
    [HideInInspector] public bool drawTowerPos;
    [HideInInspector] public bool drawPath;

    #endregion

    #region 游戏相关字段

    [HideInInspector] public MapData nowMapData; // 当前游戏的关卡地图信息
    private Cell lastClickCell; // 上一次点击的格子

    #endregion

    private void Awake()
    {
        // 计算格子数据
        CalCellSize();
    }

    private void Update()
    {
        // 编辑器模式才开启检测
        if (drawGizmos)
        {
            EditorCheckMouseEvent();
            return;
        }
        
        CheckMouseEvent();
    }

    #region 编辑器相关

    /// <summary>
    /// 编辑器检测绘画鼠标事件
    /// </summary>
    private void EditorCheckMouseEvent()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (drawTowerPos)
            {
                Cell cell = GetMousePositionCell();
                cell.IsTowerPos = true;
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
                cell.IsTowerPos = false;
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
        for (int i = 0; i <= RowNum; i++)
        {
            Vector2 from = new Vector2(0 - mapWidth / 2f, i * cellHeight - mapHeight / 2f);
            Vector2 to = new Vector2(mapWidth - mapWidth / 2f, i * cellWidth - mapHeight / 2f);
            Gizmos.DrawLine(from, to);
        }

        // 绘制列
        for (int i = 0; i <= ColumnNum; i++)
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
        cellWidth = mapWidth / ColumnNum;
        cellHeight = mapHeight / RowNum;
    }

    #endregion

    #region 格子数据相关

    /// <summary>
    /// 初始化生成格子
    /// </summary>
    public static void GenerateCell()
    {
        for (int y = 0; y < Map.RowNum; y++)
        {
            for (int x = 0; x < Map.ColumnNum; x++)
            {
                cellsList.Add(new Cell(new Point(x, y)));
            }
        }
    }

    /// <summary>
    /// 索引获取格子
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static Cell GetCell(int x, int y)
    {
        return cellsList[x + y * ColumnNum];
    }

    /// <summary>
    /// 世界坐标获取格子
    /// </summary>
    /// <param name="worldPos"></param>
    /// <returns></returns>
    public static Cell GetCell(Vector3 worldPos)
    {
        float x = (worldPos.x + mapWidth / 2f) / (mapWidth / ColumnNum);
        float y = (worldPos.y + mapHeight / 2f) / (mapHeight / RowNum);

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

    #region 游戏相关方法

    /// <summary>
    /// 初始化地图
    /// </summary>
    public void InitMap()
    {
        // 获取当前地图数据
        nowMapData = GameManager.Instance.nowLevelData.mapData;
        // 生成地图格子
        GenerateCell();
        // 覆盖格子信息
        ReFlashCellData();

        // 设置地图背景
        mapBgSpriteRenderer.sprite = Resources.Load<Sprite>(nowMapData.mapBgSpritePath);
        // 设置路径背景
        roadSpriteRenderer.sprite = Resources.Load<Sprite>(nowMapData.roadSpritePath);
    }
    
    /// <summary>
    /// 用地图数据刷新格子数据
    /// </summary>
    private void ReFlashCellData()
    {
        // 加载放塔点覆盖空数据的格子
        for (int i = 0; i < nowMapData.towerList.Count; i++)
        {
            GetCell(nowMapData.towerList[i].X, nowMapData.towerList[i].Y).IsTowerPos = true;
        }
    }

    /// <summary>
    /// 开始游戏时鼠标点击事件
    /// </summary>
    private void CheckMouseEvent()
    {
        // 左键打开升级或者建造面板
        if (Input.GetMouseButtonDown(0) && GameManager.Instance.allowClickCell)
        {
            Vector3 mouseWorldPos = Camera.main.ViewportToWorldPoint(Camera.main.ScreenToViewportPoint(Input.mousePosition));
            Cell cell = GetCell(mouseWorldPos);

            // 是放塔点
            if (cell.IsTowerPos)
            {
                // 根据点击格子选择面板显示位置
                EBuiltPanelShowDir showDir;
                if (cell.X == 0)
                {
                    // 地图左边
                    showDir = EBuiltPanelShowDir.Right;
                }
                else if (cell.X == ColumnNum - 1)
                {
                    // 地图右边
                    showDir = EBuiltPanelShowDir.Left;
                }
                else if (cell.Y == 0)
                {
                    // 地图底边
                    showDir = EBuiltPanelShowDir.Up;
                }
                else if (cell.Y == RowNum - 1)
                {
                    // 地图顶边
                    showDir = EBuiltPanelShowDir.Down;
                }
                else
                {
                    showDir = EBuiltPanelShowDir.Up;
                }

                // 在显示下一次面板前先上次旧面板
                GameFacade.Instance.SendNotification(NotificationName.HIDE_BUILTPANEL);

                // 判断格子是否存在塔
                if (cell.tower as BaseTower)
                    // 显示升级塔面板
                    ShowUpGradePanel(cell.tower as BaseTower, GetCellCenterPos(cell), showDir);
                else
                    // 显示创建塔面板
                    ShowCreatePanel(GetCellCenterPos(cell), showDir);

                // 面板打开禁止监测格子
                GameManager.Instance.allowClickCell = false;
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            // 完全停止时失效
            if (GameManager.Instance.Stop)return;

            // 右键关闭建造面板
            GameFacade.Instance.SendNotification(NotificationName.HIDE_BUILTPANEL);
            GameFacade.Instance.SendNotification(NotificationName.ALLOW_CLICKCELL, true);
        }
    }

    /// <summary>
    /// 显示升级塔面板
    /// </summary>
    private void ShowUpGradePanel(BaseTower tower, Vector3 createPos, EBuiltPanelShowDir showDir)
    {
        TowerData towerData = tower.data;

        UpGradeTowerArgsBody body = new UpGradeTowerArgsBody();

        UIDataProxy proxy = GameFacade.Instance.RetrieveProxy("UIDataProxy") as UIDataProxy;
        // 根据当前塔等级选择升级Icon和卖出Icon
        if (tower.level == 2)
        {
            // 最大等级
            body.icon = proxy?.GetSprite("Atlas/BuiltPanelAtlas", "Btn_ReachHighestLevel");
        }
        else if (GameManager.Instance.money >= towerData.prices[tower.level + 1])
        {
            // 够钱升级
            body.icon = proxy?.GetSprite("Atlas/BuiltPanelAtlas", "Btn_CanUpLevel");
            // 取下一级的价格
            body.upGradeMoney = towerData.prices[tower.level + 1];
        }
        else
        {
            // 不够钱升级
            body.icon = proxy?.GetSprite("Atlas/BuiltPanelAtlas", "Btn_CantUpLevel");
            body.upGradeMoney = towerData.prices[tower.level + 1];
        }
        
        body.createPos = createPos;
        body.sellMoney = towerData.sellPrices[tower.level];
        body.attackRange = towerData.attackRange;
        body.showDir = showDir;

        // 显示升级面板
        GameFacade.Instance.SendNotification(NotificationName.SHOW_UPGRADEPANEL, body);
    }

    /// <summary>
    /// 显示创建塔面板
    /// </summary>
    private void ShowCreatePanel(Vector3 createPos, EBuiltPanelShowDir showDir)
    {
        Dictionary<TowerData, Sprite> towersDataDic = new Dictionary<TowerData, Sprite>();
        List<TowerData> towersData = GameManager.Instance.nowLevelData.towersData;
        // 获取当前关卡可创建塔的所有Icons
        for (int i = 0; i < towersData.Count; i++)
        {
            // 判断是否够钱, 获取0级的Icon
            if (GameManager.Instance.money >= towersData[i].prices[0])
            {
                // 普通图标
                towersDataDic.Add(towersData[i], towersData[i].icon);
            }
            else
            {
                // 灰色图标
                towersDataDic.Add(towersData[i], towersData[i].greyIcon);
            }
        }

        // 不存在显示创建塔面板
        GameFacade.Instance.SendNotification(NotificationName.SHOW_CREATEPANEL, new CreatePanelArgsBody()
        {
            createPos = createPos,
            towersDataDic = towersDataDic,
            showDir = showDir
        });
    }

    #endregion
}