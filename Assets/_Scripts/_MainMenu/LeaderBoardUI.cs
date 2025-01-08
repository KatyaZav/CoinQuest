using System;
using UnityEngine;
using UnityEngine.UI;
using YG;
using YG.Utils.LB;

namespace Assets.Menu
{
    public class LeaderBoardUI : MonoBehaviour//, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Text _leaderboard;

        private void Start()
        {
            YandexGame.onGetLeaderboard += UpdateInfo;
            _leaderboard.text = GetLoadText(YandexGame.lang);

            YandexGame.GetLeaderboard("leaderboard", 10, 3, 2, null);
        }

        private void OnDestroy()
        {
            YandexGame.onGetLeaderboard -= UpdateInfo;
        }

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
}
