using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class ScrimmerAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform _scrimmer;
    [SerializeField] private Image _anticliker;
    [SerializeField] private Animator _scrimmerAnimator;
    [SerializeField] private float _duraction;

    private Sequence _animation;

    public void Activate(Action callback = null)
    {
        _animation.Complete();

        _scrimmerAnimator.SetBool("active", true);

        _animation = DOTween.Sequence();

        _animation
            .Append(_anticliker
                    .DOFade(1, _duraction/2)
                    .SetEase(Ease.InBack))
            .Join(_scrimmer
                    .DOScale(Vector2.one, _duraction)
                    .SetEase(Ease.InBounce))
            .Join(_scrimmer
                    .DOLocalMove(new Vector2(0, -600), _duraction)
                    .OnComplete(() => callback?.Invoke()));            
    }

    public void Deactivate(Action callback = null)
    {
        _animation.Complete();

        _scrimmerAnimator.SetBool("active", false);

        _animation = DOTween.Sequence();

        _animation
            .Append(_anticliker
                    .DOFade(0, _duraction)
                    .SetEase(Ease.InBack))
            .Join(_scrimmer
                    .DOScale(new Vector2(0.8f, 0.8f), _duraction)
                    .SetEase(Ease.InBounce))
            .Join(_scrimmer
                    .DOLocalMove(new Vector2(0, -1329), _duraction)
                    .OnComplete(() => callback?.Invoke()));
    }
}
