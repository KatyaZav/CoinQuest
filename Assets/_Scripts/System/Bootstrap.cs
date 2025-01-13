using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Scrimmer _scrimmer;
    [SerializeField] private UIScrimmersText _uIScrimmersText;
    [SerializeField] private CountHolder _countHolder;
    [SerializeField] private Tutorial _tutorial;

    [SerializeField] private GameObject _buttonTest;

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Start()
    {
        if (YandexGame.EnvironmentData.payload == "DeleteSaves")
        {
            _buttonTest.SetActive(true);
        }
        else
        {
            _buttonTest.SetActive(false);
        }


        _scrimmer.Init();
        _uIScrimmersText.Init();
        _countHolder.Init();

        _tutorial.Init(_scrimmer);
    }
}
