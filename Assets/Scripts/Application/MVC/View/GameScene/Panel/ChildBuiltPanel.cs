using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChildBuiltPanel : MonoBehaviour
{
    public RectTransform image;
    
    /// <summary>
    /// 根据UI坐标显示
    /// </summary>
    /// <param name="pos"></param>
    public void Show(Vector2 pos)
    {
        image.anchoredPosition = pos;
    }
}
