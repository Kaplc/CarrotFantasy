using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;


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

    [HideInInspector] public bool drawGizmos; // 开启绘制
    [HideInInspector] public bool drawTowerPos;
    [HideInInspector] public bool drawPath;
    [HideInInspector] public bool drawObstacle;
    [HideInInspector] public List<GameObject> obstacleList = new List<GameObject>();
    [HideInInspector] public int obstacleIndex; // 默认选择第一种障碍物

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
        }
    }

    #region 编辑器相关

    /// <summary>
    /// 编辑器检测绘画鼠标事件
    /// </summary>
    private void EditorCheckMouseEvent()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cell cell = GetMousePositionCell();
            if (drawTowerPos)
            {
                cell.IsTowerPos = true;
            }

            if (drawPath)
            {
                pathList.Add(cell);
            }

            if (drawObstacle)
            {
                // 创建实例
                GameObject obstacle = Instantiate(Resources.Load<GameObject>("Object/Obstacle/Obstacle" + (obstacleIndex + 1)));
                obstacle.transform.position = GetCellCenterPos(cell);
                // 记录
                cell.hasObstacle = true;
                cell.obstacleName = "Obstacle" + (obstacleIndex + 1);
                cell.obstacle = obstacle;
                obstacleList.Add(obstacle);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Cell cell = GetMousePositionCell();

            if (drawTowerPos)
            {
                cell.IsTowerPos = false;
            }

            if (drawPath)
            {
                pathList.Remove(cell);
            }

            if (drawObstacle)
            {
                Destroy(cell.obstacle as GameObject);
                cell.obstacle = null;
                cell.hasObstacle = false;
                cell.obstacleName = null;
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

    /// <summary>
    /// 编辑器模式根据保存的地图数据生成障碍物
    /// </summary>
    public void EditorCreateObstacle()
    {
        for (int i = 0; i < cellsList.Count; i++)
        {
            if (cellsList[i].hasObstacle)
            {
                // 创建实例
                GameObject obstacle = Instantiate(Resources.Load<GameObject>($"Object/Obstacle/{cellsList[i].obstacleName}"));
                obstacle.transform.position = GetCellCenterPos(cellsList[i]);
                cellsList[i].obstacle = obstacle;
                obstacleList.Add(obstacle);
            }
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
        // 清空上一次的数据
        cellsList.Clear();

        for (int y = 0; y < RowNum; y++)
        {
            for (int x = 0; x < ColumnNum; x++)
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
        // 生成障碍物
        CreateObstacle();

        // 设置地图背景
        mapBgSpriteRenderer.sprite = GameManager.Instance.FactoryManager.SpriteFactory.GetSprite(nowMapData.mapBgSpritePath);
        // 设置路径背景
        roadSpriteRenderer.sprite = GameManager.Instance.FactoryManager.SpriteFactory.GetSprite(nowMapData.roadSpritePath);
    }

    /// <summary>
    /// 根据保存的地图数据生成障碍物
    /// </summary>
    public void CreateObstacle()
    {
        for (int i = 0; i < nowMapData.obstacleList.Count; i++)
        {
            Cell cell = nowMapData.obstacleList[i];

            // 创建实例
            // GameObject obstacle = Instantiate(Resources.Load<GameObject>($"Object/Obstacle/{cell.obstacleName}"));
            GameObject obstacle = GameManager.Instance.PoolManager.GetObject($"Object/Obstacle/{cell.obstacleName}");
            obstacle.transform.position = GetCellCenterPos(cell);
            cell.obstacle = obstacle;
            obstacleList.Add(obstacle);
        }
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
    /// 点击格子回调
    /// </summary>
    public void OnMouseDown()
    {
        if (drawGizmos || GameManager.Instance.stop) return;

        // 射线检测判断是否被UI遮挡
        GraphicRaycaster gr = UIManager.Instance.canvas.GetComponent<GraphicRaycaster>();
        PointerEventData eventData = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
        List<RaycastResult> results = new List<RaycastResult>();
        gr.Raycast(eventData, results);
        // 被显示范围的Ui遮挡除外
        if (results.Count > 0 && results[0].gameObject.name != "ImageAttackRange") return;

        // 获取点击的格子
        Vector3 mouseWorldPos = Camera.main.ViewportToWorldPoint(Camera.main.ScreenToViewportPoint(Input.mousePosition));
        Cell cell = GetCell(mouseWorldPos);

        // 判断是否为放塔点
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

            // 面板已经打开则该次点击为关闭面板
            if (GameManager.Instance.openedBuiltPanel)
            {
                GameFacade.Instance.SendNotification(NotificationName.UI.HIDE_BUILTPANEL);
                return;
            }

            // 判断格子是否存在塔
            if (cell.tower as BaseTower)
            {
                // 显示升级塔面板
                ShowUpGradePanel(cell.tower as BaseTower, GetCellCenterPos(cell), showDir);
            }
            else
            {
                // 显示创建塔面板
                ShowCreatePanel(GetCellCenterPos(cell), showDir);
            }
        }
        else
        {
            // 显示禁止建造图标
            GameFacade.Instance.SendNotification(NotificationName.UI.SHOW_CANTBUILTICON, Map.GetCellCenterPos(cell));
        }
    }

    /// <summary>
    /// 显示升级塔面板
    /// </summary>
    private void ShowUpGradePanel(BaseTower tower, Vector3 createPos, EBuiltPanelShowDir showDir)
    {
        TowerData towerData = tower.data;

        UpGradeTowerArgsBody body = new UpGradeTowerArgsBody();

        // 根据当前塔等级选择升级Icon和卖出Icon
        if (tower.level == 2)
        {
            // 最大等级
            body.icon = GameManager.Instance.FactoryManager.SpriteFactory.GetSprite("Atlas/BuiltPanelAtlas", "Btn_ReachHighestLevel");
        }
        else if (GameManager.Instance.money >= towerData.prices[tower.level + 1])
        {
            // 够钱升级
            body.icon = GameManager.Instance.FactoryManager.SpriteFactory.GetSprite("Atlas/BuiltPanelAtlas", "Btn_CanUpLevel");
            // 取下一级的价格
            body.upGradeMoney = towerData.prices[tower.level + 1];
        }
        else
        {
            // 不够钱升级
            body.icon = GameManager.Instance.FactoryManager.SpriteFactory.GetSprite("Atlas/BuiltPanelAtlas", "Btn_CantUpLevel");
            body.upGradeMoney = towerData.prices[tower.level + 1];
        }

        body.createPos = createPos;
        body.sellMoney = towerData.sellPrices[tower.level];
        body.attackRange = towerData.attackRangesList[tower.level];
        body.showDir = showDir;

        // 显示升级面板
        GameFacade.Instance.SendNotification(NotificationName.UI.SHOW_UPGRADEPANEL, body);
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
        GameFacade.Instance.SendNotification(NotificationName.UI.SHOW_CREATEPANEL, new CreatePanelArgsBody()
        {
            createPos = createPos,
            towersDataDic = towersDataDic,
            showDir = showDir
        });
    }

    #endregion
}