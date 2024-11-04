using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UiItemObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Action<Items, ItemsData> OnMouseEnterEvent;
    public Action OnMouseExitEvent;

    [SerializeField] private Image _image;
    [SerializeField] private Image _rampImage;
    [SerializeField] private ItemsData _itemData;

    [SerializeField] private Color _darkColor = new Color(11, 48, 0, 255);
    [SerializeField] private Color _normalColor = Color.white;

    [SerializeField] private Color[] _colors;

    private Items _item;
    
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        OnMouseEnterEvent?.Invoke(_item, _itemData);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        OnMouseExitEvent?.Invoke();
    }

    public void SetItem(Items item, ItemsData data)
    {
        _itemData = data;
        _item = item;
        _image.sprite = item.Icon;

        if (item.name != "Closed")
            _rampImage.color = _colors[(int)Mathf.Log((int)item.GetRare, 2)];
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

[System.Serializable]
public class ColorByRare
{
    public Color color;
    public int Rare;
}
