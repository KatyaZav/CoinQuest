using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CoinGenerator _generator;
    [SerializeField] private PlayersChoice _playersChoice;
    [SerializeField] private Bank _bank;
    [SerializeField] private Scrimmer _scrimmer;
    [SerializeField] private Coin _coin;

    [Space(8), Header("Settings")]
    [SerializeField] private GameObject _coinView;
    [SerializeField] private float _time;
    [SerializeField] private float _timeBetweenCoinGet = 0.5f;
    [SerializeField] private float _timeBetweenScrimmers = 2;
    [SerializeField] private float _scaryProbability = 60;
    [SerializeField] private int _minScary, _maxScary;

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
        print("mimik getted");
        StopRound();

        int waitTime = 0;

        if (_generator.GetMimikProbability() > _scaryProbability)
            waitTime += UnityEngine.Random.Range(_minScary, _maxScary);
        else
            waitTime += (int)_timeBetweenCoinGet;

        CraryAnimationActivate(waitTime);

        Invoke("ActivateMimik", waitTime);
        Invoke("RemoveMimik", _timeBetweenScrimmers + waitTime);

        Invoke("StartRound", _time + _timeBetweenCoinGet + waitTime);
    }

    private void OnCoinDrop()
    {
        StopRound();
        Invoke("StartRound", _time + _timeBetweenCoinGet);
    }

    private void OnCoinCollect()
    {
        print("coin getted");
        StopRound();

        int time = 0;

        if (_generator.GetMimikProbability() > _scaryProbability)
        {
            time += UnityEngine.Random.Range(_minScary, _maxScary);

            CraryAnimationActivate(time);
        }

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

    private void CraryAnimationActivate(int waitTime)
    {
        Debug.LogError("animation not added");
        //throw new NotImplementedException();
    }
}
