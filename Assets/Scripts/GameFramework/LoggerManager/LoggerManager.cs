using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace GameFramework
{
    public static class LoggerManager
    {
        private static StreamWriter logWriter;

        public static void Init()
        {
            // 初始化日志文件写入器
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
            string logFileName = $"log_{currentDate}.txt";
            if (!Directory.Exists(Path.Combine(Application.persistentDataPath, "log")))
            {
                Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "log"));
            }

            logWriter = new StreamWriter(Path.Combine(Application.persistentDataPath + "/log/", logFileName), true);

            Application.logMessageReceived += (logString, stackTrace, type) =>
            {
                if (type == LogType.Error || type == LogType.Exception)
                {
                    LogError(logString + "\nStackTrace: " + stackTrace);
                }
                else if (type == LogType.Warning)
                {
                    LogWarning(logString + "\nStackTrace: " + stackTrace);
                }
                else
                {
                    Log(logString);
                }
            };
            
            DeleteOldLogFiles();
        }

        public static void Close()
        {
            // 关闭日志文件写入器
            if (logWriter != null)
            {
                logWriter.Close();
            }
        }

        private static void Log(string message)
        {
            // 输出日志到控制台
            // Debug.Log(message);

            // 输出日志到日志文件
            if (logWriter != null)
            {
                logWriter.WriteLine($"[{DateTime.Now}] [info] {message}");
                logWriter.Flush();
            }
        }

        private static void LogWarning(string message)
        {
            // 输出警告日志到控制台
            // Debug.LogWarning(message);

            // 输出警告日志到日志文件
            if (logWriter != null)
            {
                logWriter.WriteLine($"[{DateTime.Now}] [Waring] {message}");
                logWriter.Flush();
            }
        }

        private static void LogError(string message)
        {
            // 输出错误日志到控制台
            // Debug.LogError(message);

            // 输出错误日志到日志文件
            if (logWriter != null)
            {
                logWriter.WriteLine($"[{DateTime.Now}] [Error] {message}");
                logWriter.Flush();
            }
        }
        
        private static void DeleteOldLogFiles()
        {
            
            DirectoryInfo directoryInfo = new DirectoryInfo(Application.persistentDataPath + "/log");
            FileInfo[] logFiles = directoryInfo.GetFiles($"*.txt");

            if (logFiles.Length > 10)
            {
                // 按创建时间排序，删除最旧的日志文件
                List<FileInfo> sortedLogFiles = logFiles.OrderBy(f => f.CreationTime).ToList();
                for (int i = 0; i < sortedLogFiles.Count - 10; i++)
                {
                    sortedLogFiles[i].Delete();
                }
            }
        }
    }
}