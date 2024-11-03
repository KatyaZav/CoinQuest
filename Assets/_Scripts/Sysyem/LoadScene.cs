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

        print(PlayerSaves.CoinsInBank);
        print(YandexGame.savesData.ListItems.Count());

        PlayerSaves.UpdateList(loader.GetItemsList());
        
        SceneManager.LoadScene(1);
    }
}
