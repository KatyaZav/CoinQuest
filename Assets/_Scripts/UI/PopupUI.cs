using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupUI : MonoBehaviour
{
    [SerializeField] private Rotator _rotator;
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private Text _text;

    [SerializeField] private float _speed = 10;

    public void Activate(string text)
    {
        _particle.Play();
        _rotator.Activate(_speed);

        _text.text = text;
    }

    private void OnDisable()
    {
        if (_rotator.IsActive)
            _rotator.Deactivate();
    }
}
