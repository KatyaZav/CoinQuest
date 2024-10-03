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

    [SerializeField] private string _triggerName, _floatName; 

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
    [SerializeField] AudioClip Clip;
    [SerializeField] UnityEvent _onGetted;

    public void Activate()
    {
        _onGetted?.Invoke();
    }
}
