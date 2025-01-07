using DG.Tweening;
using System;

namespace Assets.UI.Tweening.Factory
{
    public interface ITweenFactory
    {
        public Sequence GetSequence(Action callback = null);
    }
}
