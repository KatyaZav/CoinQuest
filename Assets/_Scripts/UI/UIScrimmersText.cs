using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScrimmersText : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private Scrimmer _scrimmer;

    void Start()
    {
        PlayerSaves.ChangedCountScrimmers += UpdateCount;

        UpdateCount(PlayerSaves.GettedScrimmerCount);
    }

    private void OnDestroy()
    {
        PlayerSaves.ChangedCountScrimmers -= UpdateCount;        
    }

    void UpdateCount(int count)
    {
        _text.text = string.Format($"{count}/{_scrimmer.ScrimmersCount}");
    }
}
