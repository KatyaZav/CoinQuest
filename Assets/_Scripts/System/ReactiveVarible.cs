using System;

public class ReactiveVarible<T> : IReadonlyReactive<T>
    where T : IComparable
{
    public event Action<T> Changed;

    private T _value;

    public ReactiveVarible(T value)
    {
        _value = value;
    }

    public T Value
    {
        get => _value;
        set
        {
            T oldValue = _value;

            _value = value;

            if (_value.CompareTo(oldValue) != 0)
            {
                Changed?.Invoke(_value);
            }
        }
    }
}