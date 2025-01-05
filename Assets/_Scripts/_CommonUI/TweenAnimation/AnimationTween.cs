using DG.Tweening;
using System;
using UI.Tweening.Factory;

namespace UI.Tweening
{
    public class AnimationTween
    {
        private Sequence _currentAnimation;
        private ITweenFactory _startAnimation, _endAnimation;

        public AnimationTween(ITweenFactory start, ITweenFactory stop)
        {
            _startAnimation = start;
            _endAnimation = stop;
        }

        public bool IsActiveAnimation => _currentAnimation != null && _currentAnimation.active;

        public void Activate(Action callback = null)
        {
            KillActiveAnimation();

            _currentAnimation = _startAnimation.GetSequence(callback);
        }

        public void Disactivate(Action callback = null)
        {
            KillActiveAnimation();

            _currentAnimation = _endAnimation.GetSequence(callback);
        }

        public void CompleteActiveAnimation()
        {
            if (IsActiveAnimation)
                _currentAnimation.Complete(true);
        }
        
        public void KillActiveAnimation(bool needCallback = false)
        {
            if (IsActiveAnimation)
            {
                _currentAnimation.Kill(needCallback);
            }
        }
    }
}
