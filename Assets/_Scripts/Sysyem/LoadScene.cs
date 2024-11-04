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

        //YandexGame.ResetSaveProgress();
        //YandexGame.SaveProgress();

        print(PlayerSaves.Items.Count());

        foreach (var e in loader.GetItemsList())
        {
            print(e.ID + " Added: " + PlayerSaves.TryGetItemContain(e, out var y));

            if (PlayerSaves.TryGetItemContain(e, out y) == false)
            {
                var res = PlayerSaves.Items;
                res.Add(new ItemsData(e));
                YandexGame.savesData.ListItems = JsonConvert.SerializeObject(res, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

                YandexGame.SaveProgress();
                print("Add complete");
                print(YandexGame.savesData.ListItems);
                print(PlayerSaves.Items);
            }
        }

        PlayerSaves.UpdateList(loader.GetItemsList());
        
        SceneManager.LoadScene(1);
    }
}
