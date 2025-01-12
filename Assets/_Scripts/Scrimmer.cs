using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Scrimmer : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _image;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private string _triggerName, _floatName;
    [SerializeField] private float _min, _max;
    [SerializeField] private Image _slider;

    private ItemsConfig _currentScrimmer;
    private ItemsConfig[] _scrimmers;
    private bool _wasStopped;
    private float _health = 2;

    public int ScrimmersCount => _scrimmers.Length;

    public void Init()
    {
        var loader = new ItemsLoader();
        loader.Load();
        _scrimmers = loader.GetItemsList().ToArray();
    }

    public void LoseCoins()
    {
        if (_wasStopped == false)
        {
            PlayerSaves.LooseCoins();
            PlayerSaves.SetHealth(2);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_health <= 0)
            return;

        _health -= 1;
        UpdateSlider();

        print(_health);

        if (_health <= 0)
            Die();
    }

    public void Activate()
    {
        gameObject.SetActive(true);

        _wasStopped = false;
        RandomScrimmer();

        _health = PlayerSaves.Health;
        UpdateSlider();

        float random = Random.Range(0, 101) / 100f;

        _animator.SetFloat(_floatName, random);
        _animator.SetTrigger(_triggerName);

        //_image.enabled = true;
        _image.sprite = _currentScrimmer.Sprite;

        _audioSource.clip = _currentScrimmer.Clip;
        _audioSource.pitch = Random.Range(_min, _max);
        _audioSource.Play();

        if (PlayerSaves.CheakIsScrimmerGetted(_currentScrimmer.ID) == false)
            PlayerSaves.AddScrimmer(_currentScrimmer.ID);

        Invoke("LoseCoins", 2);
    }

    public void Remove()
    {
        gameObject.SetActive(false);

        _image.enabled = false;
        _currentScrimmer = null;
    }

    private void RandomScrimmer()
    {
        var index = Random.Range(0, _scrimmers.Length);
        _currentScrimmer = _scrimmers[index];
    }

    private void Die()
    {
        PlayerSaves.SetHealth(PlayerSaves.Health + 1);
        _wasStopped = true;
    }

    private void UpdateSlider()
    {
        _slider.fillAmount = _health / PlayerSaves.Health;
    }

    private void OnValidate()
    {
        _image = GetComponent<Image>();
    }
}
