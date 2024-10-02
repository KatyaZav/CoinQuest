using UnityEngine;
using UnityEngine.EventSystems;

public class Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _start;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _animator.SetBool(_start, true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _animator.SetBool(_start, false);
    }
}
