using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BasePageFlipping : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public float slidingThreshold; // 滑动像素阈值
    private float offSetMouseX; // 鼠标拖动的水平位移
    public int pageIndex; // 当前页码
    public int totalPageIndex; // 总共页码
    public float speed; // 动画速度
    private ScrollRect scrollRect;

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
        pageIndex = 1;
    }

    public virtual void NextPage()
    {
        // 右滑
        pageIndex++;
        pageIndex = Mathf.Clamp(pageIndex, 1, totalPageIndex);
        float newHorizontalNormalizedPosition = 1f / (totalPageIndex - 1) * (pageIndex - 1);
        SlideTween(newHorizontalNormalizedPosition);
    }

    public virtual void LastPage()
    {
        // 左滑
        pageIndex--;
        pageIndex = Mathf.Clamp(pageIndex, 1, totalPageIndex);
        float newHorizontalNormalizedPosition = 1f / (totalPageIndex - 1) * (pageIndex - 1);
        SlideTween(newHorizontalNormalizedPosition);
    }

    protected virtual void StayNowPage()
    {
        pageIndex = Mathf.Clamp(pageIndex, 1, totalPageIndex);
        float newHorizontalNormalizedPosition = 1f / (totalPageIndex - 1) * (pageIndex - 1);
        SlideTween(newHorizontalNormalizedPosition);
    }

    /// <summary>
    /// 实现滑动
    /// </summary>
    protected virtual void SlideContent()
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
    }

    /// <summary>
    /// 滑动动画
    /// </summary>
    protected virtual void SlideTween(float targetValue)
    {
       Tween tween = DOTween.To(
            () => scrollRect.horizontalNormalizedPosition,
            value => scrollRect.horizontalNormalizedPosition = value,
            targetValue,
            speed
        ).SetEase(Ease.Linear);
       // 添加动画完成的回调
       tween.onComplete = () =>
       {
           SendMessageUpwards("PageFlippingCompleted", SendMessageOptions.DontRequireReceiver);
       };

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