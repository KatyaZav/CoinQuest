using UnityEngine;
using UnityEngine.UI;
using YG;

public class GetItemPopupView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Text _rareText;

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Init(Items item)
    {
        gameObject.SetActive(true);

        _image.sprite = item.Icon;
        _rareText.text = GetRare(item);
    }

    private void Start()
    {
        SubscriptionKeeper.GettedNewEvent += Init;
        Close();
    }

    private void OnDestroy()
    {
        SubscriptionKeeper.GettedNewEvent -= Init;        
    }

    private string GetRare(Items item)
    {
        if (YandexGame.lang == "ru")
        {
            switch (item.GetRare)
            {
                case Rare.usual:
                    return "Обычный";
                case Rare.normal:
                    return "Редкий";
                case Rare.rare:
                    return "Легендарный";
            }
        }
        else
        {
            switch (item.GetRare)
            {
                case Rare.usual:
                    return "Regular";
                case Rare.normal:
                    return "Rare";
                case Rare.rare:
                    return "Legendary";
            }
        }

        return "not found";
    }
}
