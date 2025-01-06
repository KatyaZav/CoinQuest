using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class LoadScene : MonoBehaviour
{
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
        Debug.Log("Load data");

        var loader = new ItemsLoader();
        loader.Load();

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        PlayerSaves.UpdateList(loader.GetItemsList());        
        SceneManager.LoadSceneAsync(1);
    }
}