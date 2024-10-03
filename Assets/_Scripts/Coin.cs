using System;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    [Header("Appearance")]
    [SerializeField] private Text _coinText;
    [SerializeField] private Text _coinProbabilityText;

    [Header("Settings"), Space(10)]
    [SerializeField] private AnimationCurve _mimicProbability;
    [SerializeField] private Color _dangerousColor;
    [SerializeField] private Color _safeColor;

    public int Value { get; private set; }
    public bool IsMimic { get; private set; }
    public float Probability { get; private set; }

    public void GenerateCoin(int minValue, int maxValue)
    {
        Value = UnityEngine.Random.Range(minValue, maxValue + 1);

        float valueInRange = Mathf.InverseLerp(minValue, maxValue, Value);
        float mimicProbability = _mimicProbability.Evaluate(valueInRange) * 100;
        float randomProcent = UnityEngine.Random.Range(1, 101);

        Probability = mimicProbability;

        IsMimic = randomProcent <= Probability;

        ChangeCoinAppearance(Value, 100 - Mathf.Round(Probability));
    }

    private void ChangeCoinAppearance(float coinValue, float probability)
    {
        _coinText.text = coinValue.ToString();
        _coinProbabilityText.text = probability.ToString() + "%"; 

        if (probability > 50)
        {
            _coinProbabilityText.color = _safeColor;
        }
        else
        {
            _coinProbabilityText.color = _dangerousColor;
        }
    }

    private void OnValidate()
    {
        _coinText = GetComponentInChildren<Text>();

        if (_mimicProbability.Evaluate(0) < 0)
            Debug.LogWarning("Mimik probability under zero");

        if (_mimicProbability.Evaluate(1) > 1)
            Debug.LogWarning("Mimik probability more that 100%");
    }
}
