using App.DataClass.Map;
using App.Static.Enum;
using MapEditor;
using UnityEditor;
using UnityEngine;

namespace Editor.MapEditor
{
    [CustomEditor(typeof(NormalMapEditor))]
    public class NormalMapInspectorEditor : UnityEditor.Editor
    {
        // 当前正在编辑的地图
        private NormalMapEditor map;

        private EDrawType eDrawType = EDrawType.DrawPath;
        private SerializedProperty mapData;
        private Texture obstacleTexture;
        private SerializedProperty bgTexture;
        private SerializedProperty fgTexture;
        private SerializedProperty drawType;
        private EObstacleType eObstacleType;
        private SerializedProperty obstacleType;
        private SerializedProperty money;
        private SerializedProperty towerTypeList;

        #region Unity回调

        private void OnEnable()
        {
            map = target as NormalMapEditor; // 关联mono脚本

            // 
            mapData = serializedObject.FindProperty("mapData");
            bgTexture = serializedObject.FindProperty("bgTexture");
            fgTexture = serializedObject.FindProperty("fgTexture");
            drawType = serializedObject.FindProperty("drawType");
            obstacleType = serializedObject.FindProperty("obstacleType");
            money = serializedObject.FindProperty("money");
            towerTypeList = serializedObject.FindProperty("towerTypeList");
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
            
            EditorGUILayout.PropertyField(towerTypeList, new GUIContent("允许建造的塔类型"), true);
            
            EditorGUILayout.Space();

            #endregion

            #region 绘制对象

            eDrawType = (EDrawType)EditorGUILayout.EnumPopup(new GUIContent("绘制类型"), eDrawType);
            drawType.intValue = (int)eDrawType;
            if (eDrawType == EDrawType.DrawObstacle)
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

            if (GUILayout.Button("全部清除"))
            {
                switch (eDrawType)
                {
                    case EDrawType.DrawTower:
                        map.ClearTower();
                        break;
                    case EDrawType.DrawPath:
                        map.ClearPath();
                        break;
                    case EDrawType.DrawObstacle:
                        map.ClearObstacle();
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