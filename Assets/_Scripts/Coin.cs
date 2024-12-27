using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    [Header("Appearance")]
    [SerializeField] private Text _coinProbabilityText;
    [SerializeField] private Image _img;
    [SerializeField] private Animator _animator;

    [Header("Settings"), Space(10)]
    [SerializeField] private Color _dangerousColor;
    [SerializeField] private Color _safeColor;
    [SerializeField] private AnimationCurve _probability;

    private ItemsLoader _loader;
    private Items _item;

    private float _modifier;
    private int _extraProbability = 0;
   
    public void Init()
    {
        _loader = new ItemsLoader();
        _loader.Load(true);
    }

    public int Value { get; private set; }
    public bool IsMimic { get; private set; }
    public float Probability { get; private set; }

    public void ChangeModifier(float value)
    {
        if (value < 1)
            throw new ArgumentException($"Can't change modifier to {value}");

        _modifier = value;
    }

    public void ChangeExtraProbability(int probability)
    {
        if (probability < -100 || probability > 100)
            throw new ArgumentException("Try change probability in wrong way");

        _extraProbability = probability;
    }

    public void SetAnimation()
    {
        _animator.SetTrigger("on");
    }

    public void GenerateCoin()
    {
        _item = GetRandomItem(_loader);

        PlayerSaves.MakeSeen(_item);

        float rnd = UnityEngine.Random.Range(0, 1f);
        float mimicProbability = Math
            .Clamp(_probability.Evaluate(rnd) * 100 + (int)_item.GetRare*2 + _extraProbability, 5, 95);
        
        float randomProcent = UnityEngine.Random.Range(1, 101);

        Probability = mimicProbability;
        Value = Mathf.Max((int) Mathf.Round((int)_item.GetRare * _modifier), 1);

        IsMimic = randomProcent <= Probability;
        //Debug.Log($"{Value}, {randomProcent} < {Probability}. IsMimik = {IsMimic}");

        ChangeCoinAppearance(Value, 100 - Mathf.Round(Probability));
    }

    private Items GetRandomItem(ItemsLoader loader)
    {
        int rnd = UnityEngine.Random.Range(0, 101);
        Rare rare = Rare.usual;

        if (rnd < 70)
        {
            rare = Rare.usual;
        }
        else if (rnd < 95)
        {
            rare = Rare.normal;
        }
        else
        {
            rare = Rare.rare;
        }

        var listItems = loader.GetListByRare(rare);

        return listItems[UnityEngine.Random.Range(0, listItems.Count)];
    }

    private void ChangeCoinAppearance(float coinValue, float probability)
    {
        _coinProbabilityText.text = probability.ToString() + "%";
        _img.sprite = _item.Icon;

        if (probability > 50)
        {
            _coinProbabilityText.color = _safeColor;
        }
        else
        {
            _coinProbabilityText.color = _dangerousColor;
        }
    }

    public void GetCoin()
    {
        PlayerSaves.MakeGetted(_item);
    }
}
