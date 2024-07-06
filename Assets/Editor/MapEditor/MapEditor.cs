using System;
using System.Collections.Generic;
using App.Data.DataClass.Map;
using App.Game.Generic.Map;
using App.Static.Enum;
using MapEditorScene;
using UnityEditor;
using UnityEngine;

namespace Editor.Map
{
    [CustomEditor(typeof(EditSceneMap))]
    public class MapEditor : UnityEditor.Editor
    {
        // 当前正在编辑的地图
        private EditSceneMap editSceneMap;

        private EDrawMapType drawMapType = EDrawMapType.Normal;

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
        private SerializedProperty allowBuiltTypeListProperty;

        #endregion

        #region 图片信息

        public Sprite bgTexture;
        public Sprite fgTexture;

        #endregion

        #region 绘制

        private ENormalMapDrawType normalMapDrawType;

        private EBossMapDrawType bossMapDrawType;

        // 绘制路径
        private List<PointClass> pathList = new List<PointClass>();

        // 绘制放塔点
        private List<PointClass> towerPosList = new List<PointClass>();

        // 绘制障碍物
        private Texture obstacleTexture;
        private EObstacleType obstacleType = EObstacleType.None;
        private Transform obstaclesFarther;
        private List<ObjectPointClass> obstacleList = new List<ObjectPointClass>();

        private List<Transform> obstacleObjs = new List<Transform>();

        // 绘制塔
        private Texture towerTexture;
        private ETowerType towerType = ETowerType.None;
        private Transform towersFarther;
        private List<ObjectPointClass> towerList = new List<ObjectPointClass>();

        private List<Transform> towerObjs = new List<Transform>();

        // 绘制放塔点
        private string[] norDrawTypeStr = new[] { "路径", "建塔点", "障碍物" };
        private string[] bossDrawTypeStr = new[] { "路径", "建塔点", "障碍物", "塔" };

        #endregion


        #region Unity回调

        private void OnEnable()
        {
            editSceneMap = (EditSceneMap)target; // 关联mono脚本
            
            allowBuiltTypeListProperty = serializedObject.FindProperty("allowBuiltTypeList");
            
            // 绑定回调
            editSceneMap.onDrawGizmosSelectedAction = OnDrawGizmosSelected;
            editSceneMap.onUpdateAction = Update;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            #region 选择文件

            mapData = EditorGUILayout.ObjectField("当前地图数据", mapData, typeof(MapData), false) as MapData;

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("选择地图文件"))
            {
                string path = EditorUtility.OpenFilePanel("选择地图文件", Application.dataPath, "asset");

                SelectFile(path);
            }

            if (GUILayout.Button("创建地图文件"))
            {
                string path = EditorUtility.SaveFilePanel("创建地图文件", Application.dataPath, "NewMap", "asset");
                if (!string.IsNullOrEmpty(path))
                {
                    CreateNewFile(path);
                }
            }

            if (GUILayout.Button("重新加载"))
            {
                Load();
            }

            EditorGUILayout.EndHorizontal();

            #endregion

            #region 编辑地图类型

            EditorGUILayout.Space();

            drawMapType = (EDrawMapType)EditorGUILayout.EnumPopup("地图类型", drawMapType);

            #endregion

            #region 绘制地图

            EditorGUILayout.Space();
            bgTexture = EditorGUILayout.ObjectField("前景", bgTexture, typeof(Sprite), true) as Sprite;
            fgTexture = EditorGUILayout.ObjectField("背景", fgTexture, typeof(Sprite), true) as Sprite;

            if (GUILayout.Button("应用"))
            {
                DrawMap();
            }

            EditorGUILayout.Space();

            #endregion

            #region 地图信息

            money = EditorGUILayout.IntField("初始金币", money);

            EditorGUILayout.PropertyField(allowBuiltTypeListProperty, new GUIContent("本关运行使用的塔"), true);

            EditorGUILayout.Space();

            #endregion

