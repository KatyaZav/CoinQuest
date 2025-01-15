using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Scrimmer : MonoBehaviour
{
    [SerializeField] private ImagePart[] _imageParts;

    [Header("Components")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private ScrimmerAnimation _scrimmerAnimation;

    [Header("Settings")]
    [SerializeField] private string _triggerName, _floatName;
    [SerializeField] private float _min, _max;
    [SerializeField] private GameObject _losePopup;

    private ImagePart _currentScrimmer;

    public void Activate()
    {
        _losePopup.SetActive(true);

        PlayerSaves.LooseCoins();
        RandomScrimmer();
        
        _scrimmerAnimation.Activate();
        
        _audioSource.pitch = Random.Range(_min, _max);
        _audioSource.Play();
    }

    public void Remove()
    {
        _scrimmerAnimation.Deactivate(() => _losePopup.SetActive(false));
    }

    private void RandomScrimmer()
    {
        foreach (var item in _imageParts)
        {
            item.Randomize();
        }
    }
}
