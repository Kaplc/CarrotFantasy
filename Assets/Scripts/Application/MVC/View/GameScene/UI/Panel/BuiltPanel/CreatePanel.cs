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
    public void Show(Vector2 uiPos, Vector3 cellWorldPos, Dictionary<TowerData, Sprite> towersDataDic, EBuiltPanelShowDir showDir)
    {
        // 设置面板中心位置为格子中心
        ((RectTransform)transform).anchoredPosition = uiPos;
        
        // 创建遍历计数器
        int count = 0;
        // 创建按钮
        foreach (KeyValuePair<TowerData, Sprite> item in towersDataDic)
        {
            Button button =GameManager.Instance.FactoryManager.UIControlFactory.CreateControl("ButtonCreateTower").GetComponent<Button>();
            // 设置信息
            button.GetComponent<Image>().sprite = item.Value;

            // 设置位置
            RectTransform buttonRect = button.transform as RectTransform;
            buttonRect.SetParent(iconsRect);
            buttonRect.localScale = Vector3.one;
            switch (showDir)
            {
                case EBuiltPanelShowDir.Up:
                    buttonRect.anchoredPosition = new Vector2(-40 * (towersDataDic.Count - 1) + 80 * count, buttonRect.anchoredPosition.y);
                    break;
                case EBuiltPanelShowDir.Down:
                    buttonRect.anchoredPosition = new Vector2(-40 * (towersDataDic.Count - 1) + 80 * count, -buttonRect.anchoredPosition.y);
                    break;
                case EBuiltPanelShowDir.Right:
                    break;
                case EBuiltPanelShowDir.Left:
                    break;
            }

            count++;
            
            // 监听点击事件
            button.onClick.AddListener(() =>
            {
                GameFacade.Instance.SendNotification(NotificationName.UIEvent.CREATE_TOWER, new CreateTowerArgsBogy()
                {
                    towerData = item.Key,
                    cellWorldPos = cellWorldPos
                });
            });
        }
    }
}