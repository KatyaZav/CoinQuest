using Assets.UI.Tweening;
using UnityEngine;
using UnityEngine.UI;
using YG;
using Assets.UI.Tweening.Factory;

namespace Assets.Game.UI
{
    public class ItemPopupView : MonoBehaviour
    {
        private const float MinFade = 0, MaxFade = 1;
        private const float Duraction = 0.5f;

        [Header("UI")]
        [SerializeField] private Image _image;
        [SerializeField] private Text _rareText;
        [SerializeField] private Image _anticlickZone;
        [SerializeField] private RectTransform _popupZone;
        [SerializeField] private CanvasGroup _canvasGroup;

        [Header("Settings")]
        [SerializeField] private Color _normalColor, _usualColor, _rareColor;
        [SerializeField] private float _durection;

        private AnimationTween _animation;

        private bool _isClosing = false;

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

        public void Open(Items item)
        {
            _isClosing = false;

            gameObject.SetActive(true);

            _animation.Activate();

            _image.sprite = item.Icon;
            _rareText.text = GetRare(item);
        }

        public void Init()
        {
            var startAnimation = new PopupAnimationFactory
                (new PopupInfo(_canvasGroup, _popupZone), Vector2.zero, Vector2.one, MinFade, MaxFade, Duraction);

            var endAnimation = new PopupAnimationFactory
                (new PopupInfo(_canvasGroup, _popupZone), Vector2.one, Vector2.zero, MaxFade, MinFade, Duraction);

            _animation = new AnimationTween(startAnimation, endAnimation);

            SubscriptionKeeper.GettedNewEvent += Open;
        }

        public void OnDisable()
        {
            SubscriptionKeeper.GettedNewEvent -= Open;
        }

        private string GetRare(Items item)
        {
            switch (item.GetRare)
            {
                case Rare.Usual:
                    _rareText.color = _normalColor;
                    break;
                case Rare.Rare:
                    _rareText.color = _usualColor;
                    break;
                case Rare.Legendary:
                    _rareText.color = _rareColor;
                    break;
            }

            if (YandexGame.lang == "ru")
            {
                switch (item.GetRare)
                {
                    case Rare.Usual:
                        return "Обычный";
                    case Rare.Rare:
                        return "Редкий";
                    case Rare.Legendary:
                        return "Легендарный";
                }
            }
            else
            {
                switch (item.GetRare)
                {
                    case Rare.Usual:
                        return "Regular";
                    case Rare.Rare:
                        return "Rare";
                    case Rare.Legendary:
                        return "Legendary";
                }
            }

            return "not found";
        }
    }
}
