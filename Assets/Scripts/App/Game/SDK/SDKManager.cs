using System.Text.RegularExpressions;
using App.Static;
using GameFramework;
using UnityEngine;

namespace App.Game.SDK
{
    public class SDKManager : BaseMonoAutoSingleton<SDKManager>
    {
        private AndroidJavaClass javaClass;

        private AndroidJavaObject javaObject;

        // Start is called before the first frame update
        private void Start()
        {
        }


        public void StartPositioning()
        {
            if (javaClass == null || javaObject == null)
            {
                javaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                javaObject = javaClass.GetStatic<AndroidJavaObject>("currentActivity");
            }

            javaObject.Call("StartPositioning");
        }

        public void ReceivePositionInfo(string message)
        {
            // 解析信息

            // 正则表达式模式，匹配以 "address:" 开头和以 "$" 结尾的部分
            var address = @"address:(.*?)\$";
            var radius = @"radius:(.*?)\$";
            var code = @"code:(.*?)\$";

            // 使用正则表达式进行匹配
            var addressMatch = Regex.Match(message, address);
            var radiusMatch = Regex.Match(message, radius);
            var codeMatch = Regex.Match(message, code);

            // 输出匹配到的内容
            if (addressMatch.Success && radiusMatch.Success)
            {
                var addressResult = addressMatch.Groups[1].Value;
                var radiusResult = radiusMatch.Groups[1].Value;
                var codeResult = codeMatch.Groups[1].Value;

                var res = $"{addressResult}\n定位精度:{radiusResult}m";

                Debug.Log(res);

                switch (codeResult)
                {
                    case "161":
                        GameFacade.Instance.SendNotification(NotificationName.UI.SHOW_TIPS_PANEL, res);
                        break;
                    case "69":
                    case "70":
                    case "71":
                        GameFacade.Instance.SendNotification(NotificationName.UI.SHOW_TIPS_PANEL, "无定位权限" + codeResult);
                        break;
                    default:
                        GameFacade.Instance.SendNotification(NotificationName.UI.SHOW_TIPS_PANEL, "定位失败请检查网络连接" + codeResult);
                        break;
                }
            }
            else
            {
                Debug.Log("No match found.");
            }
        }

        public void Dispose()
        {
            if (javaClass != null)
            {
                javaClass.Dispose();
                javaClass = null;
            }

            if (javaObject != null)
            {
                javaObject.Dispose();
                javaObject = null;
            }
        }
    }
}