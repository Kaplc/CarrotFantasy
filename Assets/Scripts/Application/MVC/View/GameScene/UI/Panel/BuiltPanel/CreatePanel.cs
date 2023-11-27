using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatePanel : MonoBehaviour
{
    public RectTransform iconsRect;

    /// <summary>
    /// 根据UI坐标显示建造面板
    /// </summary>
    /// <param name="iconsDic">icons字典</param>
    /// <param name="showDir">显示方向</param>
    /// <param name="uiPos">ui的位置坐标</param>
    /// <param name="cellWorldPos">格子世界坐标</param>
    public void Show(Vector2 uiPos, Vector3 cellWorldPos, Dictionary<int, Sprite> iconsDic, EBuiltPanelShowDir showDir)
    {
        // 设置面板中心位置为格子中心
        ((RectTransform)transform).anchoredPosition = uiPos;
        
        // 创建遍历计数器
        int count = 0;
        // 创建按钮
        foreach (KeyValuePair<int, Sprite> item in iconsDic)
        {
            ButtonTower button = Instantiate(Resources.Load<GameObject>("UI/Button/ButtonCreateTower"), iconsRect).GetComponent<ButtonTower>();
            // 设置信息
            button.towerID = item.Key;
            button.icon.sprite = item.Value;
            button.cellWorldPos = cellWorldPos;

            // 设置位置
            RectTransform buttonRect = button.transform as RectTransform;
            switch (showDir)
            {
                case EBuiltPanelShowDir.Up:
                    buttonRect.anchoredPosition = new Vector2(-40 * (iconsDic.Count - 1) + 80 * count, buttonRect.anchoredPosition.y);
                    break;
                case EBuiltPanelShowDir.Down:
                    buttonRect.anchoredPosition = new Vector2(-40 * (iconsDic.Count - 1) + 80 * count, -buttonRect.anchoredPosition.y);
                    break;
                case EBuiltPanelShowDir.Right:
                    break;
                case EBuiltPanelShowDir.Left:
                    break;
            }

            count++;
        }
    }
}