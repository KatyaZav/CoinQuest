using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ImagePart
{
    [SerializeField] private string _name;

    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private Image[] _placeImage;
    [SerializeField] private Gradient _colors;

    public void Randomize()
    {
        float rnd = Random.Range(0, 1f);

        int index = Random.Range(0, _sprites.Length);
        Color color = _colors.Evaluate(rnd);

        foreach (var e in _placeImage)
        {
            e.sprite = _sprites[index];
            e.color = color;
        }
    }
}
