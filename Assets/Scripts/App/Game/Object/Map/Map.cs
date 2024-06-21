using System.Collections.Generic;
using App.Data.DataClass.Game.Object;
using App.Data.DataClass.Map;
using App.Game.Generic.Map;
using App.Game.Generic.NotificationBody;
using App.Game.Object.Tower;
using App.Game.SceneManager;
using App.Static;
using App.Static.Enum;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XLua;

namespace App.Game.Object.Map
{
    [LuaCallCSharp]
    public class Map : MonoBehaviour
    {
        public const int RowNum = 8; // 地图行数
        public const int ColumnNum = 12; // 列数

        private static float mapWidth;
        private static float mapHeight;
        private static float cellWidth;
        private static float cellHeight;

        private SpriteRenderer mapBgSpriteRenderer;
        private SpriteRenderer roadSpriteRenderer;

        private static List<Cell> cellsList = new List<Cell>(); // 所有格子
        private MapData nowMapData; // 当前游戏的关卡地图信息
        
        private List<TowerData> towerDataList = new List<TowerData>(); // 塔数据

        private ISceneManger sceneManger;

        private bool isBuilding;

        private void Awake()
        {
            sceneManger = GameManager.Instance.sceneManager;
            
            mapBgSpriteRenderer = GetComponent<SpriteRenderer>();
            roadSpriteRenderer = transform.Find("Road").GetComponent<SpriteRenderer>();
            
            // 计算格子数据
            CalCellSize();

        }

        #region 计算

        /// <summary>
        /// 计算格子大小
        /// </summary>
        private void CalCellSize()
        {
            // 地图大小
            mapWidth = mapBgSpriteRenderer.size.x * mapBgSpriteRenderer.transform.localScale.x;
            mapHeight = mapBgSpriteRenderer.size.y * mapBgSpriteRenderer.transform.localScale.y;
            // 格子大小
            cellWidth = mapWidth / ColumnNum;
            cellHeight = mapHeight / RowNum;
        }

        #endregion

        #region 格子数据相关

        /// <summary>
        /// 初始化生成格子
        /// </summary>
        private static void GenerateCell()
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
        private static Cell GetCell(int x, int y)
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
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
            return GetCell(mouseWorldPos);
        }

        #endregion

        #region 游戏相关方法

        /// <summary>
        /// 初始化地图
        /// </summary>
        public void Init(MapData mapData)
        {
            // 获取当前地图数据
            nowMapData = mapData;
            // 生成地图格子
            GenerateCell();
            // 覆盖格子信息
            ReFlashCellData();
            // 设置地图背景
            mapBgSpriteRenderer.sprite = mapData.mapBgTexture;
            // 设置路径背景
            roadSpriteRenderer.sprite = mapData.mapFgTexture;
            // 获取允许使用的塔数据
            towerDataList.Clear();
            for (int i = 0; i < mapData.towerTypeList.Count; i++)
            {
                towerDataList.Add(mapData.GetTowerData(i));
            }
        }

        /// <summary>
        /// 用地图数据刷新格子数据
        /// </summary>
        private void ReFlashCellData()
        {
            // 加载放塔点覆盖空数据的格子
            for (int i = 0; i < nowMapData.towerPosList.Count; i++)
            {
                GetCell(nowMapData.towerPosList[i].x, nowMapData.towerPosList[i].y).IsTowerPos = true;
            }
        }

        /// <summary>
        /// 点击格子回调
        /// </summary>
        public void OnMouseDown()
        {
            if (sceneManger.IsStop()) return;

            // 射线检测判断是否被UI遮挡
            GraphicRaycaster gr = UIManager.Instance.canvas.GetComponent<GraphicRaycaster>();
            PointerEventData eventData = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
            List<RaycastResult> results = new List<RaycastResult>();
            gr.Raycast(eventData, results);
            // 被显示范围的Ui遮挡除外
            if (results.Count > 0 && results[0].gameObject.name != "ImageAttackRange") return;

            // 获取点击的格子
            Cell cell = GetMousePositionCell();
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
                if (isBuilding)
                {
                    GameFacade.Instance.SendNotification(NotificationName.UI.HIDE_BUILT_PANEL);
                    isBuilding = false;
                    return;
                }

                // 判断格子是否存在塔
                if (cell.tower != null)
                {
                    // 显示升级塔面板
                    ShowUpGradePanel((ITower)cell.tower, GetCellCenterPos(cell), showDir);
                }
                else
                {
                    // 显示创建塔面板
                    ShowCreatePanel(GetCellCenterPos(cell), showDir);
                }

                isBuilding = true;
            }
            else
            {
                // 显示禁止建造图标
                GameFacade.Instance.SendNotification(NotificationName.UI.SHOW_CANT_BUILT_ICON, Map.GetCellCenterPos(cell));
            }
        }

        /// <summary>
        /// 显示升级塔面板
        /// </summary>
        private void ShowUpGradePanel(ITower tower, Vector3 createPos, EBuiltPanelShowDir showDir)
        {
            TowerData towerData = tower.GetData();
            int level = tower.GetLevel();
            
            UpGradeTowerArgsBody body = new UpGradeTowerArgsBody();
            // 根据当前塔等级选择升级Icon和卖出Icon
            if (level == 2)
            {
                // 最大等级
                body.icon = GameManager.Instance.factoryManager.SpriteFactory.GetSprite("Atlas/BuiltPanelAtlas", "Btn_ReachHighestLevel");
            }
            else if (sceneManger.GetMoney() >= towerData.prices[level + 1])
            {
                // 够钱升级
                body.icon = GameManager.Instance.factoryManager.SpriteFactory.GetSprite("Atlas/BuiltPanelAtlas", "Btn_CanUpLevel");
                // 取下一级的价格
                body.upGradeMoney = towerData.prices[level + 1];
            }
            else
            {
                // 不够钱升级
                body.icon = GameManager.Instance.factoryManager.SpriteFactory.GetSprite("Atlas/BuiltPanelAtlas", "Btn_CantUpLevel");
                body.upGradeMoney = towerData.prices[level + 1];
            }

            body.createPos = createPos;
            body.sellMoney = towerData.sellPrices[level];
            body.attackRange = towerData.attackRangesList[level];
            body.showDir = showDir;

            // 显示升级面板
            GameFacade.Instance.SendNotification(NotificationName.UI.SHOW_UPGRADE_PANEL, body);
        }

        /// <summary>
        /// 显示创建塔面板
        /// </summary>
        private void ShowCreatePanel(Vector3 createPos, EBuiltPanelShowDir showDir)
        {
            Dictionary<TowerData, Sprite> towersDataDic = new Dictionary<TowerData, Sprite>();
            // 获取当前关卡可创建塔的所有Icons
            for (int i = 0; i < towerDataList.Count; i++)
            {
                // 判断是否够钱, 获取0级的Icon
                if (GameManager.Instance.sceneManager.GetMoney() >= towerDataList[i].prices[0])
                {
                    // 普通图标
                    towersDataDic.Add(towerDataList[i], towerDataList[i].icon);
                }
                else
                {
                    // 灰色图标
                    towersDataDic.Add(towerDataList[i], towerDataList[i].greyIcon);
                }
            }

            // 不存在显示创建塔面板
            GameFacade.Instance.SendNotification(NotificationName.UI.SHOW_CREATE_PANEL, new CreatePanelArgsBody()
            {
                createPos = createPos,
                towersDataDic = towersDataDic,
                showDir = showDir
            });
        }

        #endregion
    }
}