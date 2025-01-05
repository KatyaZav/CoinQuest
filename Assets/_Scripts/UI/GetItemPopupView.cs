using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class GetItemPopupView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Text _rareText;

    [SerializeField] private Color _normalColor, _usualColor, _rareColor;

    [SerializeField] private Image _anticlickZone;
    [SerializeField] private RectTransform _popupZone;
    [SerializeField] private float _durection;

    private Sequence _animation;

    public void Close()
    {
        _animation.Complete();

        _animation
            .Append(_anticlickZone.DOFade(0, _durection / 2))
            .Join(_popupZone.DOScale(new Vector2(0.0f, 0.0f), _durection/3) 
                .OnComplete(() => gameObject.SetActive(false)));
    }

    public void Open(Items item)
    {
        gameObject.SetActive(true);

        _animation
            .Append(_anticlickZone.DOFade(1, _durection / 2))
            .Join(_popupZone.DOScale(Vector2.one, _durection));

        _image.sprite = item.Icon;
        _rareText.text = GetRare(item);
    }

    private void Start()
    {
        SubscriptionKeeper.GettedNewEvent += Open;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        SubscriptionKeeper.GettedNewEvent -= Open;        
    }

    private string GetRare(Items item)
    {
        switch (item.GetRare)
        {
            case Rare.Usual:
                _rareText.color = _normalColor;
                break;
            case Rare.Rare:
                _rareText.color = _usualColor;
                break;
            case Rare.Legendary:
                _rareText.color = _rareColor;
                break;
        }

        if (YandexGame.lang == "ru")
        {
            switch (item.GetRare)
            {
                case Rare.Usual:
                    return "Обычный";
                case Rare.Rare:
                    return "Редкий";
                case Rare.Legendary:
                    return "Легендарный";
            }
        }
        else
        {
            switch (item.GetRare)
            {
                case Rare.Usual:
                    return "Regular";
                case Rare.Rare:
                    return "Rare";
                case Rare.Legendary:
                    return "Legendary";
            }
        }

        return "not found";
    }


}
