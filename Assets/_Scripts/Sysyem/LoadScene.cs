using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;
using static UnityEditor.Progress;

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

        YG.YandexGame.ResetSaveProgress();
        YandexGame.SaveProgress();

        print(PlayerSaves.Items.Count());

        foreach (var e in loader.GetItemsList())
        {
            print(e.Name + " " + PlayerSaves.TryGetItemContain(e, out var y));

            if (PlayerSaves.TryGetItemContain(e, out y) == false)
            {
                var res = PlayerSaves.Items;
                res.Add(new ItemsData(e));
                YandexGame.savesData.ListItems = JsonConvert.SerializeObject(res, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                print("Add complete");
                print(PlayerSaves.Items.Count());
            }
        }

        PlayerSaves.UpdateList(loader.GetItemsList());
        
        SceneManager.LoadScene(1);
    }
}
