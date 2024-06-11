using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using XLua;

[LuaCallCSharp]
public class UIInterfaceClass : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public UnityAction<PointerEventData> onBeginDragAction;
    public UnityAction<PointerEventData> onEndDragAction;
    public UnityAction<PointerEventData> onDragAction;

    public void OnBeginDrag(PointerEventData eventData)
    {
        onBeginDragAction?.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        onDragAction?.Invoke(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        onEndDragAction?.Invoke(eventData);
    }
}