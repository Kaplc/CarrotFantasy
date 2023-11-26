using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 面板显示方向
/// </summary>
public enum EBuiltPanelShowDir
{
    Up,
    Down,
    Right,
    Left
}

public class BuiltPanel : BasePanel
{
    public CreatePanel createPanel; // 创建面板
    public GameObject upGradePanel; // 升级面板


    protected override void Init()
    {
    }

    /// <summary>
    /// 世界坐标转UI坐标
    /// </summary>
    public Vector2 WorldPosToUIPos(Vector3 cellCenterPos)
    {
        Vector2 screenPos = UIManager.Instance.uiCamera.ViewportToScreenPoint(UIManager.Instance.uiCamera.WorldToViewportPoint(cellCenterPos));
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform as RectTransform, screenPos, UIManager.Instance.uiCamera,
            out Vector2 uiPos);
        return uiPos;
    }

    /// <summary>
    /// 显示创建塔面板
    /// </summary>
    /// <param name="createPos">创建位置</param>
    /// <param name="iconsDic"></param>
    /// <param name="showDir">显示位置</param>
    public void ShowCreatePanel(Vector3 createPos, Dictionary<int, Sprite> iconsDic, EBuiltPanelShowDir showDir)
    {
        createPanel.gameObject.SetActive(true);
        Vector2 pos = WorldPosToUIPos(createPos);
        createPanel.Show(pos, iconsDic, showDir);
    }

    public void ShowUpGradePanel()
    {
        
    }
    
    
    /// <summary>
    /// 隐藏创建面板
    /// </summary>
    public void HideCreatePanel()
    {
        createPanel.gameObject.SetActive(false);
    }
    
    /// <summary>
    /// 隐藏升级面板
    /// </summary>
    public void HideUpGradePanel()
    {
        upGradePanel.gameObject.SetActive(false);
    }
}