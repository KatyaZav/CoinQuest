using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;

public class OnMouseScaleAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private const float DuractionTime = 0.5f;

    private Sequence _animation;

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        Open();
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        Hide();
    }

    public void Open()
    {
        _animation.Complete();

        _animation
            .Append(transform.DOScale(new Vector2(1.1f, 1.1f), DuractionTime));
    }

    public void Hide(Action callback = null)
    {
        _animation.Complete();

        _animation
            .Append(transform.DOScale(Vector2.one, DuractionTime));
    }
}
