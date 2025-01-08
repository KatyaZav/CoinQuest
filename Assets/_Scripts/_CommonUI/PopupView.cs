using Assets.UI.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Assets.UI.Tweening.Factory;
using System;

namespace Assets.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class PopupView : MonoBehaviour
    {
        private readonly Vector2 _minScale = Vector2.zero;
        private readonly Vector2 _maxScale = Vector2.one;

        private const float MinFade = 0, MaxFade = 1;
        private const float Duration = 0.5f;

        [Header("UI")]
        [SerializeField] private Image _image;
        [SerializeField] private Text _descriptionText, _headerText;
        [SerializeField] private Image _anticlickZone;

        [SerializeField] private RectTransform _popupZone;
        
        private CanvasGroup _canvasGroup;

        private AnimationTween _animation;

        private bool _isClosing = false;

        public void Init(string header)
        {
            _headerText.text = header;

            _canvasGroup = GetComponent<CanvasGroup>();

            var startAnimation = new PopupAnimationFactory
                (new PopupInfo(_canvasGroup, _popupZone), _minScale, _maxScale, MinFade, MaxFade, Duration);

            var endAnimation = new PopupAnimationFactory
                (new PopupInfo(_canvasGroup, _popupZone), _maxScale, _minScale, MaxFade, MinFade, Duration);

            _animation = new AnimationTween(startAnimation, endAnimation);
        }

        public bool Inited => _animation != null;

        public void Close()
        {
            if (_isClosing)
            {
                _animation.CompleteActiveAnimation();
                _isClosing = false;
                return;
            }

            _isClosing = true;
            _animation.Disactivate(() => gameObject.SetActive(false));
        }

        public void Open(Sprite sprite, string text)
        {
            if (Inited == false)
                throw new Exception("Popup wasn't init!");

            _isClosing = false;

            _image.sprite = sprite;
            _descriptionText.text = text;

            gameObject.SetActive(true);
            _animation.Activate();
        }
    }
}
