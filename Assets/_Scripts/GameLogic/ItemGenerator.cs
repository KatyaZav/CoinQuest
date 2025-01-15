using Assets.Gameplay.UI;
using System;
using UnityEngine;

namespace Assets.Gameplay
{
    public class ItemGenerator : MonoBehaviour
    {
        [SerializeField] private ItemView _itemView;

        [Header("Settings")]
        [SerializeField] private AnimationCurve _failProbability;
        [SerializeField] private Color _dangerousColor;
        [SerializeField] private Color _safeColor;

        private ItemsLoader _loader;
        private Items _item;

        private float _countModifier;
        private int _extraFailProbability = 0;

        private Sprite _secretImage;
        private string _secretText = "";

        public void Init(ItemsLoader itemLoader)
        {
            _loader = itemLoader;
        }

        public float FailProbability { get; private set; }
        public int CoinValue { get; private set; }
        public bool IsMimik { get; private set; }

        public Items Item => _item;

        public bool IsSecretImage => _secretImage != null;
        public bool IsSecretText => _secretText != "";

        public void MakeImageSecret(Sprite sprite)
        {
            _secretImage = sprite;
        }

        public void DisactivateImageSecret()
        {
            _secretImage = null;
        }

        public void MakeTextSecret(string text)
        {
            _secretText = text;
        }

        public void DisactivateTextSecret()
        {
            _secretText = "";
        }

        public void ChangeCountModifier(float value)
        {
            if (value < 1)
                throw new ArgumentException($"Can't change modifier to {value}");

            _countModifier = value;
        }

        /// <summary>
        /// Add extra probability
        /// </summary>
        /// <param name="probability">Lose probability</param>
        public void ChangeExtraProbability(int probability)
        {
            if (probability < -100 || probability > 100)
                throw new ArgumentException("Try change probability in wrong way");

            _extraFailProbability = probability;
        }

        /// <summary>
        /// Generate new item with fail probability and value 
        /// </summary>
        public void GenerateCoin()
        {
            _item = GetRandomItem(_loader);

            PlayerSaves.MakeSeen(_item);

            CoinValue = GetValue();

            float rnd = UnityEngine.Random.Range(0, 1f);
            float randomProcent = UnityEngine.Random.Range(1, 101);
            FailProbability = GetFailProbability(rnd, _item);

            IsMimik = randomProcent < FailProbability;

            ChangeCoinAppearance(CoinValue, 100 - Mathf.Round(FailProbability));
        }

        private Items GetRandomItem(ItemsLoader loader)
        {
            int rnd = UnityEngine.Random.Range(0, 101);
            Rare rare = GetRare(rnd);

            var listItems = loader.GetListByRare(rare);

            return listItems[UnityEngine.Random.Range(0, listItems.Count)];
        }

        private int GetValue()
        {
            return Mathf.Max((int)Mathf.Round((int)_item.GetRare * _countModifier), 1);
        }
        private float GetFailProbability(float random, Items item)
        {
            float minRange, maxRange;

            switch (item.GetRare)
            {
                case Rare.Usual:
                    minRange = 5;
                    maxRange = 70;
                    break;
                case Rare.Rare:
                    minRange = 10;
                    maxRange = 80;
                    break;
                case Rare.Legendary:
                    minRange = 20;
                    maxRange = 95;
                    break;
                default:
                    throw new InvalidOperationException($"Not have range for {item.GetRare}");
            }

            float probability = Math.Clamp(_failProbability.Evaluate(random) * 100, minRange, maxRange);

            return Math.Clamp(probability + _extraFailProbability, 0, 100);
        }

        private Rare GetRare(float random)
        {
            Rare rare;

            if (random < 80)
            {
                rare = Rare.Usual;
            }
            else if (random < 95)
            {
                rare = Rare.Rare;
            }
            else
            {
                rare = Rare.Legendary;
            }

            return rare;
        }

        private void ChangeCoinAppearance(float coinValue, float probability)
        {
            _itemView.SetImage(IsSecretImage == false ? _item.Icon : _secretImage);
            _itemView.SetTextColor(probability > 50 ? _safeColor : _dangerousColor);
            
            if (IsSecretText)
                MakeSecretText();
            else
                MakeText(probability);

            _itemView.ActivateStartAnimation();
        }

        private void MakeText(float probability)
        {
            _itemView.SetProbabilityText(probability.ToString() + "%");
        }

        private void MakeSecretText()
        {
            _itemView.SetProbabilityText(_secretText);
        }
    }
}