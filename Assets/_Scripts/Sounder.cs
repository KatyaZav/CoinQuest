using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class Sounder : MonoBehaviour
{
    [SerializeField] AudioSource[] _sources;

    private bool isOn => YandexGame.savesData.IsSoundOn;

    private void Start()
    {
        Updater();
    }

    public void ChangeSound()
    {
        YandexGame.savesData.IsSoundOn = !YandexGame.savesData.IsSoundOn;
        YandexGame.SaveProgress();

        Updater();
    }

    void Updater()
    {
        foreach (var source in _sources)
        {
            source.mute = !isOn;
        }
    }
}
