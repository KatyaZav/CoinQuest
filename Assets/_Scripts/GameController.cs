using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CoinGenerator _generator;
    [SerializeField] private PlayersChoice _playersChoice;
    [SerializeField] private Bank _bank;
    [SerializeField] private Scrimmer _scrimmer;
    [SerializeField] private Coin _coin;
    [SerializeField] private SliderPoints _sliderPoints;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _winSound, _dropSound;

    [Space(5), Header("Settings")]
    [SerializeField] private GameObject _coinView;
    [SerializeField] private float _time;
    [SerializeField] private float _timeBetweenCoinGet = 0.5f;
    [SerializeField] private float _timeBetweenScrimmers = 2;
    [SerializeField] private float _scaryProbability = 60;
    [SerializeField] private int _minScary, _maxScary;
    [SerializeField] private float _min, _max;

    [Space(5), Header("Particles")]
    [SerializeField] private ParticleSystem _winSystem;
    //[SerializeField] private ParticleSystem _loseSystem;

    private void OnDisable()
    {
        _playersChoice.CoinCollectedEvent -= OnCoinCollect;
        _playersChoice.CoinDropedEvent -= OnCoinDrop;
        _playersChoice.MimikGettedEvent -= OnMimikGet;
    }
    private void OnValidate()
    {
        _generator = FindAnyObjectByType<CoinGenerator>();
        _bank = FindAnyObjectByType<Bank>();
    }

    void Start()
    {
        _bank.Init();

        _playersChoice.CoinCollectedEvent += OnCoinCollect;
        _playersChoice.CoinDropedEvent += OnCoinDrop;
        _playersChoice.MimikGettedEvent += OnMimikGet;

        StartRound();
    }

    #region Input Events
    private void OnMimikGet()
    {
        StopRound();

        int waitTime = 0;

        if (_generator.GetMimikProbability() > _scaryProbability)
            waitTime += UnityEngine.Random.Range(_minScary, _maxScary);
        else
            waitTime += (int)_timeBetweenCoinGet;

        CraryAnimationActivate(waitTime, false);

        Invoke("ActivateMimik", waitTime);
        Invoke("RemoveMimik", _timeBetweenScrimmers + waitTime);

        Invoke("StartRound", _time + _timeBetweenCoinGet + waitTime);
    }

    private void OnCoinDrop()
    {
        StopRound();
        _audioSource.clip = _dropSound;
        _audioSource.Play();
        Invoke("StartRound", _timeBetweenCoinGet);
    }

    private void OnCoinCollect()
    {
        StopRound();

        int time = 0;

        if (_generator.GetMimikProbability() > _scaryProbability)
        {
            time += UnityEngine.Random.Range(_minScary, _maxScary);
        }

        CraryAnimationActivate(time, true);

        Invoke("StartRound", _timeBetweenCoinGet + time);
    }
    #endregion

    #region Game logic
    private void StartRound()
    {
        _coinView.SetActive(false);

        GenerateCoin();

        _coin.SetAnimation();
        _playersChoice.ActivateButtons();
    }
    private void StopRound()
    {
        _playersChoice.DisableButtons();

        _coinView.SetActive(true);
        _coin.gameObject.SetActive(false);
    }
    private void GenerateCoin()
    {
        _coin.gameObject.SetActive(true);
        _generator.GenerateCoin();
        _coin.SetAnimation();
    }

    private void ActivateMimik()
    {
        _scrimmer.Activate();
    }

    public void RemoveMimik()
    {
        _scrimmer.Remove();
    }
    #endregion

    private void CraryAnimationActivate(int waitTime, bool success)
    {
        if (success)
        {
            Invoke("AddCoins", waitTime);
        }
        else
        {
            Invoke("LooseCoins", waitTime);
        }
        //throw new NotImplementedException();
    }

    void AddCoins()
    {
        _sliderPoints.AddValue();
        _audioSource.clip = _winSound;
        _audioSource.pitch = Random.Range(_min, _max);
        _audioSource.Play();

        PlayerSaves.AddCoins(_generator.GetCoinValue());
        _winSystem.Play();
    }

    void LooseCoins()
    {
        PlayerSaves.LooseCoins();
        //_loseSystem.Play();
    }
}
