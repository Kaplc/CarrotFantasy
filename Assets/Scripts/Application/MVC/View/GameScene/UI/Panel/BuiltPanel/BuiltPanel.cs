using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using UnityEngine;
using UnityEngine.Events;


public class BuiltPanel : BasePanel
{
    private bool showCreatePanel;
    private bool showUpGradePanel;
    public CreatePanel createPanel; // 创建面板
    public UpGradePanel upGradePanel; // 升级面板
    public RectTransform cantBuiltIconRectTransform; // 禁止建造图标
    private float lastShowCantBuiltIconTime; // 上次显示禁止建造图标时间

    public bool IsShowCreatePanel
    {
        get => showCreatePanel;
        set
        {
            showCreatePanel = value;
            if (value)
            {
                // 创建面板出现升级面板就隐藏
                createPanel.gameObject.SetActive(true);
                upGradePanel.gameObject.SetActive(false);
                cantBuiltIconRectTransform.gameObject.SetActive(false);
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
                upGradePanel.gameObject.SetActive(true);
                createPanel.gameObject.SetActive(false);
                cantBuiltIconRectTransform.gameObject.SetActive(false);
            }
        }
    }

    protected override void Init()
    {
    }

    public override void Update()
    {
        base.Update();
        if (Time.realtimeSinceStartup - lastShowCantBuiltIconTime > 1)
        {
            cantBuiltIconRectTransform.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 世界坐标转UI坐标
    /// </summary>
    private Vector2 WorldPosToUIPos(Vector3 cellCenterPos)
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
    
    /// <summary>
    /// 显示升级面板
    /// </summary>
    /// <param name="cellWorldPos">格子中心点</param>
    /// <param name="icon">图标</param>
    /// <param name="upGradeMoney">升级的金币</param>
    /// <param name="sellMoney">卖出获得的金币</param>
    /// <param name="attackRange">攻击范围</param>
    /// <param name="showDir">UI显示方向</param>
    public void ShowUpGradePanel(Vector3 cellWorldPos, Sprite icon, int upGradeMoney, int sellMoney, float attackRange, EBuiltPanelShowDir showDir)
    {
        IsShowUpGradePanel = true;
        upGradePanel.cellWorldPos = cellWorldPos;
        Vector2 uiPos = WorldPosToUIPos(cellWorldPos);
        upGradePanel.Show(uiPos, icon, upGradeMoney, sellMoney, attackRange, showDir);
    }

    public void ShowCantBuiltIcon(Vector3 pos)
    {
        cantBuiltIconRectTransform.gameObject.SetActive(true);
        createPanel.gameObject.SetActive(false);
        upGradePanel.gameObject.SetActive(false);
        
        cantBuiltIconRectTransform.anchoredPosition = WorldPosToUIPos(pos);
        lastShowCantBuiltIconTime = Time.realtimeSinceStartup;
    }
}