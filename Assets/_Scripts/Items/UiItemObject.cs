using System;
using UnityEngine;
using UnityEngine.UI;

public class UiItemObject : MonoBehaviour
{
    public Action OnMouseEnterEvent;
    public Action OnMouseExitEvent;

    private Color _darkColor = new Color(11, 48, 0, 255);
    private Color _normalColor = Color.white;

    [SerializeField] private Image _image;
    
    public void SetImage(Sprite sprite)
    {
        _image.sprite = sprite;
    }

    public void SetNormalColor()
    {
        _image.color = _normalColor;
    }

    public void SetDarkColor()
    {
        _image.color = _darkColor;
    }

    private void OnMouseEnter()
    {
        OnMouseEnterEvent?.Invoke();
    }

    private void OnMouseExit()
    {
        OnMouseExitEvent?.Invoke();
    }
}
