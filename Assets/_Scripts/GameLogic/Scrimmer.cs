using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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

[System.Serializable]
public class ImagePart
{
    [SerializeField] private string _name;

    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private Image[] _placeImage;
    [SerializeField] private Gradient _colors;

    public void Randomize()
    {
        float rnd = Random.Range(0, 1f);

        int index = Random.Range(0, _sprites.Length);
        Color color = _colors.Evaluate(rnd);

        foreach (var e in _placeImage)
        {
            e.sprite = _sprites[index];
            e.color = color;
        }
    }
}
