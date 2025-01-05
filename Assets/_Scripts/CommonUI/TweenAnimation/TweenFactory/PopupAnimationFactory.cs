using DG.Tweening;
using System;
using UnityEngine;

namespace UI.Tweening.Factory
{
    public class PopupAnimationFactory : ITweenFactory
    {
        private float _duraction, _startFade, _finalFade;

        private Vector2 _startScale;
        private Vector2 _finalScale;

        private Sequence _animation;

        private CanvasGroup _canvasGroup;
        private RectTransform _popupTransform;

        public PopupAnimationFactory(PopupInfo popup, Vector2 startScale, Vector2 finalScale,
            float startFade = 0, float finalFade = 0.9f, float duration = 0.5f)
        {
            _duraction = duration;
            _startScale = startScale;
            _finalScale = finalScale;

            _startFade = startFade;
            _finalFade = finalFade;

            _popupTransform = popup.RectTransform;
            _canvasGroup = popup.CanvasGroup;
        }

        public Sequence GetSequence(Action callback = null)
        {
            _animation = DOTween.Sequence();

            _animation
                .Append(
                    _popupTransform
                        .DOScale(_finalScale, _duraction)
                        .From(_startScale)
                        .SetEase(Ease.OutQuad))
                .Join(_canvasGroup
                        .DOFade(_finalFade, _duraction)
                        .From(_startFade)
                        .SetEase(Ease.InCirc))
                .OnComplete(() => callback?.Invoke());

            return _animation;
        }
    }
}
