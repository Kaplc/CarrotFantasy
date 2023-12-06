using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollViewPageFlippingEffect : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public float slidingThreshold; // 滑动像素阈值
    private float offSetMouseX; // 鼠标拖动的水平位移
    private int pageIndex; // 当前页码
    public int totalPageIndex; // 总共页码
    private ScrollRect scrollRect;
    public Text txPage;

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
        pageIndex = 1;
        UpdatePageIndex();
    }

    private void UpdatePageIndex()
    {
        txPage.text = $"{pageIndex}/{totalPageIndex}";
    }

    /// <summary>
    /// 设置ScrollView的内容位置
    /// </summary>
    private void SetContentPos()
    {
        float newHorizontalNormalizedPosition = 0;
        // 到达滑动阈值视为滑向下一页或上一页
        if (offSetMouseX <= -slidingThreshold)
        {
            // 右滑
            pageIndex++;
            pageIndex = Mathf.Clamp(pageIndex, 1, totalPageIndex);
            newHorizontalNormalizedPosition = 1f / (totalPageIndex - 1) * (pageIndex - 1);
        }
        else if(offSetMouseX > slidingThreshold)
        {
            // 左滑
            pageIndex--;
            pageIndex = Mathf.Clamp(pageIndex, 1, totalPageIndex);
            newHorizontalNormalizedPosition = 1f / (totalPageIndex-1) * (pageIndex - 1);
        }
        else
        {
            pageIndex = Mathf.Clamp(pageIndex, 1, totalPageIndex);
            newHorizontalNormalizedPosition = 1f / (totalPageIndex-1) * (pageIndex - 1);
        }
        
        DOTween.To(
            () => scrollRect.horizontalNormalizedPosition, 
            value => scrollRect.horizontalNormalizedPosition = value,
            newHorizontalNormalizedPosition, 
            0.2f
        ).SetEase(Ease.Linear);
        
        UpdatePageIndex();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        offSetMouseX = Input.mousePosition.x;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        offSetMouseX = Input.mousePosition.x - offSetMouseX;
        SetContentPos();
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        // print(Input.mousePosition.x - offSetMouseX);
    }
}