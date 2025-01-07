using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Action OnMouseEnterEvent;
    public Action OnMouseExitEvent;

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        OnMouseEnterEvent?.Invoke();
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        OnMouseExitEvent?.Invoke();
    }
}
