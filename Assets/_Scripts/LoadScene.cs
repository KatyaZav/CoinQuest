using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class LoadScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        YandexGame.GetDataEvent += StartGame;

        if (YandexGame.SDKEnabled)
            StartGame();
    }

    private void OnDestroy()
    {
        YandexGame.GetDataEvent -= StartGame;        
    }

    void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
