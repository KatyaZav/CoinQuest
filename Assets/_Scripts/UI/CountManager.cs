using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CountManager : MonoBehaviour
{
    [SerializeField] private PopupUI _popupUI;
    [SerializeField] private List<Texts> _texts;
    
    public void Init()
    {
        PlayerSaves.ChangedFinalCountMoney += OnChangeMoney;
    }

    private void OnDestroy()
    {
        PlayerSaves.ChangedFinalCountMoney -= OnChangeMoney;
    }

    private void OnChangeMoney(int previous, int current)
    {
        var item = _texts.FirstOrDefault(item => item.MaxCount > current);

        if (item == null)
            return;

        if (item.Cheaked(previous, current))
        {
            string needText = item.GetText(YG.YandexGame.lang);
            _popupUI.Enable(needText);
        }
    }
}

[System.Serializable]
public class Texts
{
    [SerializeField] private string _rus, _eng;
    [SerializeField] private int _minCount;
    [SerializeField] private int _maxCount;

    public int MaxCount => _maxCount;

    public bool Cheaked(int prev, int cur)
    {
        //Debug.Log($"{prev} {cur}  -- {_minCount} {_maxCount}");

        if (prev > _minCount)
            return false;

        if (cur >= _minCount && cur <= _maxCount)
            return true;

        throw new System.Exception("Something wrong");
    }

    public string GetText(string lang)
    {
        if (lang == "ru")
            return _rus;

        if (lang == "en")
            return _eng;

        throw new System.ArgumentNullException("Not found translation to language");
    }
}
