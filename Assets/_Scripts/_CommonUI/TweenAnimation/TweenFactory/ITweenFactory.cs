using DG.Tweening;
using System;

namespace UI.Tweening.Factory
{
    public interface ITweenFactory
    {
        public Sequence GetSequence(Action callback = null);
    }
}
