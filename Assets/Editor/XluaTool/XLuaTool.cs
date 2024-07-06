using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class LuaFile
{
    public string name;
    public bool isFolder;
    public List<LuaFile> children;

    public LuaFile(string name, bool isFolder)
    {
        this.name = name;
        this.isFolder = isFolder;
        this.children = new List<LuaFile>();
    }
}

public class XLuaTool : EditorWindow
{

    [MenuItem("Editor/XLuaTool/GenerateTxt")]
    public static void OpenWindow()
    {
        XLuaTool window = GetWindow<XLuaTool>();
        window.titleContent = new GUIContent("XLua工具");
        window.Show();
    }


    private string[] luaFilesPath;
    private string[] luaFilesLocalPath;
    private Vector2 scrollPosition;
    private string luaDirectoryPath;
    private string directoryPath;

    private List<LuaFile> fileCatgory = new List<LuaFile>();
    private Dictionary<LuaFile, bool> foldoutStates = new Dictionary<LuaFile, bool>();

    private void OnGUI()
    {
        if (GUILayout.Button("选择Lua文件所在文件夹"))
        {
            // 选择Lua文件夹，将所有lua文件转为txt文件
            luaDirectoryPath = EditorUtility.OpenFolderPanel("选择Lua文件夹", luaDirectoryPath, "");
            if (string.IsNullOrEmpty(luaDirectoryPath))
            {
                Debug.Log("未选择文件夹");
                luaFilesPath = null;
                return;
            }

            // 递归遍历文件夹下所有文件
            luaFilesPath = Directory.GetFiles(luaDirectoryPath, "*.lua", SearchOption.AllDirectories);

            luaFilesLocalPath = new string[luaFilesPath.Length];
            // 获取文件夹名
            string directoryName = luaDirectoryPath.Split('/')[luaDirectoryPath.Split('/').Length - 1];

            for (int i = 0; i < luaFilesPath.Length; i++)
            {
                luaFilesLocalPath[i] = directoryName + luaFilesPath[i].Replace(luaDirectoryPath, "");
            }

            // 保存文件夹结构
            fileCatgory.Clear();

            foreach (var luaFilePath in luaFilesLocalPath)
            {
                string[] res = luaFilePath.Split('\\');

                LuaFile parent = null;
                LuaFile current = null;

                for (int i = 0; i < res.Length; i++)
                {
                    if (i == 0)
                    {
                        current = fileCatgory.Find(x => x.name == res[i]);
                        if (current == null)
                        {
                            current = new LuaFile(res[i], true);
                            fileCatgory.Add(current);
                        }
                    }
                    else
                    {
                        parent = current;
                        current = parent.children.Find(x => x.name == res[i]);
                        if (current == null)
                        {
                            current = new LuaFile(res[i], i != res.Length - 1);
                            parent.children.Add(current);
                        }
                    }
                }
            }
        }


        if (GUILayout.Button("生成Txt"))
        {
            // 选择生成的文件夹
            directoryPath = EditorUtility.OpenFolderPanel("选择生成文件夹", directoryPath, "");
            if (string.IsNullOrEmpty(directoryPath))
            {
                Debug.Log("未选择文件夹");
                return;
            }

            // 创建文件夹
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // 每次清空旧文件
            if (Directory.Exists(directoryPath))
            {
                string[] oldFilesPath = Directory.GetFiles(directoryPath, "*.txt");
                for (int i = 0; i < oldFilesPath.Length; i++)
                {
                    File.Delete(oldFilesPath[i]);
                }
            }

            // 保持目录结构并修改后缀名
            for (int i = 0; i < luaFilesLocalPath.Length; i++)
            {
                string[] res = luaFilesLocalPath[i].Split('\\');

                string newPath = directoryPath;
                for (int j = 0; j < res.Length; j++)
                {
                    newPath += "/" + res[j];
                    if (j == res.Length - 1)
                    {
                        newPath = newPath.Replace(".lua", ".txt");
                    }
                    else
                    {
                        if (!Directory.Exists(newPath))
                        {
                            Directory.CreateDirectory(newPath);
                        }
                    }
                }

                File.Copy(luaFilesPath[i], newPath);
            }

            AssetDatabase.Refresh();
        }

        if (luaFilesLocalPath != null)
        {
            // 文件总数
            EditorGUILayout.LabelField("文件总数：" + luaFilesLocalPath.Length);

            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            DrawFolder(fileCatgory);

            EditorGUILayout.EndScrollView();
        }
    }

    private void DrawFolder(List<LuaFile> luaFiles)
    {


        foreach (var luaFile in luaFiles)
        {
            if (luaFile.isFolder)
            {
                if (!foldoutStates.ContainsKey(luaFile))
                {
                    foldoutStates[luaFile] = false;
                }

                // 名称为红色字体
                foldoutStates[luaFile] = EditorGUILayout.Foldout(foldoutStates[luaFile], luaFile.name, true);

                if (foldoutStates[luaFile])
                {
                    EditorGUI.indentLevel++;
                    DrawFolder(luaFile.children);
                    EditorGUI.indentLevel--;
                }
            }
            else
            {
                // lua文件为绿色字体
                GUIStyle style = new GUIStyle();
                style.normal.textColor = Color.green;
                EditorGUILayout.LabelField(luaFile.name, style);
            }
        }

    }

    public void GenerateTxt()
    {
        string luaFilePath = Application.dataPath + "/Lua/";

        if (!Directory.Exists(luaFilePath))
        {
            Debug.Log("无Lua文件夹");
            return;
        }

        // 每次清空旧文件
        if (Directory.Exists(luaFilePath + "LuaText/"))
        {
            string[] oldFilesPath = Directory.GetFiles(luaFilePath + "LuaText/", "*.txt");
            for (int i = 0; i < oldFilesPath.Length; i++)
            {
                File.Delete(oldFilesPath[i]);
            }
        }

        string[] filesPath = Directory.GetFiles(luaFilePath, "*.lua");

        for (int i = 0; i < filesPath.Length; i++)
        {
            if (!Directory.Exists(luaFilePath + "LuaText/"))
                Directory.CreateDirectory(luaFilePath + "LuaText/");

            string[] res = filesPath[i].Split('/');

            File.Copy(filesPath[i], luaFilePath + "LuaText/" + res[res.Length - 1] + ".txt");
        }

        AssetDatabase.Refresh();
    }


}
