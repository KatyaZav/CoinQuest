using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Scrimmer _scrimmer;
    [SerializeField] private UIScrimmersText _uIScrimmersText;
    [SerializeField] private CountHolder _countHolder;

    void Start()
    {
        _scrimmer.Init();
        _uIScrimmersText.Init();
        _countHolder.Init();
    }
}
