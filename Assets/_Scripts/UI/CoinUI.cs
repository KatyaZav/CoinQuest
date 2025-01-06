using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour
{
    [SerializeField] Text _coinText;

    void Start()
    {
        PlayerSaves.CoinsInPocket.Changed += ChangeUI;

        ChangeUI(PlayerSaves.CoinsInPocket.Value);
    }

    private void OnDisable()
    {
        PlayerSaves.CoinsInPocket.Changed -= ChangeUI;        
    }

    private void ChangeUI(int value)
    {
        _coinText.text = value.ToString();
    }
}
