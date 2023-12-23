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
    public Vector3 cellWorldPos;

    private void Awake()
    {
        btnUpGrade.onClick.AddListener(() => { GameFacade.Instance.SendNotification(NotificationName.UIEvent.UPGRADE_TOWER, cellWorldPos); });
        btnSell.onClick.AddListener(() => { GameFacade.Instance.SendNotification(NotificationName.UIEvent.SELL_TOWER, cellWorldPos); });
    }

    public void Show(Vector2 uiPos, Sprite icon, int upGradeMoney, int sellMoney, float attackRange, EBuiltPanelShowDir showDir)
    {
        btnUpGrade.GetComponent<Image>().sprite = icon;

        if (upGradeMoney == 0)
        {
            // 如果为0则为最大等级不显示价钱
            txUpGradeMoney.gameObject.SetActive(false);
        }
        else
        {
            txUpGradeMoney.text = upGradeMoney.ToString();
        }

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