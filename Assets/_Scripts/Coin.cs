using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    [Header("Appearance")]
    [SerializeField] private Text _coinProbabilityText;
    [SerializeField] private Animator _animator;

    [Header("Settings"), Space(10)]
    [SerializeField] private Color _dangerousColor;
    [SerializeField] private Color _safeColor;
    [SerializeField] private AnimationCurve _probability;

    private ItemsLoader _loader;
    private Items _item;

    public int Value { get; private set; }
    public bool IsMimic { get; private set; }
    public float Probability { get; private set; }

    private void Start()
    {
        _loader = new ItemsLoader();
        _loader.Load(true);
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
        float mimicProbability = Math.Clamp(_probability.Evaluate(rnd) * 100 + (int)_item.GetRare*2, 10, 90);
        float randomProcent = UnityEngine.Random.Range(1, 101);

        Probability = mimicProbability;
        Value = (int)_item.GetRare;

        IsMimic = randomProcent <= Probability;
        //Debug.Log($"{Value}, {randomProcent} < {Probability}. IsMimik = {IsMimic}");

        ChangeCoinAppearance(Value, 100 - Mathf.Round(Probability));
    }

    private Items GetRandomItem(ItemsLoader loader)
    {
        int rnd = UnityEngine.Random.Range(0, 101);
        Items.Rare rare = Items.Rare.usual;

        if (rnd < 50)
        {
            rare = Items.Rare.usual;
        }
        else if (rnd < 90)
        {
            rare = Items.Rare.normal;
        }
        else
        {
            rare = Items.Rare.rare;
        }

        var listItems = loader.GetListByRare(rare);

        return listItems[UnityEngine.Random.Range(0, listItems.Count)];
    }

    private void ChangeCoinAppearance(float coinValue, float probability)
    {
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

    public void GetCoin()
    {
        PlayerSaves.MakeGetted(_item);
    }
}
