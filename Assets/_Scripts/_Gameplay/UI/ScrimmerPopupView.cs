using UnityEngine;
using DG.Tweening;
using System;
using Assets.UI.Tweening.Factory;
using Assets.UI.Tweening;

namespace Assets.Gameplay.UI
{
    public class ScrimmerPopupView : MonoBehaviour
    {
        private readonly Vector2 _minScale = Vector2.zero;
        private readonly Vector2 _maxScale = Vector2.one;

        private const float MinFade = 0, MaxFade = 1;
        private const float Duration = 1;

        [Header("UI")]
        [SerializeField] private RectTransform _popupZone;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private RectTransform _catTransform;

        private AnimationTween _animation;
        private Tween _tweenCat;

        public void Init()
        {
            var startAnimation = new PopupAnimationFactory
                (new PopupInfo(_canvasGroup, _popupZone), _minScale, _maxScale, MinFade, MaxFade, Duration);

            var endAnimation = new PopupAnimationFactory
                (new PopupInfo(_canvasGroup, _popupZone), _maxScale, _minScale, MaxFade, MinFade, Duration/2);

            _animation = new AnimationTween(startAnimation, endAnimation);
        }

        public void Activate(Action callback = null)
        {
            _animation.Activate(callback);

            _tweenCat?.Kill();
            _tweenCat = _catTransform
                .DOMoveY(320, Duration)
                .From(-450)
                .SetEase(Ease.InOutQuart);
        }

        public void Deactivate(Action callback = null)
        {
            _animation.Disactivate(callback);

            _tweenCat?.Kill();
            _tweenCat = _catTransform
                .DOMoveY(-450, Duration)
                .From(320)
                .SetEase(Ease.InOutQuart);
        }
    }
}
