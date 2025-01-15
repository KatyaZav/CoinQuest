using Assets.UI.Tweening;
using Assets.UI.Tweening.Factory;
using UnityEngine;

public class AnswerHolderBehavior : MonoBehaviour
{
    [SerializeField] private float _durection = 0.4f;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private RectTransform _rectTransform;

    [SerializeField] private Vector2 _startPosition, _finishPosition;

    private AnimationTween _animation;

    public void Init()
    {
        var startAnimation = new MovingFactory(_rectTransform, _startPosition, _finishPosition, _durection);
        var finishAnimation = new MovingFactory(_rectTransform, _finishPosition, _startPosition, _durection);

        _animation = new AnimationTween(startAnimation, finishAnimation);
    }

    public void Activate()
    {
        _canvasGroup.interactable = true;
        _animation.Activate();
    }

    public void Disactivate()
    {
        _canvasGroup.interactable = false;
        _animation.Disactivate();
    }
}
