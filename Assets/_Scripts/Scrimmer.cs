using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scrimmer : MonoBehaviour
{
    [SerializeField] ScrimmerType[] _scrimmers;
    [SerializeField] Image _image;
    [SerializeField] ScrimmerType _currentScrimmer;

    public void Generate()
    {
        PlayerSaves.LooseCoins();
        RandomScrimmer();

        _image.enabled = true;
        _image.sprite = _currentScrimmer.Sprite;
    }

    public void Remove()
    {
        _image.enabled = false;
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
}
