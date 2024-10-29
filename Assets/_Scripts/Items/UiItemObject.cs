using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UiItemObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Action<Items> OnMouseEnterEvent;
    public Action OnMouseExitEvent;

    [SerializeField] private Image _image;
    [SerializeField] private Items _item;

    private Color _darkColor = new Color(11, 48, 0, 255);
    private Color _normalColor = Color.white;

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        OnMouseEnterEvent?.Invoke(_item);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        OnMouseExitEvent?.Invoke();
    }

    public void SetItem(Items item)
    {
        _item = item;
        _image.sprite = item.Icon;
    }

    public void SetNormalColor()
    {
        _image.color = _normalColor;
    }

    public void SetDarkColor()
    {
        _image.color = _darkColor;
    }
}
