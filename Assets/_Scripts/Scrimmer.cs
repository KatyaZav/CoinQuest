using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Scrimmer : MonoBehaviour
{
    [SerializeField] ScrimmerType[] _scrimmers;
    [SerializeField] Image _image;
    
    ScrimmerType _currentScrimmer;

    public void Activate()
    {
        PlayerSaves.LooseCoins();
        RandomScrimmer();

        _currentScrimmer.Activate();

        _image.enabled = true;
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
