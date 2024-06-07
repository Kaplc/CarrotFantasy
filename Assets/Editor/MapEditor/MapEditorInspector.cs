using App.DataClass.Map;
using App.Static.Enum;
using MapEditor;
using UnityEditor;
using UnityEngine;

namespace Editor.MapEditor
{
    [CustomEditor(typeof(global::MapEditor.MapEditor))]
    public class MapEditorInspector : UnityEditor.Editor
    {
        // 当前正在编辑的地图
        private global::MapEditor.MapEditor map;

        private bool isNormalMap;
        private string[] editorMapType = new[] { "普通模式", "Boss模式" };
        private bool[] editorMapTypeValue = new[] { true, false };

        private EDrawType eDrawType = EDrawType.DrawPath;
        private SerializedProperty mapData;
       
        private SerializedProperty bgTexture;
        private SerializedProperty fgTexture;
        
        private SerializedProperty money;
        
        private SerializedProperty drawType;
        // 绘制障碍物
        private EObstacleType eObstacleType;
        private SerializedProperty obstacleType;
        private Texture obstacleTexture;
        // 绘制塔
        private ETowerType eTowerType;
        private SerializedProperty towerType;
        private Texture towerTexture;
        // 绘制放塔点
        private SerializedProperty allowBuiltTypeList;

        private string[] norDrawTypeStr = new[] { "路径", "建塔点", "障碍物"};
        private string[] bossDrawTypeStr = new[] { "路径", "建塔点", "障碍物", "塔"};

        #region Unity回调

        private void OnEnable()
        {
            map = target as global::MapEditor.MapEditor; // 关联mono脚本

            // 
            mapData = serializedObject.FindProperty("mapData");
            bgTexture = serializedObject.FindProperty("bgTexture");
            fgTexture = serializedObject.FindProperty("fgTexture");
            drawType = serializedObject.FindProperty("drawType");
            obstacleType = serializedObject.FindProperty("obstacleType");
            money = serializedObject.FindProperty("money");
            allowBuiltTypeList = serializedObject.FindProperty("allowBuiltTypeList");
            towerType = serializedObject.FindProperty("towerType");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            #region 选择文件

            EditorGUILayout.ObjectField(mapData, typeof(MapData), new GUIContent("当前地图数据"));

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("选择地图文件"))
            {
                string path = EditorUtility.OpenFilePanel("选择地图文件", Application.dataPath, "asset");

                map.SelectFile(path);
            }

            if (GUILayout.Button("创建地图文件"))
            {
                string path = EditorUtility.SaveFilePanel("创建地图文件", Application.dataPath, "NewMap", "asset");
                if (!string.IsNullOrEmpty(path))
                {
                    map.CreateNewFile(path);
                }
            }

            if (GUILayout.Button("重新加载"))
            {
                map.Load();
            }

            EditorGUILayout.EndHorizontal();

            #endregion

            #region 编辑地图类型

            EditorGUILayout.Space();

            for (int i = 0; i < editorMapType.Length; i++)
            {
                editorMapTypeValue[i] = EditorGUILayout.ToggleLeft(editorMapType[i], editorMapTypeValue[i]);

                if (editorMapTypeValue[i])
                {
                    for (int j = 0; j < editorMapTypeValue.Length; j++)
                    {
                        editorMapTypeValue[j] = false;
                    }

                    editorMapTypeValue[i] = true;
                    isNormalMap = i == 0;
                }
            }

            #endregion

            #region 绘制地图

            EditorGUILayout.Space();
            bgTexture.objectReferenceValue = EditorGUILayout.ObjectField("前景", bgTexture.objectReferenceValue, typeof(Sprite), true) as Sprite;
            fgTexture.objectReferenceValue = EditorGUILayout.ObjectField("背景", fgTexture.objectReferenceValue, typeof(Sprite), true) as Sprite;

            if (GUILayout.Button("应用"))
            {
                map.DrawMap();
            }

            EditorGUILayout.Space();

            #endregion

            #region 地图信息

            money.intValue = EditorGUILayout.IntField("初始金币", money.intValue);

            EditorGUILayout.PropertyField(allowBuiltTypeList, new GUIContent("允许建造的塔类型"), true);

            EditorGUILayout.Space();

            #endregion

            #region 绘制对象

            if (isNormalMap)
            {
                drawType.intValue = EditorGUILayout.Popup(new GUIContent("绘制类型"), drawType.intValue, norDrawTypeStr);
            }
            else
            {
                drawType.intValue = EditorGUILayout.Popup(new GUIContent("绘制类型"), drawType.intValue, bossDrawTypeStr);
            }
            eDrawType = (EDrawType)drawType.intValue;

            if (drawType.intValue == 2)
            {
                eObstacleType = (EObstacleType)EditorGUILayout.EnumPopup(new GUIContent("障碍物类型"), eObstacleType);
                obstacleType.enumValueIndex = (int)eObstacleType;
                obstacleTexture = EditorGUIUtility.Load("ObstacleTexture/" + eObstacleType + ".png") as Texture;
                // 绘制预览图
                if (obstacleTexture)
                {
                    GUI.DrawTexture(GUILayoutUtility.GetRect(obstacleTexture.width, obstacleTexture.height), obstacleTexture, ScaleMode.ScaleToFit);
                }
            }
            else if (drawType.intValue == 3)
            {
                eTowerType = (ETowerType)EditorGUILayout.EnumPopup(new GUIContent("塔类型"), eTowerType);
                towerType.enumValueIndex = (int)eTowerType;
                obstacleTexture = EditorGUIUtility.Load("TowerTexture/" + eTowerType + ".png") as Texture;
                // 绘制预览图
                if (obstacleTexture)
                {
                    GUI.DrawTexture(GUILayoutUtility.GetRect(obstacleTexture.width, obstacleTexture.height), obstacleTexture, ScaleMode.ScaleToFit);
                }
            }

            if (GUILayout.Button("全部清除"))
            {
                switch (eDrawType)
                {
                    case EDrawType.DrawTowerPos:
                        map.ClearTowerPos();
                        break;
                    case EDrawType.DrawPath:
                        map.ClearPath();
                        break;
                    case EDrawType.DrawObstacle:
                        map.ClearObstacle();
                        break;
                    case EDrawType.DrawTower:
                        map.ClearTower();
                        break;
                }
            }

            #endregion

            if (GUILayout.Button("保存"))
            {
                map.Save();
            }

            serializedObject.ApplyModifiedProperties();
            Repaint();
        }

        #endregion
    }
}