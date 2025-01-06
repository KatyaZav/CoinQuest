using System;

public interface IReadonlyReactive<T> where T : IComparable
{
    event Action<T> Changed;
    T Value { get; }
}