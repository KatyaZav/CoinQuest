using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupUI : MonoBehaviour
{
    [SerializeField] private Spinner _spinner;
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private Text _text;

    [SerializeField] private float _speed;

    public void Enable(string text)
    {
        gameObject.SetActive(true);

        _text.text = text;
        _particle.Play();

        _spinner.StartSpin(_speed);
    }

    private void OnDisable()
    {
        _spinner.StopSpin();
    }
}
