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
    private bool showCreatePanel;
    private bool showUpGradePanel;
    public CreatePanel createPanel; // 创建面板
    public UpGradePanel upGradePanel; // 升级面板
    
    public bool IsShowCreatePanel
    {
        get => showCreatePanel;
        set
        {
            showCreatePanel = value;
            if (value)
            {
                // 创建面板出现升级面板就隐藏
                upGradePanel.gameObject.SetActive(false);  
            }
        }
    }
    
    public bool IsShowUpGradePanel
    {
        get => showUpGradePanel;
        set
        {
            showUpGradePanel = value;
            if (value)
            {
                createPanel.gameObject.SetActive(false);  
            }
        }
    }

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
    /// <param name="cellWorldPos"></param>
    /// <param name="towersDataDic"></param>
    /// <param name="showDir">显示位置</param>
    public void ShowCreatePanel(Vector3 cellWorldPos, Dictionary<TowerData, Sprite> towersDataDic, EBuiltPanelShowDir showDir)
    {
        IsShowCreatePanel = true;
        Vector2 uiPos = WorldPosToUIPos(cellWorldPos);
        createPanel.Show(uiPos, cellWorldPos, towersDataDic, showDir);
    }

    public void ShowUpGradePanel(Vector3 cellWorldPos, Sprite icon, int upGradeMoney, int sellMoney, float attackRange, EBuiltPanelShowDir showDir)
    {
        IsShowUpGradePanel = true;
        upGradePanel.cellWorldPos = cellWorldPos;
        Vector2 uiPos = WorldPosToUIPos(cellWorldPos);
        upGradePanel.Show(uiPos, icon, upGradeMoney, sellMoney, attackRange, showDir);
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