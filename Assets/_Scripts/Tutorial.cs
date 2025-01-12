using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private Scrimmer _scrimmer;

    public void Init(Scrimmer scrimmer)
    {
        if (PlayerSaves.WasTutorial == false)
        {
            _scrimmer = scrimmer;
            _scrimmer.MimikDied += DisactivateTutorial;
            _scrimmer.MimikLived += Disactivate;
            _scrimmer.MimikActivate += Activate;
        }
    }

    private void OnDestroy()
    {
        if (PlayerSaves.WasTutorial == false)
        {
            Unsubscribe();
        }
    }

    private void Activate()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0.5f;
    }

    private void Unsubscribe()
    {
        _scrimmer.MimikDied -= DisactivateTutorial;
        _scrimmer.MimikLived -= Disactivate;
        _scrimmer.MimikActivate -= Activate;
    }

    private void Disactivate()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    private void DisactivateTutorial()
    {
        Disactivate();
        PlayerSaves.GetTutorial();

        Unsubscribe();
    }
}
