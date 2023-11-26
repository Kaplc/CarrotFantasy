using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatePanel : MonoBehaviour
{
    public bool show; // 已经打开面板标识
    public RectTransform iconsRect;

    /// <summary>
    /// 根据UI坐标显示建造面板
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="icons"></param>
    /// <param name="showDir"></param>
    public void Show(Vector2 pos, Dictionary<int, Sprite> iconsDic, EBuiltPanelShowDir showDir)
    {
        // 设置面板中心位置为格子中心
        ((RectTransform)transform).anchoredPosition = pos;

        // 创建按钮
        for (int i = 0; i < iconsDic.Count; i++)
        {
            RectTransform buttonRect = Instantiate(Resources.Load<GameObject>("UI/Button/ButtonCreateTower"), iconsRect).GetComponent<RectTransform>();
            // 设置位置
            switch (showDir)
            {
                case EBuiltPanelShowDir.Up:
                    buttonRect.anchoredPosition = new Vector2(-40 * (iconsDic.Count - 1) + 80 * i, buttonRect.anchoredPosition.y);
                    break;
                case EBuiltPanelShowDir.Down:
                    buttonRect.anchoredPosition = new Vector2(-40 * (iconsDic.Count - 1) + 80 * i, -buttonRect.anchoredPosition.y);
                    break;
                case EBuiltPanelShowDir.Right:
                    break;
                case EBuiltPanelShowDir.Left:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(showDir), showDir, null);
            }

            // 设置Icon
            buttonRect.GetComponent<Image>().sprite = iconsDic[i];
        }

        show = true;
    }
}