            #region 绘制对象

            switch (drawMapType)
            {
                case EDrawMapType.Normal:
                    normalMapDrawType = (ENormalMapDrawType)EditorGUILayout.Popup("绘制类型", (int)normalMapDrawType, norDrawTypeStr);
                    break;
                case EDrawMapType.Boss:
                    bossMapDrawType = (EBossMapDrawType)EditorGUILayout.Popup("绘制类型", (int)bossMapDrawType, bossDrawTypeStr);
                    break;
            }


            #region 显示示意图

            switch (drawMapType)
            {
                case EDrawMapType.Normal:
                    // 显示普通模式的绘制类型
                    switch (normalMapDrawType)
                    {
                        case ENormalMapDrawType.DrawPath:
                            break;
                        case ENormalMapDrawType.DrawTowerPos:
                            break;
                        case ENormalMapDrawType.DrawObstacle:
                            obstacleType = (EObstacleType)EditorGUILayout.EnumPopup(new GUIContent("障碍物类型"), obstacleType);
                            obstacleTexture = EditorGUIUtility.Load("ObstacleTexture/" + obstacleType + ".png") as Texture;
                            // 绘制预览图
                            if (obstacleTexture)
                            {
                                GUI.DrawTexture(GUILayoutUtility.GetRect(obstacleTexture.width, obstacleTexture.height), obstacleTexture,
                                    ScaleMode.ScaleToFit);
                            }

                            break;
                    }

                    break;
                case EDrawMapType.Boss:
                    switch (bossMapDrawType)
                    {
                        case EBossMapDrawType.DrawPath:
                            break;
                        case EBossMapDrawType.DrawTowerPos:
                            break;
                        case EBossMapDrawType.DrawObstacle:
                            obstacleType = (EObstacleType)EditorGUILayout.EnumPopup(new GUIContent("障碍物类型"), obstacleType);
                            obstacleTexture = EditorGUIUtility.Load("ObstacleTexture/" + obstacleType + ".png") as Texture;
                            // 绘制预览图
                            if (obstacleTexture)
                            {
                                GUI.DrawTexture(GUILayoutUtility.GetRect(obstacleTexture.width, obstacleTexture.height), obstacleTexture,
                                    ScaleMode.ScaleToFit);
                            }

                            break;
                        case EBossMapDrawType.DrawTower:
                            towerType = (ETowerType)EditorGUILayout.EnumPopup(new GUIContent("塔类型"), towerType);
                            obstacleTexture = EditorGUIUtility.Load("TowerTexture/" + towerType + ".png") as Texture;
                            // 绘制预览图
                            if (obstacleTexture)
                            {
                                GUI.DrawTexture(GUILayoutUtility.GetRect(obstacleTexture.width, obstacleTexture.height), obstacleTexture,
                                    ScaleMode.ScaleToFit);
                            }

                            break;
                    }

                    break;
            }

            #endregion

            if (GUILayout.Button("全部清除"))
            {
                switch (drawMapType)
                {
                    case EDrawMapType.Normal:
                        switch (normalMapDrawType)
                        {
                            case ENormalMapDrawType.DrawTowerPos:
                                ClearTowerPos();
                                break;
                            case ENormalMapDrawType.DrawPath:
                                ClearPath();
                                break;
                            case ENormalMapDrawType.DrawObstacle:
                                ClearObstacle();
                                break;
                        }

                        break;
                    case EDrawMapType.Boss:
                        switch (bossMapDrawType)
                        {
                            case EBossMapDrawType.DrawTowerPos:
                                ClearTowerPos();
                                break;
                            case EBossMapDrawType.DrawPath:
                                ClearPath();
                                break;
                            case EBossMapDrawType.DrawObstacle:
                                ClearObstacle();
                                break;
                            case EBossMapDrawType.DrawTower:
                                ClearTower();
                                break;
                        }

                        break;
                }
            }

            #endregion

            if (GUILayout.Button("保存"))
            {
                Save();
            }

