using System;
using System.Collections.Generic;
using UnityEngine;

public class CountHolder : MonoBehaviour
{
    private const string Path = "Achives";

    [SerializeField] private PopupUI _popupUI;

    List<ScoreThreshold> _allScores;
    List<ScoreThreshold> _scores;

    public void Init()
    {
        PlayerSaves.ChangedFinalBankMoney += OnChange;

        var prev = PlayerSaves.PreviousMoney;
        var cur = PlayerSaves.CoinsInBank;

        _allScores = new ResourceLoader().Load<ScoreThreshold>(Path);
        _allScores.Sort((a, b) => a.MinScore.CompareTo(b.MinScore));

        _scores = new List<ScoreThreshold>(_allScores);

        foreach (var score in _scores)
        {
            if (score.MinScore < prev || score.MinScore < cur)            
                _scores.Remove(score);            
            else
                break;
        }
    }

    private void OnDestroy()
    {
        PlayerSaves.ChangedFinalBankMoney -= OnChange;        
    }

    private void OnChange(int prev, int cur)
    {
        ScoreThreshold first = _scores[0];

        if (first.MinScore < cur && first.MinScore > prev)
        {
            _popupUI.Activate(first.GetText(YG.YandexGame.lang), first.MinScore);
            _scores.Remove(first);
        }
    }
}


