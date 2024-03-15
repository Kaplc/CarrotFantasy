using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class SDKManager : MonoBehaviour
{
    AndroidJavaClass javaClass;

    AndroidJavaObject javaObject;

    // Start is called before the first frame update
    void Start()
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
        string address = @"address:(.*?)\$";
        string radius = @"radius:(.*?)\$";
        string code = @"code:(.*?)\$";

        // 使用正则表达式进行匹配
        Match addressMatch = Regex.Match(message, address);
        Match radiusMatch = Regex.Match(message, radius);
        Match codeMatch = Regex.Match(message, code);

        // 输出匹配到的内容
        if (addressMatch.Success && radiusMatch.Success)
        {
            string addressResult = addressMatch.Groups[1].Value;
            string radiusResult = radiusMatch.Groups[1].Value;
            string codeResult = codeMatch.Groups[1].Value;

            string res = $"{addressResult}\n定位精度:{radiusResult}m";

            Debug.Log(res);

            switch (codeResult)
            {
                case "161":
                    GameFacade.Instance.SendNotification(NotificationName.UI.SHOW_TIPS_PAENL, res);
                    break;
                case "69":
                case "70":
                case "71":
                    GameFacade.Instance.SendNotification(NotificationName.UI.SHOW_TIPS_PAENL, "无定位权限" + codeResult);
                    break;
                default:
                    GameFacade.Instance.SendNotification(NotificationName.UI.SHOW_TIPS_PAENL, "定位失败请检查网络连接" + codeResult);
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