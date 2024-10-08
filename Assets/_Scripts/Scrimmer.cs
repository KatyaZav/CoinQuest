using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Scrimmer : MonoBehaviour
{
    [SerializeField] private ScrimmerType[] _scrimmers;
    [SerializeField] private Image _image;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private string _triggerName, _floatName;
    [SerializeField] private float _min, _max;

    private ScrimmerType _currentScrimmer;

    public void Activate()
    {

        PlayerSaves.LooseCoins();
        RandomScrimmer();

        _currentScrimmer.Activate();

        float random = Random.Range(0, 101) / 100f;

        _animator.SetFloat(_floatName, random);
        _animator.SetTrigger(_triggerName);

        //_image.enabled = true;
        _image.sprite = _currentScrimmer.Sprite;

        _audioSource.clip = _currentScrimmer.Clip;
        _audioSource.pitch = Random.Range(_min, _max);
        _audioSource.Play();
    }

    public void Remove()
    {
        _image.enabled = false;
        _currentScrimmer = null;
    }

    private void RandomScrimmer()
    {
        var index = Random.Range(0, _scrimmers.Length);
        _currentScrimmer = _scrimmers[index];
    }

    private void OnValidate()
    {
        _image = GetComponent<Image>();
    }
}

[System.Serializable]
public class ScrimmerType
{
    [SerializeField] string _name;
    [SerializeField] public Sprite Sprite;
    [SerializeField] AudioClip _clip;
    [SerializeField] UnityEvent _onGetted;

    public AudioClip Clip => _clip;

    public void Activate()
    {
        _onGetted?.Invoke();
    }
}
