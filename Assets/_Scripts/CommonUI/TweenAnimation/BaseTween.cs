using DG.Tweening;
using System;

namespace UI.Tween
{
    public class BaseTween
    {
        private Sequence _currentAnimation, _startAnimation, _endAnimation;

        public BaseTween(Sequence startAnimation, Sequence endAnimation)
        {
            _startAnimation = startAnimation;
            _endAnimation = endAnimation;
        }

        public bool IsActiveAnimation => _currentAnimation != null && _currentAnimation.active;

        public void Activate(Action callback = null)
        {
            KillActiveAnimation();

            _currentAnimation = _startAnimation;
            _currentAnimation.Play();
        }

        public void Disactivate(Action callback = null)
        {
            KillActiveAnimation();

            _currentAnimation = _endAnimation;
            _currentAnimation.Play();
        }

        public void CompleteActiveAnimation()
        {
            if (IsActiveAnimation)
                _currentAnimation.Complete(true);
        }

        public void KillActiveAnimation()
        {
            if (IsActiveAnimation)
            {
                _currentAnimation.Kill();
            }
        }
    }
}
