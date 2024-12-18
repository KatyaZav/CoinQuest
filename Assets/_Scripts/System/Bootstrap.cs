using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Scrimmer _scrimmer;
    [SerializeField] private UIScrimmersText _uIScrimmersText;

    void Start()
    {
        _scrimmer.Init();
        _uIScrimmersText.Init();
    }
}
