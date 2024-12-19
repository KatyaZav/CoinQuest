using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Scrimmer _scrimmer;
    [SerializeField] private UIScrimmersText _uIScrimmersText;
    [SerializeField] private CountManager _popupUI;

    void Start()
    {
        _scrimmer.Init();
        _uIScrimmersText.Init();
        _popupUI.Init();
    }
}
