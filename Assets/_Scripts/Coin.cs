using System;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    [Header("Appearance")]
    [SerializeField] Text _coinText;

    [Header("Settings"), Space(10)]
    [SerializeField] AnimationCurve _mimicProbability;
    public int Value { get; private set; }
    public bool IsMimic { get; private set; }

    public void GenerateCoin(int minValue, int maxValue)
    {
        Value = UnityEngine.Random.Range(minValue, maxValue + 1);

        float mimicProbability = _mimicProbability.Evaluate(Value);
        float normalizedValue = Mathf.InverseLerp(0, 1, mimicProbability);
        float resultMimikProbability = Mathf.Lerp(minValue, maxValue, normalizedValue);

        IsMimic = Value >= resultMimikProbability;

        ChangeCoinAppearance();
    }

    private void ChangeCoinAppearance()
    {
        _coinText.text = Value.ToString();
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
