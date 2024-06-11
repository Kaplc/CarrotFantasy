using System;
using System.Collections.Generic;
using App.DataClass.Map;
using App.Generic.Map;
using App.Static.Enum;
using UnityEditor;
using UnityEngine;

namespace MapEditor
{
    public class MapEditor : MonoBehaviour
    {
        private bool isPlaying;

        #region 地图信息

        private const int X_CELL_COUNT = 12;
        private const int Y_CELL_COUNT = 8;
        private float cellWith;
        private float cellHigh;
        private Vector3 leftDown;
        private SpriteRenderer bgSr;
        private SpriteRenderer fgSr;

        #endregion

        public MapData mapData;

        #region 初始金币和允许的塔

        public int money;
        public List<ETowerType> allowBuiltTypeList;

        #endregion

        #region 图片信息

        public Sprite bgTexture;
        public Sprite fgTexture;

        #endregion

        #region 绘制

        public int drawType;
        // 绘制路径
        private List<PointClass> pathList = new List<PointClass>();
        // 绘制放塔点
        private List<PointClass> towerPosList = new List<PointClass>();
        // 绘制障碍物
        public EObstacleType obstacleType = EObstacleType.None;
        public Transform obstaclesFarther;
        private List<ObjectPointClass> obstacleList = new List<ObjectPointClass>();
        private List<Transform> obstacleObjs = new List<Transform>();
        // 绘制塔
        public ETowerType towerType = ETowerType.None;
        public Transform towersFarther;
        private List<ObjectPointClass> towerList = new List<ObjectPointClass>();
        private List<Transform> towerObjs = new List<Transform>();
        
        #endregion

        private void Awake()
        {
            isPlaying = true;
            bgSr = GetComponent<SpriteRenderer>();
            fgSr = transform.Find("Road").GetComponent<SpriteRenderer>();
            bgSr.sprite = bgTexture;
            fgSr.sprite = fgTexture;
            cellWith = bgSr.size.x / X_CELL_COUNT;
            cellHigh = bgSr.size.y / Y_CELL_COUNT;
            // 计算地图左下角坐标
            leftDown = transform.position - new Vector3(bgSr.size.x / 2, bgSr.size.y / 2, 0);
            
            obstaclesFarther = transform.Find("Obstacles");
            towersFarther = transform.Find("Towers");
            
            Load();
        }

