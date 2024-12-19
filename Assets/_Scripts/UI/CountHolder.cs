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

        List<ScoreThreshold> temp = new List<ScoreThreshold>(_allScores);

        foreach (var score in _allScores)
        {
            if (score.MinScore < prev || score.MinScore < cur)            
                temp.Remove(score);            
            else
                break;
        }

        _scores = temp;
    }

    private void OnDestroy()
    {
        PlayerSaves.ChangedFinalBankMoney -= OnChange;        
    }

    private void OnChange(int prev, int cur)
    {
        ScoreThreshold first = _scores[0];

        if (first.MinScore <= cur)
        {
            _popupUI.Activate(first.GetText(YG.YandexGame.lang), first.MinScore);
            _scores.Remove(first);
        }
    }
}


