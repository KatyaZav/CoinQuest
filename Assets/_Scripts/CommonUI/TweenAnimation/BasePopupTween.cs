using UnityEngine;
using DG.Tweening;
using System;
public class BasePopupTween
{
    private float _duraction, _minFade, _maxFade;

    private Vector2 _minScale;
    private Vector2 _maxScale;

    private Sequence _animation;

    private CanvasGroup _canvasGroup;
    private RectTransform _popupTransform;

    public BasePopupTween(PopupInfo popup)
    {
        _duraction = 0.7f;
        _minScale = Vector2.zero;
        _maxScale = Vector2.one;

        _minFade = 0;
        _maxFade = 0.9f;

        _popupTransform = popup.RectTransform;
        _canvasGroup = popup.CanvasGroup;
    }

    public BasePopupTween(float duration, float minFade, float maxFade, Vector2 minScale, Vector2 maxScale, PopupInfo popup)
    {
        _duraction = duration;
        _minScale = minScale;
        _maxScale = maxScale;

        _minFade = minFade;
        _maxFade = maxFade;

        _popupTransform = popup.RectTransform;
        _canvasGroup = popup.CanvasGroup;
    }

    public bool IsActiveAnimation => _animation != null && _animation.active;

    public void Activate(Action callback = null)
    {
        KillActiveAnimation();

        _animation = DOTween.Sequence();

        _animation
            .Append(
                _popupTransform
                    .DOScale(_maxScale, _duraction)
                    .From(_minScale)
                    .SetEase(Ease.OutQuad)
                    .OnComplete(() => callback?.Invoke()))
            .Join(_canvasGroup
                    .DOFade(_maxFade, _duraction)
                    .From(_minFade)
                    .SetEase(Ease.InCirc));
    }

    public void Disactivate(Action callback = null)
    {
        KillActiveAnimation();

        _animation = DOTween.Sequence();

        _animation
            .Append(
                _popupTransform
                    .DOScale(_minScale, _duraction)
                    .From(_maxScale)
                    .SetEase(Ease.OutQuad)
                    .OnComplete(() => callback?.Invoke()))
            .Join(_canvasGroup
                    .DOFade(_minFade, _duraction)
                    .From(_maxFade)
                    .SetEase(Ease.InCirc));
    }

    public void CompleteAnimation()
    {
        if (IsActiveAnimation == false)
            throw new ArgumentNullException("Not found active animation");

        _animation.Complete(true);
    } 

    public void KillActiveAnimation()
    {
        if (IsActiveAnimation)
        {
            _animation.Kill();
        }
    }
}
