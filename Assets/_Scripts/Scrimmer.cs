using UnityEngine;
using UnityEngine.UI;

public class Scrimmer : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private string _triggerName, _floatName;
    [SerializeField] private float _min, _max;

    private Items _currentScrimmer;
    private Items[] _scrimmers;

    public int ScrimmersCount => _scrimmers.Length;

    private void Start()
    {
        var loader = new ItemsLoader();
        loader.Load();
        _scrimmers = loader.GetItemsList().ToArray();
    }

    public void Activate()
    {
        PlayerSaves.LooseCoins();
        RandomScrimmer();

        float random = Random.Range(0, 101) / 100f;

        _animator.SetFloat(_floatName, random);
        _animator.SetTrigger(_triggerName);

        //_image.enabled = true;
        _image.sprite = _currentScrimmer.Sprite;

        _audioSource.clip = _currentScrimmer.Clip;
        _audioSource.pitch = Random.Range(_min, _max);
        _audioSource.Play();

        if (PlayerSaves.CheakIsScrimmerGetted(_currentScrimmer.ID))
            PlayerSaves.AddScrimmer(_currentScrimmer.ID);
    }

    public void Remove()
    {
        _image.enabled = false;
        _currentScrimmer = null;
    }

    private void RandomScrimmer()
    {
        var index = Random.Range(0, _scrimmers.Length);
        _currentScrimmer = _scrimmers[index];
    }

    private void OnValidate()
    {
        _image = GetComponent<Image>();
    }
}
