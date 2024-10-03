using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Waiting : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private string[] _loadingWorlds;
    [SerializeField] private float _timeBetween;

    private int _currentIndex = 0;

    public void StartTimer(float time)
    {
        _currentIndex = 0;
       StartCoroutine(StartTimerLogic(time - 0.2f));
    }

    private IEnumerator StartTimerLogic(float time)
    {
        float currentTime = 0;
        float timer = _timeBetween;

        while (currentTime < time)
        {
            yield return new WaitForFixedUpdate();
            currentTime += Time.deltaTime;
            timer += Time.deltaTime;

            if (timer > _timeBetween)
            {
                timer = 0;

                _text.text = _loadingWorlds[_currentIndex];

                _currentIndex = (_currentIndex + 1) % _loadingWorlds.Length;
            }
        }

        _text.text = "";
    }
}
