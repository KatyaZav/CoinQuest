using System;
using UnityEngine;

public class CountHolder : MonoBehaviour
{
    public void Init()
    {
        PlayerSaves.ChangedFinalBankMoney += OnChange;
    }

    private void OnDestroy()
    {
        PlayerSaves.ChangedFinalBankMoney -= OnChange;        
    }

    private void OnChange(int prev, int cur)
    {
    }
}


