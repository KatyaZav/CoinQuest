using UnityEngine.SceneManagement;

namespace Assets.System
{
    public class SceneLoader
    {
        public void Load(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
