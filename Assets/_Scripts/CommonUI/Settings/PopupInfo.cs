using UnityEngine;

public class PopupInfo
{
    private CanvasGroup _canvasGroup;

    private RectTransform _popupTransform;

    public PopupInfo(CanvasGroup canvasGroup, RectTransform popupTransform)
    {
        _canvasGroup = canvasGroup;
        _popupTransform = popupTransform;
    }

    public CanvasGroup CanvasGroup => _canvasGroup;

    public RectTransform RectTransform => _popupTransform;
}
