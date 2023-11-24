using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuiltPanel : BasePanel
{
    public ChildBuiltPanel childBuiltPanel; // 子面板建造面板
    public GameObject upgradePanel; // 子面板升级面板
    

    protected override void Init()
    {
        
    }
    
    
    /// <summary>
    /// 世界坐标转UI坐标
    /// </summary>
    public Vector2 WorldPosToUIPos(Vector3 cellCenterPos)
    {
        Vector2 screenPos = UIManager.Instance.uiCamera.ViewportToScreenPoint(UIManager.Instance.uiCamera.WorldToViewportPoint(cellCenterPos));
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform as RectTransform, screenPos, UIManager.Instance.uiCamera, out Vector2 uiPos);
        return uiPos;
    }
    
    public void ShowBuiltPanel(Vector3 cellCenterPos)
    {
        childBuiltPanel.Show(WorldPosToUIPos(cellCenterPos));
    }

    public void ShowUpGradePanel()
    {


    }
}