        private void Update()
        {
            if (!isPlaying)
            {
                return;
            }

            // 鼠标左键绘制
            if (Input.GetMouseButtonDown(0))
            {
                if (!mapData)
                {
                    Debug.LogError("地图数据为空");
                    return;
                }

                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 offset = mousePos - leftDown;
                int x = (int)(offset.x / cellWith);
                int y = (int)(offset.y / cellHigh);
                switch (drawType)
                {
                    case 0:
                        DrawPath(x, y);
                        break;
                    case 1:
                        DrawTowerPos(x, y);
                        break;
                    case 2:
                        DrawObstacle(x, y, obstacleType);
                        break;
                    case 3:
                        DrawTower(x, y, towerType);
                        break;
                }
            }

            // 鼠标右键取消
            if (Input.GetMouseButtonDown(1))
            {
                if (!mapData)
                {
                    Debug.LogError("地图数据为空");
                    return;
                }

                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 offset = mousePos - leftDown;
                int x = (int)(offset.x / cellWith);
                int y = (int)(offset.y / cellHigh);
                switch (drawType)
                {
                    case 0:
                        pathList.Remove(new PointClass(x, y));
                        break;
                    case 1:
                        towerPosList.Remove(new PointClass(x, y));
                        break;
                    case 2:
                        obstacleList.Remove(new ObjectPointClass(x, y));
                        GenerateObstacle();
                        break;
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (!isPlaying)
            {
                return;
            }

            #region 绘制网格

            Gizmos.color = Color.green;

            // 竖向线
            for (int i = 0; i <= X_CELL_COUNT; i++)
            {
                Vector3 start = leftDown + new Vector3(i * cellWith, 0, 0);
                Vector3 end = start + new Vector3(0, bgSr.size.y, 0);
                Gizmos.DrawLine(start, end);
            }

            // 横向线
            for (int i = 0; i <= Y_CELL_COUNT; i++)
            {
                Vector3 start = leftDown + new Vector3(0, i * cellHigh, 0);
                Vector3 end = start + new Vector3(bgSr.size.x, 0, 0);
                Gizmos.DrawLine(start, end);
            }

            #endregion

            #region 绘制路径

            // 绘制起点和终点
            if (pathList.Count > 0)
            {
                // 起点
                Gizmos.DrawIcon(GetCellPos(pathList[0].x, pathList[0].y), "start.png", true);
            }

            if (pathList.Count > 1)
            {
                // 终点
                Gizmos.DrawIcon(GetCellPos(pathList[pathList.Count - 1].x, pathList[pathList.Count - 1].y), "end.png", true);
            }

            // 绘制路径
            Gizmos.color = Color.red;
            for (int i = 0; i < pathList.Count - 1; i++)
            {
                // 路径
                Gizmos.DrawLine(GetCellPos(pathList[i].x, pathList[i].y), GetCellPos(pathList[i + 1].x, pathList[i + 1].y));
            }

            #endregion

            #region 绘制放塔点

            foreach (PointClass p in towerPosList)
            {
                Gizmos.DrawIcon(GetCellPos(p.x, p.y), "holder.png", true);
            }

            #endregion
        }

        #region 生成障碍物和塔

        private void GenerateObstacle()
        {
            // 清除之前的障碍物
            for (int i = 0; i < obstacleObjs.Count; i++)
            {
                Destroy(obstacleObjs[i].gameObject);
            }

            obstacleObjs.Clear();

            if (obstacleList.Count == 0)
            {
                return;
            }

            // 生成新的障碍物
            foreach (ObjectPointClass p in obstacleList)
            {
                if (p.obstacleType == EObstacleType.None)
                {
                    continue;
                }
                GameObject obstacle = Instantiate(EditorGUIUtility.Load("ObstaclePrefabs/" + p.obstacleType + ".prefab") as GameObject, obstaclesFarther);;
                obstacle.transform.position = GetCellPos(p.x, p.y);
                obstacleObjs.Add(obstacle.transform);
            }
        }
        
        private void GenerateTower()
        {
            // 清除之前的障碍物
            for (int i = 0; i < towerObjs.Count; i++)
            {
                Destroy(towerObjs[i].gameObject);
            }

            towerObjs.Clear();

            if (towerList.Count == 0)
            {
                return;
            }

            // 生成新的障碍物
            foreach (ObjectPointClass p in towerList)
            {
                if (p.towerType == ETowerType.None)
                {
                    continue;
                }
                
                GameObject tower = Instantiate(EditorGUIUtility.Load("TowerPrefabs/" + p.towerType + ".prefab") as GameObject, towersFarther);;
                tower.transform.position = GetCellPos(p.x, p.y);
                towerObjs.Add(tower.transform);
            }
        }
        
        #endregion

        #region 地图计算

        private Vector2 GetCellPos(int x, int y)
        {
            return leftDown + new Vector3(x * cellWith + cellWith / 2, y * cellHigh + cellHigh / 2);
        }

        #endregion

        #region 绘制相关

        public void DrawMap()
        {
            bgSr.sprite = bgTexture;
            fgSr.sprite = fgTexture;
        }

        private void DrawPath(int x, int y)
        {
            // 重复的点不添加
            if (pathList.Contains(new PointClass(x, y)))
            {
                return;
            }

            pathList.Add(new PointClass(x, y));
        }

        private void DrawTowerPos(int x, int y)
        {
            // 重复的点不添加
            if (towerPosList.Contains(new PointClass(x, y)))
            {
                return;
            }

            towerPosList.Add(new PointClass(x, y));
        }

        private void DrawObstacle(int x, int y, EObstacleType type)
        {
            // 重复的点不添加
            if (obstacleList.Contains(new ObjectPointClass(x, y)))
            {
                return;
            }

            obstacleList.Add(new ObjectPointClass(x, y, type));

            GenerateObstacle();
        }
        
        private void DrawTower(int x, int y, ETowerType type)
        {
            // 重复的点不添加
            if (towerList.Contains(new ObjectPointClass(x, y)))
            {
                return;
            }

            towerList.Add(new ObjectPointClass(x, y, type));

            GenerateTower();
        }

        /// <summary>
        /// 清除
        /// </summary>
        public void ClearTowerPos()
        {
            towerPosList.Clear();
        }

        public void ClearPath()
        {
            pathList.Clear();
        }

        public void ClearObstacle()
        {
            obstacleList.Clear();

            for (int i = 0; i < obstacleObjs.Count; i++)
            {
                Destroy(obstacleObjs[i].gameObject);
            }

            obstacleObjs.Clear();
        }

        public void ClearTower()
        {
            towerList.Clear();

            for (int i = 0; i < towerObjs.Count; i++)
            {
                Destroy(towerObjs[i].gameObject);
            }
            towerObjs.Clear();
        }

        #endregion

        #region 文件操作

        public void SelectFile(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                // 分离出相对于Assets的路径
                path = path.Substring(path.IndexOf("Assets", StringComparison.Ordinal));
                mapData = AssetDatabase.LoadAssetAtPath<MapData>(path);
            }
        }

        /// <summary>
        /// 创建新地图文件
        /// </summary>
        public void CreateNewFile(string path)
        {
            // 分离出相对于Assets的路径
            path = path.Substring(path.IndexOf("Assets", StringComparison.Ordinal));

            MapData newMapData = ScriptableObject.CreateInstance<MapData>();

            // 保存文件
            AssetDatabase.CreateAsset(newMapData, path);

            AssetDatabase.SaveAssets();

            // 移动该文件到指定路径
            AssetDatabase.Refresh();

            // 重新加载文件
            mapData = AssetDatabase.LoadAssetAtPath<MapData>(path);
        }

        public void Load()
        {
            if (!mapData)
            {
                Debug.LogError("地图数据为空");
                return;
            }

            if (!CheekData())
            {
                return;
            }

            // 复制数据
            if (mapData.mapBgTexture)
            {
                bgTexture = mapData.mapBgTexture;
            }

            if (mapData.mapFgTexture)
            {
                fgTexture = mapData.mapFgTexture;
            }

            DrawMap();

            money = mapData.money;
            allowBuiltTypeList = new List<ETowerType>();
            foreach (ETowerType e in mapData.towerTypeList)
            {
                allowBuiltTypeList.Add(e);
            }

            pathList = new List<PointClass>();
            towerPosList = new List<PointClass>();
            obstacleList = new List<ObjectPointClass>();
            towerList = new List<ObjectPointClass>();

            foreach (PointClass p in mapData.pathList)
            {
                pathList.Add(p);
            }

            foreach (PointClass p in mapData.towerPosList)
            {
                towerPosList.Add(p);
            }

            foreach (ObjectPointClass p in mapData.obstacleList)
            {
                obstacleList.Add(p);
            }

            foreach (ObjectPointClass p in mapData.towerList)
            {
                towerList.Add(p);
            }

            GenerateObstacle();
            GenerateTower();
        }

        private bool CheekData()
        {
            // 检查数据合法
            foreach (var i in towerList)
            {
                if (i.towerType == ETowerType.None)
                {
                    Debug.LogError("塔类型为空");
                    return false;
                }
            }

            return true;
        }

        public void Save()
        {
            if (!mapData)
            {
                Debug.LogError("地图数据为空");
                return;
            }

            mapData.mapBgTexture = bgTexture;
            mapData.mapFgTexture = fgTexture;
            mapData.money = money;

            mapData.towerTypeList = new List<ETowerType>();
            foreach (var v in allowBuiltTypeList)
            {
                mapData.towerTypeList.Add(v);
            }

            mapData.pathList = new List<PointClass>();
            foreach (var v in pathList)
            {
                mapData.pathList.Add(v);
            }

            mapData.towerPosList = new List<PointClass>();
            foreach (var v in towerPosList)
            {
                mapData.towerPosList.Add(v);
            }

            mapData.obstacleList = new List<ObjectPointClass>();
            foreach (var v in obstacleList)
            {
                mapData.obstacleList.Add(v);
            }
            
            mapData.towerList = new List<ObjectPointClass>();
            foreach (var v in towerList)
            {
                mapData.towerList.Add(v);
            }

            EditorUtility.SetDirty(mapData);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        #endregion
    }
}