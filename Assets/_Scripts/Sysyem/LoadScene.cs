using Newtonsoft.Json;
using System.Linq;
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
        var loader = new ItemsLoader();
        loader.Load();

        print(PlayerSaves.Items.Count());
        PlayerSaves.UpdateList(loader.GetItemsList());
        print(PlayerSaves.Items.Count());
        
        SceneManager.LoadScene(1);
    }
}
