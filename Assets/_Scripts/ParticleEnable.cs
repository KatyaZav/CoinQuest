using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEnable : MonoBehaviour
{
    [SerializeField] ParticleSystem _freeButtonParticle;

    private void OnEnable()
    {
        _freeButtonParticle.Play();
    }
}
