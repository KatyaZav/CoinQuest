using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ScalerFadeTween : MonoBehaviour
{
    [SerializeField] private float _duration;

    [SerializeField] private RectTransform _rect;
    [SerializeField] private Text _text;

    private Sequence _animation;

    public void Open(Action callback = null)
    {
        _animation.Kill();
        //_animation.Complete();

        _animation = DOTween.Sequence();
        _animation
            .Append(_text.DOFade(0, _duration / 4))
            .Append(transform.DOScale(Vector2.one, _duration))
            .Join(transform.GetComponent<Image>().DOFade(1, _duration))
            .Join(_text.DOFade(1, _duration))
            .OnComplete(() => callback?.Invoke());
    }

    public void Hide(Action callback = null)
    {
        _animation.Kill();
        //_animation.Complete();

        _animation = DOTween.Sequence();
        _animation
            .Append(transform.DOScale(Vector2.zero, _duration))
            .Join(transform.GetComponent<Image>().DOFade(0, _duration))
            .Join(_text.DOFade(0, _duration))
            .OnComplete(() => callback?.Invoke());
    }
}

