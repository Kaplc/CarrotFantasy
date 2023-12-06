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
    public int pageIndex; // 当前页码
    public int totalPageIndex; // 总共页码
    private ScrollRect scrollRect;
    public Text txPage;

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
        pageIndex = 1;
        UpdatePageIndex();
    }

    public void NextPage()
    {
        // 右滑
        pageIndex++;
        pageIndex = Mathf.Clamp(pageIndex, 1, totalPageIndex);
        float newHorizontalNormalizedPosition = 1f / (totalPageIndex - 1) * (pageIndex - 1);
        SlideTween(newHorizontalNormalizedPosition);
    }

    public void LastPage()
    {
        // 左滑
        pageIndex--;
        pageIndex = Mathf.Clamp(pageIndex, 1, totalPageIndex);
        float newHorizontalNormalizedPosition = 1f / (totalPageIndex - 1) * (pageIndex - 1);
        SlideTween(newHorizontalNormalizedPosition);
    }

    private void StayNowPage()
    {
        pageIndex = Mathf.Clamp(pageIndex, 1, totalPageIndex);
        float newHorizontalNormalizedPosition = 1f / (totalPageIndex - 1) * (pageIndex - 1);
        SlideTween(newHorizontalNormalizedPosition);
    }

    private void UpdatePageIndex()
    {
        if (!txPage) return;
        txPage.text = $"{pageIndex}/{totalPageIndex}";
    }

    /// <summary>
    /// 实现滑动
    /// </summary>
    private void SlideContent()
    {
        // 到达滑动阈值视为滑向下一页或上一页
        if (offSetMouseX <= -slidingThreshold)
        {
            NextPage();
        }
        else if (offSetMouseX > slidingThreshold)
        {
            LastPage();
        }
        else
        {
            // 不够滑动阈值回弹
            StayNowPage();
        }
        UpdatePageIndex();
    }

    /// <summary>
    /// 滑动动画
    /// </summary>
    private void SlideTween(float targetValue)
    {
        DOTween.To(
            () => scrollRect.horizontalNormalizedPosition,
            value => scrollRect.horizontalNormalizedPosition = value,
            targetValue,
            0.2f
        ).SetEase(Ease.Linear);

        if (pageIndex == 1)
        {
            // 告诉父对象现在是第一页
            SendMessageUpwards("FirstPage", SendMessageOptions.DontRequireReceiver);
        }else if (pageIndex == totalPageIndex)
        {
            SendMessageUpwards("FinallyPage", SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            SendMessageUpwards("NormalPage", SendMessageOptions.DontRequireReceiver);
        }
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        offSetMouseX = Input.mousePosition.x;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        offSetMouseX = Input.mousePosition.x - offSetMouseX;
        SlideContent();
    }

    public void OnDrag(PointerEventData eventData)
    {
        // print(Input.mousePosition.x - offSetMouseX);
    }
}