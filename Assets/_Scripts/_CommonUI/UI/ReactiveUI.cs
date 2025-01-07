using System;
using UnityEngine.UI;

namespace Assets.UI
{
    public class ReactiveUI<T> where T : IComparable
    {
        private IReadonlyReactive<T> _varible;
        private Text _currentText;

        public ReactiveUI(IReadonlyReactive<T> varible, Text currentText)
        {
            _varible = varible;
            _currentText = currentText;
        }

        public bool Inited => _varible != null && _currentText != null;

        public void OnInit()
        {
            if (Inited == false)
                throw new Exception("UI wasn't init!");

            ChangeText(_varible.Value);

            _varible.Changed += ChangeText;
        }

        public void OnDisable()
        {
            if (_varible == null)
                throw new Exception("UI wasn't init!");

            _varible.Changed -= ChangeText;
        }

        private void ChangeText(T data)
        {
            _currentText.text = data.ToString();
        }
    }
}
