using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YG;
using YG.Utils.LB;

public class PawsUI : MonoBehaviour//, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Text _leaderboard;

    private bool _wasInit = false;

    private void Start()
    {
        GetComponent<Text>().text = PlayerSaves.CoinsInBank.Value.ToString();

        YandexGame.onGetLeaderboard += UpdateInfo;
        _leaderboard.text = GetLoadText(YandexGame.lang);

        YandexGame.GetLeaderboard("leaderboard", 10, 3, 2, null);
    }

    private void OnDestroy()
    {
        YandexGame.onGetLeaderboard -= UpdateInfo;        
    }

    /*public void OnPointerEnter(PointerEventData eventData)
    {
        _leaderboard.gameObject.SetActive(true);

        if (_wasInit == false)
        {
            _leaderboard.text = GetLoadText(YandexGame.lang);

            YandexGame.GetLeaderboard("leaderboard", 10, 3, 2, null);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _wasInit = false;
        _leaderboard.gameObject.SetActive(false);
    }*/
    
    private string GetText(string lang)
    {
        switch (lang)
        {
            case "ru":
                return "Место в лидерборде:";
            case "en":
                return "Place on the leaderboard:";
            default:
                return "error";
        }
    }
    private string GetLoadText(string lang)
    {
        switch (lang)
        {
            case "ru":
                return "Обновление данных";
            case "en":
                return "Refreshing data";
            default:
                return "error";
        }
    }
    private string GetErrorText(string lang)
    {
        switch (lang)
        {
            case "ru":
                return "Набери больше очков!";
            case "en":
                return "Score more points!";
            default:
                return "error";
        }
    }

    private void UpdateInfo(LBData data)
    {
        _wasInit = true;

        if (data.thisPlayer == null)
        {
            _leaderboard.text = GetErrorText(YandexGame.lang);
            return;
        }

        int rank = data.thisPlayer.rank;
        _leaderboard.text = String.Format($"{GetText(YandexGame.lang)} " +
                $"{rank}");        
    }
}
