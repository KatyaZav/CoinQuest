using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour
{
    [SerializeField] Text _coinText;

    void Start()
    {
        SubscriptionKeeper.MoneyValueChangedEvent += ChangeUI;

        ChangeUI();
    }

    private void OnDisable()
    {
        SubscriptionKeeper.MoneyValueChangedEvent -= ChangeUI;        
    }

    private void ChangeUI()
    {
        _coinText.text = PlayerSaves.CoinsInPocket.ToString();
    }
}
