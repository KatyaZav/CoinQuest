using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Assets.UI.Tweening.Factory;
using System;

namespace Assets.Gameplay.UI
{
    public class ItemView : MonoBehaviour
    {
        private readonly Vector3 UnzeroRotation = new Vector3(0, 0, 250);
        private readonly Vector3 ZeroRotation = new Vector3(0, 0, 0);
        
        private readonly Vector2 MinScale = Vector2.zero;
        private readonly Vector2 MaxScale = Vector2.one;

        [Header("Main gameobject")]
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private CanvasGroup _canvasGroup;
        
        [Header("Appearance")]
        [SerializeField] private Text _coinProbabilityText;
        [SerializeField] private Image _image;
        
        private Sequence _currentAnimation;

        private float _fade, _duraction;
        private Vector2 _scale;

        private ITweenFactory _startAnimation, _collectAnimation, _destroyAnimation;

        public void Init()
        {
            _startAnimation = new RotationFadeScalerFactory(
                _rectTransform, _canvasGroup, MinScale, MaxScale, ZeroRotation, UnzeroRotation);
            _destroyAnimation = new FadeScalerFactory(_rectTransform, _canvasGroup, Vector2.zero, 0);
            _collectAnimation = new RotationFadeScalerFactory(
                _rectTransform, _canvasGroup, MaxScale, MinScale, UnzeroRotation, ZeroRotation);
        }

        public void SetImage(Sprite icon)
        {
            _image.sprite = icon;
        }

        public void SetProbabilityText(string probability)
        {
            _coinProbabilityText.text = probability;
        }

        public void SetTextColor(Color32 color)
        {
            _coinProbabilityText.color = color;
        }

        public void ActivateStartAnimation(Action callback = null)
        {
            _currentAnimation?.Kill();

            _currentAnimation = _startAnimation.GetSequence(callback);
            _currentAnimation.Play();
        }

        public void ActivateCollectAnimation(Action callback = null)
        {
            _currentAnimation?.Kill();

            _currentAnimation = _collectAnimation.GetSequence(callback);
            _currentAnimation.Play();
        }

        public void ActivateDestroyAnimation(Action callback = null)
        {
            _currentAnimation?.Kill();

            _currentAnimation = _destroyAnimation.GetSequence(callback);
            _currentAnimation.Play();
        }
    }
}