            serializedObject.ApplyModifiedProperties();
            Repaint();
        }

        private void Awake()
        {
            if (editSceneMap == false)
            {
                editSceneMap = (EditSceneMap)target; // 关联mono脚本
            }

            bgSr = editSceneMap.GetComponent<SpriteRenderer>();
            fgSr = editSceneMap.transform.Find("Road").GetComponent<SpriteRenderer>();
            bgSr.sprite = bgTexture;
            fgSr.sprite = fgTexture;
            cellWith = bgSr.size.x / X_CELL_COUNT;
            cellHigh = bgSr.size.y / Y_CELL_COUNT;
            // 计算地图左下角坐标
            leftDown = editSceneMap.transform.position - new Vector3(bgSr.size.x / 2, bgSr.size.y / 2, 0);

            obstaclesFarther = editSceneMap.transform.Find("Obstacles");
            towersFarther = editSceneMap.transform.Find("Towers");

            Load();
        }

        private void Update()
        {
            // if (!isPlaying)
            // {
            //     return;
            // }

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

                if (drawMapType == EDrawMapType.Normal)
                {
                    switch (normalMapDrawType)
                    {
                        case ENormalMapDrawType.DrawPath:
                            DrawPath(x, y);
                            break;
                        case ENormalMapDrawType.DrawTowerPos:
                            DrawTowerPos(x, y);
                            break;
                        case ENormalMapDrawType.DrawObstacle:
                            DrawObstacle(x, y, obstacleType);
                            break;
                    }
                }
                else
                {
                    switch (bossMapDrawType)
                    {
                        case EBossMapDrawType.DrawPath:
                            DrawPath(x, y);
                            break;
                        case EBossMapDrawType.DrawTowerPos:
                            DrawTowerPos(x, y);
                            break;
                        case EBossMapDrawType.DrawObstacle:
                            DrawObstacle(x, y, obstacleType);
                            break;
                        case EBossMapDrawType.DrawTower:
                            DrawTower(x, y, towerType);
                            break;
                    }
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

                if (drawMapType == EDrawMapType.Normal)
                {
                    switch (normalMapDrawType)
                    {
                        case ENormalMapDrawType.DrawPath:
                            pathList.Remove(new PointClass(x, y));
                            break;
                        case ENormalMapDrawType.DrawTowerPos:
                            towerPosList.Remove(new PointClass(x, y));
                            break;
                        case ENormalMapDrawType.DrawObstacle:
                            obstacleList.Remove(new ObjectPointClass(x, y));
                            GenerateObstacle();
                            break;
                    }
                }
                else
                {
                    switch (bossMapDrawType)
                    {
                        case EBossMapDrawType.DrawPath:
                            pathList.Remove(new PointClass(x, y));
                            break;
                        case EBossMapDrawType.DrawTowerPos:
                            towerPosList.Remove(new PointClass(x, y));
                            break;
                        case EBossMapDrawType.DrawObstacle:
                            obstacleList.Remove(new ObjectPointClass(x, y));
                            GenerateObstacle();
                            break;
                        case EBossMapDrawType.DrawTower:
                            DrawTower(x, y, towerType);
                            break;
                    }
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            // if (!isPlaying)
            // {
            //     return;
            // }

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

                GameObject obstacle = Instantiate(EditorGUIUtility.Load("ObstaclePrefabs/" + p.obstacleType + ".prefab") as GameObject,
                    obstaclesFarther);
                ;
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

                GameObject tower = Instantiate(EditorGUIUtility.Load("TowerPrefabs/" + p.towerType + ".prefab") as GameObject, towersFarther);
                ;
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
            editSceneMap.allowBuiltTypeList.Clear();
            foreach (ETowerType e in mapData.towerTypeList)
            {
                editSceneMap.allowBuiltTypeList.Add(e);
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
            foreach (var v in editSceneMap.allowBuiltTypeList)
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

        #endregion
    }
}