using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpGradePanel : MonoBehaviour
{
    public Button btnUpGrade;
    public Text txUpGradeMoney;
    public Button btnSell;
    public Text txSellMoney;
    public Image imgAttackRange;
    
    private void Awake()
    {
        btnUpGrade.onClick.AddListener(() =>
        {
            
        });
        btnSell.onClick.AddListener(() =>
        {
            
        });
    }

    public void Show(Vector2 uiPos, Sprite icon,int upGradeMoney, int sellMoney, float attackRange, EBuiltPanelShowDir showDir)
    {
        btnUpGrade.GetComponent<Image>().sprite = icon;
        txUpGradeMoney.text = upGradeMoney.ToString();
        txSellMoney.text = sellMoney.ToString();
        // 缩放攻击访问大小
        ((RectTransform)imgAttackRange.transform).localScale = new Vector3(attackRange, attackRange, attackRange);
        
        // 设置位置
        ((RectTransform)transform).anchoredPosition = uiPos;
        // 显示方向
        switch (showDir)
        {
            case EBuiltPanelShowDir.Up:
                break;
            case EBuiltPanelShowDir.Down:
                break;
            case EBuiltPanelShowDir.Right:
                break;
            case EBuiltPanelShowDir.Left:
                break;
        }
    }
}
