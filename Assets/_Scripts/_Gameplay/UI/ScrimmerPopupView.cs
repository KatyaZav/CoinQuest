using UnityEngine;
using DG.Tweening;
using System;
using Assets.UI.Tweening.Factory;
using Assets.UI.Tweening;

namespace Assets.Gameplay.UI
{
    public class ScrimmerPopupView : MonoBehaviour
    {
        private readonly Vector2 _start = new Vector2(0, 320);
        private readonly Vector2 _finish = new Vector2(0, -450);

        private readonly Vector2 _minScale = Vector2.zero;
        private readonly Vector2 _maxScale = Vector2.one;

        private const float MinFade = 0, MaxFade = 1;
        private const float Duration = 1;

        [Header("UI")]
        [SerializeField] private RectTransform _popupZone;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private RectTransform _catTransform;

        private AnimationTween _animation, _catAnimation;
        private bool _isClosing = false;

        private Action _callback;

        public void Init()
        {
            var start = new MovingFactory(_catTransform, _finish, _start, Duration);
            var finish = new MovingFactory(_catTransform, _start, _finish, Duration);

            _catAnimation = new AnimationTween(start, finish);

            var startAnimation = new PopupAnimationFactory
                (new PopupInfo(_canvasGroup, _popupZone), _minScale, _maxScale, MinFade, MaxFade, Duration/2);

            var endAnimation = new PopupAnimationFactory
                (new PopupInfo(_canvasGroup, _popupZone), _maxScale, _maxScale, MaxFade, MinFade, Duration/2);

            _animation = new AnimationTween(startAnimation, endAnimation);
        }

        public void AddCallback(Action callback)
        {
            _callback = callback;
        }

        public void Close()
        {
            if (_isClosing)
            {
                _animation.CompleteActiveAnimation();
                _catAnimation.CompleteActiveAnimation();

                _isClosing = false;
                return;
            }

            _isClosing = true;
            Deactivate();
        }

        public void Activate(Action callback = null)
        {
            _isClosing = false;
            gameObject.SetActive(true);

            _animation.Activate(() =>
            {
                _catAnimation.Activate();
                callback?.Invoke();
            });
        }

        public void Deactivate(Action callback = null)
        {
            _animation.Disactivate(() => {
                callback?.Invoke();
                });
            _catAnimation.Disactivate(() =>
                {
                    _callback?.Invoke();
                    gameObject.SetActive(false);
                });
        }
    }
}